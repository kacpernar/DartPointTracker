import { createMemoryHistory, createRouter } from 'vue-router'

import HomeView from './components/start-game.vue'
import Game from './components/play-game.vue'

const routes = [
  { path: '/', name: 'start-game', component: HomeView },
  { path: '/game', name: 'game', component: Game }
]

const router = createRouter({
  history: createMemoryHistory(),
  routes,
})

export default router