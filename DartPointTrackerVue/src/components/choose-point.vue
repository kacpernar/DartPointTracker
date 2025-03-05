<template>
  <div class="number-pad">
    <button v-for="number in numbers" :key="number" class="btn btn-point" @click="() => buttonClick(number)">{{ number
      }}</button>
    <button class="btn btn-point" @click="() => buttonClick(bullseye)" :disabled="tripleClicked">{{ bullseye }}</button>
    <button class="btn btn-light-purple" @click="doubleClick">x2</button>
    <button class="btn btn-primary" @click="tripleClick">x3</button>
  </div>
  <button class="btn btn-grey" @click="backClick">Back</button>
</template>

<script setup lang="ts">
import { ref } from 'vue';
const emit = defineEmits(['score', 'back']);
const numbers = Array.from(Array(21).keys()); // Numbers 0 to 20
const bullseye = 25;
let doubleClicked = ref(false);
let tripleClicked = ref(false);
function buttonClick(value: number) {
  if (doubleClicked.value) {
    emit('score', value * 2);
    doubleClicked.value = false;
  } else if (tripleClicked.value) {
    emit('score', value * 3);
    tripleClicked.value = false;
  } else {
    emit('score', value);}};
function doubleClick() {
  doubleClicked.value = true;
  tripleClicked.value = false;};
function tripleClick() {
  doubleClicked.value = false;
  tripleClicked.value = true;};
function backClick() {
  emit('back');};
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

.btn-light-purple {
  color: #fff;
  background-color: #7072bb;
}

.btn-grey {
  background-color: #797373;
  color: #fff;
}
</style>