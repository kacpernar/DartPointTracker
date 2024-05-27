<template>
  <div v-if="currentGame">
    <div v-if="currentGame.isFinished" class="container">
      <div class="container">
        <GameSummary :ranking="currentGame.ranking"></GameSummary>
        <div>
          <button class="btn purple-form" :disabled="currentGame.gameSaved" @click="sendGame">
            <span class="bi bi-file-arrow-up"></span>
            Save Game
          </button>
          <button class="btn btn-light-purple" @click="goToRanking">
            <span class="bi bi-trophy"></span>
            Ranking
          </button>
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


<script lang="ts">
import ChoosePoint from '../components/choose-point.vue';
import GameSummary from '../components/game-summary.vue';
import { defineComponent } from 'vue';
import { useGameStore } from '../stores/gameStore';
import { useRouter } from 'vue-router';

export default defineComponent({
  name: 'PlayGame',
  components: {
    ChoosePoint,
    GameSummary
  },
  setup() {
    var gameStore = useGameStore();
    var currentGame = gameStore.currentGame;
    const router = useRouter();

    function addDartThrow(score: number) {
      currentGame?.addDartThrow(score);
    }
    function removeLastThrow() {
      currentGame?.removeLastDartThrow();
    }
    function goToRanking() {
      router.push('/ranking');
    }
    function sendGame() {
      console.log('send game');
    }
    return { currentGame, addDartThrow, removeLastThrow, goToRanking, sendGame };
  }
});

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