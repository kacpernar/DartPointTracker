import { ref } from 'vue'
import { defineStore } from 'pinia'
import Player from '../models/player'
import Game from '../models/game'
export const usePlayerStore = defineStore('player', () => {
  const players = ref<Player[]>([])
  const apiUrl = 'https://localhost:7276'
  const basicAuth = 'Basic ' + btoa('username:password')
  async function createPlayer(name: string | null): Promise<string | null> {
    try {
      if (!name) {
        return 'Name cannot be empty'
      }
      const response = await fetch(apiUrl + `/Player?playerName=${name}`, {
        method: 'POST',
        headers: { Authorization: basicAuth }
      })
      if (response.ok) {
        await getPlayers()
        return null
      }
      const errorMessage = await response.text()
      return errorMessage
    } catch (error) {
      return 'You are offline. Please connect to the internet to create a new player.'
    }
  }
  async function getPlayers(): Promise<void> {
    try {
      const response = await fetch(apiUrl + '/Players', {
        headers: { Authorization: basicAuth }
      })
      if (!response.ok) {
        console.error(`Failed to fetch players from API: ${response.statusText}`)
        players.value = []
        return
      }
      const data = await response.json()
      players.value =
        data.map(
          (playerData: any) =>
            new Player(playerData.name, playerData.id, playerData.eloRankingScore)
        ) ?? []
    } catch (error) {
      console.error(`Failed to fetch players from API: ${error}`)
      players.value = []
    }
  }
  async function sendGame(game: Game): Promise<[boolean, string]> {
    try {
      const playerList = game.ranking.map((player) => ({
        Id: player.id,
        Place: game.ranking.indexOf(player) + 1
      }))
      const response = await fetch(apiUrl + '/Game', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', Authorization: basicAuth },
        body: JSON.stringify(playerList)
      })
      if (response.ok) {
        game.gameSaved = true
        return [
          true,
          'The game was saved successfully, check the ranking to see the updated leaderboard.'
        ]
      }
      game.gameSaved = false
      return [false, 'Failed to save game, please try again.']
    } catch (error) {
      game.gameSaved = true
      return [
        false,
        'Seems like you are offline. Your game will be saved once you are back online.'
      ]
    }
  }
  return { players, createPlayer, getPlayers, sendGame }
})
