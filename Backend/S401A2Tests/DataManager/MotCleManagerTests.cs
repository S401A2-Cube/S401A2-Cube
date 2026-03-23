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
    public class MotCleManagerTests
    {
        private CubeDBContext _context;
        private MotCleManager _manager;
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
            _manager = new MotCleManager(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
            _connection.Close();
        }

        // GET: api/MotCles
        [TestMethod]
        public async Task GetAllAsync_ReturnAllMotCles()
        {
            //ARANGE
            _context.MotCles.Add(new MotCle { MotCleId = 1, Nom = "Velo" });
            _context.MotCles.Add(new MotCle { MotCleId = 2, Nom = "VTT" });
            await _context.SaveChangesAsync();

            //ACT
            var result = await _manager.GetAllAsync();

            //ASSERT
            Assert.IsNotNull(result);
            var resultList = result.ToList();
            Assert.AreEqual(2, resultList.Count);
            Assert.AreEqual("Velo", resultList[0].Nom);
        }

        // GET: api/MotCles/5
        [TestMethod]
        public async Task GetMotCles_ExistingId_ReturnCadre()
        {
            //ARANGE
            var newMotCle = new MotCle { MotCleId = 1, Nom = "Velo" };
            _context.MotCles.Add(newMotCle);
            await _context.SaveChangesAsync();
            var id = newMotCle.MotCleId;
            //ACT
            var result = await _manager.GetByIdAsync(id);

            //ASSERT
            Assert.AreEqual("Velo", result.Nom);
        }

        [TestMethod]
        public async Task GetMotCle_UnknownId_ReturnsNotFound()
        {
            // ARRANGE
            _context.MotCles.Add(new MotCle { MotCleId = 1, Nom = "Velo" });
            await _context.SaveChangesAsync();


            // ACT
            var result = await _manager.GetByIdAsync(99);

            // ASSERT
            Assert.AreEqual(null, result);
        }

        // POST: api/MotCles
        [TestMethod]
        public async Task PostMotCles_ValidModel_ReturnsCreatedAtAction()
        {
            //ARANGE
            var newMotCle = new MotCle { MotCleId = 1, Nom = "Velo" };

            //ACT
            await _manager.AddAsync(newMotCle);

            //ASSERT
            var motCleInDB = await _context.MotCles.FindAsync(1);
            Assert.IsNotNull(motCleInDB, "Le motCle devrait être présent dans la base de données.");
            Assert.AreEqual("Velo", motCleInDB.Nom);
        }

        [TestMethod]
        public async Task PostMotCles_InvalidModel_ReturnsBadRequest()
        {
            //ARANGE
            var newMotCle = new MotCle { MotCleId = 1 };

            //ASSERT
            await Assert.ThrowsExceptionAsync<DbUpdateException>(async () => { await _manager.AddAsync(newMotCle); });
        }

        // PUT: api/MotCles/5
        [TestMethod]
        public async Task PutMotCles_ValidUpdate_ReturnsNoContent()
        {
            //ARANGE
            var motCleExisting = new MotCle { MotCleId = 1, Nom = "Velo" };
            var motCleToUpdate = new MotCle { MotCleId = 1, Nom = "VTT" };
            _context.MotCles.Add(motCleToUpdate);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
            //ACT
            await _manager.UpdateAsync(motCleToUpdate, motCleExisting);

            //ASSERT
            _context.ChangeTracker.Clear();

            var newMotCle = await _context.MotCles.FindAsync(motCleExisting.MotCleId);
            Assert.AreEqual("Velo", newMotCle.Nom);
        }

        [TestMethod]
        public async Task PutMotCles_IdMismatch_ReturnsBadRequest()
        {
            // Arrange
            var motCleExisting = new MotCle { MotCleId = 1, Nom = "Velo" };
            var motCleToUpdate = new MotCle { MotCleId = 2, Nom = "VTT" };

            // Assert
            await Assert.ThrowsExceptionAsync<DbUpdateConcurrencyException>(async () => { await _manager.UpdateAsync(motCleToUpdate, motCleExisting); });
        }

        // DELETE: api/MotCles/5
        [TestMethod]
        public async Task DeleteMotCles_ValidId_ReturnsNoContent()
        {
            // Arrange
            var motCleExisting = new MotCle { MotCleId = 1, Nom = "Velo" };
            _context.MotCles.Add(motCleExisting);
            await _context.SaveChangesAsync();

            // ACT
            await _manager.DeleteAsync(motCleExisting);

            //  ASSERT
            var motCles = await _manager.GetAllAsync();

            Assert.IsNotNull(motCles);
        }
    }
}
