using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using S401A2.Controllers;
using S401A2.Model.EntityFramework;
using S401A2.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S401A2.Controllers.Tests
{
    [TestClass()]
    public class LignePaniersControllerTests
    {
        private Mock<IDataRepository<LignePanier>> _mockRepository;
        private LignePaniersController _controller;

        // Initialize runs before every single test, ensuring a fresh setup
        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IDataRepository<LignePanier>>();
            _controller = new LignePaniersController(_mockRepository.Object);
        }

        // GET: api/LignePaniers
        [TestMethod]
        public async Task GetLignePaniers_ReturnsOkWithAllLignePaniers()
        {
            // Arrange
            var mockLignePaniers = new List<LignePanier>
            {
                new LignePanier 
                {  
                    Id = 1,
                    ArticleId = 1,
                    ClientId = 1,
                    CommandeId = 1,
                    CouleurId = 1,
                    TailleId = 1,
                    QtePanier = 2
                },
                new LignePanier
                {
                    Id = 2,
                    ArticleId = 1,
                    ClientId = 1,
                    CommandeId = 1,
                    CouleurId = 1,
                    TailleId = 1,
                    QtePanier = 2
                }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync())
                           .ReturnsAsync(mockLignePaniers);

            // Act
            var actionResult = await _controller.GetLignePaniers();

            // Assert
            Assert.IsNotNull(actionResult);
            var resultList = actionResult.ToList();
            Assert.AreEqual(2, resultList.Count);
            Assert.AreEqual(2, resultList[0].QtePanier);
        }

        // GET: api/LignePaniers/1
        [TestMethod]
        public async Task GetLignePanier_ExistingId_ReturnsArticle()
        {
            // Arrange
            var mockLignePanier = new LignePanier
            {
                Id = 1,
                ArticleId = 1,
                ClientId = 1,
                CommandeId = 1,
                CouleurId = 1,
                TailleId = 1,
                QtePanier = 2
            };
            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync(mockLignePanier);

            // Act
            var actionResult = await _controller.GetLignePanier(1);

            // Assert
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(mockLignePanier.QtePanier, actionResult.Value.QtePanier);
        }

        [TestMethod]
        public async Task GetLignePanier_UnknownId_ReturnsNotFound()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(99))
                           .ReturnsAsync((LignePanier)null);

            // Act
            var actionResult = await _controller.GetLignePanier(99);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Should return NotFound for unknown ID");
        }

        // POST: api/LignePaniers
        [TestMethod]
        public async Task PostLignePanier_ValidModel_ReturnsCreatedAtAction()
        {
            // Arrange
            var newLignePanier = new LignePanier
            {
                Id = 1,
                ArticleId = 1,
                ClientId = 1,
                CommandeId = 1,
                CouleurId = 1,
                TailleId = 1,
                QtePanier = 2
            };

            // We simulate that AddAsync executes successfully (it returns Task, so we just setup a completed task)
            _mockRepository.Setup(repo => repo.AddAsync(newLignePanier))
                           .Returns(Task.CompletedTask);

            // Act
            var actionResult = await _controller.PostLignePanier(newLignePanier);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult));
            var createdResult = actionResult.Result as CreatedAtActionResult;
            Assert.AreEqual("GetLignePanier", createdResult.ActionName);
            Assert.AreEqual(1, ((LignePanier)createdResult.Value).Id);
        }

        [TestMethod]
        public async Task PostLignePanier_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var newLignePanier = new LignePanier
            {
                Id = 1,
                ArticleId = 1,
                ClientId = 1,
                CommandeId = 1,
                CouleurId = 1,
                TailleId = 1,
            };
            _controller.ModelState.AddModelError("Quantity", "Quantity is required");

            // Act
            var actionResult = await _controller.PostLignePanier(newLignePanier);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestObjectResult));
        }

        // PUT: api/LignePaniers/1
        [TestMethod]
        public async Task PutLignePanier_IdMismatch_ReturnsBadRequest()
        {
            // Arrange
            var LignePanierModifie = new LignePanier
            {
                Id = 2,
                ArticleId = 1,
                ClientId = 1,
                CommandeId = 1,
                CouleurId = 1,
                TailleId = 1,
                QtePanier = 2
            };

            // Act
            // Passing ID 1 in the URL, but the object has ID 2
            var actionResult = await _controller.PutLignePanier(1, LignePanierModifie);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task PutLignePanier_UnknownId_ReturnsNotFound()
        {
            // Arrange
            var LignePanierModifie = new LignePanier
            {
                Id = 1,
                ArticleId = 1,
                ClientId = 1,
                CommandeId = 1,
                CouleurId = 1,
                TailleId = 1,
                QtePanier = 2
            };
            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync((LignePanier)null);

            // Act
            var actionResult = await _controller.PutLignePanier(1, LignePanierModifie);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PutLignePanier_ValidUpdate_ReturnsNoContent()
        {
            // Arrange
            var existingLignePanier = new LignePanier
            {
                Id = 1,
                ArticleId = 1,
                ClientId = 1,
                CommandeId = 1,
                CouleurId = 1,
                TailleId = 1,
                QtePanier = 2
            };
            var updatedLignePanier = new LignePanier
            {
                Id = 1,
                ArticleId = 1,
                ClientId = 1,
                CommandeId = 1,
                CouleurId = 1,
                TailleId = 1,
                QtePanier = 3
            };

            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync(existingLignePanier);

            _mockRepository.Setup(repo => repo.UpdateAsync(existingLignePanier, updatedLignePanier))
                           .Returns(Task.CompletedTask);

            // Act
            var actionResult = await _controller.PutLignePanier(1, updatedLignePanier);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }

        // DELETE: api/LignePaniers/5
        [TestMethod]
        public async Task DeleteArticle_UnknownId_ReturnsNotFound()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(99))
                           .ReturnsAsync((LignePanier)null);

            // Act
            var actionResult = await _controller.DeleteLignePanier(99);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteLignePanier_ValidId_ReturnsNoContent()
        {
            // Arrange
            var existingLignePanier = new LignePanier
            {
                Id = 1,
                ArticleId = 1,
                ClientId = 1,
                CommandeId = 1,
                CouleurId = 1,
                TailleId = 1,
                QtePanier = 2
            };

            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync(existingLignePanier);

            _mockRepository.Setup(repo => repo.DeleteAsync(existingLignePanier))
                           .Returns(Task.CompletedTask);

            // Act
            var actionResult = await _controller.DeleteLignePanier(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }
    }
}