<template>
    <div class="number-pad">
      <button v-for="number in numbers" :key="number" class="btn btn-point" @click="() => buttonClick(number)">{{ number }}</button>
      <button class="btn btn-point" @click="() => buttonClick(bullseye)" :disabled="tripleClicked">{{ bullseye }}</button>
      <button class="btn btn-light-purple" @click="doubleClick">x2</button>
      <button class="btn btn-primary" @click="tripleClick">x3</button>
    </div>
    <button class="btn btn-grey" @click="backClick">Back</button>
  </template>
  
  <script lang="ts">
  import { ref, defineComponent } from 'vue';
  
  export default defineComponent({
    name: 'ChoosePoint',
    emits: ['score', 'back'],
    setup() {
      const numbers = Array.from(Array(21).keys()); // Numbers 0 to 20
      const bullseye = 25;
      let doubleClicked = ref(false);
      let tripleClicked = ref(false);
  
      
      return { numbers, bullseye, doubleClicked, tripleClicked }
    },
    methods: {
      buttonClick(value: number) {
        if (this.doubleClicked) {
          this.$emit('score', value * 2)
          this.doubleClicked = false;
        } else if (this.tripleClicked) {
          this.$emit('score', value * 3)
          this.tripleClicked = false;
        }
        else{
          this.$emit('score', value)
        }
      },
      doubleClick() {
        this.doubleClicked = true;
        this.tripleClicked = false;
      },
      tripleClick() {
        this.doubleClicked = false;
        this.tripleClicked = true;
      },
      backClick() {
        this.$emit('back')
      }
    }
  });

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
.btn-light-purple{
    color: #fff;
    background-color: #7072bb;
}
.btn-grey{
    background-color: #797373;
    color: #fff;
}
  </style>
  