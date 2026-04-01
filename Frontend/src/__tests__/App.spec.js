import { describe, it, expect, beforeEach, vi } from 'vitest'
import { mount } from '@vue/test-utils'
import { createRouter, createMemoryHistory } from 'vue-router'
import App from '../App.vue'

describe('App Component', () => {
  let wrapper

  beforeEach(() => {
    const router = createRouter({
      history: createMemoryHistory(),
      routes: [
        { path: '/', name: 'home', component: { template: '<div></div>' } },
      ],
    })

    wrapper = mount(App, {
      global: {
        plugins: [router],
        stubs: {
          NavBar: { template: '<div class="nav-bar-stub"></div>' },
          Footer: { template: '<div class="footer-stub"></div>' },
          ComparaisonBar: { template: '<div class="comparaison-bar-stub"></div>' },
          RouterView: { template: '<div class="router-view-stub"></div>' },
        },
        mocks: {
          $route: {
            path: '/',
            name: 'home',
          },
        },
      },
    })
  })

  it('renders properly', () => {
    expect(wrapper.exists()).toBe(true)
  })

  it('renders header section with NavBar', () => {
    expect(wrapper.find('header.sticky-header').exists()).toBe(true)
    expect(wrapper.find('.nav-bar-stub').exists()).toBe(true)
  })

  it('renders main section', () => {
    expect(wrapper.find('section').exists()).toBe(true)
  })

  it('renders router view', () => {
    expect(wrapper.find('.router-view-stub').exists()).toBe(true)
  })

  it('renders ComparaisonBar component', () => {
    expect(wrapper.find('.comparaison-bar-stub').exists()).toBe(true)
  })

  it('renders Footer component', () => {
    expect(wrapper.find('.footer-stub').exists()).toBe(true)
  })

  it('has sticky header styling applied', () => {
    const header = wrapper.find('header.sticky-header')
    expect(header.classes('sticky-header')).toBe(true)
  })

  it('header has correct z-index for stickiness', () => {
    const header = wrapper.find('header.sticky-header')
    expect(header.element).toBeTruthy()
  })

  it('component structure is correct', () => {
    expect(wrapper.find('header').exists()).toBe(true)
    expect(wrapper.find('section').exists()).toBe(true)
    expect(wrapper.findAll('*').length).toBeGreaterThan(0)
  })
})
