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
    public class CadresControllerTests
    {
        private Mock<IDataRepository<Cadre>> _mockRepository;
        private CadresController _controller;

        // Initialize runs before every single test, ensuring a fresh setup
        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IDataRepository<Cadre>>();
            _controller = new CadresController(_mockRepository.Object);
        }

        // GET: api/Cadres
        [TestMethod]
        public async Task GetCadres_ReturnsOkWithAllCadres()
        {
            // Arrange
            var mockCadres = new List<Cadre>
            {
                new Cadre { IdMateriau = 1, NomMat = "Aluminium", FormeCadre = "Wave" },
                new Cadre { IdMateriau = 2, NomMat = "Carbonne", FormeCadre = "VTT" },
            };

            _mockRepository.Setup(repo => repo.GetAllAsync())
                           .ReturnsAsync(mockCadres);

            // Act
            var actionResult = await _controller.GetCadres();

            // Assert
            Assert.IsNotNull(actionResult);
            var resultList = actionResult.ToList();
            Assert.AreEqual(2, resultList.Count);
            Assert.AreEqual("Aluminium", resultList[0].NomMat);
        }

        // GET: api/Cadres/5
        [TestMethod]
        public async Task GetCadre_ExistingId_ReturnsArticle()
        {
            // Arrange
            var mockCadre = new Cadre { IdMateriau = 1, NomMat = "Aluminium", FormeCadre = "Wave" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync(mockCadre);

            // Act
            var actionResult = await _controller.GetCadre(1);

            // Assert
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(mockCadre.NomMat, actionResult.Value.NomMat);
        }

        [TestMethod]
        public async Task GetCadre_UnknownId_ReturnsNotFound()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(99))
                           .ReturnsAsync((Cadre)null);

            // Act
            var actionResult = await _controller.GetCadre(99);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Should return NotFound for unknown ID");
        }

        // POST: api/Cadres
        [TestMethod]
        public async Task PostCadre_ValidModel_ReturnsCreatedAtAction()
        {
            // Arrange
            var newCadre = new Cadre { IdMateriau = 1, NomMat = "Aluminium", FormeCadre = "Wave" };

            // We simulate that AddAsync executes successfully (it returns Task, so we just setup a completed task)
            _mockRepository.Setup(repo => repo.AddAsync(newCadre))
                           .Returns(Task.CompletedTask);

            // Act
            var actionResult = await _controller.PostCadre(newCadre);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult));
            var createdResult = actionResult.Result as CreatedAtActionResult;
            Assert.AreEqual("GetCadre", createdResult.ActionName);
            Assert.AreEqual(1, ((Cadre)createdResult.Value).IdMateriau);
        }

        [TestMethod]
        public async Task PostCadre_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var newCadre = new Cadre { NomMat = "Incomplete cadre" };
            _controller.ModelState.AddModelError("NomMat", "NomMat is required");

            // Act
            var actionResult = await _controller.PostCadre(newCadre);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestObjectResult));
        }

        // PUT: api/Cadres/5
        [TestMethod]
        public async Task PutCadre_IdMismatch_ReturnsBadRequest()
        {
            // Arrange
            var cadreModifie = new Cadre { IdMateriau = 2, NomMat = "Aluminium", FormeCadre = "Wave" };

            // Act
            // Passing ID 1 in the URL, but the object has ID 2
            var actionResult = await _controller.PutCadre(1, cadreModifie);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task PutCadre_UnknownId_ReturnsNotFound()
        {
            // Arrange
            var cadreModifie = new Cadre { IdMateriau = 1, NomMat = "Aluminium", FormeCadre = "Wave" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync((Cadre)null);

            // Act
            var actionResult = await _controller.PutCadre(1, cadreModifie);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PutCadre_ValidUpdate_ReturnsNoContent()
        {
            // Arrange
            var existingCadre = new Cadre { IdMateriau = 1, NomMat = "Aluminium", FormeCadre = "Wave" };
            var updatedCadre = new Cadre { IdMateriau = 1, NomMat = "Carbonne", FormeCadre = "Wave" };

            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync(existingCadre);

            _mockRepository.Setup(repo => repo.UpdateAsync(existingCadre, updatedCadre))
                           .Returns(Task.CompletedTask);

            // Act
            var actionResult = await _controller.PutCadre(1, updatedCadre);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }

        // DELETE: api/Cadres/5
        [TestMethod]
        public async Task DeleteCadre_UnknownId_ReturnsNotFound()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(99))
                           .ReturnsAsync((Cadre)null);

            // Act
            var actionResult = await _controller.DeleteCadre(99);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteCadre_ValidId_ReturnsNoContent()
        {
            // Arrange
            var existingCadre = new Cadre { IdMateriau = 1, NomMat = "Aluminium", FormeCadre = "Wave" };

            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync(existingCadre);

            _mockRepository.Setup(repo => repo.DeleteAsync(existingCadre))
                           .Returns(Task.CompletedTask);

            // Act
            var actionResult = await _controller.DeleteCadre(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }
    }
}