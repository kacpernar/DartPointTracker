<template>
  <MainLayout/>
</template>

<script>
import MainLayout from './components/MainLayout.vue'

export default {
  name: 'App',
  components: {
    MainLayout
  }
}
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
</style>
