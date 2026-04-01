import { describe, it, expect, beforeEach } from 'vitest'
import { setActivePinia, createPinia } from 'pinia'
import { useCounterStore } from '../../stores/counter'

describe('Counter Store', () => {
  beforeEach(() => {
    setActivePinia(createPinia())
  })

  it('initializes with count of 0', () => {
    const store = useCounterStore()
    expect(store.count).toBe(0)
  })

  it('increments count', () => {
    const store = useCounterStore()
    store.increment()
    expect(store.count).toBe(1)
  })

  it('increments count multiple times', () => {
    const store = useCounterStore()
    store.increment()
    store.increment()
    store.increment()
    expect(store.count).toBe(3)
  })

  it('computes double count', () => {
    const store = useCounterStore()
    expect(store.doubleCount).toBe(0)
    store.increment()
    expect(store.doubleCount).toBe(2)
  })

  it('doubles count correctly', () => {
    const store = useCounterStore()
    store.increment()
    store.increment()
    store.increment()
    store.increment()
    expect(store.doubleCount).toBe(8)
  })

  it('maintains independent store instances', () => {
    const store1 = useCounterStore()
    const store2 = useCounterStore()
    
    store1.increment()
    expect(store1.count).toBe(1)
    expect(store2.count).toBe(1)
  })
})
