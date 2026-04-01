import { describe, it, expect } from 'vitest'
import { mount } from '@vue/test-utils'
import Input from '../../components/Input.vue'

describe('Input Component', () => {
  it('renders input element', () => {
    const wrapper = mount(Input, {
      props: {
        label: 'Username'
      }
    })
    
    expect(wrapper.find('input').exists()).toBe(true)
  })

  it('displays label', () => {
    const wrapper = mount(Input, {
      props: {
        label: 'Email'
      }
    })
    
    expect(wrapper.text()).toContain('Email')
  })

  it('renders with correct type', () => {
    const wrapper = mount(Input, {
      props: {
        label: 'Email',
        type: 'email'
      }
    })
    
    expect(wrapper.find('input').attributes('type')).toBe('email')
  })

  it('marks field as required', () => {
    const wrapper = mount(Input, {
      props: {
        label: 'Password',
        required: true
      }
    })
    
    expect(wrapper.text()).toContain('*')
  })

  it('marks field as optional when not required', () => {
    const wrapper = mount(Input, {
      props: {
        label: 'Notes',
        required: false
      }
    })
    
    expect(wrapper.text()).toContain('optionnel')
  })

  it('supports text input type', () => {
    const wrapper = mount(Input, {
      props: {
        label: 'Username',
        type: 'text'
      }
    })
    
    expect(wrapper.find('input').attributes('type')).toBe('text')
  })

  it('supports password input type', () => {
    const wrapper = mount(Input, {
      props: {
        label: 'Password',
        type: 'password'
      }
    })
    
    expect(wrapper.find('input').attributes('type')).toBe('password')
  })

  it('supports date input type', () => {
    const wrapper = mount(Input, {
      props: {
        label: 'Birth Date',
        type: 'date'
      }
    })
    
    expect(wrapper.find('input').attributes('type')).toBe('date')
  })

  it('supports number input type', () => {
    const wrapper = mount(Input, {
      props: {
        label: 'Quantity',
        type: 'number'
      }
    })
    
    expect(wrapper.find('input').attributes('type')).toBe('number')
  })

  it('updates v-model on input', async () => {
    const wrapper = mount(Input, {
      props: {
        label: 'Test',
        modelValue: ''
      }
    })
    
    await wrapper.find('input').setValue('test value')
    expect(wrapper.emitted('update:modelValue')).toBeTruthy()
  })
})
