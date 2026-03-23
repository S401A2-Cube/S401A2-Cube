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
    public class TailleManagerTests
    {
        private CubeDBContext _context;
        private TailleManager _manager;
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
            _manager = new TailleManager(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
            _connection.Close();
        }

        // GET: api/Tailles
        [TestMethod]
        public async Task GetAllAsync_ReturnAllTailles()
        {
            //ARANGE
            _context.Tailles.Add(new Taille { IdTaille = 1, LibelleTaille = "XL" });
            _context.Tailles.Add(new Taille { IdTaille = 2, LibelleTaille = "XXL" });
            await _context.SaveChangesAsync();

            //ACT
            var result = await _manager.GetAllAsync();

            //ASSERT
            Assert.IsNotNull(result);
            var resultList = result.ToList();
            Assert.AreEqual(2, resultList.Count);
            Assert.AreEqual("XL", resultList[0].LibelleTaille);
        }

        // GET: api/Tailles/5
        [TestMethod]
        public async Task GetTailles_ExistingId_ReturnCadre()
        {
            //ARANGE
            var newTaille = new Taille { IdTaille = 1, LibelleTaille = "XL" };
            _context.Tailles.Add(newTaille);
            await _context.SaveChangesAsync();
            var id = newTaille.IdTaille;
            //ACT
            var result = await _manager.GetByIdAsync(id);

            //ASSERT
            Assert.AreEqual("XL", result.LibelleTaille);
        }

        [TestMethod]
        public async Task GetTailles_UnknownId_ReturnsNotFound()
        {
            // ARRANGE
            _context.Tailles.Add(new Taille { IdTaille = 1, LibelleTaille = "XL" });
            await _context.SaveChangesAsync();


            // ACT
            var result = await _manager.GetByIdAsync(99);

            // ASSERT
            Assert.AreEqual(null, result);
        }

        // POST: api/Tailles
        [TestMethod]
        public async Task PostTailles_ValidModel_ReturnsCreatedAtAction()
        {
            //ARANGE
            var newTaille = new Taille { IdTaille = 1, LibelleTaille = "XL" };

            //ACT
            await _manager.AddAsync(newTaille);

            //ASSERT
            var tailleInDB = await _context.Tailles.FindAsync(1);
            Assert.IsNotNull(tailleInDB, "La taille devrait être présent dans la base de données.");
            Assert.AreEqual("XL", tailleInDB.LibelleTaille);
        }

        [TestMethod]
        public async Task PostTailles_InvalidModel_ReturnsBadRequest()
        {
            //ARANGE
            var newTaille = new Taille { IdTaille = 1};

            //ASSERT
            await Assert.ThrowsExceptionAsync<DbUpdateException>(async () => { await _manager.AddAsync(newTaille); });
        }

        // PUT: api/Tailles/5
        [TestMethod]
        public async Task PutTailles_ValidUpdate_ReturnsNoContent()
        {
            //ARANGE
            var tailleExisting = new Taille { IdTaille = 1, LibelleTaille = "XL" };
            var tailleToUpdate = new Taille { IdTaille = 1, LibelleTaille = "XXL" };
            _context.Tailles.Add(tailleToUpdate);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
            //ACT
            await _manager.UpdateAsync(tailleToUpdate, tailleExisting);

            //ASSERT
            _context.ChangeTracker.Clear();

            var newTaille = await _context.Tailles.FindAsync(tailleExisting.IdTaille);
            Assert.AreEqual("XL", newTaille.LibelleTaille);
        }

        [TestMethod]
        public async Task PutTailles_IdMismatch_ReturnsBadRequest()
        {
            // Arrange
            var tailleExisting = new Taille { IdTaille = 1, LibelleTaille = "XL" };
            var tailleToUpdate = new Taille { IdTaille = 2, LibelleTaille = "XXL" };

            // Assert
            await Assert.ThrowsExceptionAsync<DbUpdateConcurrencyException>(async () => { await _manager.UpdateAsync(tailleToUpdate, tailleExisting); });
        }

        // DELETE: api/Tailles/5
        [TestMethod]
        public async Task DeleteTailles_ValidId_ReturnsNoContent()
        {
            // Arrange
            var tailleExisting = new Taille { IdTaille = 1, LibelleTaille = "XL" };
            _context.Tailles.Add(tailleExisting);
            await _context.SaveChangesAsync();

            // ACT
            await _manager.DeleteAsync(tailleExisting);

            //  ASSERT
            var tailles = await _manager.GetAllAsync();

            Assert.IsNotNull(tailles);
        }
    }
}
