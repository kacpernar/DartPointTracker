class Player {
  id: string | undefined;
  name: string;
  score: number;
  dartThrows: { throwNumber: number; points: number }[];
  currentRoundThrows : (number | null)[];
  throwNumberInGameAtWin: number;
  won: boolean;
  lastRoundOverThrow: boolean;
  eloRankingScore: number;
  rank: number;
  selected: boolean;

  constructor(name: string, id?: string, eloRankingScore?: number) {
    this.id = id; // Initialize with appropriate value
    this.name = name;
    this.dartThrows = [];
    this.score = 0;
    this.throwNumberInGameAtWin = 0;
    this.won = false;
    this.currentRoundThrows = [null, null, null]
    this.lastRoundOverThrow = false;
    this.rank = 0;
    this.selected = false;
    this.eloRankingScore = eloRankingScore || 0;
  }

  get currentThrowNumber(): number {
    return this.dartThrows.length;
  }
  get currentRoundThrowNumber(): number {
    return this.currentThrowNumber % 3;
  }

  startNewRound(): void {
    this.currentRoundThrows = [null, null, null];
  }

  addDartThrow(points: number): { validThrow: boolean; currentRoundThrowNumber: number } {
    this.currentRoundThrows[this.currentRoundThrowNumber] = points;
    this.dartThrows.push({ throwNumber: this.currentThrowNumber, points });
    this.score -= points;

    if (this.score < 0) {
      this.score += this.currentRoundThrows.filter((x): x is number => x !== null).reduce((acc, x) => acc + x, 0);
      this.lastRoundOverThrow = true;
      return { validThrow: false, currentRoundThrowNumber: this.currentRoundThrowNumber };
    } else if (this.score === 0) {
      this.won = true;
    }

    return { validThrow: true, currentRoundThrowNumber: this.currentRoundThrowNumber };
  }

  removeLastDartThrow(): void {
    if (this.currentThrowNumber === 0) return;

    const lastThrow = this.dartThrows.pop();
    if (lastThrow) {
      this.currentRoundThrows[this.currentRoundThrowNumber] = null;
      if (this.lastRoundOverThrow) {
        this.score -= this.currentRoundThrows.filter((x): x is number => x !== null).reduce((acc, x) => acc + x, 0);
        this.lastRoundOverThrow = false;
      } else {
        this.score += lastThrow.points;
      }
    }
  }

  reloadPreviousRound(): void {
    if (this.currentThrowNumber < 3) return;

    this.currentRoundThrows[0] = this.dartThrows[this.dartThrows.length - 3].points;
    this.currentRoundThrows[1] = this.dartThrows[this.dartThrows.length - 2].points;
    this.currentRoundThrows[2] = this.dartThrows[this.dartThrows.length - 1].points;
  }
}

export default Player;
