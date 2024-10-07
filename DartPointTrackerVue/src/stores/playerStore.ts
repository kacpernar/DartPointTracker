import { defineStore } from 'pinia'
import Player from '../models/player'
import Game from '../models/game'

const apiUrl = 'https://app-dartsmaster-dev-nsure-01.azurewebsites.net'
export const usePlayerStore = defineStore('player', {
  state: () => ({
    players: [] as Player[]
  }),

  actions: {
    async createPlayer(name: string | null): Promise<string | null> {
      try {
        if (!name) {
          return 'Name cannot be empty'
        }

        const response = await fetch(apiUrl + `/Player?playerName=${name}`, {
          method: 'POST'
        })
        if (response.ok) {
          const player = (await response.json()) as Player
          if (player) {
            this.getPlayers()
            return null
          }
        }
        return 'Player with this name already exists'
      } catch (error) {
        return 'You are offline. Please connect to the internet to create a new player.'
      }
    },

    async getPlayers(): Promise<Player[]> {
      try {
        const players = await this.fetchPlayersFromApi()
        this.players = players ?? []
      } catch (error) {
        console.error(`Failed to fetch players from API: ${error}`)
        this.players = []
      }
      return this.players
    },

    async sendGame(game: Game): Promise<[boolean, string]> {
      try {
        const playerList = game.ranking.map((player) => ({
          Id: player.id,
          Place: game.ranking.indexOf(player) + 1
        }))

        const response = await fetch(apiUrl + '/Game', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(playerList)
        })
        if (response.ok) {
          game.gameSaved = true
          return [
            true,
            'The game was saved successfully, check the ranking to see the updated leaderboard.'
          ]
        } else {
          game.gameSaved = false
          return [false, 'Failed to save game, please try again.']
        }
      } catch (error) {
        console.error(`Failed to send game to API: ${error}`)
        game.gameSaved = true
        return [
          false,
          'Seems like you are offline. Your game will be saved once you are back online.'
        ]
      }
    },

    async fetchPlayersFromApi(): Promise<Player[] | null> {
      try {
        const response = await fetch(apiUrl + '/Players')
        if (!response.ok) {
          return null
        }
        const data = await response.json()
        return data.map(
          (playerData: any) =>
            new Player(playerData.name, playerData.id, playerData.eloRankingScore)
        )
      } catch (error) {
        console.error(`Failed to fetch players from API: ${error}`)
        return null
      }
    }
  }
})
