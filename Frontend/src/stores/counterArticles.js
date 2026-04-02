import { defineStore } from 'pinia'
import { computed, ref } from 'vue'

export const useCartStore = defineStore('counterArticles', () => {
  const lignes = ref([])

  const nbArticles = computed(() =>
    lignes.value.reduce((total, l) => total + (l.qtePanier || 0), 0)
  )

  return { lignes, nbArticles }
})