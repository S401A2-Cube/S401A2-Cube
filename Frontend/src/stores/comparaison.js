import { defineStore } from 'pinia'
import { ref, computed } from 'vue'

export const useComparaisonStore = defineStore('comparaison', () => {
    const MAX = 3

    const items = ref([])

    const isFull = computed(() => items.value.length >= MAX)
    const count = computed(() => items.value.length)


    function isSelected(articleId) {
        return items.value.some(i => i.article.id === articleId) 
    }

    function toggle(article, velo) {
        const IsPresent = isSelected(article.id)

        if (IsPresent) {
            items.value = items.value.filter(item => item.article.id !== article.id)
        }
        else {
            if (!isFull.value) {items.value.push({article,velo})
            
            } else {console.log("limite")}
        }
    }
    return { items, isFull, count, isSelected, toggle }
})