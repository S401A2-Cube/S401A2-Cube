// Custom Cypress Commands
// https://on.cypress.io/custom-commands

Cypress.Commands.add('login', (email, password) => {
  cy.visit('/')
  cy.get('button').contains('Connexion').click()
  cy.get('input[type="email"]').type(email)
  cy.get('input[type="password"]').type(password)
  cy.get('button').contains('Se connecter').click()
})

Cypress.Commands.add('logout', () => {
  cy.get('button').contains('Déconnexion').click()
})

Cypress.Commands.add('addToCart', (productName) => {
  cy.contains(productName).parent().find('button').contains('Ajouter').click()
})

Cypress.Commands.add('navigateTo', (page) => {
  cy.get('nav').find(`a[href="/${page}"]`).click()
})
