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
    public class ModeleManagerTests
    {
        private CubeDBContext _context;
        private ModeleManager _manager;
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
            _manager = new ModeleManager(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
            _connection.Close();
        }

        // GET: api/Modeles
        [TestMethod]
        public async Task GetAllAsync_ReturnAllModeles()
        {
            //ARANGE
            _context.Modeles.Add(new Modele { IdModele = 1, NomModele = "X200"});
            _context.Modeles.Add(new Modele { IdModele = 2, NomModele = "X220" });
            await _context.SaveChangesAsync();

            //ACT
            var result = await _manager.GetAllAsync();

            //ASSERT
            Assert.IsNotNull(result);
            var resultList = result.ToList();
            Assert.AreEqual(2, resultList.Count);
            Assert.AreEqual("X200", resultList[0].NomModele);
        }

        // GET: api/Modele/5
        [TestMethod]
        public async Task GetModeles_ExistingId_ReturnCadre()
        {
            //ARANGE
            var newModele = new Modele { IdModele = 1, NomModele = "X200" };
            _context.Modeles.Add(newModele);
            await _context.SaveChangesAsync();
            var id = newModele.IdModele;
            //ACT
            var result = await _manager.GetByIdAsync(id);

            //ASSERT
            Assert.AreEqual("X200", result.NomModele);
        }

        [TestMethod]
        public async Task GetModele_UnknownId_ReturnsNotFound()
        {
            // ARRANGE
            _context.Modeles.Add(new Modele { IdModele = 1, NomModele = "X200" });
            await _context.SaveChangesAsync();


            // ACT
            var result = await _manager.GetByIdAsync(99);

            // ASSERT
            Assert.AreEqual(null, result);
        }

        // POST: api/Modeles
        [TestMethod]
        public async Task PostModeles_ValidModel_ReturnsCreatedAtAction()
        {
            //ARANGE
            var newModele = new Modele { IdModele = 1, NomModele = "X200" };

            //ACT
            await _manager.AddAsync(newModele);

            //ASSERT
            var modeleInDB = await _context.Modeles.FindAsync(1);
            Assert.IsNotNull(modeleInDB, "Le modele devrait être présent dans la base de données.");
            Assert.AreEqual("X200", modeleInDB.NomModele);
        }

        [TestMethod]
        public async Task PostModele_InvalidModel_ReturnsBadRequest()
        {
            //ARANGE
            var newModele = new Modele { IdModele = 1};

            //ASSERT
            await Assert.ThrowsExceptionAsync<DbUpdateException>(async () => { await _manager.AddAsync(newModele); });
        }

        // PUT: api/Geometries/5
        [TestMethod]
        public async Task PutModeles_ValidUpdate_ReturnsNoContent()
        {
            //ARANGE
            var modeleExisting = new Modele { IdModele = 1, NomModele = "X200" };
            var modeleToUpdate = new Modele { IdModele = 1, NomModele = "X220" };
            _context.Modeles.Add(modeleToUpdate);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
            //ACT
            await _manager.UpdateAsync(modeleToUpdate, modeleExisting);

            //ASSERT
            _context.ChangeTracker.Clear();

            var newGeometrie = await _context.Modeles.FindAsync(modeleExisting.IdModele);
            Assert.AreEqual("X200", newGeometrie.NomModele);
        }

        [TestMethod]
        public async Task PutModeles_IdMismatch_ReturnsBadRequest()
        {
            // Arrange
            var modeleExisting = new Modele { IdModele = 1, NomModele = "X200" };
            var modeleToUpdate = new Modele { IdModele = 2, NomModele = "X220" };

            // Assert
            await Assert.ThrowsExceptionAsync<DbUpdateConcurrencyException>(async () => { await _manager.UpdateAsync(modeleToUpdate, modeleExisting); });
        }

        // DELETE: api/Modeles/5
        [TestMethod]
        public async Task DeleteModeles_ValidId_ReturnsNoContent()
        {
            // Arrange
            var modeleExisting = new Modele { IdModele = 1, NomModele = "X200" };
            _context.Modeles.Add(modeleExisting);
            await _context.SaveChangesAsync();

            // ACT
            await _manager.DeleteAsync(modeleExisting);

            //  ASSERT
            var modeles = await _manager.GetAllAsync();

            Assert.IsNotNull(modeles);
        }
    }
}
