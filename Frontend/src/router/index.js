import { createRouter, createWebHistory } from 'vue-router'

import HomeView from '../views/HomeView.vue'
import AboutView from '../views/AboutView.vue'
import ArticleView from '@/views/ArticleView.vue'
import CompteView from '@/views/CompteView.vue'

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
    },
    {
      path:'/compte',
      name:'compte',
      component: CompteView
    }
  ],
})

export default router
