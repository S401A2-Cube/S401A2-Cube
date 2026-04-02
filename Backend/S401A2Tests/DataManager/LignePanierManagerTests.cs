using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using S401A2.Model.DataManager;
using S401A2.Model.EntityFramework;
using S401A2.Models.Repository;
using System;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Humanizer;
using APICube.Models.EntityFramework;

namespace S401A2Tests.DataManager
{
    [TestClass()]
    public class LignePanierManagerTests
    {
        private CubeDBContext _context;
        private LignePanierManager _manager;
        private SqliteConnection _connection;

        [TestInitialize]
        public void Setup()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<CubeDBContext>()
                .UseSqlite(_connection)
                .Options;

            _context = new CubeDBContext(options);

            _context.Database.EnsureCreated();

            //INITIALISATION
            _context.Add(new Civilite { Id = 1, Nom = "Monsieur" });
            _context.SaveChanges();
            _context.Add(new Client { Id = 1, CiviliteId = 1, Nom = "Personeni", Prenom = "Nathan", Email = "nathan.personeni@etu.univ-smb.fr", Mdp = "123456789", DateNaissance = (new DateTime(2006, 02, 20)), Role = 1 });
            _context.SaveChanges();
            _context.Add(new Adresse { Id = 1, Rue = "9 rue de l'arc en ciel", CodePostale = "74100", Ville = "Annecy", Pays = "France" });
            _context.SaveChanges();
            _context.Commandes.Add(new Commande { Id = 1, Livraison = "Standard", AdresseIdFact = 1, AdresseIdLivr = 1, ClientId = 1 });
            _context.Add(new Couleur { IdCouleur = 1, EffetPeinture = "Brillant", NomCouleur = "Rose" });
            _context.SaveChanges();
            _context.Add(new Taille { IdTaille = 1, LibelleTaille = "XS" });
            _context.Add(new Categorie { CategorieId = 1, Nom = "VTT" }); 
            _context.SaveChanges();
            _context.Articles.Add(new Article { ArticleId = 1, Reference = "X200", Nom = "VTT Fox", Description = "Velo tout terrain de la marque FOX", Prix = 2500, Poids = 3, QteStock = 3, Annee = 2024, DispoEnLigne = true, CategorieId = 1 });
            _context.SaveChanges();
            _manager = new LignePanierManager(_context);

        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
            _connection.Close();
        }

        // GET: api/LignePaniers
        [TestMethod]
        public async Task GetAllAsync_ReturnAllLignes()
        {
            //ARANGE
            _context.LignePaniers.Add(new LignePanier 
            { 
                Id = 1, 
                ArticleId = 1,
                ClientId = 1,
                CommandeId = 1,
                CouleurId = 1,
                TailleId = 1, 
                QtePanier = 2
            });
            _context.LignePaniers.Add(new LignePanier
            {
                Id = 2,
                ArticleId = 1,
                ClientId = 1,
                CommandeId = 1,
                CouleurId = 1,
                TailleId = 1,
                QtePanier = 1
            });
            await _context.SaveChangesAsync();

            //ACT
            var result = await _manager.GetAllAsync();

            //ASSERT
            Assert.IsNotNull(result);
            var resultList = result.ToList();
            Assert.AreEqual(2, resultList.Count);
            Assert.AreEqual(2, resultList[0].QtePanier);
        }

        // GET: api/LignePaniers/1
        [TestMethod]
        public async Task GetLignePaniers_ExistingId_ReturnCadre()
        {
            //ARANGE
            var newLignePanier = new LignePanier
            {
                Id = 1,
                ArticleId = 1,
                ClientId = 1,
                CommandeId = 1,
                CouleurId = 1,
                TailleId = 1,
                QtePanier = 2
            };
            _context.LignePaniers.Add(newLignePanier);
            await _context.SaveChangesAsync();
            var id = newLignePanier.Id;
            //ACT
            var result = await _manager.GetByIdAsync(id);

            //ASSERT
            Assert.AreEqual(2, result.QtePanier);
        }

        [TestMethod]
        public async Task GetLignePaniers_UnknownId_ReturnsNotFound()
        {
            // ARRANGE
            _context.LignePaniers.Add(new LignePanier
            {
                Id = 1,
                ArticleId = 1,
                ClientId = 1,
                CommandeId = 1,
                CouleurId = 1,
                TailleId = 1,
                QtePanier = 2
            });
            await _context.SaveChangesAsync();

            // ACT
            var result = await _manager.GetByIdAsync(99);

            // ASSERT
            Assert.AreEqual(null, result);
        }

        // POST: api/LignePaniers
        [TestMethod]
        public async Task PostLignePaniers_ValidModel_ReturnsCreatedAtAction()
        {
            //ARANGE
            var newLignePanier = new LignePanier
            {
                Id = 1,
                ArticleId = 1,
                ClientId = 1,
                CommandeId = 1,
                CouleurId = 1,
                TailleId = 1,
                QtePanier = 2
            };

            //ACT
            await _manager.AddAsync(newLignePanier);

            //ASSERT
            var LignePanierInDB = await _context.LignePaniers.FindAsync(1);
            Assert.IsNotNull(LignePanierInDB, "La LignePanier devrait être présent dans la base de données.");
            Assert.AreEqual(2, LignePanierInDB.QtePanier);
        }

        [TestMethod]
        public async Task PostLignePaniers_InvalidModel_ReturnsBadRequest()
        {
            //ARANGE
            var newLignePanier = new LignePanier { Id = 1 };

            //ASSERT
            await Assert.ThrowsExceptionAsync<DbUpdateException>(async () => { await _manager.AddAsync(newLignePanier); });
        }

        // PUT: api/LignePaniers/1
        [TestMethod]
        public async Task PutLignePaniers_ValidUpdate_ReturnsNoContent()
        {
            //ARANGE
            var LignePanierExisting = new LignePanier
            {
                Id = 1,
                ArticleId = 1,
                ClientId = 1,
                CommandeId = 1,
                CouleurId = 1,
                TailleId = 1,
                QtePanier = 2
            };
            var LignePanierToUpdate = new LignePanier
            {
                Id = 1,
                ArticleId = 1,
                ClientId = 1,
                CommandeId = 1,
                CouleurId = 1,
                TailleId = 1,
                QtePanier = 3
            };
            _context.LignePaniers.Add(LignePanierToUpdate);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
            //ACT
            await _manager.UpdateAsync(LignePanierToUpdate, LignePanierExisting);

            //ASSERT
            _context.ChangeTracker.Clear();

            var newLignePanier = await _context.LignePaniers.FindAsync(LignePanierExisting.Id);
            Assert.AreEqual(2, newLignePanier.QtePanier);
        }

        [TestMethod]
        public async Task PutLignePaniers_IdMismatch_ReturnsBadRequest()
        {
            // Arrange
            var LignePanierExisting = new LignePanier
            {
                Id = 1,
                ArticleId = 1,
                ClientId = 1,
                CommandeId = 1,
                CouleurId = 1,
                TailleId = 1,
                QtePanier = 2
            };
            var LignePanierToUpdate = new LignePanier
            {
                Id = 2,
                ArticleId = 1,
                ClientId = 1,
                CommandeId = 1,
                CouleurId = 1,
                TailleId = 1,
                QtePanier = 3
            };

            // Assert
            await Assert.ThrowsExceptionAsync<DbUpdateConcurrencyException>(async () => { await _manager.UpdateAsync(LignePanierToUpdate, LignePanierExisting); });
        }

        // DELETE: api/LignePaniers/1
        [TestMethod]
        public async Task DeleteLignePaniers_ValidId_ReturnsNoContent()
        {
            // Arrange
            var LignePanierExisting = new LignePanier
            {
                Id = 1,
                ArticleId = 1,
                ClientId = 1,
                CommandeId = 1,
                CouleurId = 1,
                TailleId = 1,
                QtePanier = 2
            };
            _context.LignePaniers.Add(LignePanierExisting);
            await _context.SaveChangesAsync();

            // ACT
            await _manager.DeleteAsync(LignePanierExisting);

            //  ASSERT
            var LignePaniers = await _manager.GetAllAsync();

            Assert.IsNotNull(LignePaniers);
        }
    }
}