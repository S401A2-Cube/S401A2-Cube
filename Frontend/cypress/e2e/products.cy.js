describe('Products Browsing', () => {
  beforeEach(() => {
    cy.intercept('GET', '**/api/Couleurs', [
      { idCouleur: 1, nomCouleur: 'Rouge', effetPeinture: 'Brillant' }
    ]).as('getCouleurs')

    cy.intercept('GET', '**/api/Millesimes', [
      { idMillesime: 1, annee: 2025 }
    ]).as('getMillesimes')

    cy.intercept('GET', '**/api/Tailles', [
      { idTaille: 1, libelleTaille: 'M' }
    ]).as('getTailles')

    cy.intercept('GET', '**/api/Cadres', [
      { idMateriau: 1, nomMat: 'Aluminium', formeCadre: 'Diamant' }
    ]).as('getCadres')

    cy.intercept('GET', '**/api/Velos', [
      {
        idVelo: 1,
        idArticle: 1,
        lienVue360: null,
        idModele: 1,
        modeleVelo: { idModele: 1, nomModele: 'Aim Race' },
        article: {
          nom: 'Aim Race',
          prix: 799,
          categorieArticle: { nom: 'MTB' },
          images: ['https://example.com/bike.png']
        },
        couleurs: [{ idCouleur: 1, nomCouleur: 'Rouge', effetPeinture: 'Brillant' }],
        tailles: [{ idTaille: 1, libelleTaille: 'M' }],
        cadres: [{ idMateriau: 1, nomMat: 'Aluminium', formeCadre: 'Diamant' }],
        millesimes: [{ idMillesime: 1, annee: 2025 }],
        geometries: []
      }
    ]).as('getVelos')

    cy.visit('/Article')
    cy.wait(['@getCouleurs', '@getMillesimes', '@getTailles', '@getCadres', '@getVelos'])
  })

  it('should display products', () => {
    cy.get('.item-container').should('have.length.greaterThan', 0)
  })

  it('should display product image', () => {
    cy.get('.item-container .item-image').first().should('be.visible')
  })

  it('should display product name', () => {
    cy.get('.item-container .item-name').first().should('contain', 'Aim Race')
  })

  it('should display product price', () => {
    cy.get('.item-container .item-price').first().should('contain', '799')
  })

  it('should display product status and CTA', () => {
    cy.get('.item-container').first().within(() => {
      cy.contains('Disponible en ligne').should('be.visible')
      cy.contains('Voir le produit').should('be.visible')
    })
  })

  it('should filter products by category', () => {
    cy.contains('.side_filtre', 'Couleurs').click()
    cy.contains('.filter-btn', 'Rouge').click()
    cy.get('.item-container').should('have.length', 1)
  })

  it('should display product details on click', () => {
    cy.get('.item-container').first().click()
    cy.url().should('include', '/detail/article/1')
  })

  it('should display filters panel', () => {
    cy.get('#side-filter').should('be.visible')
    cy.contains('.side_filtre', 'Tailles').should('be.visible')
  })
})
