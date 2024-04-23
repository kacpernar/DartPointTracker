<template>
  <div v-if="currentGame">
    <div v-if="currentGame.isFinished" class="container">
    </div>
      <div v-else class="players-wrapper">
      <div v-for="player in currentGame.players" :key="player.id"  :class="['player-info' , player.id === currentGame.currentPlayer.id ? 'active-player' : 'inactive-player' ]" >
        <div>{{ player.name }}: {{ player.score }}</div>
        <div class="input-group">
          <template v-if="player.won">
            <h1>Won</h1>
          </template>
          <template v-else>
            <div class="circle-container">
              <div class="circle" v-for="dartThrow in player.currentRoundThrows" :key="dartThrow">{{ dartThrow }}</div>
            </div>
          </template>
        </div>
      </div>
    </div>
  <ChoosePoint @score="addDartThrow" @back="removeLastThrow"/>
  </div>
</template>
    
  
  <script lang="ts">
  import ChoosePoint from '../components/choose-point.vue';
  import { ref, defineComponent } from 'vue';
  
  export default defineComponent({
    name: 'PlayGame',
    components: {
      ChoosePoint
    },
    setup() {
      const currentGame = ref({
        players: [
          { id: 1, name: 'Player 1', score: 301, currentRoundThrows: [], won: false },
          { id: 2, name: 'Player 2', score: 301, currentRoundThrows: [], won: false }
        ],
        currentPlayer: { id: 1, name: 'Player 1', score: 301, currentRoundThrows: [], won: false },
        isFinished: false
      })
  
  
      return { currentGame }
    },
    methods: {
      addDartThrow(score: number) {
        console.log(score)
      },
      removeLastThrow() {
        console.log('Remove last throw')
      }
    }
  });

  </script>
  
  
<style scoped>
    .players-wrapper{
    flex-direction: row;
    flex-wrap: wrap;
    display: flex;
    margin-bottom: 3rem;
    margin-top: 1rem;
}
.player-info{
    margin-bottom: 10px;
    padding: 10px;
    flex: 1;
}
.input-group {
    margin-bottom: 10px;
}
.circle-container {
    display: flex; /* Use flexbox for horizontal layout */
    flex-wrap: nowrap; /* Prevent wrapping to next row */
}
.circle {
    width: 40px; /* Adjust the width as needed */
    height: 40px; /* Adjust the height as needed */
    font-size: 20px;
    color: #fff;
    text-align: center;
    line-height: 40px; /* Center content vertically */
    border-radius: 50%;
    background: #aeb5cd;
    margin-right: 10px; /* Add space between circles */
}
.active-player{
    border-left: 10px blueviolet solid;
}
.inactive-player{
    border-left: 10px gray solid;
}
.btn-light-purple{
    color: #fff;
    background-color: #7072bb;
}
</style>
  