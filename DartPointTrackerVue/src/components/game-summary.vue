<template>
    <div>
        <div class="mb-3 text-center">
            SUMMARY
        </div>
        <table>
            <thead>
                <tr>
                    <th>Place</th>
                    <th>Name</th>
                    <th>Throws</th>
                    <th>Score</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(player, index) in ranking" :key="player.id">
                    <td>{{ index + 1 }}</td>
                    <td>{{ player.name }}</td>
                    <td>{{ player.currentThrowNumber }}</td>
                    <td>{{ player.score }}</td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script setup lang="ts">
import Player from '../models/player';
import { type PropType } from 'vue'

defineProps({
    ranking: {
        type: Object as PropType<Player[]>,
        required: true
    }
});

</script>