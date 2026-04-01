describe('Shopping Cart', () => {
  beforeEach(() => {
    cy.visit('/panier')
  })

  it('should display shopping cart icon in navbar', () => {
    cy.get('#navbar a[href="/panier"]').should('exist')
  })

  it('should display cart page content', () => {
    cy.contains('h2', 'PANIER').should('be.visible')
    cy.get('#ligne-panier').should('have.length.greaterThan', 0)
  })

  it('should display line item details', () => {
    cy.get('#ligne-panier').first().within(() => {
      cy.get('h2').should('exist')
      cy.contains('REFERENCE:').should('exist')
      cy.contains('SIZE:').should('exist')
      cy.contains('COLOR:').should('exist')
    })
  })

  it('should increase quantity with plus button', () => {
    cy.get('#ligne-panier').first().within(() => {
      cy.get('.input-panier').invoke('val').then((value) => {
        const initial = Number(value)
        cy.get('.btn-qty').last().click()
        cy.get('.input-panier').should('have.value', String(initial + 1))
      })
    })
  })

  it('should decrease quantity with minus button', () => {
    cy.get('#ligne-panier').first().within(() => {
      cy.get('.input-panier').invoke('val').then((value) => {
        const initial = Number(value)
        cy.get('.btn-qty').first().click()
        const expected = Math.max(1, initial - 1)
        cy.get('.input-panier').should('have.value', String(expected))
      })
    })
  })

  it('should update quantity by typing in cart input', () => {
    cy.get('.input-panier').first().clear().type('5')
    cy.get('.input-panier').first().should('have.value', '5')
  })

  it('should display cart total', () => {
    cy.get('#ligne-panier .action-ligne h3').first().should('contain', '€ TTC')
  })

  it('should show checkout button', () => {
    cy.contains('button', 'Valider le panier').should('be.visible')
  })
})
