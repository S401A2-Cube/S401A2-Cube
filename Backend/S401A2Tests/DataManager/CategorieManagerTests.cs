using APICube.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;
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

namespace S401A2Tests.DataManager
{
    [TestClass]
    public class CategorieManagerTests
    {
        private CubeDBContext _context;
        private CategorieManager _manager;
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
            _manager = new CategorieManager(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
            _connection.Close();
        }

        // GET: api/Categories
        [TestMethod]
        public async Task GetAllAsync_ReturnAllCategories()
        {
            //ARANGE
            _context.Categories.Add(new Categorie { CategorieId = 1, Nom = "Route" });
            _context.Categories.Add(new Categorie { CategorieId = 2, Nom = "VTT" });
            await _context.SaveChangesAsync();

            //ACT
            var result = await _manager.GetAllAsync();

            //ASSERT
            Assert.IsNotNull(result);
            var resultList = result.ToList();
            Assert.AreEqual(2, resultList.Count);
            Assert.AreEqual("Route", resultList[0].Nom);
        }

        // GET: api/Categories/5
        [TestMethod]
        public async Task GetCategorie_ExistingId_ReturnCadre()
        {
            //ARANGE
            var newCategorie = new Categorie { CategorieId = 1, Nom = "Route" };
            _context.Categories.Add(newCategorie);
            await _context.SaveChangesAsync();
            var id = newCategorie.CategorieId;
            //ACT
            var result = await _manager.GetByIdAsync(id);

            //ASSERT
            Assert.AreEqual("Route", result.Nom);
        }

        [TestMethod]
        public async Task GetCategorie_UnknownId_ReturnsNotFound()
        {
            // ARRANGE
            _context.Categories.Add(new Categorie { CategorieId = 1, Nom = "Route" });
            await _context.SaveChangesAsync();


            // ACT
            var result = await _manager.GetByIdAsync(99);

            // ASSERT
            Assert.AreEqual(null, result);
        }

        // POST: api/Categories
        [TestMethod]
        public async Task PostCategorie_ValidModel_ReturnsCreatedAtAction()
        {
            //ARANGE
            var newCategorie = new Categorie { CategorieId = 1, Nom = "Route" };

            //ACT
            await _manager.AddAsync(newCategorie);

            //ASSERT
            var categorieInDB = await _context.Categories.FindAsync(1);
            Assert.IsNotNull(categorieInDB, "La categorie devrait être présent dans la base de données.");
            Assert.AreEqual("Route", categorieInDB.Nom);
        }

        [TestMethod]
        public async Task PostCategorie_InvalidModel_ReturnsBadRequest()
        {
            //ARANGE
            var newCategorie = new Categorie { CategorieId = 1};

            //ASSERT
            await Assert.ThrowsExceptionAsync<DbUpdateException>(async () => { await _manager.AddAsync(newCategorie); });
        }

        // PUT: api/Categorie/5
        [TestMethod]
        public async Task PutCategorie_ValidUpdate_ReturnsNoContent()
        {
            //ARANGE
            var categorieExisting = new Categorie { CategorieId = 1, Nom = "Route" };
            var categorieToUpdate = new Categorie { CategorieId = 1, Nom = "VTT" };
            _context.Categories.Add(categorieToUpdate);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
            //ACT
            await _manager.UpdateAsync(categorieToUpdate, categorieExisting);

            //ASSERT
            _context.ChangeTracker.Clear();

            var newCategorie = await _context.Categories.FindAsync(categorieExisting.CategorieId);
            Assert.AreEqual("Route", newCategorie.Nom);
        }

        [TestMethod]
        public async Task PutCategorie_IdMismatch_ReturnsBadRequest()
        {
            // Arrange
            var categorieExisting = new Categorie { CategorieId = 1, Nom = "Route" };
            var categorieToUpdate = new Categorie { CategorieId = 2, Nom = "VTT" };

            // Assert
            await Assert.ThrowsExceptionAsync<DbUpdateConcurrencyException>(async () => { await _manager.UpdateAsync(categorieToUpdate, categorieExisting); });
        }

        // DELETE: api/Cadress/5
        [TestMethod]
        public async Task DeleteCategorie_ValidId_ReturnsNoContent()
        {
            // Arrange
            var categorieExisting = new Categorie { CategorieId = 1, Nom = "Route" };
            _context.Categories.Add(categorieExisting);
            await _context.SaveChangesAsync();

            // ACT
            await _manager.DeleteAsync(categorieExisting);

            //  ASSERT
            var categorie = await _manager.GetAllAsync();

            Assert.IsNotNull(categorie);
        }
    }
}
