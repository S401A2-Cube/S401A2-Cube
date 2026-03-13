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
    public class CategoriesControllerTests
    {
        private Mock<IDataRepository<Categorie>> _mockRepository;
        private CategoriesController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IDataRepository<Categorie>>();
            _controller = new CategoriesController(_mockRepository.Object);
        }

        // GET: api/Categories
        [TestMethod]
        public async Task GetCategories_ReturnsAllCategories()
        {
            // Arrange
            var mockCategories = new List<Categorie>
            {
                new Categorie { CategorieId = 1, Nom = "VTT Électriques" },
                new Categorie { CategorieId = 2, Nom = "Vélos de Route" }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync())
                           .ReturnsAsync(mockCategories);

            // Act
            var result = await _controller.GetCategories();

            // Assert
            Assert.IsNotNull(result);
            var resultList = result.ToList();
            Assert.AreEqual(2, resultList.Count);
            Assert.AreEqual("VTT Électriques", resultList[0].Nom);
        }

        // GET: api/Categories/5
        [TestMethod]
        public async Task GetCategorie_ExistingId_ReturnsCategorie()
        {
            // Arrange
            var mockCategorie = new Categorie { CategorieId = 1, Nom = "Gravel Bikes" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync(mockCategorie);

            // Act
            var actionResult = await _controller.GetCategorie(1);

            // Assert
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(mockCategorie.Nom, actionResult.Value.Nom);
        }

        [TestMethod]
        public async Task GetCategorie_UnknownId_ReturnsNotFound()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(99))
                           .ReturnsAsync((Categorie)null);

            // Act
            var actionResult = await _controller.GetCategorie(99);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Should return NotFound for an unknown ID");
        }

        // POST: api/Categories
        [TestMethod]
        public async Task PostCategorie_ValidModel_ReturnsCreatedAtAction()
        {
            // Arrange
            var newCategorie = new Categorie { CategorieId = 5, Nom = "Vélos Urbains" };

            _mockRepository.Setup(repo => repo.AddAsync(newCategorie))
                           .Returns(Task.CompletedTask);

            // Act
            var actionResult = await _controller.PostCategorie(newCategorie);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult));
            var createdResult = actionResult.Result as CreatedAtActionResult;
            Assert.AreEqual("GetCategorie", createdResult.ActionName);
            Assert.AreEqual(5, ((Categorie)createdResult.Value).CategorieId);
        }

        [TestMethod]
        public async Task PostCategorie_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var newCategorie = new Categorie { CategorieId = 6 }; // Missing required Nom
            _controller.ModelState.AddModelError("Nom", "Le nom est requis");

            // Act
            var actionResult = await _controller.PostCategorie(newCategorie);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestObjectResult));
        }

        // PUT: api/Categories/5
        [TestMethod]
        public async Task PutCategorie_IdMismatch_ReturnsBadRequest()
        {
            // Arrange
            var categorieModifiee = new Categorie { CategorieId = 2, Nom = "Équipement Cycliste" };

            // Act
            var actionResult = await _controller.PutCategorie(1, categorieModifiee);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task PutCategorie_UnknownId_ReturnsNotFound()
        {
            // Arrange
            var categorieModifiee = new Categorie { CategorieId = 1, Nom = "Vélos Enfant" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync((Categorie)null);

            // Act
            var actionResult = await _controller.PutCategorie(1, categorieModifiee);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PutCategorie_ValidUpdate_ReturnsNoContent()
        {
            // Arrange
            var existingCategorie = new Categorie { CategorieId = 1, Nom = "Ancienne Catégorie" };
            var updatedCategorie = new Categorie { CategorieId = 1, Nom = "Vélos de Trekking" };

            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync(existingCategorie);

            _mockRepository.Setup(repo => repo.UpdateAsync(existingCategorie, updatedCategorie))
                           .Returns(Task.CompletedTask);

            // Act
            var actionResult = await _controller.PutCategorie(1, updatedCategorie);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }

        // DELETE: api/Categories/5
        [TestMethod]
        public async Task DeleteCategorie_UnknownId_ReturnsNotFound()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(99))
                           .ReturnsAsync((Categorie)null);

            // Act
            var actionResult = await _controller.DeleteCategorie(99);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteCategorie_ValidId_ReturnsNoContent()
        {
            // Arrange
            var existingCategorie = new Categorie { CategorieId = 1, Nom = "Catégorie Obsolète" };

            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync(existingCategorie);

            _mockRepository.Setup(repo => repo.DeleteAsync(existingCategorie))
                           .Returns(Task.CompletedTask);

            // Act
            var actionResult = await _controller.DeleteCategorie(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }
    }
}