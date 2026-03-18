//using Microsoft.AspNetCore.Mvc;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using S401A2.Controllers;
//using S401A2.Model.EntityFramework;
//using S401A2.Models.Repository;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace S401A2.Controllers.Tests
//{
//    [TestClass]
//    public class GeometriesControllerTests
//    {
//        private Mock<IDataRepository<Geometrie>> _mockRepository;
//        private GeometriesController _controller;

//        // Initialize runs before every single test, ensuring a fresh setup
//        [TestInitialize]
//        public void Setup()
//        {
//            _mockRepository = new Mock<IDataRepository<Geometrie>>();
//            _controller = new GeometriesController(_mockRepository.Object);
//        }

//        // GET: api/Articles
//        [TestMethod]
//        public async Task GetGeometries_ReturnsOkWithAllArticles()
//        {
//            // Arrange
//            var mockGeometries = new List<Geometrie>
//            {
//                new Geometrie { IdGeometrie = 1, NomPiece = "Selle" ,TaillePiece = 12 },
//                new Geometrie { IdGeometrie = 2, NomPiece = "Guidon" ,TaillePiece = 40 }
//            };

//            _mockRepository.Setup(repo => repo.GetAllAsync())
//                           .ReturnsAsync(mockGeometries);

//            // Act
//            var actionResult = await _controller.GetGeometrie();

//            // Assert
//            Assert.IsNotNull(actionResult);
//            var resultList = actionResult.ToList();
//            Assert.AreEqual(2, resultList.Count);
//            Assert.AreEqual("Selle", resultList[0].);
//        }

//        // GET: api/Articles/5
//        [TestMethod]
//        public async Task GetArticle_ExistingId_ReturnsArticle()
//        {
//            // Arrange
//            var mockArticle = new Article { ArticleId = 1, Nom = "TWO15", Reference = "TWO15RACE" };
//            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
//                           .ReturnsAsync(mockArticle);

//            // Act
//            var actionResult = await _controller.GetArticle(1);

//            // Assert
//            Assert.IsNotNull(actionResult.Value);
//            Assert.AreEqual(mockArticle.Nom, actionResult.Value.Nom);
//        }

//        [TestMethod]
//        public async Task GetArticle_UnknownId_ReturnsNotFound()
//        {
//            // Arrange
//            _mockRepository.Setup(repo => repo.GetByIdAsync(99))
//                           .ReturnsAsync((Article)null);

//            // Act
//            var actionResult = await _controller.GetArticle(99);

//            // Assert
//            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Should return NotFound for unknown ID");
//        }

//        // POST: api/Articles
//        [TestMethod]
//        public async Task PostArticle_ValidModel_ReturnsCreatedAtAction()
//        {
//            // Arrange
//            var newArticle = new Article { ArticleId = 10, Nom = "TWO18", Reference = "TWO16PRO" };

//            // We simulate that AddAsync executes successfully (it returns Task, so we just setup a completed task)
//            _mockRepository.Setup(repo => repo.AddAsync(newArticle))
//                           .Returns(Task.CompletedTask);

//            // Act
//            var actionResult = await _controller.PostArticle(newArticle);

//            // Assert
//            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult));
//            var createdResult = actionResult.Result as CreatedAtActionResult;
//            Assert.AreEqual("GetArticle", createdResult.ActionName);
//            Assert.AreEqual(10, ((Article)createdResult.Value).ArticleId);
//        }

//        [TestMethod]
//        public async Task PostArticle_InvalidModel_ReturnsBadRequest()
//        {
//            // Arrange
//            var newArticle = new Article { Nom = "Incomplete bike" };
//            _controller.ModelState.AddModelError("Reference", "Reference is required");

//            // Act
//            var actionResult = await _controller.PostArticle(newArticle);

//            // Assert
//            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestObjectResult));
//        }

//        // PUT: api/Articles/5
//        [TestMethod]
//        public async Task PutArticle_IdMismatch_ReturnsBadRequest()
//        {
//            // Arrange
//            var articleModifie = new Article { ArticleId = 2, Nom = "TWO15" };

//            // Act
//            // Passing ID 1 in the URL, but the object has ID 2
//            var actionResult = await _controller.PutArticle(1, articleModifie);

//            // Assert
//            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
//        }

//        [TestMethod]
//        public async Task PutArticle_UnknownId_ReturnsNotFound()
//        {
//            // Arrange
//            var articleModifie = new Article { ArticleId = 1, Nom = "TWO15" };
//            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
//                           .ReturnsAsync((Article)null);

//            // Act
//            var actionResult = await _controller.PutArticle(1, articleModifie);

//            // Assert
//            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
//        }

//        [TestMethod]
//        public async Task PutArticle_ValidUpdate_ReturnsNoContent()
//        {
//            // Arrange
//            var existingArticle = new Article { ArticleId = 1, Nom = "Old Name" };
//            var updatedArticle = new Article { ArticleId = 1, Nom = "New Name" };

//            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
//                           .ReturnsAsync(existingArticle);

//            _mockRepository.Setup(repo => repo.UpdateAsync(existingArticle, updatedArticle))
//                           .Returns(Task.CompletedTask);

//            // Act
//            var actionResult = await _controller.PutArticle(1, updatedArticle);

//            // Assert
//            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
//        }

//        // DELETE: api/Articles/5
//        [TestMethod]
//        public async Task DeleteArticle_UnknownId_ReturnsNotFound()
//        {
//            // Arrange
//            _mockRepository.Setup(repo => repo.GetByIdAsync(99))
//                           .ReturnsAsync((Article)null);

//            // Act
//            var actionResult = await _controller.DeleteArticle(99);

//            // Assert
//            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
//        }

//        [TestMethod]
//        public async Task DeleteArticle_ValidId_ReturnsNoContent()
//        {
//            // Arrange
//            var existingArticle = new Article { ArticleId = 1, Nom = "To Delete" };

//            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
//                           .ReturnsAsync(existingArticle);

//            _mockRepository.Setup(repo => repo.DeleteAsync(existingArticle))
//                           .Returns(Task.CompletedTask);

//            // Act
//            var actionResult = await _controller.DeleteArticle(1);

//            // Assert
//            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
//        }
//    }
//}