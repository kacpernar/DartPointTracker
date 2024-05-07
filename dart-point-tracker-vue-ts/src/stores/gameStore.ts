import { ref } from 'vue';
import { defineStore } from 'pinia'
import Player from '../models/player'; 
import Game from '../models/game'; 


export const gameStore = defineStore( 'gameStore', () =>{
  const currentGame = ref<Game | null>(null);

  function InitializeGame(players: Player[], points: number) {
    currentGame.value = new Game(players, points);
  }
  return { Game }
})
 
