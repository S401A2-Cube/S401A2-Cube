// Cypress E2E Support File
// Learn more about support files:
// https://on.cypress.io/support-file-syntax

// Import commands
import './commands'

// Disable uncaught exception handler for API errors
Cypress.on('uncaught:exception', (err, runnable) => {
  // returning false here prevents Cypress from
  // failing the test
  return false
})

// Set base URL for tests
before(() => {
  cy.visit('/')
})
