describe('App Navigation', () => {
  beforeEach(() => {
    cy.visit('/')
  })

  it('should display the navbar', () => {
    cy.get('#navbar').should('be.visible')
  })

  it('should display the footer', () => {
    cy.get('#footer').should('be.visible')
  })

  it('should navigate between pages', () => {
    cy.get('#navbar a[href="/Article"]').click()
    cy.url().should('include', '/Article')
  })

  it('should display logo and branding', () => {
    cy.get('#navbar img[alt="Logo"]').should('be.visible')
  })

  it('should have account and cart navigation', () => {
    cy.get('#navbar a[href="/connexion"]').should('exist')
    cy.get('#navbar a[href="/panier"]').should('exist')
  })
})
