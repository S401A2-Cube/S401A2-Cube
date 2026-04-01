describe('Authentication', () => {
  beforeEach(() => {
    cy.visit('/connexion')
  })

  it('should display login form', () => {
    cy.contains('h2', 'Connexion / Inscription').should('be.visible')
    cy.get('form').should('be.visible')
  })

  it('should validate email field', () => {
    cy.contains('label', 'Email').should('be.visible')
    cy.get('form input[type="text"]').first().should('have.attr', 'required')
  })

  it('should validate password field', () => {
    cy.get('input[type="password"]').should('exist')
  })

  it('should display signup form', () => {
    cy.contains('a', 'Créez-en un').click()
    cy.url().should('include', '/inscription')
    cy.get('form').should('be.visible')
  })

  it('should have required fields in signup', () => {
    cy.visit('/inscription')
    cy.get('input[required]').should('have.length.greaterThan', 0)
  })

  it('should display account links in navbar', () => {
    cy.visit('/')
    cy.get('#navbar a[href="/connexion"]').should('exist')
    cy.get('#navbar a[href="/panier"]').should('exist')
  })
})
