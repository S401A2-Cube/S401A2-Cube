using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using S401A2.Controllers;
using S401A2.Model.EntityFramework;
using S401A2.Models.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S401A2.Controllers.Tests
{
    [TestClass]
    public class MotClesControllerTests
    {
        private Mock<IDataRepository<MotCle>> _mockRepository;
        private MotClesController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IDataRepository<MotCle>>();
            _controller = new MotClesController(_mockRepository.Object);
        }

        // GET: api/MotCles
        [TestMethod]
        public async Task GetMotCles_ReturnsAllMotCles()
        {
            // Arrange
            var mockMotCles = new List<MotCle>
            {
                new MotCle { MotCleId = 1, Nom = "Carbone" },
                new MotCle { MotCleId = 2, Nom = "Tout Suspendu" }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync())
                           .ReturnsAsync(mockMotCles);

            // Act
            var result = await _controller.GetMotCles();

            // Assert
            Assert.IsNotNull(result);
            var resultList = result.ToList();
            Assert.AreEqual(2, resultList.Count);
            Assert.AreEqual("Carbone", resultList[0].Nom);
        }

        // GET: api/MotCles/5
        [TestMethod]
        public async Task GetMotCle_ExistingId_ReturnsMotCle()
        {
            // Arrange
            var mockMotCle = new MotCle { MotCleId = 1, Nom = "Moteur Bosch" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync(mockMotCle);

            // Act
            var actionResult = await _controller.GetMotCle(1);

            // Assert
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(mockMotCle.Nom, actionResult.Value.Nom);
        }

        [TestMethod]
        public async Task GetMotCle_UnknownId_ReturnsNotFound()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(99))
                           .ReturnsAsync((MotCle)null);

            // Act
            var actionResult = await _controller.GetMotCle(99);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Should return NotFound for an unknown ID");
        }

        // POST: api/MotCles
        [TestMethod]
        public async Task PostMotCle_ValidModel_ReturnsCreatedAtAction()
        {
            // Arrange
            var newMotCle = new MotCle { MotCleId = 5, Nom = "Aérodynamique" };

            _mockRepository.Setup(repo => repo.AddAsync(newMotCle))
                           .Returns(Task.CompletedTask);

            // Act
            var actionResult = await _controller.PostMotCle(newMotCle);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult));
            var createdResult = actionResult.Result as CreatedAtActionResult;
            Assert.AreEqual("GetMotCle", createdResult.ActionName);
            Assert.AreEqual(5, ((MotCle)createdResult.Value).MotCleId);
        }

        [TestMethod]
        public async Task PostMotCle_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var newMotCle = new MotCle { MotCleId = 6 }; // Missing required Nom
            _controller.ModelState.AddModelError("Nom", "Le nom est requis");

            // Act
            var actionResult = await _controller.PostMotCle(newMotCle);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestObjectResult));
        }

        // PUT: api/MotCles/5
        [TestMethod]
        public async Task PutMotCle_IdMismatch_ReturnsBadRequest()
        {
            // Arrange
            var motCleModifie = new MotCle { MotCleId = 2, Nom = "Shimano Ultegra" };

            // Act
            var actionResult = await _controller.PutMotCle(1, motCleModifie);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task PutMotCle_UnknownId_ReturnsNotFound()
        {
            // Arrange
            var motCleModifie = new MotCle { MotCleId = 1, Nom = "Freins à Disque" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync((MotCle)null);

            // Act
            var actionResult = await _controller.PutMotCle(1, motCleModifie);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PutMotCle_ValidUpdate_ReturnsNoContent()
        {
            // Arrange
            var existingMotCle = new MotCle { MotCleId = 1, Nom = "Ancien Mot Clé" };
            var updatedMotCle = new MotCle { MotCleId = 1, Nom = "Batterie 750Wh" };

            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync(existingMotCle);

            _mockRepository.Setup(repo => repo.UpdateAsync(existingMotCle, updatedMotCle))
                           .Returns(Task.CompletedTask);

            // Act
            var actionResult = await _controller.PutMotCle(1, updatedMotCle);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }

        // DELETE: api/MotCles/5
        [TestMethod]
        public async Task DeleteMotCle_UnknownId_ReturnsNotFound()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(99))
                           .ReturnsAsync((MotCle)null);

            // Act
            var actionResult = await _controller.DeleteMotCle(99);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteMotCle_ValidId_ReturnsNoContent()
        {
            // Arrange
            var existingMotCle = new MotCle { MotCleId = 1, Nom = "Mot Clé Obsolète" };

            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync(existingMotCle);

            _mockRepository.Setup(repo => repo.DeleteAsync(existingMotCle))
                           .Returns(Task.CompletedTask);

            // Act
            var actionResult = await _controller.DeleteMotCle(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }
    }
}