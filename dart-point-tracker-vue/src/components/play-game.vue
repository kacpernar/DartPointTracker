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
    
  
  <script setup>
  import ChoosePoint from './choose-point.vue';
  import { ref } from 'vue';
  
  const currentGame = ref({
  players: [
    { id: 1, name: 'Player 1', score: 301, currentRoundThrows: [null, null, null], dartThrows: [], throwNumber: 0, won: false, lastRoundOverThrow: false },
    { id: 2, name: 'Player 2', score: 301, currentRoundThrows: [null, null, null], dartThrows: [], throwNumber: 0, won: false, lastRoundOverThrow: false }
  ],
  currentPlayer: { id: 1, name: 'Player 1' },
  isFinished: false,
  totalThrows: 0,
  ranking: []
});

const addDartThrow = (points) => {
  currentGame.value.totalThrows++;
  const currentPlayer = currentGame.value.players.find(player => player.id === currentGame.value.currentPlayer.id);
  
  const [validThrow, currentRoundThrowNumber] = addDartThrowToPlayer(currentPlayer, points);

  if (currentPlayer.won) {
    currentGame.value.ranking.push(currentPlayer);
    currentPlayer.throwNumberInGameAtWin = currentGame.value.totalThrows;
    if (currentGame.value.players.filter(player => !player.won).length === 1) {
      currentGame.value.isFinished = true;
      currentGame.value.ranking.push(currentGame.value.players.find(player => !player.won));
      return;
    }
    nextPlayer();
  } else if (!validThrow || currentRoundThrowNumber === 0) {
    nextPlayer();
  }
};

const nextPlayer = () => {
  let nextPlayerIndex = currentGame.value.players.findIndex(player => player.id === currentGame.value.currentPlayer.id) + 1;
  if (nextPlayerIndex >= currentGame.value.players.length) {
    nextPlayerIndex = 0;
  }
  let nextPlayer = currentGame.value.players[nextPlayerIndex];
  while (nextPlayer.won) {
    nextPlayerIndex++;
    if (nextPlayerIndex >= currentGame.value.players.length) {
      nextPlayerIndex = 0;
    }
    nextPlayer = currentGame.value.players[nextPlayerIndex];
  }
  currentGame.value.currentPlayer = nextPlayer;
  currentGame.value.currentPlayer.currentRoundThrows = [null, null, null];
};

const addDartThrowToPlayer = (player, points) => {
  player.currentRoundThrows[player.throwNumber % 3] = points;
  player.dartThrows.push({ number: player.throwNumber, points: points });
  player.throwNumber++;
  player.score -= points;
  switch (player.score) {
    case player.score < 0:
      player.score += player.currentRoundThrows.reduce((acc, curr) => acc + (curr || 0), 0);
      player.lastRoundOverThrow = true;
      return [false, player.throwNumber % 3];
    case 0:
      player.won = true;
      break;
  }
  return [true, player.throwNumber % 3];
};

const removeLastThrow = () => {
  if(currentGame.value.totalThrows > 0){
    currentGame.value.totalThrows--;
  }
  else{
    return;
  }
  const currentPlayer = currentGame.value.currentPlayer;
  if(currentPlayer.currentRoundThrowNumber === 0){
    reloadPreviousRound(currentPlayer);
    previousPlayer(currentGame.value);
  }
  while (currentPlayer.won) {
    if(currentPlayer.throwNumberInGameAtWin === currentGame.value.totalThrows + 1){
      currentPlayer.won = false;
      break;
    }
    previousPlayer(currentGame.value.players, currentPlayer);
  }
  removeLastDartThrow(currentPlayer);

}
const previousPlayer = (players, currentPlayer) => {
    const index = players.value.indexOf(currentPlayer.value);
    currentPlayer.value = index === 0 ? players.value[players.value.length - 1] : players.value[index - 1];
  }

const reloadPreviousRound = (player) => {
    if(player.throwNumber < 3){
      return;
    }
    else{
      player.currentRoundThrows[0] = player.dartThrows[player.dartThrows.length - 3].points;
      player.currentRoundThrows[1] = player.dartThrows[player.dartThrows.length - 2].points;
      player.currentRoundThrows[2] = player.dartThrows[player.dartThrows.length - 1].points;
    }
}

const removeLastDartThrow = (player) => {
  if (player.throwNumber === 0) {
    return;
  }

  const lastThrow = player.dartThrows[player.dartThrows.length - 1];
  player.dartThrows.pop();
  player.throwNumber--;
  player.currentRoundThrows[player.throwNumber % 3] = null;

  if (player.lastRoundOverThrow) {
    player.score += player.currentRoundThrows.reduce((acc, curr) => acc + (curr || 0), 0);
    player.lastRoundOverThrow = false;
  } else {
    player.score += lastThrow.points;
  }
};
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
  