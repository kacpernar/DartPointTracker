<template>
    <div class="number-pad">
      <button v-for="number in numbers" :key="number" class="btn btn-point" @click="() => buttonClick(number)">{{ number }}</button>
      <button class="btn btn-point" @click="() => buttonClick(bullseye)" :disabled="tripleClicked">{{ bullseye }}</button>
      <button class="btn btn-light-purple" @click="doubleClick">x2</button>
      <button class="btn btn-primary" @click="tripleClick">x3</button>
    </div>
    <button class="btn btn-grey" >Back</button>
  </template>
  
  <script setup>
  import { ref, defineEmits } from 'vue';
  
  const emit = defineEmits(['score']);
  const numbers = Array.from(Array(21).keys()); // Numbers 0 to 20
  const bullseye = 25;
  let doubleClicked = ref(false);
  let tripleClicked = ref(false);
  
  const buttonClick = (value) => {
    if (doubleClicked.value) {
      emit('score', value * 2)
      doubleClicked.value = false;
    } else if (tripleClicked.value) {
        emit('score', value * 3)
      tripleClicked.value = false;
    }
    emit('score', value)
  };
  
  const doubleClick = () => {
    doubleClicked.value = true;
    tripleClicked.value = false;
  };
  
  const tripleClick = () => {
    doubleClicked.value = false;
    tripleClicked.value = true;
  };
  
  </script>
  
  <style scoped>
.number-pad {
    display: grid;
    grid-template-columns: repeat(4, 1fr);
    grid-gap: 8px;
}
.btn-point {
    padding: 10px;
    font-size: 24px;
    background-color: #aeb5cd;
    color: #fff;
    border: none;
    border-radius: 5px;
    cursor: pointer;
}
  </style>
  