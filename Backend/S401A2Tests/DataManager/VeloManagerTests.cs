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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace S401A2Tests.DataManager
{
    [TestClass]
    public class VeloManagerTests
    {
        private CubeDBContext _context;
        private VeloManager _manager;
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
            _context.Add(new Categorie { CategorieId = 1, Nom = "VTT" });
            _context.SaveChanges();
            _context.Add(new Article { ArticleId = 1, Reference = "X200", Nom = "VTT Fox", Description = "Velo tout terrain de la marque FOX", Prix = 2500, Poids = 3, QteStock = 3, Annee = 2024, DispoEnLigne = true, CategorieId = 1 });
            _context.SaveChanges();
            _context.Add(new Modele { IdModele = 1, NomModele = "X200" });
            _context.SaveChanges();
            _manager = new VeloManager(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
            _connection.Close();
        }

        // GET: api/Velos
        [TestMethod]
        public async Task GetAllAsync_ReturnAllVelos()
        {
            //ARANGE
            _context.Velos.Add(new Velo { IdVelo = 1, IdArticle = 1, LienVue360 = "google.com", IdModele = 1 });
            _context.Velos.Add(new Velo { IdVelo = 2, IdArticle = 1, LienVue360 = "google.fr", IdModele = 1 });
            await _context.SaveChangesAsync();

            //ACT
            var result = await _manager.GetAllAsync();

            //ASSERT
            Assert.IsNotNull(result);
            var resultList = result.ToList();
            Assert.AreEqual(2, resultList.Count);
            Assert.AreEqual("google.com", resultList[0].LienVue360);
        }

        // GET: api/Velos/5
        [TestMethod]
        public async Task GetVelos_ExistingId_ReturnCadre()
        {
            //ARANGE
            var newVelo = new Velo { IdVelo = 1, IdArticle = 1, LienVue360 = "google.com", IdModele = 1 };
            _context.Velos.Add(newVelo);
            await _context.SaveChangesAsync();
            var id = newVelo.IdVelo;
            //ACT
            var result = await _manager.GetByIdAsync(id);

            //ASSERT
            Assert.AreEqual("google.com", result.LienVue360);
        }

        [TestMethod]
        public async Task GetVelos_UnknownId_ReturnsNotFound()
        {
            // ARRANGE
            _context.Velos.Add(new Velo { IdVelo = 1, IdArticle = 1, LienVue360 = "google.com", IdModele = 1 });
            await _context.SaveChangesAsync();


            // ACT
            var result = await _manager.GetByIdAsync(99);

            // ASSERT
            Assert.AreEqual(null, result);
        }

        // POST: api/Velos
        [TestMethod]
        public async Task PostVelos_ValidModel_ReturnsCreatedAtAction()
        {
            //ARANGE
            var newVelos = new Velo { IdVelo = 1, IdArticle = 1, LienVue360 = "google.com", IdModele = 1 };

            //ACT
            await _manager.AddAsync(newVelos);

            //ASSERT
            var veloInDB = await _context.Velos.FindAsync(1);
            Assert.IsNotNull(veloInDB, "Le velo devrait être présent dans la base de données.");
            Assert.AreEqual("google.com", veloInDB.LienVue360);
        }

        [TestMethod]
        public async Task PostVelos_InvalidModel_ReturnsBadRequest()
        {
            //ARANGE
            var newVelo = new Velo { };

            //ASSERT
            await Assert.ThrowsExceptionAsync<DbUpdateException>(async () => { await _manager.AddAsync(newVelo); });
        }

        // PUT: api/Velos/5
        [TestMethod]
        public async Task PutVelos_ValidUpdate_ReturnsNoContent()
        {
            //ARANGE
            var veloExisting = new Velo { IdVelo = 1, IdArticle = 1, LienVue360 = "google.com", IdModele = 1 };
            var veloToUpdate = new Velo { IdVelo = 1, IdArticle = 1, LienVue360 = "google.fr", IdModele = 1 };
            _context.Velos.Add(veloToUpdate);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
            //ACT
            await _manager.UpdateAsync(veloToUpdate, veloExisting);

            //ASSERT
            _context.ChangeTracker.Clear();

            var newVelo = await _context.Velos.FindAsync(veloExisting.IdVelo);
            Assert.AreEqual("google.com", newVelo.LienVue360);
        }

        [TestMethod]
        public async Task PutVelos_IdMismatch_ReturnsBadRequest()
        {
            // Arrange
            var veloExisting = new Velo { IdVelo = 1, IdArticle = 1, LienVue360 = "google.com", IdModele = 1 };
            var veloToUpdate = new Velo { IdVelo = 2, IdArticle = 1, LienVue360 = "google.com", IdModele = 1 };

            // Assert
            await Assert.ThrowsExceptionAsync<DbUpdateConcurrencyException>(async () => { await _manager.UpdateAsync(veloToUpdate, veloExisting); });
        }

        // DELETE: api/Velos/5
        [TestMethod]
        public async Task DeleteVelos_ValidId_ReturnsNoContent()
        {
            // Arrange
            var veloExisting = new Velo { IdVelo = 1, IdArticle = 1, LienVue360 = "google.com", IdModele = 1 };
            _context.Velos.Add(veloExisting);
            await _context.SaveChangesAsync();

            // ACT
            await _manager.DeleteAsync(veloExisting);

            //  ASSERT
            var velos = await _manager.GetAllAsync();

            Assert.IsNotNull(velos);
        }
    }
}
