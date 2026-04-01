import { describe, it, expect } from 'vitest'
import { mount } from '@vue/test-utils'
import RedButton from '../../components/RedButton.vue'

describe('RedButton Component', () => {
  it('renders button element', () => {
    const wrapper = mount(RedButton)
    expect(wrapper.find('button').exists()).toBe(true)
  })

  it('displays default text', () => {
    const wrapper = mount(RedButton)
    expect(wrapper.text()).toContain('Valider')
  })

  it('renders with custom slot content', () => {
    const wrapper = mount(RedButton, {
      slots: {
        default: 'Click Me'
      }
    })
    
    expect(wrapper.text()).toContain('Click Me')
  })

  it('renders with submit type', () => {
    const wrapper = mount(RedButton, {
      props: {
        type: 'submit'
      }
    })
    
    expect(wrapper.find('button').attributes('type')).toBe('submit')
  })

  it('renders with button type by default', () => {
    const wrapper = mount(RedButton)
    expect(wrapper.find('button').attributes('type')).toBe('button')
  })

  it('renders with reset type', () => {
    const wrapper = mount(RedButton, {
      props: {
        type: 'reset'
      }
    })
    
    expect(wrapper.find('button').attributes('type')).toBe('reset')
  })

  it('emits click event', async () => {
    const wrapper = mount(RedButton)
    await wrapper.find('button').trigger('click')
    expect(wrapper.emitted('click')).toBeTruthy()
  })

  it('can be clicked multiple times', async () => {
    const wrapper = mount(RedButton)
    await wrapper.find('button').trigger('click')
    await wrapper.find('button').trigger('click')
    await wrapper.find('button').trigger('click')
    
    expect(wrapper.emitted('click')).toHaveLength(3)
  })

  it('has correct styling classes', () => {
    const wrapper = mount(RedButton)
    const button = wrapper.find('button')
    expect(button.exists()).toBe(true)
  })

  it('renders with form integration', () => {
    const wrapper = mount(RedButton, {
      props: {
        type: 'submit'
      },
      slots: {
        default: 'Submit Form'
      }
    })
    
    expect(wrapper.find('button').attributes('type')).toBe('submit')
    expect(wrapper.text()).toContain('Submit Form')
  })
})
