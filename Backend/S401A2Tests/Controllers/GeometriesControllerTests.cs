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
    public class GeometriesControllerTests
    {
        private Mock<IDataRepository<Geometrie>> _mockRepository;
        private GeometriesController _controller;

        // Initialize runs before every single test, ensuring a fresh setup
        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IDataRepository<Geometrie>>();
            _controller = new GeometriesController(_mockRepository.Object);
        }

        // GET: api/Geometries
        [TestMethod]
        public async Task GetGeometries_ReturnsOkWithAllGeometries()
        {
            // Arrange
            var mockGeometries = new List<Geometrie>
            {
                new Geometrie { IdGeometrie = 1, NomPiece = "Selle" ,TaillePiece = 12 },
                new Geometrie { IdGeometrie = 2, NomPiece = "Guidon" ,TaillePiece = 40 }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync())
                           .ReturnsAsync(mockGeometries);

            // Act
            var actionResult = await _controller.GetGeometrie();

            // Assert
            Assert.IsNotNull(actionResult);
            var resultList = actionResult.ToList();
            Assert.AreEqual(2, resultList.Count);
            Assert.AreEqual("Selle", resultList[0].NomPiece);
        }

        // GET: api/Geometries/5
        [TestMethod]
        public async Task GetGeometrie_ExistingId_ReturnsArticle()
        {
            // Arrange
            var mockGeometrie = new Geometrie { IdGeometrie = 1, NomPiece = "Selle", TaillePiece = 12 };
            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync(mockGeometrie);

            // Act
            var actionResult = await _controller.GetGeometrie(1);

            // Assert
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(mockGeometrie.NomPiece, actionResult.Value.NomPiece);
        }

        [TestMethod]
        public async Task GetGeometrie_UnknownId_ReturnsNotFound()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(99))
                           .ReturnsAsync((Geometrie)null);

            // Act
            var actionResult = await _controller.GetGeometrie(99);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Should return NotFound for unknown ID");
        }

        // POST: api/Geometries
        [TestMethod]
        public async Task PostGeometrie_ValidModel_ReturnsCreatedAtAction()
        {
            // Arrange
            var newGeometrie = new Geometrie { IdGeometrie = 1, NomPiece = "Selle", TaillePiece = 12 };

            // We simulate that AddAsync executes successfully (it returns Task, so we just setup a completed task)
            _mockRepository.Setup(repo => repo.AddAsync(newGeometrie))
                           .Returns(Task.CompletedTask);

            // Act
            var actionResult = await _controller.PostGeometrie(newGeometrie);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult));
            var createdResult = actionResult.Result as CreatedAtActionResult;
            Assert.AreEqual("GetGeometrie", createdResult.ActionName);
            Assert.AreEqual(1, ((Geometrie)createdResult.Value).IdGeometrie);
        }

        [TestMethod]
        public async Task PostGeometrie_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var newGeometrie = new Geometrie { IdGeometrie = 1, NomPiece = "", TaillePiece = 12 };
            _controller.ModelState.AddModelError("NomPiece", "NomPiece is required");

            // Act
            var actionResult = await _controller.PostGeometrie(newGeometrie);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestObjectResult));
        }

        // PUT: api/Geometries/5
        [TestMethod]
        public async Task PutGeometrie_IdMismatch_ReturnsBadRequest()
        {
            // Arrange
            var geometrieModifie = new Geometrie { IdGeometrie = 2, NomPiece = "Selle" ,TaillePiece = 12 };

            // Act
            // Passing ID 1 in the URL, but the object has ID 2
            var actionResult = await _controller.PutGeometrie(1, geometrieModifie);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task PutGeometrie_UnknownId_ReturnsNotFound()
        {
            // Arrange
            var geometrieModifie = new Geometrie { IdGeometrie = 1, NomPiece = "Selle", TaillePiece = 12 };
            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync((Geometrie)null);

            // Act
            var actionResult = await _controller.PutGeometrie(1, geometrieModifie);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PutGeometrie_ValidUpdate_ReturnsNoContent()
        {
            // Arrange
            var existingGeometrie = new Geometrie { IdGeometrie = 1, NomPiece = "Selle", TaillePiece = 12 };
            var updatedGeometrie = new Geometrie { IdGeometrie = 1, NomPiece = "Guidon", TaillePiece = 12 };

            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync(existingGeometrie);

            _mockRepository.Setup(repo => repo.UpdateAsync(existingGeometrie, updatedGeometrie))
                           .Returns(Task.CompletedTask);

            // Act
            var actionResult = await _controller.PutGeometrie(1, updatedGeometrie);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }

        // DELETE: api/Geometries/5
        [TestMethod]
        public async Task DeleteGeometrie_UnknownId_ReturnsNotFound()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(99))
                           .ReturnsAsync((Geometrie)null);

            // Act
            var actionResult = await _controller.DeleteGeometrie(99);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteGeometrie_ValidId_ReturnsNoContent()
        {
            // Arrange
            var existingGeometrie = new Geometrie { IdGeometrie = 1, NomPiece = "Selle", TaillePiece = 12 };

            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync(existingGeometrie);

            _mockRepository.Setup(repo => repo.DeleteAsync(existingGeometrie))
                           .Returns(Task.CompletedTask);

            // Act
            var actionResult = await _controller.DeleteGeometrie(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }
    }
}