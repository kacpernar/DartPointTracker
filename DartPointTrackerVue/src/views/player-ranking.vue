<template>
    <div class="container">
        <div class="row">
            <p v-if="isLoading">Loading players...</p>
            <div v-else-if="rankedPlayers.length > 0">
                <div class="mb-3 text-center">
                    PLAYER RANKING
                </div>
                <table class="table">
                    <thead>
                        <tr>
                            <th>Rank</th>
                            <th>Name</th>
                            <th>Elo Score</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="player in rankedPlayers" :key="player.id"
                            :style="'background-color: ' + getRowStyle(player)">
                            <td>{{ player.rank }}</td>
                            <td>{{ player.name }}</td>
                            <td>{{ player.eloRankingScore }}</td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <p v-else>No players available.</p>
        </div>
    </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { usePlayerStore } from '../stores/playerStore';
import Player from '../models/player';

const playerStore = usePlayerStore();
const rankedPlayers = ref<Player[]>([]);
const isLoading = ref(true);

onMounted(async () => {
    const sw1 = performance.now();
    await playerStore.getPlayers();
    const sw1End = performance.now();
    console.log(`Time to get players: ${(sw1End - sw1).toFixed(1)} ms`);

    const sw = performance.now();
    rankedPlayers.value = playerStore.players
        .slice()
        .sort((a: Player, b: Player) => b.eloRankingScore - a.eloRankingScore)
        .map((player: Player, index: number) => {
            player.rank = index + 1;
            return player;
        });

    const swEnd = performance.now();
    console.log(`Time to rank players: ${(swEnd - sw).toFixed(1)} ms`);

    isLoading.value = false;
});

function getRowStyle(player: Player) {
    if (player.rank === 1) {
        return 'green';
    }
    if (player.rank === 2 || player.rank === 3) {
        return 'orange';
    }
    return '';
}
</script>

<style scoped></style>
