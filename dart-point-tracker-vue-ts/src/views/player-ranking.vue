<template>
    <div class="container">
        <div class="row">
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
                    <tr v-for="player in rankedPlayers" :key="player.id" :style="'background-color: ' + (getRowStyle(player))">
                        <td>{{ player.rank }}</td>
                        <td>{{ player.name }}</td>
                        <td>{{ player.eloRankingScore }}</td>
                    </tr>
                </tbody>
            </table>
            <p v-if="rankedPlayers.length === 0">No players available.</p>
        </div>
    </div>
</template>

<script lang="ts">
import { computed, defineComponent, onMounted } from 'vue';
import { usePlayerStore } from '../stores/playerStore';
import Player from '../models/player';

export default defineComponent({
    name: 'PlayerRanking',

    setup() {
        const playerStore = usePlayerStore();

        // Fetch players when the component is mounted
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

        function getRowStyle (player: Player) {
            if (player.rank === 1) {
                return 'green';
            }
            if (player.rank === 2 || player.rank === 3) {
                return 'orange';
            }
            return '';
        }

        return { rankedPlayers, getRowStyle };
    },
});
</script>

<style scoped>



</style>