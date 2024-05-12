import { defineStore } from 'pinia';
import Player from '../models/player';
import Game from '../models/game'; 

const apiUrl = 'https://app-dartsmaster-dev-nsure-01.azurewebsites.net'
export const usePlayerStore = defineStore('player', {
  state: () => ({
    players: [] as Player[],
  }),

  actions: {
    async createPlayer(name: string | null): Promise<string | null> {
      try {
        if (!name) {
          return 'Name cannot be empty';
        }
        const response = await fetch(apiUrl + `/Player?playerName=${name}`);
        if (response.ok) {
          const player = await response.json() as Player;
          this.players.push(player);
          await this.cachePlayers(this.players);
          return null;
        }
        return 'Player with this name already exists';
      } catch (error) {
        return 'Check your internet connection and try again.';
      }
    },

    async getPlayers() {
      try {
        let players = await this.fetchPlayersFromApi();
        if (!players || players.length === 0) {
          players = await this.getCachedPlayers();
        }
        if (players) {
          this.players = players;
          await this.cachePlayers(this.players);
        }
      } catch (error) {
        console.error(`Failed to fetch or cache players: ${error}`);
      }
    },

    async sendGame(game: Game) {
      try {
        const playerList = game.ranking.map(player => ({ Id: player.id, Place: game.ranking.indexOf(player) + 1 }));
        const response = await fetch(apiUrl + '/Game', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(playerList)
        });
        if (response.ok) {
          game.gameSaved = true;
        }
      } catch (error) {
        console.error(`Failed to send game to API: ${error}`);
      }
    },

    async cachePlayers(players: Player[] | null) {
      try {
        localStorage.setItem('players', JSON.stringify(players));
      } catch (error) {
        console.error(`Failed to cache players: ${error}`);
      }
    },

    async getCachedPlayers(): Promise<Player[] | null> {
      try {
        const playersJson = localStorage.getItem('players');
        return playersJson ? JSON.parse(playersJson) : [];
      } catch (error) {
        console.error(`Failed to retrieve cached players: ${error}`);
        return null;
      }
    },

    async fetchPlayersFromApi(): Promise<Player[] | null> {
      try {
        const response = await fetch(apiUrl + '/Players');
        return response.ok ? await response.json() : null;
      } catch (error) {
        console.error(`Failed to fetch players from API: ${error}`);
        return null;
      }
    }
  }
});
