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
    public class TaillesControllerTests
    {
        private Mock<IDataRepository<Taille>> _mockRepository;
        private TaillesController _controller;

        // Initialize runs before every single test, ensuring a fresh setup
        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IDataRepository<Taille>>();
            _controller = new TaillesController(_mockRepository.Object);
        }

        // GET: api/Tailles
        [TestMethod]
        public async Task GetTailles_ReturnsOkWithAllArticles()
        {
            // Arrange
            var mockTailles = new List<Taille>
            {
                new Taille { IdTaille = 1, LibelleTaille = "X"},
                new Taille { IdTaille = 2, LibelleTaille = "L"}
            };

            _mockRepository.Setup(repo => repo.GetAllAsync())
                           .ReturnsAsync(mockTailles);

            // Act
            var actionResult = await _controller.GetTailles();

            // Assert
            Assert.IsNotNull(actionResult);
            var resultList = actionResult.ToList();
            Assert.AreEqual(2, resultList.Count);
            Assert.AreEqual("X", resultList[0].LibelleTaille);
        }

        // GET: api/Tailles/5
        [TestMethod]
        public async Task GetTaille_ExistingId_ReturnsArticle()
        {
            // Arrange
            var mockTaille = new Taille { IdTaille = 1, LibelleTaille = "X" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync(mockTaille);

            // Act
            var actionResult = await _controller.GetTaille(1);

            // Assert
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(mockTaille.LibelleTaille, actionResult.Value.LibelleTaille);
        }

        [TestMethod]
        public async Task GetTaille_UnknownId_ReturnsNotFound()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(99))
                           .ReturnsAsync((Taille)null);

            // Act
            var actionResult = await _controller.GetTaille(99);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Should return NotFound for unknown ID");
        }

        // POST: api/Tailles
        [TestMethod]
        public async Task PostTaille_ValidModel_ReturnsCreatedAtAction()
        {
            // Arrange
            var newTaille = new Taille { IdTaille = 1, LibelleTaille = "X" };

            // We simulate that AddAsync executes successfully (it returns Task, so we just setup a completed task)
            _mockRepository.Setup(repo => repo.AddAsync(newTaille))
                           .Returns(Task.CompletedTask);

            // Act
            var actionResult = await _controller.PostTaille(newTaille);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult));
            var createdResult = actionResult.Result as CreatedAtActionResult;
            Assert.AreEqual("GetTaille", createdResult.ActionName);
            Assert.AreEqual(1, ((Taille)createdResult.Value).IdTaille);
        }

        [TestMethod]
        public async Task PostTaille_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var newTaille = new Taille { IdTaille = 1, LibelleTaille = "zz" };
            _controller.ModelState.AddModelError("Reference", "Reference is required");

            // Act
            var actionResult = await _controller.PostTaille(newTaille);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestObjectResult));
        }

        // PUT: api/Tailles/5
        [TestMethod]
        public async Task PutTaille_IdMismatch_ReturnsBadRequest()
        {
            // Arrange
            var tailleModifie = new Taille { IdTaille = 2, LibelleTaille = "X" };

            // Act
            // Passing ID 1 in the URL, but the object has ID 2
            var actionResult = await _controller.PutTaille(1, tailleModifie);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task PutTaille_UnknownId_ReturnsNotFound()
        {
            // Arrange
            var tailleModifie = new Taille { IdTaille = 1, LibelleTaille = "X" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync((Taille)null);

            // Act
            var actionResult = await _controller.PutTaille(1, tailleModifie);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PutTaille_ValidUpdate_ReturnsNoContent()
        {
            // Arrange
            var existingTaille = new Taille { IdTaille = 1, LibelleTaille = "X" };
            var updatedTaille = new Taille { IdTaille = 1, LibelleTaille = "XXL" };

            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync(existingTaille);

            _mockRepository.Setup(repo => repo.UpdateAsync(existingTaille, updatedTaille))
                           .Returns(Task.CompletedTask);

            // Act
            var actionResult = await _controller.PutTaille(1, updatedTaille);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }

        // DELETE: api/Tailles/5
        [TestMethod]
        public async Task DeleteArticle_UnknownId_ReturnsNotFound()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(99))
                           .ReturnsAsync((Taille)null);

            // Act
            var actionResult = await _controller.DeleteTaille(99);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteTaille_ValidId_ReturnsNoContent()
        {
            // Arrange
            var existingTaille = new Taille { IdTaille = 1, LibelleTaille = "X" };

            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync(existingTaille);

            _mockRepository.Setup(repo => repo.DeleteAsync(existingTaille))
                           .Returns(Task.CompletedTask);

            // Act
            var actionResult = await _controller.DeleteTaille(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }
    }
}