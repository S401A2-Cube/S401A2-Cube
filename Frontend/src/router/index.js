import { createRouter, createWebHistory } from 'vue-router'

import HomeView from '../views/HomeView.vue'
import AboutView from '../views/AboutView.vue'
import ArticleView from '@/views/ArticleView.vue'
import SearchView from '@/views/SearchView.vue'
import InscriptionView from '@/views/InscriptionView.vue'
import ConnexionView from '@/views/ConnexionView.vue'
import ComparaisonView from '@/views/ComparaisonView.vue'

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
      path:'/Article',
      name:'article',
      component: SearchView
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
      path:'/inscription',
      name:'inscription',
      component: InscriptionView
    },
    {
      path:'/comparaison',
      name:'comparaison',
      component: ComparaisonView
    },
    {
      path:'/connexion',
      name:'connexion',
      component: ConnexionView
    }
  ],
})

export default router
