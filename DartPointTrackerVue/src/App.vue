<template>
    <MainLayout />
</template>

<script setup lang="ts">
import MainLayout from './components/MainLayout.vue'
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

.btn-light-purple {
    color: #fff;
    background-color: #7072bb;
}

.btn:hover {
    box-shadow: 0 8px 16px rgba(0, 0, 0, 0.3);
    --bs-btn-hover-color: #fff;
}

.btn-light-purple:hover {
    background-color: #5a5cb2;
    /* Slightly darkened color */
}

.btn-grey {
    background-color: #797373;
    color: #fff;
}

.btn-purple {
    background-color: #50487b;
    color: #fff;
    border-radius: 5px;
    border: none;
}

.btn-purple:hover {
    background-color: #50487b;
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

.container {
    font-size: 24px;
    padding: 20px;
}

.text-center {
    color: midnightblue;
    font-weight: bold;
}

table {
    font-size: 18px;
    border-collapse: collapse;
    width: 100%;
}

th,
td {
    border: 1px solid #ddd;
    padding: 8px;
    text-align: left;
}

th {
    background-color: #f2f2f2;
}

tr:nth-child(even) td {
    background-color: #f2f2f2;
}

tr:hover td {
    background-color: #ddd;
}

.btn:focus,
.btn:active:focus,
.btn-link.nav-link:focus,
.form-control:focus,
.form-check-input:focus {
    box-shadow: 0 0 0 0.1rem white, 0 0 0 0.25rem #258cfb;
}

.alert {
    margin-top: 10px;
}
</style>
