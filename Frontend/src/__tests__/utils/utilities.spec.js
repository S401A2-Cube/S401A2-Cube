import { describe, it, expect } from 'vitest'

export const formatPrice = (price) => {
  return `$${(price).toFixed(2)}`
}

export const calculateDiscount = (price, discountPercent) => {
  return price * (1 - discountPercent / 100)
}

export const sanitizeInput = (input) => {
  return input.trim().toLowerCase()
}

export const validateEmail = (email) => {
  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/
  return emailRegex.test(email)
}

describe('Utility Functions', () => {
  describe('formatPrice', () => {
    it('formats price with dollar sign', () => {
      expect(formatPrice(10)).toBe('$10.00')
    })

    it('formats price with decimals', () => {
      expect(formatPrice(19.99)).toBe('$19.99')
    })

    it('formats price with two decimal places', () => {
      expect(formatPrice(5)).toBe('$5.00')
    })

    it('handles large prices', () => {
      expect(formatPrice(1000.5)).toBe('$1000.50')
    })
  })

  describe('calculateDiscount', () => {
    it('calculates 10% discount', () => {
      expect(calculateDiscount(100, 10)).toBe(90)
    })

    it('calculates 50% discount', () => {
      expect(calculateDiscount(100, 50)).toBe(50)
    })

    it('calculates no discount', () => {
      expect(calculateDiscount(100, 0)).toBe(100)
    })

    it('calculates discount on decimal prices', () => {
      expect(calculateDiscount(19.99, 20)).toBeCloseTo(15.992, 2)
    })
  })

  describe('sanitizeInput', () => {
    it('trims whitespace', () => {
      expect(sanitizeInput('  hello  ')).toBe('hello')
    })

    it('converts to lowercase', () => {
      expect(sanitizeInput('HELLO')).toBe('hello')
    })

    it('does both trim and lowercase', () => {
      expect(sanitizeInput('  HELLO WORLD  ')).toBe('hello world')
    })

    it('handles empty string', () => {
      expect(sanitizeInput('')).toBe('')
    })
  })

  describe('validateEmail', () => {
    it('validates correct email', () => {
      expect(validateEmail('user@example.com')).toBe(true)
    })

    it('rejects email without @ symbol', () => {
      expect(validateEmail('userexample.com')).toBe(false)
    })

    it('rejects email without domain', () => {
      expect(validateEmail('user@')).toBe(false)
    })

    it('rejects email without local part', () => {
      expect(validateEmail('@example.com')).toBe(false)
    })

    it('validates email with subdomain', () => {
      expect(validateEmail('user@mail.example.com')).toBe(true)
    })

    it('rejects email with spaces', () => {
      expect(validateEmail('user @example.com')).toBe(false)
    })
  })
})
