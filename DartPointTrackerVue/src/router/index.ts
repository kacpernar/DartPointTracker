import { createRouter, createWebHashHistory } from 'vue-router'
import StartGame from '@/views/start-game.vue'
import playerRanking from '@/views/player-ranking.vue'
import PlayGame from '@/views/play-game.vue'

const router = createRouter({
  history: createWebHashHistory(),
  routes: [
    {
      path: '/',
      name: 'home',
      component: StartGame
    },
    {
      path: '/game',
      name: 'game',
      component: PlayGame
    },
    {
      path: '/ranking',
      name: 'ranking',
      component: playerRanking
    }
  ]
})

export default router
