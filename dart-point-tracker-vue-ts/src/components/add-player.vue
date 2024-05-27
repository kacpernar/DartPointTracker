<template>
    <div>
        <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="" aria-hidden="true" ref="modalEle">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Select Players:</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label for="playerName">Player Name:</label>
                            <input type="text" class="form-control" id="playerName" v-model="newPlayerName" />
                        </div>
                        <div class="alert alert-success" role="alert" v-if="isSuccess">
                            Player added successfully!
                        </div>
                        <div class="alert alert-danger" role="alert" v-if="isFailed">
                            {{ message }}
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn purple-form" @click="addNewPlayer()">Add Player</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { ref, defineComponent, onMounted } from 'vue';
import { usePlayerStore } from '@/stores/playerStore';
import { Modal } from "bootstrap";

export default defineComponent({
    
    setup(props, { expose }) {
        
        const playerStore = usePlayerStore();
        const modalEle = ref(null);
        const isSuccess = ref(false);
        const isFailed = ref(false);
        const message = ref<string | null>(null);
        let thisModalObj: Modal | null = null;
        const newPlayerName= ref<string | null>(null);

        onMounted(() => {
            if (modalEle.value) {
                thisModalObj = new Modal(modalEle.value);
            }
        });

        async function addNewPlayer() {
            message.value = await playerStore.createPlayer(newPlayerName.value);
            if (!message.value) {
                newPlayerName.value = '';
                isFailed.value = false;
                isSuccess.value = true;
            } else {
                isSuccess.value = false;
                isFailed.value = true;
            }
        }
        function show() {
            if (thisModalObj) {
                thisModalObj.show();
            }
        }

        expose({ show: show })

        return { modalEle, isSuccess, isFailed, addNewPlayer, newPlayerName, message };
    }
});
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
    flex-direction: column; /* Change to column */
    gap: 5px; /* Add gap between rectangles */
}
/* Add your component-specific styles here */
</style>