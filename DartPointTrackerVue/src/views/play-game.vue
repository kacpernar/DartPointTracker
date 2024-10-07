<template>
  <div v-if="currentGame">
    <div v-if="currentGame.isFinished" class="container">
      <div class="container">
        <GameSummary :ranking="currentGame.ranking"></GameSummary>
        <div>
          <button class="btn btn-purple" :disabled="currentGame.gameSaved" @click="sendGame">
            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor"
              class="bi bi-file-arrow-up" viewBox="0 0 16 16">
              <path
                d="M8 11a.5.5 0 0 0 .5-.5V6.707l1.146 1.147a.5.5 0 0 0 .708-.708l-2-2a.5.5 0 0 0-.708 0l-2 2a.5.5 0 1 0 .708.708L7.5 6.707V10.5a.5.5 0 0 0 .5.5" />
              <path
                d="M4 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h8a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2zm0 1h8a1 1 0 0 1 1 1v12a1 1 0 0 1-1 1H4a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1" />
            </svg>
            Save Game
          </button>
          <button class="btn btn-light-purple" @click="goToRanking">
            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trophy-fill"
              viewBox="0 0 16 16">
              <path
                d="M2.5.5A.5.5 0 0 1 3 0h10a.5.5 0 0 1 .5.5q0 .807-.034 1.536a3 3 0 1 1-1.133 5.89c-.79 1.865-1.878 2.777-2.833 3.011v2.173l1.425.356c.194.048.377.135.537.255L13.3 15.1a.5.5 0 0 1-.3.9H3a.5.5 0 0 1-.3-.9l1.838-1.379c.16-.12.343-.207.537-.255L6.5 13.11v-2.173c-.955-.234-2.043-1.146-2.833-3.012a3 3 0 1 1-1.132-5.89A33 33 0 0 1 2.5.5m.099 2.54a2 2 0 0 0 .72 3.935c-.333-1.05-.588-2.346-.72-3.935m10.083 3.935a2 2 0 0 0 .72-3.935c-.133 1.59-.388 2.885-.72 3.935" />
            </svg>
            Ranking
          </button>
        </div>
        <div v-if="_message" :class="[_isSuccess ? 'alert-success' : 'alert-danger', 'alert']" role="alert">
          {{ _message }}
        </div>
      </div>

    </div>
    <div v-else>
      <div class="players-wrapper">
        <div v-for="player in currentGame.players" :key="player.id"
          :class="['player-info', player.id === currentGame.currentPlayer.id ? 'active-player' : 'inactive-player']">
          <div>{{ player.name }}: {{ player.score }}</div>
          <div class="input-group">
            <template v-if="player.won">
              <h1>Won</h1>
            </template>
            <template v-else>
              <div class="circle-container">
                <div class="circle" v-for="(dartThrow, index) in player.currentRoundThrows" :key="index">{{ dartThrow }}
                </div>
              </div>
            </template>
          </div>
        </div>
      </div>
      <ChoosePoint @score="addDartThrow" @back="removeLastThrow" />
    </div>

  </div>
</template>


<script setup lang="ts">
import ChoosePoint from '../components/choose-point.vue';
import GameSummary from '../components/game-summary.vue';
import Game from '../models/game';
import { useGameStore } from '../stores/gameStore';
import { usePlayerStore } from '../stores/playerStore';
import { useRouter } from 'vue-router';
import { ref } from 'vue';

var gameStore = useGameStore();
var currentGame = gameStore.currentGame;
var playerStore = usePlayerStore();
const router = useRouter();

const _isSuccess = ref<boolean | null>(null);
const _message = ref<string | null>(null);

function addDartThrow(score: number) {
  currentGame?.addDartThrow(score);
}
function removeLastThrow() {
  currentGame?.removeLastDartThrow();
}
function goToRanking() {
  router.push('/ranking');
}
async function sendGame() {
  const [success, message] = await playerStore.sendGame(currentGame as Game);
  _isSuccess.value = success;
  _message.value = message;
}


</script>


<style scoped>
.players-wrapper {
  flex-direction: row;
  flex-wrap: wrap;
  display: flex;
  margin-bottom: 3rem;
  margin-top: 1rem;
}

.player-info {
  margin-bottom: 10px;
  padding: 10px;
  flex: 1;
}

.input-group {
  margin-bottom: 10px;
}

.circle-container {
  display: flex;
  /* Use flexbox for horizontal layout */
  flex-wrap: nowrap;
  /* Prevent wrapping to next row */
}

.circle {
  width: 40px;
  /* Adjust the width as needed */
  height: 40px;
  /* Adjust the height as needed */
  font-size: 20px;
  color: #fff;
  text-align: center;
  line-height: 40px;
  /* Center content vertically */
  border-radius: 50%;
  background: #aeb5cd;
  margin-right: 10px;
  /* Add space between circles */
}

.active-player {
  border-left: 10px blueviolet solid;
}

.inactive-player {
  border-left: 10px gray solid;
}

.btn-light-purple {
  color: #fff;
  background-color: #7072bb;
}
</style>