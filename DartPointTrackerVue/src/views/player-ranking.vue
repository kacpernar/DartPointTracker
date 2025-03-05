<template>
    <div class="container">
        <div class="row">
            <div v-if="rankedPlayers.length > 0">
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
import { computed, onMounted } from 'vue';
import { usePlayerStore } from '../stores/playerStore';
import Player from '../models/player';
const playerStore = usePlayerStore();
onMounted(async () => {
    await playerStore.getPlayers();
});
const rankedPlayers = computed(() => {
    return playerStore.players
        .slice()
        .sort((a: Player, b: Player) => b.eloRankingScore - a.eloRankingScore)
        .map((player: Player, index: number) => {
            player.rank = index + 1;
            return player;
        });
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