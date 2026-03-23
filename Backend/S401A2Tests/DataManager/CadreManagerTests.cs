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
    public class CadreManagerTests
    {
        private CubeDBContext _context;
        private CadreManager _manager;
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
            _manager = new CadreManager(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
            _connection.Close();
        }

        // GET: api/Cadres
        [TestMethod]
        public async Task GetAllAsync_ReturnAllCadres()
        {
            //ARANGE
            _context.Cadres.Add(new Cadre { IdMateriau = 1, NomMat="Aluminium",FormeCadre="Wave"});
            _context.Cadres.Add(new Cadre { IdMateriau = 2, NomMat = "Carbonne", FormeCadre = "Diamant"});
            await _context.SaveChangesAsync();

            //ACT
            var result = await _manager.GetAllAsync();

            //ASSERT
            Assert.IsNotNull(result);
            var resultList = result.ToList();
            Assert.AreEqual(2, resultList.Count);
            Assert.AreEqual("Aluminium", resultList[0].NomMat);
        }

        // GET: api/Cadres/5
        [TestMethod]
        public async Task GetCadre_ExistingId_ReturnCadre()
        {
            //ARANGE
            var newCadre = new Cadre { IdMateriau = 1, NomMat = "Aluminium", FormeCadre = "Wave" };
            _context.Cadres.Add(newCadre);
            await _context.SaveChangesAsync();
            var id = newCadre.IdMateriau;
            //ACT
            var result = await _manager.GetByIdAsync(id);

            //ASSERT
            Assert.AreEqual("Aluminium", result.NomMat);
        }

        [TestMethod]
        public async Task GetCadre_UnknownId_ReturnsNotFound()
        {
            // ARRANGE
            _context.Cadres.Add(new Cadre { IdMateriau = 1, NomMat = "Aluminium", FormeCadre = "Wave" });
            await _context.SaveChangesAsync();


            // ACT
            var result = await _manager.GetByIdAsync(99);

            // ASSERT
            Assert.AreEqual(null, result);
        }

        // POST: api/Cadres
        [TestMethod]
        public async Task PostCadre_ValidModel_ReturnsCreatedAtAction()
        {
            //ARANGE
            var newCadre = new Cadre { IdMateriau = 1, NomMat = "Aluminium", FormeCadre = "Wave" };

            //ACT
            await _manager.AddAsync(newCadre);

            //ASSERT
            var cadreInDB = await _context.Cadres.FindAsync(1);
            Assert.IsNotNull(cadreInDB, "Le cadre devrait être présent dans la base de données.");
            Assert.AreEqual("Aluminium", cadreInDB.NomMat);
        }

        [TestMethod]
        public async Task PostCadre_InvalidModel_ReturnsBadRequest()
        {
            //ARANGE
            var newCadre = new Cadre { IdMateriau = 1, FormeCadre = "Wave" };

            //ASSERT
            await Assert.ThrowsExceptionAsync<DbUpdateException>(async () => { await _manager.AddAsync(newCadre); });
        }

        // PUT: api/Cadres/5
        [TestMethod]
        public async Task PutCadre_ValidUpdate_ReturnsNoContent()
        {
            //ARANGE
            var cadreExisting = new Cadre { IdMateriau = 1, NomMat = "Aluminium", FormeCadre = "Wave" };
            var cadreToUpdate = new Cadre { IdMateriau = 1, NomMat = "Carbonne", FormeCadre = "Wave"};
            _context.Cadres.Add(cadreToUpdate);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
            //ACT
            await _manager.UpdateAsync(cadreToUpdate, cadreExisting);

            //ASSERT
            _context.ChangeTracker.Clear();

            var newCadre = await _context.Cadres.FindAsync(cadreExisting.IdMateriau);
            Assert.AreEqual("Aluminium", newCadre.NomMat);
        }

        [TestMethod]
        public async Task PutCadre_IdMismatch_ReturnsBadRequest()
        {
            // Arrange
            var cadreExisting = new Cadre { IdMateriau = 1, NomMat = "Aluminium", FormeCadre = "Wave" };
            var cadreToUpdate = new Cadre { IdMateriau = 2, NomMat = "Carbonne", FormeCadre = "Wave" };

            // Assert
            await Assert.ThrowsExceptionAsync<DbUpdateConcurrencyException>(async () => { await _manager.UpdateAsync(cadreToUpdate, cadreExisting); });
        }

        // DELETE: api/Cadress/5
        [TestMethod]
        public async Task DeleteCadre_ValidId_ReturnsNoContent()
        {
            // Arrange
            var cadreExisting = new Cadre { IdMateriau = 1, NomMat = "Aluminium", FormeCadre = "Wave" };
            _context.Cadres.Add(cadreExisting);
            await _context.SaveChangesAsync();

            // ACT
            await _manager.DeleteAsync(cadreExisting);

            //  ASSERT
            var cadre = await _manager.GetAllAsync();

            Assert.IsNotNull(cadre);
        }
    }
}
