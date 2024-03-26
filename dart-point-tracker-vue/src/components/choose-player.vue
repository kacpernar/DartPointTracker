<template>
    <div :class="'modal ' + modalClass" tabindex="-1" role="dialog" :style="{ display: modalDisplay }">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">Select Players:</h5>
          </div>
          <div class="modal-body">
            <div class="player-list">
              <div v-for="player in playersToSelect" :key="player.id" class="player-item item" :class="{ 'purple-form': player.selected }" >
                {{ player.name }}
              </div>
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn purple-form" @click="close">Add</button>
          </div>
        </div>
      </div>
    </div>
  
    <div v-if="showBackdrop" class="modal-backdrop fade show"></div>
  </template>
  
  <script>
  export default {
    props: {
      players: {
        type: Array,
        required: true
      },
      playersInGame: {
        type: Array,
        required: true
      }
    },
    data() {
      return {
        playersToSelect: [],
        modalDisplay: 'none',
        modalClass: '',
        showBackdrop: false
      };
    },
    methods: {
      open() {
        this.modalDisplay = 'block';
        this.modalClass = 'show';
        this.showBackdrop = true;
        this.playersToSelect = this.players.filter(player => !this.playersInGame.some(p => p.id === player.id));
      },
      close() {
        this.modalDisplay = 'none';
        this.modalClass = '';
        this.showBackdrop = false;
        this.$emit('close');
      }
      
    }
  };
  </script>
  
  <style scoped>

  </style>
  