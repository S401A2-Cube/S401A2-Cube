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
    public class ClientManagerTests
    {
        private CubeDBContext _context;
        private ClientManager _manager;
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
            _manager = new ClientManager(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
            _connection.Close();
        }

        // GET: api/Clients
        [TestMethod]
        public async Task GetAllAsync_ReturnAllClients()
        {
            //ARANGE
            _context.Clients.Add( new Client { Id = 1,CiviliteId=1, Nom = "Personeni", Prenom = "Nathan", Email = "nathan.personeni@etu.univ-smb.fr", Mdp = "123456789", DateNaissance = (new DateTime(2006, 02, 20)), Role = 1 });
            _context.Clients.Add(new Client { Id = 2,CiviliteId=1, Nom = "Coltelli", Prenom = "Raphael", Email = "raphael.coltelli@etu.univ-smb.fr", Mdp = "123456789", DateNaissance = (new DateTime(2006, 05, 15)), Role = 2 });
            await _context.SaveChangesAsync();

            //ACT
            var result = await _manager.GetAllAsync();

            //ASSERT
            Assert.IsNotNull(result);
            var resultList = result.ToList();
            Assert.AreEqual(2, resultList.Count);
            Assert.AreEqual("Personeni", resultList[0].Nom);
        }

        // GET: api/Clients/5
        [TestMethod]
        public async Task GetClient_ExistingId_ReturnClient()
        {
            //ARANGE
            var newClient = new Client {Nom = "Personeni",CiviliteId=1, Prenom = "Nathan", Email = "nathan.personeni@etu.univ-smb.fr", Mdp = "123456789", DateNaissance = (new DateTime(2006, 02, 20)), Role = 1 };
            _context.Clients.Add(newClient);
            await _context.SaveChangesAsync();
            var id = newClient.Id;
            //ACT
            var result = await _manager.GetByIdAsync(id);

            //ASSERT
            Assert.AreEqual("Personeni", result.Nom);
        }

        [TestMethod]
        public async Task GetClient_UnknownId_ReturnsNotFound()
        {
            // ARRANGE
            _context.Clients.Add(new Client { Id = 1, Nom = "Personeni",CiviliteId=1, Prenom = "Nathan", Email = "nathan.personeni@etu.univ-smb.fr", Mdp = "123456789", DateNaissance = (new DateTime(2006, 02, 20)), Role = 1 });
            await _context.SaveChangesAsync();


            // ACT
            var result = await _manager.GetByIdAsync(99);

            // ASSERT
            Assert.AreEqual(null,result);
        }

        // POST: api/Clients
        [TestMethod]
        public async Task PostClient_ValidModel_ReturnsCreatedAtAction()
        {
            //ARANGE
            var newClient = new Client { Id = 1, Nom = "Personeni",CiviliteId=1, Prenom = "Nathan", Email = "nathan.personeni@etu.univ-smb.fr", Mdp = "123456789", DateNaissance = (new DateTime(2006, 02, 20)), Role = 1 };

            //ACT
            await _manager.AddAsync(newClient);

            //ASSERT
            var clientInDB = await _context.Clients.FindAsync(1);
            Assert.IsNotNull(clientInDB, "Le client devrait être présent dans la base de données.");
            Assert.AreEqual("Personeni", clientInDB.Nom);
        }

        [TestMethod]
        public async Task PostClient_InvalidModel_ReturnsBadRequest()
        {
            //ARANGE
            var newClient = new Client { Id = 1, Prenom = "Nathan",CiviliteId=1 ,Email = "nathan.personeni@etu.univ-smb.fr", Mdp = "123456789", DateNaissance = (new DateTime(2006, 02, 20)), Role = 1 };

            //ASSERT
            await Assert.ThrowsExceptionAsync<DbUpdateException>(async () => { await _manager.AddAsync(newClient); });
        }

        // PUT: api/Clients/5
        [TestMethod]
        public async Task PutClient_ValidUpdate_ReturnsNoContent()
        {
            //ARANGE
            var clientExisting = new Client { Id = 1, Prenom = "Nathan",CiviliteId=1, Nom="Personeni", Email = "nathan.personeni@etu.univ-smb.fr", Mdp = "123456789", DateNaissance = (new DateTime(2006, 02, 20)), Role = 1 };
            var clientToUpdate = new Client { Id = 1, Prenom = "Clement",CiviliteId=1, Nom = "Personeni", Email = "nathan.personeni@etu.univ-smb.fr", Mdp = "123456789", DateNaissance = (new DateTime(2006, 02, 20)), Role = 1 };
            _context.Clients.Add(clientExisting);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
            //ACT
            await _manager.UpdateAsync(clientToUpdate,clientExisting);
            
            //ASSERT
            _context.ChangeTracker.Clear();

            var newClient = await _context.Clients.FindAsync(clientExisting.Id);
            Assert.AreEqual("Nathan", newClient.Prenom);
        }

        [TestMethod]
        public async Task PutClient_IdMismatch_ReturnsBadRequest()
        {
            // Arrange
            var clientExisting = new Client { Id = 1, Nom = "Personeni",CiviliteId=1, Prenom = "Nathan", Email = "nathan.personeni@etu.univ-smb.fr", Mdp = "123456789", DateNaissance = (new DateTime(2006, 02, 20)), Role = 1 };
            var clientToUpdate = new Client { Id = 2, Nom = "Personeni", CiviliteId = 1 ,Prenom = "Nathan", Email = "nathan.personeni@etu.univ-smb.fr", Mdp = "123456789", DateNaissance = (new DateTime(2006, 02, 20)), Role = 1 };

            // Assert
            await Assert.ThrowsExceptionAsync<DbUpdateConcurrencyException>(async () => { await _manager.UpdateAsync(clientToUpdate, clientExisting); });
        }

        // DELETE: api/Clients/5
        [TestMethod]
        public async Task DeleteClient_ValidId_ReturnsNoContent()
        {
            // Arrange
            var clientExisting = new Client { Id = 2, Nom = "Personeni", CiviliteId=1,Prenom = "Nathan", Email = "nathan.personeni@etu.univ-smb.fr", Mdp = "123456789", DateNaissance = (new DateTime(2006, 02, 20)), Role = 1 };
            _context.Clients.Add(clientExisting);
            await _context.SaveChangesAsync();

            // ACT
            await _manager.DeleteAsync(clientExisting);

            //  ASSERT
            var client = await _manager.GetAllAsync();

            Assert.IsNotNull(client);
        }
    }
}
