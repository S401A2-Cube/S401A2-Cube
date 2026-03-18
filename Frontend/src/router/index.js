import { createRouter, createWebHistory } from 'vue-router'

import HomeView from '../views/HomeView.vue'
import AboutView from '../views/AboutView.vue'
import ArticleView from '@/views/ArticleView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: 
  [
    {
      path:'/',
      name:'home',
      component: HomeView
    },
    {
      path:'/about',
      name:'about',
      component: AboutView
    },
    {
      path:'/detail/article/:id',
      name:'detail',
      component: ArticleView
    }
  ],
})

export default router
