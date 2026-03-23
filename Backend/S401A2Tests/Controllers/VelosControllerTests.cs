using APICube.Models.EntityFramework;
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
    public class VelosControllerTests
    {
        private Mock<IDataRepository<Velo>> _mockRepository;
        private VelosController _controller;

        // Initialize runs before every single test, ensuring a fresh setup
        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IDataRepository<Velo>>();
            _controller = new VelosController(_mockRepository.Object);
        }

        // GET: api/Velos
        [TestMethod]
        public async Task GetVelos_ReturnsOkWithAllArticles()
        {
            // Arrange
            var mockVelos = new List<Velo>
            {
                new Velo { IdVelo = 1, IdArticle = 1, LienVue360 = "google.com" },
                new Velo { IdVelo = 2, IdArticle = 2, LienVue360 = "etu.univ-smb.fr" }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync())
                           .ReturnsAsync(mockVelos);

            // Act
            var actionResult = await _controller.GetVelos();

            // Assert
            Assert.IsNotNull(actionResult);
            var resultList = actionResult.ToList();
            Assert.AreEqual(2, resultList.Count);
            Assert.AreEqual("google.com", resultList[0].LienVue360);
        }

        // GET: api/Velos/5
        [TestMethod]
        public async Task GetVelos_ExistingId_ReturnsArticle()
        {
            // Arrange
            var mockVelo = new Velo { IdVelo = 1, IdArticle = 1, LienVue360 = "google.com" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync(mockVelo);

            // Act
            var actionResult = await _controller.GetVelo (1);

            // Assert
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(mockVelo.LienVue360, actionResult.Value.LienVue360);
        }

        [TestMethod]
        public async Task GetVelo_UnknownId_ReturnsNotFound()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(99))
                           .ReturnsAsync((Velo)null);

            // Act
            var actionResult = await _controller.GetVelo(99);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Should return NotFound for unknown ID");
        }

        // POST: api/Velos
        [TestMethod]
        public async Task PostVelo_ValidModel_ReturnsCreatedAtAction()
        {
            // Arrange
            var newVelo = new Velo { IdVelo = 1, IdArticle = 1, LienVue360 = "google.com" };

            // We simulate that AddAsync executes successfully (it returns Task, so we just setup a completed task)
            _mockRepository.Setup(repo => repo.AddAsync(newVelo))
                           .Returns(Task.CompletedTask);

            // Act
            var actionResult = await _controller.PostVelo(newVelo);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult));
            var createdResult = actionResult.Result as CreatedAtActionResult;
            Assert.AreEqual("GetVelo", createdResult.ActionName);
            Assert.AreEqual(1, ((Velo)createdResult.Value).IdVelo);
        }

        [TestMethod]
        public async Task PostVelo_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var newVelo = new Velo { IdVelo = 1, IdArticle = 1, LienVue360 = "" };
            _controller.ModelState.AddModelError("LienVue360", "LienVue360 is required");

            // Act
            var actionResult = await _controller.PostVelo(newVelo);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestObjectResult));
        }

        // PUT: api/Velos/5
        [TestMethod]
        public async Task PutVelo_IdMismatch_ReturnsBadRequest()
        {
            // Arrange
            var veloModifie = new Velo { IdVelo = 2, IdArticle = 1, LienVue360 = "google.com" };

            // Act
            // Passing ID 1 in the URL, but the object has ID 2
            var actionResult = await _controller.PutVelo(1, veloModifie);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task PutVelo_UnknownId_ReturnsNotFound()
        {
            // Arrange
            var veloModifie = new Velo { IdVelo = 1, IdArticle = 1, LienVue360 = "google.com" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync((Velo)null);

            // Act
            var actionResult = await _controller.PutVelo(1, veloModifie);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PutVelo_ValidUpdate_ReturnsNoContent()
        {
            // Arrange
            var existingVelo = new Velo { IdVelo = 1, IdArticle = 1, LienVue360 = "google.com" };
            var updatedVelo = new Velo { IdVelo = 1, IdArticle = 1, LienVue360 = "google.fr" };

            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync(existingVelo);

            _mockRepository.Setup(repo => repo.UpdateAsync(existingVelo, updatedVelo))
                           .Returns(Task.CompletedTask);

            // Act
            var actionResult = await _controller.PutVelo(1, updatedVelo);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }

        // DELETE: api/Velos/5
        [TestMethod]
        public async Task DeleteVelo_UnknownId_ReturnsNotFound()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(99))
                           .ReturnsAsync((Velo)null);

            // Act
            var actionResult = await _controller.DeleteVelo(99);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteArticle_ValidId_ReturnsNoContent()
        {
            // Arrange
            var existingVelo = new Velo { IdVelo = 1, IdArticle = 1, LienVue360 = "google.com" };

            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync(existingVelo);

            _mockRepository.Setup(repo => repo.DeleteAsync(existingVelo))
                           .Returns(Task.CompletedTask);

            // Act
            var actionResult = await _controller.DeleteVelo(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }
    }
}