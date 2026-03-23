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
    public class ModelesControllerTests
    {
        private Mock<IDataRepository<Modele>> _mockRepository;
        private ModelesController _controller;

        // Initialize runs before every single test, ensuring a fresh setup
        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IDataRepository<Modele>>();
            _controller = new ModelesController(_mockRepository.Object);
        }

        // GET: api/Modeles
        [TestMethod]
        public async Task GetModeles_ReturnsOkWithAllModeles()
        {
            // Arrange
            var mockModeles = new List<Modele>
            {
                new Modele { IdModele = 1, NomModele = "Z45" },
                new Modele { IdModele = 2, NomModele = "R5" }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync())
                           .ReturnsAsync(mockModeles);

            // Act
            var actionResult = await _controller.GetModeles();

            // Assert
            Assert.IsNotNull(actionResult);
            var resultList = actionResult.ToList();
            Assert.AreEqual(2, resultList.Count);
            Assert.AreEqual("Z45", resultList[0].NomModele);
        }

        // GET: api/Modeles/5
        [TestMethod]
        public async Task GetModele_ExistingId_ReturnsArticle()
        {
            // Arrange
            var mockModele = new Modele { IdModele = 1, NomModele = "Z45" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync(mockModele);

            // Act
            var actionResult = await _controller.GetModele(1);

            // Assert
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(mockModele.NomModele, actionResult.Value.NomModele);
        }

        [TestMethod]
        public async Task GetModele_UnknownId_ReturnsNotFound()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(99))
                           .ReturnsAsync((Modele)null);

            // Act
            var actionResult = await _controller.GetModele(99);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Should return NotFound for unknown ID");
        }

        // POST: api/Modeles
        [TestMethod]
        public async Task PostModele_ValidModel_ReturnsCreatedAtAction()
        {
            // Arrange
            var newModele = new Modele { IdModele = 1, NomModele = "Z45" };

            // We simulate that AddAsync executes successfully (it returns Task, so we just setup a completed task)
            _mockRepository.Setup(repo => repo.AddAsync(newModele))
                           .Returns(Task.CompletedTask);

            // Act
            var actionResult = await _controller.PostModele(newModele);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult));
            var createdResult = actionResult.Result as CreatedAtActionResult;
            Assert.AreEqual("GetModele", createdResult.ActionName);
            Assert.AreEqual(1, ((Modele)createdResult.Value).IdModele);
        }

        [TestMethod]
        public async Task PostModele_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var newModele = new Modele { IdModele = 1, NomModele = "Z45" };
            _controller.ModelState.AddModelError("NomModele", "NomModele is required");

            // Act
            var actionResult = await _controller.PostModele(newModele);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestObjectResult));
        }

        // PUT: api/Modeles/5
        [TestMethod]
        public async Task PutModlee_IdMismatch_ReturnsBadRequest()
        {
            // Arrange
            var modeleModifie = new Modele { IdModele = 2, NomModele = "Z45" };

            // Act
            // Passing ID 1 in the URL, but the object has ID 2
            var actionResult = await _controller.PutModele(1, modeleModifie);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task PutModele_UnknownId_ReturnsNotFound()
        {
            // Arrange
            var modeleModifie = new Modele { IdModele = 1, NomModele = "Z45" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync((Modele)null);

            // Act
            var actionResult = await _controller.PutModele(1, modeleModifie);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PutModele_ValidUpdate_ReturnsNoContent()
        {
            // Arrange
            var existingModele = new Modele { IdModele = 1, NomModele = "Z45" };
            var updatedModele = new Modele { IdModele = 1, NomModele = "Z455" };

            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync(existingModele);

            _mockRepository.Setup(repo => repo.UpdateAsync(existingModele, updatedModele))
                           .Returns(Task.CompletedTask);

            // Act
            var actionResult = await _controller.PutModele(1, updatedModele);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }

        // DELETE: api/Modeles/5
        [TestMethod]
        public async Task DeleteModele_UnknownId_ReturnsNotFound()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(99))
                           .ReturnsAsync((Modele)null);

            // Act
            var actionResult = await _controller.DeleteModele(99);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteModele_ValidId_ReturnsNoContent()
        {
            // Arrange
            var existingModele = new Modele { IdModele = 1, NomModele = "Z45" };

            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync(existingModele);

            _mockRepository.Setup(repo => repo.DeleteAsync(existingModele))
                           .Returns(Task.CompletedTask);

            // Act
            var actionResult = await _controller.DeleteModele(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }
    }
}