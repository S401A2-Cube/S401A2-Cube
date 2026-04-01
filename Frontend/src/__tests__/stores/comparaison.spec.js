import { describe, it, expect, beforeEach } from 'vitest'
import { setActivePinia, createPinia } from 'pinia'
import { useComparaisonStore } from '../../stores/comparaison'

describe('Comparaison Store', () => {
  let store

  beforeEach(() => {
    setActivePinia(createPinia())
    store = useComparaisonStore()
  })

  it('initializes with empty items', () => {
    expect(store.items).toEqual([])
  })

  it('initializes with count of 0', () => {
    expect(store.count).toBe(0)
  })

  it('initializes not full', () => {
    expect(store.isFull).toBe(false)
  })

  it('adds article to comparaison', () => {
    const article = { id: 1, name: 'Test Article' }
    const velo = { id: 1, name: 'Test Velo' }
    
    store.toggle(article, velo)
    expect(store.items.length).toBe(1)
    expect(store.count).toBe(1)
  })

  it('removes article from comparaison', () => {
    const article = { id: 1, name: 'Test Article' }
    const velo = { id: 1, name: 'Test Velo' }
    
    store.toggle(article, velo)
    store.toggle(article, velo)
    expect(store.items.length).toBe(0)
  })

  it('checks if article is selected', () => {
    const article = { id: 1, name: 'Test Article' }
    const velo = { id: 1, name: 'Test Velo' }
    
    expect(store.isSelected(article.id)).toBe(false)
    store.toggle(article, velo)
    expect(store.isSelected(article.id)).toBe(true)
  })

  it('adds multiple articles', () => {
    const articles = [
      { id: 1, name: 'Article 1' },
      { id: 2, name: 'Article 2' },
      { id: 3, name: 'Article 3' }
    ]
    const velo = { id: 1, name: 'Test Velo' }
    
    articles.forEach(article => {
      store.toggle(article, velo)
    })
    
    expect(store.count).toBe(3)
    expect(store.isFull).toBe(true)
  })

  it('prevents adding more than MAX items', () => {
    const articles = [
      { id: 1, name: 'Article 1' },
      { id: 2, name: 'Article 2' },
      { id: 3, name: 'Article 3' },
      { id: 4, name: 'Article 4' }
    ]
    const velo = { id: 1, name: 'Test Velo' }
    
    articles.forEach(article => {
      store.toggle(article, velo)
    })
    
    expect(store.count).toBe(3)
    expect(store.isFull).toBe(true)
  })

  it('marks as full after reaching MAX', () => {
    const articles = [
      { id: 1, name: 'Article 1' },
      { id: 2, name: 'Article 2' },
      { id: 3, name: 'Article 3' }
    ]
    const velo = { id: 1, name: 'Test Velo' }
    
    articles.forEach(article => {
      store.toggle(article, velo)
    })
    
    expect(store.isFull).toBe(true)
  })

  it('toggles article selection', () => {
    const article = { id: 1, name: 'Test Article' }
    const velo = { id: 1, name: 'Test Velo' }
    
    store.toggle(article, velo)
    expect(store.isSelected(article.id)).toBe(true)
    
    store.toggle(article, velo)
    expect(store.isSelected(article.id)).toBe(false)
  })
})
