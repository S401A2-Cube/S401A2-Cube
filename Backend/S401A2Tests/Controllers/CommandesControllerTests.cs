using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using APICube.Models.EntityFramework;
using S401A2.Models.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using S401A2.Controllers;

namespace S401A2.Controllers.Tests
{
    [TestClass]
    public class CommandesControllerTests
    {
        private Mock<IDataRepository<Commande>> _mockRepository;
        private CommandesController _controller;

        // Initialize runs before every single test, ensuring a fresh setup
        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IDataRepository<Commande>>();
            _controller = new CommandesController(_mockRepository.Object);
        }

        // GET: api/Commandes
        [TestMethod]
        public async Task GetCommandes_ReturnsOkWithAllCommandes()
        {
            // Arrange
            var mockCommandes = new List<Commande>
            {
                new Commande { Id = 1, Livraison = "Standard", ClientId = 1 },
                new Commande { Id = 2, Livraison = "Express", ClientId = 2 }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync())
                           .ReturnsAsync(mockCommandes);

            // Act
            var actionResult = await _controller.GetCommandes();

            // Assert
            Assert.IsNotNull(actionResult);
            var resultList = actionResult.ToList();
            Assert.AreEqual(2, resultList.Count);
            Assert.AreEqual("Standard", resultList[0].Livraison);
        }

        // GET: api/Commandes/5
        [TestMethod]
        public async Task GetCommande_ExistingId_ReturnsCommande()
        {
            // Arrange
            var mockCommande = new Commande { Id = 1, Livraison = "Standard", ClientId = 1 };
            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync(mockCommande);

            // Act
            var actionResult = await _controller.GetCommande(1);

            // Assert
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(mockCommande.Livraison, actionResult.Value.Livraison);
        }

        [TestMethod]
        public async Task GetCommande_UnknownId_ReturnsNotFound()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(99))
                           .ReturnsAsync((Commande)null);

            // Act
            var actionResult = await _controller.GetCommande(99);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Should return NotFound for unknown ID");
        }

        // POST: api/Commandes
        [TestMethod]
        public async Task PostCommande_ValidModel_ReturnsCreatedAtAction()
        {
            // Arrange
            var newCommande = new Commande { Id = 10, Livraison = "Express", ClientId = 3 };

            // We simulate that AddAsync executes successfully (it returns Task, so we just setup a completed task)
            _mockRepository.Setup(repo => repo.AddAsync(newCommande))
                           .Returns(Task.CompletedTask);

            // Act
            var actionResult = await _controller.PostCommande(newCommande);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult));
            var createdResult = actionResult.Result as CreatedAtActionResult;
            Assert.AreEqual("GetCommande", createdResult.ActionName);
            Assert.AreEqual(10, ((Commande)createdResult.Value).Id);
        }

        [TestMethod]
        public async Task PostCommande_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var newCommande = new Commande { ClientId = 3 };
            _controller.ModelState.AddModelError("Livraison", "Livraison is required");

            // Act
            var actionResult = await _controller.PostCommande(newCommande);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestObjectResult));
        }

        // PUT: api/Commandes/5
        [TestMethod]
        public async Task PutCommande_IdMismatch_ReturnsBadRequest()
        {
            // Arrange
            var commandeModifie = new Commande { Id = 2, Livraison = "Standard" };

            // Act
            // Passing ID 1 in the URL, but the object has ID 2
            var actionResult = await _controller.PutCommande(1, commandeModifie);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task PutCommande_UnknownId_ReturnsNotFound()
        {
            // Arrange
            var commandeModifie = new Commande { Id = 1, Livraison = "Standard" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync((Commande)null);

            // Act
            var actionResult = await _controller.PutCommande(1, commandeModifie);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PutCommande_ValidUpdate_ReturnsNoContent()
        {
            // Arrange
            var existingCommande = new Commande { Id = 1, Livraison = "Standard" };
            var updatedCommande = new Commande { Id = 1, Livraison = "Express" };

            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync(existingCommande);

            _mockRepository.Setup(repo => repo.UpdateAsync(existingCommande, updatedCommande))
                           .Returns(Task.CompletedTask);

            // Act
            var actionResult = await _controller.PutCommande(1, updatedCommande);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }

        // DELETE: api/Commandes/5
        [TestMethod]
        public async Task DeleteCommande_UnknownId_ReturnsNotFound()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(99))
                           .ReturnsAsync((Commande)null);

            // Act
            var actionResult = await _controller.DeleteCommande(99);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteCommande_ValidId_ReturnsNoContent()
        {
            // Arrange
            var existingCommande = new Commande { Id = 1, Livraison = "To Delete" };

            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync(existingCommande);

            _mockRepository.Setup(repo => repo.DeleteAsync(existingCommande))
                           .Returns(Task.CompletedTask);

            // Act
            var actionResult = await _controller.DeleteCommande(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }
    }
}