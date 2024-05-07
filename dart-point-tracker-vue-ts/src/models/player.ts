import { ref, computed } from 'vue';

class Player {
  id: string;
  name: string;
  score: number;
  dartThrows: { throwNumber: number; points: number }[] = [];
  currentThrowNumber = ref(0);
  currentRoundThrows = [ref<number | null>(null), ref<number | null>(null), ref<number | null>(null)];
  throwNumberInGameAtWin: number;
  won: boolean;
  private lastRoundOverThrow: boolean;
  eloRankingScore: number;
  rank: number;
  selected: boolean;

  constructor(name: string) {
    this.id = ''; // Initialize with appropriate value
    this.name = name;
    this.score = 0;
    this.throwNumberInGameAtWin = 0;
    this.won = false;
    this.lastRoundOverThrow = false;
    this.eloRankingScore = 0;
    this.rank = 0;
    this.selected = false;
  }

  get currentRoundThrowNumber(): number {
    return this.currentThrowNumber.value % 3;
  }

  startNewRound(): void {
    this.currentRoundThrows.forEach((throwRef) => (throwRef.value = null));
  }

  addDartThrow(points: number): { validThrow: boolean; currentRoundThrowNumber: number } {
    this.currentRoundThrows[this.currentRoundThrowNumber].value = points;
    this.dartThrows.push({ throwNumber: this.currentThrowNumber.value, points });
    this.score -= points;

    if (this.score < 0) {
      this.score += this.currentRoundThrows.reduce((acc, currentThrow) => (currentThrow.value ? acc + currentThrow.value : acc), 0);
      this.lastRoundOverThrow = true;
      return { validThrow: false, currentRoundThrowNumber: this.currentRoundThrowNumber };
    } else if (this.score === 0) {
      this.won = true;
    }

    return { validThrow: true, currentRoundThrowNumber: this.currentRoundThrowNumber };
  }

  removeLastDartThrow(): void {
    if (this.currentThrowNumber.value === 0) return;

    const lastThrow = this.dartThrows.pop();
    if (lastThrow) {
      this.currentRoundThrows[this.currentRoundThrowNumber].value = null;
      if (this.lastRoundOverThrow) {
        this.score -= this.currentRoundThrows.reduce((acc, currentThrow) => (currentThrow.value ? acc + currentThrow.value : acc), 0);
        this.lastRoundOverThrow = false;
      } else {
        this.score += lastThrow.points;
      }
    }
  }

  reloadPreviousRound(): void {
    if (this.currentThrowNumber.value < 3) return;

    this.currentRoundThrows[0].value = this.dartThrows[this.dartThrows.length - 3].points;
    this.currentRoundThrows[1].value = this.dartThrows[this.dartThrows.length - 2].points;
    this.currentRoundThrows[2].value = this.dartThrows[this.dartThrows.length - 1].points;
  }
}

export default Player;
