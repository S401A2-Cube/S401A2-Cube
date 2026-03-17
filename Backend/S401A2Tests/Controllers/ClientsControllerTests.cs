using APICube.Models.EntityFramework;
using Microsoft.AspNetCore.Identity;
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
    public class ClientsControllerTests
    {
        private Mock<IDataRepository<Client>> _mockRepository;
        private ClientsController _controller;

        // Initialize runs before every single test, ensuring a fresh setup
        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IDataRepository<Client>>();
            _controller = new ClientsController(_mockRepository.Object);
        }

        // GET: api/Articles
        [TestMethod]
        public async Task GetArticles_ReturnsOkWithAllArticles()
        {
            // Arrange
            var mockClient = new List<Client>
            {
                new Client { Id = 1, Nom = "Personeni", Prenom = "Nathan", Email="nathan.personeni@etu.univ-smb.fr", Mdp="123456789", DateNaissance= (new DateTime(2006,02,20)), Role=1 },
                new Client { Id = 2, Nom = "Coltelli", Prenom = "Raphael", Email="raphael.coltelli@etu.univ-smb.fr", Mdp="123456789", DateNaissance= (new DateTime(2006,05,15)),Role=2 },
            };

            _mockRepository.Setup(repo => repo.GetAllAsync())
                           .ReturnsAsync(mockClient);

            // Act
            var actionResult = await _controller.GetClients();

            // Assert
            Assert.IsNotNull(actionResult);

            // 1. On extrait le résultat HTTP (la réponse "Ok")
            var okResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okResult, "Le contrôleur devrait retourner un OkObjectResult");

            // 2. On extrait la valeur contenue dans le "Ok" et on la cast en IEnumerable
            var clientsCollection = okResult.Value as IEnumerable<Client>;
            Assert.IsNotNull(clientsCollection, "La valeur retournée ne devrait pas être nulle");

            // 3. Maintenant, on peut faire le ToList() !
            var resultList = clientsCollection.ToList();

            Assert.AreEqual(2, resultList.Count);

            // ⚠️ ATTENTION : J'ai corrigé cette ligne. 
            // Dans ton mock, le nom du premier client est "Personeni", pas "TWO15".
            Assert.AreEqual("Personeni", resultList[0].Nom);
        }

        // GET: api/Articles/5
        [TestMethod]
        public async Task GetArticle_ExistingId_ReturnsArticle()
        {
            // Arrange
            var mockClient = new Client { Id = 1, Nom = "Personeni", Prenom = "Nathan", Email = "nathan.personeni@etu.univ-smb.fr", Mdp = "123456789", DateNaissance = (new DateTime(2006, 02, 20)), Role = 1 };
            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync(mockClient);

            // Act
            var actionResult = await _controller.GetClient(1);

            // Assert
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(mockClient.Nom, actionResult.Value.Nom);
        }

        [TestMethod]
        public async Task GetArticle_UnknownId_ReturnsNotFound()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(99))
                           .ReturnsAsync((Client)null);

            // Act
            var actionResult = await _controller.GetClient(99);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Should return NotFound for unknown ID");
        }

        // POST: api/Articles
        [TestMethod]
        public async Task PostArticle_ValidModel_ReturnsCreatedAtAction()
        {
            // Arrange
            var newClient = new Client { Id = 1, Nom = "Personeni", Prenom = "Nathan", Email = "nathan.personeni@etu.univ-smb.fr", Mdp = "123456789", DateNaissance = (new DateTime(2006, 02, 20)), Role = 1 };

            // We simulate that AddAsync executes successfully (it returns Task, so we just setup a completed task)
            _mockRepository.Setup(repo => repo.AddAsync(newClient))
                           .Returns(Task.CompletedTask);

            // Act
            var actionResult = await _controller.PostClient(newClient);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult));
            var createdResult = actionResult.Result as CreatedAtActionResult;
            Assert.AreEqual("GetClient", createdResult.ActionName);
            Assert.AreEqual(1, ((Client)createdResult.Value).Id);
        }

        [TestMethod]
        public async Task PostArticle_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var newClient = new Client { Nom = "Incomplete name" };
            _controller.ModelState.AddModelError("Reference", "Reference is required");

            // Act
            var actionResult = await _controller.PostClient(newClient);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestObjectResult));
        }

        // PUT: api/Articles/5
        [TestMethod]
        public async Task PutArticle_IdMismatch_ReturnsBadRequest()
        {
            // Arrange
            var clientModifie = new Client { Id = 2, Nom = "Personeni", Prenom = "Nathan", Email = "nathan.personeni@etu.univ-smb.fr", Mdp = "123456789", DateNaissance = (new DateTime(2006, 02, 20)), Role = 1 };

            // Act
            // Passing ID 1 in the URL, but the object has ID 2
            var actionResult = await _controller.PutClient(1, clientModifie);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task PutArticle_UnknownId_ReturnsNotFound()
        {
            // Arrange
            var clientModifie = new Client { Id = 1, Nom = "Personeni", Prenom = "Nathan", Email = "nathan.personeni@etu.univ-smb.fr", Mdp = "123456789", DateNaissance = (new DateTime(2006, 02, 20)), Role = 1 };
            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync((Client)null);

            // Act
            var actionResult = await _controller.PutClient(1, clientModifie);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PutArticle_ValidUpdate_ReturnsNoContent()
        {
            // Arrange
            var existingClient = new Client { Id = 1, Nom = "Personeni", Prenom = "Nathan", Email = "nathan.personeni@etu.univ-smb.fr", Mdp = "123456789", DateNaissance = (new DateTime(2006, 02, 20)), Role = 1 };
            var updatedClient = new Client { Id = 1, Nom = "Personeni", Prenom = "Raphael", Email = "nathan.personeni@etu.univ-smb.fr", Mdp = "123456789", DateNaissance = (new DateTime(2006, 02, 20)), Role = 1 };

            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync(existingClient);

            _mockRepository.Setup(repo => repo.UpdateAsync(existingClient, updatedClient))
                           .Returns(Task.CompletedTask);

            // Act
            var actionResult = await _controller.PutClient(1, updatedClient);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }

        // DELETE: api/Articles/5
        [TestMethod]
        public async Task DeleteArticle_UnknownId_ReturnsNotFound()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(99))
                           .ReturnsAsync((Client)null);

            // Act
            var actionResult = await _controller.DeleteClient(99);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteArticle_ValidId_ReturnsNoContent()
        {
            // Arrange
            var existingClient = new Client { Id = 1, Nom = "Personeni", Prenom = "Nathan", Email = "nathan.personeni@etu.univ-smb.fr", Mdp = "123456789", DateNaissance = (new DateTime(2006, 02, 20)), Role = 1 };

            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                           .ReturnsAsync(existingClient);

            _mockRepository.Setup(repo => repo.DeleteAsync(existingClient))
                           .Returns(Task.CompletedTask);

            // Act
            var actionResult = await _controller.DeleteClient(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }
    }
}