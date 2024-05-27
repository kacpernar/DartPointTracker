<template>
  <MainLayout/>
</template>

<script lang="ts" >
import { defineComponent } from 'vue'
import MainLayout from './components/MainLayout.vue'

export default defineComponent({
  name: 'App',
  components: {
    MainLayout
  }
});
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
}
.btn {
    font-size: 24px; 
    padding: 10px 20px; 
    width: 100%;
    margin-top: 8px;
}
.btn-light-purple{
    color: #fff;
    background-color: #7072bb;
}
.btn-grey{
    background-color: #797373;
    color: #fff;
}
.item {
    padding: 10px 20px;
    border-radius: 5px;
    border: none;
    background-color: #aeb5cd;
    color: #fff;
    cursor: pointer;
    transition: background-color 0.3s ease;
}
.item.selected {
    background-color: #4d297a;
}
.item:hover {
    background-color: #5a6268;
}
.purple-form {
    background-color: #50487b;
    color: #fff;
    border-radius: 5px;
    border: none;
}
</style>
