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

  <div class="nav-scrollable" :class="{ 'collapsed': collapseNavMenu }" @click="toggleNavMenu">
    <nav class="flex-column">
      <div class="nav-item px-3">
        <router-link class="nav-link" :to="{ name: 'home' }">
          <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor"
            class="bi bi-house-door-fill" viewBox="0 0 16 16">
            <path
              d="M6.5 14.5v-3.505c0-.245.25-.495.5-.495h2c.25 0 .5.25.5.5v3.5a.5.5 0 0 0 .5.5h4a.5.5 0 0 0 .5-.5v-7a.5.5 0 0 0-.146-.354L13 5.793V2.5a.5.5 0 0 0-.5-.5h-1a.5.5 0 0 0-.5.5v1.293L8.354 1.146a.5.5 0 0 0-.708 0l-6 6A.5.5 0 0 0 1.5 7.5v7a.5.5 0 0 0 .5.5h4a.5.5 0 0 0 .5-.5" />
          </svg>
          Home
        </router-link>
      </div>
      <div class="nav-item px-3">
        <router-link class="nav-link" :to="{ name: 'game' }">
          <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-controller"
            viewBox="0 0 16 16">
            <path
              d="M11.5 6.027a.5.5 0 1 1-1 0 .5.5 0 0 1 1 0m-1.5 1.5a.5.5 0 1 0 0-1 .5.5 0 0 0 0 1m2.5-.5a.5.5 0 1 1-1 0 .5.5 0 0 1 1 0m-1.5 1.5a.5.5 0 1 0 0-1 .5.5 0 0 0 0 1m-6.5-3h1v1h1v1h-1v1h-1v-1h-1v-1h1z" />
            <path
              d="M3.051 3.26a.5.5 0 0 1 .354-.613l1.932-.518a.5.5 0 0 1 .62.39c.655-.079 1.35-.117 2.043-.117.72 0 1.443.041 2.12.126a.5.5 0 0 1 .622-.399l1.932.518a.5.5 0 0 1 .306.729q.211.136.373.297c.408.408.78 1.05 1.095 1.772.32.733.599 1.591.805 2.466s.34 1.78.364 2.606c.024.816-.059 1.602-.328 2.21a1.42 1.42 0 0 1-1.445.83c-.636-.067-1.115-.394-1.513-.773-.245-.232-.496-.526-.739-.808-.126-.148-.25-.292-.368-.423-.728-.804-1.597-1.527-3.224-1.527s-2.496.723-3.224 1.527c-.119.131-.242.275-.368.423-.243.282-.494.575-.739.808-.398.38-.877.706-1.513.773a1.42 1.42 0 0 1-1.445-.83c-.27-.608-.352-1.395-.329-2.21.024-.826.16-1.73.365-2.606.206-.875.486-1.733.805-2.466.315-.722.687-1.364 1.094-1.772a2.3 2.3 0 0 1 .433-.335l-.028-.079zm2.036.412c-.877.185-1.469.443-1.733.708-.276.276-.587.783-.885 1.465a14 14 0 0 0-.748 2.295 12.4 12.4 0 0 0-.339 2.406c-.022.755.062 1.368.243 1.776a.42.42 0 0 0 .426.24c.327-.034.61-.199.929-.502.212-.202.4-.423.615-.674.133-.156.276-.323.44-.504C4.861 9.969 5.978 9.027 8 9.027s3.139.942 3.965 1.855c.164.181.307.348.44.504.214.251.403.472.615.674.318.303.601.468.929.503a.42.42 0 0 0 .426-.241c.18-.408.265-1.02.243-1.776a12.4 12.4 0 0 0-.339-2.406 14 14 0 0 0-.748-2.295c-.298-.682-.61-1.19-.885-1.465-.264-.265-.856-.523-1.733-.708-.85-.179-1.877-.27-2.913-.27s-2.063.091-2.913.27" />
          </svg>
          Game
        </router-link>
      </div>
      <div class="nav-item px-3">
        <router-link class="nav-link" :to="{ path: '/ranking' }">
          <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trophy-fill"
            viewBox="0 0 16 16">
            <path
              d="M2.5.5A.5.5 0 0 1 3 0h10a.5.5 0 0 1 .5.5q0 .807-.034 1.536a3 3 0 1 1-1.133 5.89c-.79 1.865-1.878 2.777-2.833 3.011v2.173l1.425.356c.194.048.377.135.537.255L13.3 15.1a.5.5 0 0 1-.3.9H3a.5.5 0 0 1-.3-.9l1.838-1.379c.16-.12.343-.207.537-.255L6.5 13.11v-2.173c-.955-.234-2.043-1.146-2.833-3.012a3 3 0 1 1-1.132-5.89A33 33 0 0 1 2.5.5m.099 2.54a2 2 0 0 0 .72 3.935c-.333-1.05-.588-2.346-.72-3.935m10.083 3.935a2 2 0 0 0 .72-3.935c-.133 1.59-.388 2.885-.72 3.935" />
          </svg>
          Ranking
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
  background-color: rgba(0, 0, 0, 0.4);
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
  background-color: rgba(255, 255, 255, 0.25);
  color: white;
}

.nav-item a:hover {
  background-color: rgba(255, 255, 255, 0.1);
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