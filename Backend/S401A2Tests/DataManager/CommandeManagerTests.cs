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
    [TestClass]
    public class CommandeManagerTests
    {
        private CubeDBContext _context;
        private CommandeManager _manager;
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
                _manager = new CommandeManager(_context);
            }

            [TestCleanup]
            public void Cleanup()
            {
                _context.Database.EnsureDeleted();
                _context.Dispose();
                _connection.Close();
            }

            // GET: api/Commandes
            [TestMethod]
            public async Task GetAllAsync_ReturnAllCommandes()
            {
                //ARANGE
                _context.Commandes.Add(new Commande { Id = 1, Livraison = "Standard", AdresseIdFact=1,AdresseIdLivr=1, ClientId = 1 });
                _context.Commandes.Add(new Commande { Id = 2, Livraison = "Express", AdresseIdFact = 1, AdresseIdLivr = 1,ClientId = 1 });
                await _context.SaveChangesAsync();

                //ACT
                var result = await _manager.GetAllAsync();

                //ASSERT
                Assert.IsNotNull(result);
                var resultList = result.ToList();
                Assert.AreEqual(2, resultList.Count);
                Assert.AreEqual("Standard", resultList[0].Livraison);
            }

        // GET: api/Commandes/5
        [TestMethod]
        public async Task GetCommandes_ExistingId_ReturnCadre()
        {
            //ARANGE
            var newCommandes = new Commande { Id = 1, Livraison = "Standard", AdresseIdFact = 1, AdresseIdLivr = 1, ClientId = 1 };
            _context.Commandes.Add(newCommandes);
            await _context.SaveChangesAsync();
            var id = newCommandes.Id;
            //ACT
            var result = await _manager.GetByIdAsync(id);

            //ASSERT
            Assert.AreEqual("Standard", result.Livraison);
        }

        [TestMethod]
        public async Task GetCommandes_UnknownId_ReturnsNotFound()
        {
            // ARRANGE
            _context.Commandes.Add(new Commande { Id = 1, Livraison = "Standard", AdresseIdFact = 1, AdresseIdLivr = 1, ClientId = 1 });
            await _context.SaveChangesAsync();


            // ACT
            var result = await _manager.GetByIdAsync(99);

            // ASSERT
            Assert.AreEqual(null, result);
        }

        // POST: api/Commandes
        [TestMethod]
        public async Task PostCommandes_ValidModel_ReturnsCreatedAtAction()
        {
            //ARANGE
            var newCommande = new Commande { Id = 1, Livraison = "Standard", AdresseIdFact = 1, AdresseIdLivr = 1, ClientId = 1 };

            //ACT
            await _manager.AddAsync(newCommande);

            //ASSERT
            var commandeInDB = await _context.Commandes.FindAsync(1);
            Assert.IsNotNull(commandeInDB, "La commande devrait être présent dans la base de données.");
            Assert.AreEqual("Standard", commandeInDB.Livraison);
        }

        [TestMethod]
        public async Task PostCommandes_InvalidModel_ReturnsBadRequest()
        {
            //ARANGE
            var newCommande = new Commande { Id = 1, ClientId = 1 };

            //ASSERT
            await Assert.ThrowsExceptionAsync<DbUpdateException>(async () => { await _manager.AddAsync(newCommande); });
        }

        // PUT: api/Commandes/5
        [TestMethod]
        public async Task PutCommandes_ValidUpdate_ReturnsNoContent()
        {
            //ARANGE
            var commandeExisting = new Commande { Id = 1, Livraison = "Standard", AdresseIdFact = 1, AdresseIdLivr = 1, ClientId = 1 };
            var commandeToUpdate = new Commande { Id = 1, Livraison = "Express", AdresseIdFact = 1, AdresseIdLivr = 1, ClientId = 1 };
            _context.Commandes.Add(commandeToUpdate);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
            //ACT
            await _manager.UpdateAsync(commandeToUpdate, commandeExisting);

            //ASSERT
            _context.ChangeTracker.Clear();

            var newCommande = await _context.Commandes.FindAsync(commandeExisting.Id);
            Assert.AreEqual("Standard", newCommande.Livraison);
        }

        [TestMethod]
        public async Task PutCommande_IdMismatch_ReturnsBadRequest()
        {
            // Arrange
            var commandeExisting = new Commande { Id = 1, Livraison = "Standard", AdresseIdFact = 1, AdresseIdLivr = 1,  ClientId = 1 };
            var commandeToUpdate = new Commande { Id = 2, Livraison = "Express", AdresseIdFact = 1, AdresseIdLivr = 1, ClientId = 1 };

            // Assert
            await Assert.ThrowsExceptionAsync<DbUpdateConcurrencyException>(async () => { await _manager.UpdateAsync(commandeToUpdate, commandeExisting); });
        }

        // DELETE: api/Commandes/5
        [TestMethod]
        public async Task DeleteCommandes_ValidId_ReturnsNoContent()
        {
            // Arrange
            var commandeExisting = new Commande { Id = 1, Livraison = "Standard", AdresseIdFact = 1, AdresseIdLivr = 1, ClientId = 1 };
            _context.Commandes.Add(commandeExisting);
            await _context.SaveChangesAsync();

            // ACT
            await _manager.DeleteAsync(commandeExisting);

            //  ASSERT
            var commandes = await _manager.GetAllAsync();

            Assert.IsNotNull(commandes);
        }
    }
}
