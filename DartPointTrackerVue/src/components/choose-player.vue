<template>
    <div>
        <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="" aria-hidden="true" ref="modalEle">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Select Players:</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"
                            @click="resetSelectedPlayers"></button>
                    </div>
                    <div class="modal-body">
                        <div class="player-list">
                            <div v-for="player in playersToSelect" :key="player.name"
                                :class="['player-item', 'item', { 'purple-form': player.selected }]"
                                @click="selectPlayer(player)">
                                {{ player.name }}
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-purple" @click="addPlayersAndClose">Add</button>
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal"
                            @click="resetSelectedPlayers">
                            Close
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { ref, type PropType } from 'vue';
import Player from '../models/player';
import { onMounted } from "vue";
import { Modal } from "bootstrap";

const props = defineProps({
    players: {
        type: Array as PropType<Player[]>,
        required: true
    },
    playersInGame: {
        type: Array as PropType<Player[]>,
        required: true
    }
});
// Emits
const emit = defineEmits(['add-players']);
// Expose the `show` function to the parent component
defineExpose({ show });

const selectedPlayers = ref<Player[]>([]);

const playersToSelect = ref<Player[]>([]);

function selectPlayer(player: Player) {
    player.selected = !player.selected;
    if (player.selected) {
        selectedPlayers.value.push(player);
    } else {
        const index = selectedPlayers.value.findIndex(p => p.id === player.id);
        if (index !== -1) {
            selectedPlayers.value.splice(index, 1);
        }
    }
}

function addPlayersAndClose() {
    emit('add-players', selectedPlayers.value);
    selectedPlayers.value = [];
    closeModal();
}

function resetSelectedPlayers() {
    selectedPlayers.value.forEach(player => player.selected = false);
    selectedPlayers.value = [];
    closeModal();
}

const modalEle = ref(null);
let thisModalObj: Modal | null = null;

onMounted(() => {
    if (modalEle.value) {
        thisModalObj = new Modal(modalEle.value);
    }
});

function show() {
    if (thisModalObj) {
        playersToSelect.value = props.players.filter(player => !props.playersInGame.includes(player));
        thisModalObj.show();
    }
}

function closeModal() {
    if (thisModalObj) {
        thisModalObj.hide();
    }
}

</script>

<style scoped>
.player-item {
    height: 50px;
    font-size: 24px;
    display: flex;
    align-items: center;
    justify-content: center;
    cursor: pointer;
}

.player-list {
    display: flex;
    flex-direction: column;
    gap: 5px;
}
</style>
