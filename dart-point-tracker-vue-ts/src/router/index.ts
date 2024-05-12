import { createRouter, createWebHashHistory, RouteRecordRaw } from 'vue-router'
import StartGame from '../views/start-game.vue'
import playerRanking from '@/views/player-ranking.vue'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'home',
    component: StartGame
  },
  {
    path: '/game',
    name: 'game',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/play-game.vue')
  },
  {
    path: '/ranking',
    name: 'ranking',
    component: playerRanking
  }

]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router
