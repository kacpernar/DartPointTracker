<template>
<div class="container">
    <div class="row justify-content-center">
        <div class="col-lg-6">
            <div class="mb-3 text-center">
                GAME MODE
            </div>
            <div class="mb-3 d-flex justify-content-center game-mode-container">
                <button v-for="gamePoints in gameSettings" :key="gamePoints" :class="[gamePoints === selectedGamePoints ? 'purple-form' : 'item' ]" @click="selectGameMode(gamePoints)">{{ gamePoints }}</button>
            </div>
            <div class="mb-3 text-center">
                PLAYERS
            </div>

            <div class="mb-3">
                <div class="player-list">
                    <div v-for="player in playersInGame" :key="player.id" class="player-container">
                        <span class="bi bi-person"></span>
                        <span class="player-name">{{ player.name }}</span>
                        <div class="trash btn-grey">
                            <span class="bi bi-trash"></span>
                        </div>
                    </div>
                </div>
                <div class="col-auto">
                    <button class="btn purple-form" >
                        <span class="bi bi-person-plus"></span>
                        Add Players
                    </button>
                    <button class="btn btn-light-purple" >
                        <span class="bi bi-person-plus"></span>
                        Create New Player
                    </button>
                </div>

            </div>
            <button class="btn start mt-2" @onclick="StartGame" :disabled="playersInGame.length === 0">
                <span class="bi bi-play"></span>
                Start Game
            </button>
        </div>
    </div>
</div>

</template>

<script setup>
  
  import { ref } from 'vue';
  var selectedGamePoints = ref(301)
  
  var playersInGame = ref([{ id: 1, name: 'Player 1' }, { id: 2, name: 'Player 2' }])
  const gameSettings = [101, 201, 301, 401, 501]

  function selectGameMode(gamePoints){
    selectedGamePoints.value = gamePoints
  }
</script>

<style scoped>
  .container {
    font-size: 24px;
    padding: 20px;
}

.game-mode-container button {
    padding: 10px 20px;
}
.game-mode-container {
    display: flex;
    justify-content: center;
    gap: 10px;
    flex-wrap: wrap;
    margin-top: 20px;
}

.purple-form {
    background-color: #50487b;
    color: #fff;
    border-radius: 5px;
    border: none;
}
.start{
    background-color: #29165f;
    color: #fff;
    margin-top: 2.5rem;
}
.start:disabled {
    background-color: #aeb5cd;
    color: #fff;
}

.player-container {
    display: flex; /* Use flexbox */
    justify-content: space-between; /* Distribute items evenly */
    align-items: center; /* Center items vertically */
    width: 100%; /* Fill available space */
    gap: 5px;
    padding: 10px 20px;
    border-radius: 5px;
    border: none;
    background-color: #aeb5cd;
    color: #fff;
    transition: background-color 0.3s ease;
}
.player-container .bi {
    margin: 5px;
}

.player-name {
    flex-grow: 1; /* Allow player name to grow and fill remaining space */
    margin-right: 10px; /* Add some margin between name and icon */
}

.player-list {
    display: flex;
    flex-direction: column; /* Change to column */
    gap: 5px; /* Add gap between rectangles */
}
.trash{
    display: flex;
    justify-content: center;
    align-items: center;
    border-radius: 5px;
}

.item {
    padding: 10px 20px;
    border-radius: 5px;
    border: none;
    background-color: #aeb5cd;
    color: #fff;
    cursor: pointer;
    transition: background-color 0.3s ease;
}
.item.selected {
    background-color: #4d297a;
}
.item:hover {
    background-color: #5a6268;
}
.btn-light-purple{
    color: #fff;
    background-color: #7072bb;
}
</style>
