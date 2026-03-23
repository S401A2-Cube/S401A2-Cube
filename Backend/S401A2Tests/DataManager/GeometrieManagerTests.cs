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
    public class GeometrieManagerTests
    {
        private CubeDBContext _context;
        private GeometrieManager _manager;
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
            _manager = new GeometrieManager(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
            _connection.Close();
        }

        // GET: api/Geometrie
        [TestMethod]
        public async Task GetAllAsync_ReturnAllGeometries()
        {
            //ARANGE
            _context.Geometries.Add(new Geometrie { IdGeometrie = 1, NomPiece = "Fourche", TaillePiece = 1 });
            _context.Geometries.Add(new Geometrie { IdGeometrie = 2, NomPiece = "Guidon", TaillePiece = 3 });
            await _context.SaveChangesAsync();

            //ACT
            var result = await _manager.GetAllAsync();

            //ASSERT
            Assert.IsNotNull(result);
            var resultList = result.ToList();
            Assert.AreEqual(2, resultList.Count);
            Assert.AreEqual("Fourche", resultList[0].NomPiece);
        }

        // GET: api/Geometries/5
        [TestMethod]
        public async Task GetGeometries_ExistingId_ReturnCadre()
        {
            //ARANGE
            var newGeometrie = new Geometrie { IdGeometrie = 1, NomPiece = "Fourche", TaillePiece = 1 };
            _context.Geometries.Add(newGeometrie);
            await _context.SaveChangesAsync();
            var id = newGeometrie.IdGeometrie;
            //ACT
            var result = await _manager.GetByIdAsync(id);

            //ASSERT
            Assert.AreEqual("Fourche", result.NomPiece);
        }

        [TestMethod]
        public async Task GetGeometrie_UnknownId_ReturnsNotFound()
        {
            // ARRANGE
            _context.Geometries.Add(new Geometrie { IdGeometrie = 1, NomPiece = "Fourche", TaillePiece = 1 });
            await _context.SaveChangesAsync();


            // ACT
            var result = await _manager.GetByIdAsync(99);

            // ASSERT
            Assert.AreEqual(null, result);
        }

        // POST: api/Geometries
        [TestMethod]
        public async Task PostGeometries_ValidModel_ReturnsCreatedAtAction()
        {
            //ARANGE
            var newGeometrie = new Geometrie { IdGeometrie = 1, NomPiece = "Fourche", TaillePiece = 1 };

            //ACT
            await _manager.AddAsync(newGeometrie);

            //ASSERT
            var geometrieInDB = await _context.Geometries.FindAsync(1);
            Assert.IsNotNull(geometrieInDB, "La geometrie devrait être présent dans la base de données.");
            Assert.AreEqual("Fourche", geometrieInDB.NomPiece);
        }

        [TestMethod]
        public async Task PostGeometrie_InvalidModel_ReturnsBadRequest()
        {
            //ARANGE
            var newGeometrie = new Geometrie { IdGeometrie = 1, TaillePiece = 1 };

            //ASSERT
            await Assert.ThrowsExceptionAsync<DbUpdateException>(async () => { await _manager.AddAsync(newGeometrie); });
        }

        // PUT: api/Geometries/5
        [TestMethod]
        public async Task PutGeometries_ValidUpdate_ReturnsNoContent()
        {
            //ARANGE
            var geometrieExisting = new Geometrie { IdGeometrie = 1, NomPiece = "Fourche", TaillePiece = 1 };
            var geometrieToUpdate = new Geometrie { IdGeometrie = 1, NomPiece = "Guidon", TaillePiece = 1 };
            _context.Geometries.Add(geometrieToUpdate);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
            //ACT
            await _manager.UpdateAsync(geometrieToUpdate, geometrieExisting);

            //ASSERT
            _context.ChangeTracker.Clear();

            var newGeometrie = await _context.Geometries.FindAsync(geometrieExisting.IdGeometrie);
            Assert.AreEqual("Fourche", newGeometrie.NomPiece);
        }

        [TestMethod]
        public async Task PutGeometries_IdMismatch_ReturnsBadRequest()
        {
            // Arrange
            var geometrieExisting = new Geometrie { IdGeometrie = 1, NomPiece = "Fourche", TaillePiece = 1 };
            var geometrieToUpdate = new Geometrie { IdGeometrie = 2, NomPiece = "Fourche", TaillePiece = 1 };

            // Assert
            await Assert.ThrowsExceptionAsync<DbUpdateConcurrencyException>(async () => { await _manager.UpdateAsync(geometrieToUpdate, geometrieExisting); });
        }

        // DELETE: api/Geometries/5
        [TestMethod]
        public async Task DeleteGeometries_ValidId_ReturnsNoContent()
        {
            // Arrange
            var geometrieExisting = new Geometrie { IdGeometrie = 1, NomPiece = "Fourche", TaillePiece = 1 };
            _context.Geometries.Add(geometrieExisting);
            await _context.SaveChangesAsync();

            // ACT
            await _manager.DeleteAsync(geometrieExisting);

            //  ASSERT
            var geometries = await _manager.GetAllAsync();

            Assert.IsNotNull(geometries);
        }
    }
}
