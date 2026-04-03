export const client = {
  id: 1,
  nom: "Dupont",
  prenom: "Jean",
  email: "jean.dupont@gmail.com",
  adresseClient: {
    rue: "9 Rue Marcadet",
    codePostale: "75018",
    ville: "Paris",
    pays: "France"
  },
  clientCommande: [
    {
      id: 106,
      livraison: "2024-03-15",
      adresseCommandeFact: {
        rue: "9 Rue Marcadet",
        codePostale: "75018",
        ville: "Paris",
        pays: "France"
      },
      articleCommande: [
        {
          articleId: 1,
          nom: "NUROAD C:62 SLT",
          reference: "156500",
          prix: 2449
        }
      ]
    },
    {
      id: 107,
      livraison: "2024-06-20",
      adresseCommandeFact: {
        rue: "9 Rue Marcadet",
        codePostale: "75018",
        ville: "Paris",
        pays: "France"
      },
      articleCommande: [
        {
          articleId: 2,
          nom: "STEREO 120 RACE",
          reference: "156502",
          prix: 1899
        },
        {
          articleId: 3,
          nom: "ACID HELMET PRO",
          reference: "156503",
          prix: 89
        }
      ]
    },
    {
      id: 108,
      livraison: "2024-09-05",
      adresseCommandeFact: {
        rue: "9 Rue Marcadet",
        codePostale: "75018",
        ville: "Paris",
        pays: "France"
      },
      articleCommande: [
        {
          articleId: 4,
          nom: "ATTAIN SLX",
          reference: "156504",
          prix: 3199
        },
        {
          articleId: 5,
          nom: "ACID PEDAL PRO",
          reference: "156505",
          prix: 45
        },
        {
          articleId: 6,
          nom: "ACID SADDLE",
          reference: "156506",
          prix: 79
        }
      ]
    },
    {
      id: 109,
      livraison: "2024-11-30",
      adresseCommandeFact: {
        rue: "9 Rue Marcadet",
        codePostale: "75018",
        ville: "Paris",
        pays: "France"
      },
      articleCommande: [
        {
          articleId: 7,
          nom: "REACTION C:62 PRO",
          reference: "156507",
          prix: 2799
        }
      ]
    },
    {
      id: 110,
      livraison: "2025-01-10",
      adresseCommandeFact: {
        rue: "9 Rue Marcadet",
        codePostale: "75018",
        ville: "Paris",
        pays: "France"
      },
      articleCommande: [
        {
          articleId: 8,
          nom: "ACCESS WS EAZ",
          reference: "156508",
          prix: 999
        },
        {
          articleId: 9,
          nom: "ACID REACT PRO GLOVES",
          reference: "156509",
          prix: 35
        }
      ]
    }
  ]
}