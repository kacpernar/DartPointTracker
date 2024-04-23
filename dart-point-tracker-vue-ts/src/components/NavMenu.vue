<script lang="ts">
import { ref, defineComponent } from 'vue';

export default defineComponent({
  name: 'NavMenu',
  setup() {
    const collapseNavMenu = ref(true)
    
    return { collapseNavMenu }
  },
  methods: {
    toggleNavMenu() {
      this.collapseNavMenu = !this.collapseNavMenu;
    }
  }
});

</script>

<template>
    <div :class="['top-row', 'ps-3', 'navbar', 'navbar-dark']">
      <div class="container-fluid">
        <a class="navbar-brand" href="#">Dart Point Tracker</a>
        <button title="Navigation menu" class="navbar-toggler" @click="toggleNavMenu">
          <span class="navbar-toggler-icon"></span>
        </button>
      </div>
    </div>
  
    <div class="nav-scrollable" :class="{'collapsed' : collapseNavMenu}" @click="toggleNavMenu">
      <nav class="flex-column">
        <div class="nav-item px-3">
          <router-link class="nav-link" :to="{ name: 'home' }">
            <span class="bi bi-house-door-fill" aria-hidden="true"></span> Home
          </router-link>
        </div>
        <div class="nav-item px-3">
          <router-link class="nav-link" :to="{ name: 'game' }">
            <span class="bi bi-controller" aria-hidden="true"></span> Game
          </router-link>
        </div>
        <div class="nav-item px-3">
          <router-link class="nav-link" :to="{ path: '/ranking' }">
            <span class="bi bi-trophy" aria-hidden="true"></span> Ranking
          </router-link>
        </div>
      </nav>
    </div>
  </template>
  

  
  <style scoped>

   .navbar-toggler {
    background-color: rgba(255, 255, 255, 0.1);
}

.top-row {
    height: 3.5rem;
    background-color: rgba(0,0,0,0.4);
}

.navbar-brand {
    font-size: 1.1rem;
}

.bi {
    width: 2rem;
    font-size: 1.1rem;
    vertical-align: text-top;
    top: -2px;
}

.nav-item {
    font-size: 0.9rem;
    padding-bottom: 0.5rem;
}

    .nav-item:first-of-type {
        padding-top: 1rem;
    }

    .nav-item:last-of-type {
        padding-bottom: 1rem;
    }

    .nav-item a {
        color: #d7d7d7;
        border-radius: 4px;
        height: 3rem;
        display: flex;
        align-items: center;
        line-height: 3rem;
    }

.nav-item a.active {
    background-color: rgba(255,255,255,0.25);
    color: white;
}

.nav-item a:hover {
    background-color: rgba(255,255,255,0.1);
    color: white;
}
@media (max-width: 640.98px) {
  .collapsed {
        display: none;
    }
}

@media (min-width: 641px) {
    .navbar-toggler {
        display: none;
    }

    .collapsed {
        /* Never collapse the sidebar for wide screens */
        display: block;
    }
    
    .nav-scrollable {
        /* Allow sidebar to scroll for tall menus */
        height: calc(100vh - 3.5rem);
        overflow-y: auto;
    }
}

  </style>
  