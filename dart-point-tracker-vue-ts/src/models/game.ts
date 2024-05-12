import Player from './player'; 

class Game {
  gameId: string;
  gamePoints: number;
  players: Player[];
  ranking: Player[];
  currentPlayer: Player;
  isFinished: boolean;
  gameSaved: boolean;
  private totalThrows: number;

  constructor(players: Player[], gamePoints: number) {
    this.gameId = (<any>crypto).randomUUID(); 
    this.gamePoints = gamePoints;
    this.players = players.map(player => {
      player.score = gamePoints;
      return player;
    });
    this.ranking = [];
    this.currentPlayer = players[0];
    this.isFinished = false;
    this.gameSaved = false;
    this.totalThrows = 0;
  }

  addDartThrow(points: number): void {
    this.totalThrows++;
    const { validThrow, currentRoundThrowNumber } = this.currentPlayer.addDartThrow(points);

    if (this.currentPlayer.won) {
      this.ranking.push(this.currentPlayer);
      this.currentPlayer.throwNumberInGameAtWin = this.totalThrows;
      if (this.players.filter(player => player.won).length === this.players.length - 1) {
        this.isFinished = true;
        this.ranking.push(this.players.find(player => !player.won)!);
        return;
      }
      this.nextPlayer();
    } else if (!validThrow || currentRoundThrowNumber === 0) {
      this.nextPlayer();
    }
  }

  removeLastDartThrow(): void {
    if (this.totalThrows > 0) {
      this.totalThrows--;
    } else {
      return;
    }

    if (this.currentPlayer.currentRoundThrowNumber === 0) {
      this.currentPlayer.reloadPreviousRound();
      this.previousPlayer();
    }

    while (this.currentPlayer.won) {
      if (this.currentPlayer.throwNumberInGameAtWin === this.totalThrows + 1) {
        this.currentPlayer.won = false;
        break;
      }
      this.previousPlayer();
    }

    this.currentPlayer.removeLastDartThrow();
  }

  private previousPlayer(): void {
    const index = this.players.indexOf(this.currentPlayer);
    this.currentPlayer = index === 0 ? this.players[this.players.length - 1] : this.players[index - 1];
  }

  private nextPlayer(): void {
    do {
      const index = this.players.indexOf(this.currentPlayer);
      this.currentPlayer = index === this.players.length - 1 ? this.players[0] : this.players[index + 1];
    } while (this.currentPlayer.won);

    this.currentPlayer.startNewRound();
  }
}

export default Game;
