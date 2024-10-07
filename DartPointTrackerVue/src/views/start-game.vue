<template>
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-lg-6">
                <div class="mb-3 text-center">
                    GAME MODE
                </div>
                <div class="mb-3 d-flex justify-content-center game-mode-container">
                    <button v-for="gamePoints in gameSettings" :key="gamePoints"
                        :class="[gamePoints === selectedGamePoints ? 'purple-form' : 'item']"
                        @click="selectGameMode(gamePoints)">{{ gamePoints }}</button>
                </div>
                <div class="mb-3 text-center">
                    PLAYERS
                </div>

                <div class="mb-3">
                    <div class="player-list">
                        <div v-for="player in playersInGame" :key="player.id" class="player-container">
                            <div>
                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="currentColor"
                                    class="bi bi-person-fill" viewBox="0 0 16 16">
                                    <path d="M3 14s-1 0-1-1 1-4 6-4 6 3 6 4-1 1-1 1zm5-6a3 3 0 1 0 0-6 3 3 0 0 0 0 6" />
                                </svg>
                                <span class="player-name">{{ player.name }}</span>
                            </div>

                            <div class="trash btn-grey" @click="excludePlayer(player)">
                                <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor"
                                    class="bi bi-trash3-fill" viewBox="0 0 16 16">
                                    <path
                                        d="M11 1.5v1h3.5a.5.5 0 0 1 0 1h-.538l-.853 10.66A2 2 0 0 1 11.115 16h-6.23a2 2 0 0 1-1.994-1.84L2.038 3.5H1.5a.5.5 0 0 1 0-1H5v-1A1.5 1.5 0 0 1 6.5 0h3A1.5 1.5 0 0 1 11 1.5m-5 0v1h4v-1a.5.5 0 0 0-.5-.5h-3a.5.5 0 0 0-.5.5M4.5 5.029l.5 8.5a.5.5 0 1 0 .998-.06l-.5-8.5a.5.5 0 1 0-.998.06m6.53-.528a.5.5 0 0 0-.528.47l-.5 8.5a.5.5 0 0 0 .998.058l.5-8.5a.5.5 0 0 0-.47-.528M8 4.5a.5.5 0 0 0-.5.5v8.5a.5.5 0 0 0 1 0V5a.5.5 0 0 0-.5-.5" />
                                </svg>
                            </div>
                        </div>
                    </div>
                    <div class="col-auto">
                        <button class="btn btn-purple" @click="showChoosePlayerModal">
                            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="currentColor"
                                class="bi bi-person-fill-add" viewBox="0 0 16 16">
                                <path
                                    d="M12.5 16a3.5 3.5 0 1 0 0-7 3.5 3.5 0 0 0 0 7m.5-5v1h1a.5.5 0 0 1 0 1h-1v1a.5.5 0 0 1-1 0v-1h-1a.5.5 0 0 1 0-1h1v-1a.5.5 0 0 1 1 0m-2-6a3 3 0 1 1-6 0 3 3 0 0 1 6 0" />
                                <path
                                    d="M2 13c0 1 1 1 1 1h5.256A4.5 4.5 0 0 1 8 12.5a4.5 4.5 0 0 1 1.544-3.393Q8.844 9.002 8 9c-5 0-6 3-6 4" />
                            </svg>
                            Add Players
                        </button>
                        <button class="btn btn-light-purple" @click="openAddPlayerModal">
                            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="currentColor"
                                class="bi bi-person-fill-add" viewBox="0 0 16 16">
                                <path
                                    d="M12.5 16a3.5 3.5 0 1 0 0-7 3.5 3.5 0 0 0 0 7m.5-5v1h1a.5.5 0 0 1 0 1h-1v1a.5.5 0 0 1-1 0v-1h-1a.5.5 0 0 1 0-1h1v-1a.5.5 0 0 1 1 0m-2-6a3 3 0 1 1-6 0 3 3 0 0 1 6 0" />
                                <path
                                    d="M2 13c0 1 1 1 1 1h5.256A4.5 4.5 0 0 1 8 12.5a4.5 4.5 0 0 1 1.544-3.393Q8.844 9.002 8 9c-5 0-6 3-6 4" />
                            </svg>
                            Create New Player
                        </button>
                    </div>

                </div>
                <button class="btn start mt-2" @click="StartGame()" :disabled="playersInGame.length === 0">
                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="currentColor"
                        class="bi bi-play-fill" viewBox="0 0 16 16">
                        <path
                            d="m11.596 8.697-6.363 3.692c-.54.313-1.233-.066-1.233-.697V4.308c0-.63.692-1.01 1.233-.696l6.363 3.692a.802.802 0 0 1 0 1.393" />
                    </svg>
                    Start Game
                </button>
            </div>
        </div>
    </div>

    <ChoosePlayer ref="choosePlayerModal" :playersInGame="playersInGame" @add-players="addSelectedPlayers"
        :players="players" />
    <AddPlayer ref="addPlayerModal" />
</template>

<script setup lang="ts">

import { ref, onMounted } from 'vue';
import { useGameStore } from '../stores/gameStore';
import { usePlayerStore } from '../stores/playerStore';
import { useRouter } from 'vue-router';
import Player from '../models/player';
import ChoosePlayer from '../components/choose-player.vue';
import AddPlayer from '../components/add-player.vue';
import { Modal } from 'bootstrap';

const gameSettings = [101, 201, 301, 401, 501]
const router = useRouter();
const gameStore = useGameStore();
const playerStore = usePlayerStore();

var selectedGamePoints = ref(301)
const players = ref<Player[]>([]);
const playersInGame = ref<Player[]>([]);

let choosePlayerModal = ref<Modal | null>(null);
let addPlayerModal = ref<Modal | null>(null);

function showChoosePlayerModal() {
    if (choosePlayerModal.value !== null) {
        choosePlayerModal.value.show();
    }
}

function openAddPlayerModal() {
    if (addPlayerModal.value !== null) {
        addPlayerModal.value.show();
    }
}

onMounted(async () => {
    players.value = await playerStore.getPlayers();
});


function addSelectedPlayers(players: Player[]) {
    playersInGame.value = playersInGame.value.concat(players);
}
function excludePlayer(player: Player) {
    playersInGame.value.splice(playersInGame.value.indexOf(player), 1);
    player.selected = false;
}


function selectGameMode(gamePoints: number) {
    selectedGamePoints.value = gamePoints
}

function StartGame() {

    gameStore.InitializeGame(playersInGame.value, selectedGamePoints.value);
    playersInGame.value = [];
    router.push('/game');
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



.start {
    background-color: #29165f;
    color: #fff;
    margin-top: 2.5rem;
}

.start:disabled {
    background-color: #aeb5cd;
    color: #fff;
}

.player-container {
    display: flex;
    /* Use flexbox */
    justify-content: space-between;
    /* Distribute items evenly */
    align-items: center;
    /* Center items vertically */
    width: 100%;
    /* Fill available space */
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
    flex-grow: 1;
    /* Allow player name to grow and fill remaining space */
    margin-right: 10px;
    /* Add some margin between name and icon */
}

.player-list {
    display: flex;
    flex-direction: column;
    /* Change to column */
    gap: 5px;
    /* Add gap between rectangles */
}

.trash {
    display: flex;
    justify-content: center;
    align-items: center;
    border-radius: 5px;
}

.btn-light-purple {
    color: #fff;
    background-color: #7072bb;
}
</style>
