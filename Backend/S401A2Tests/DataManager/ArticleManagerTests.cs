using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using S401A2.Model.DataManager;
using S401A2.Model.EntityFramework;
using S401A2.Models.Repository;
using System;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Humanizer;
using APICube.Models.EntityFramework;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace S401A2Tests.DataManager
{
    [TestClass]
    public class ArticleManagerTests
    {
        private CubeDBContext _context;
        private ArticleManager _manager;
        private SqliteConnection _connection;

        [TestInitialize]
        public void Setup()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<CubeDBContext>()
                .UseSqlite(_connection)
                .Options;

            _context = new CubeDBContext(options);

            _context.Database.EnsureCreated();

            //INITIALISATION
            _context.Add(new Categorie { CategorieId = 1, Nom = "VTT" });
            _context.SaveChanges();
            _manager = new ArticleManager(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
            _connection.Close();
        }

        // GET: api/Articles
        [TestMethod]
        public async Task GetAllAsync_ReturnAllArticles()
        {
            //ARANGE
            _context.Articles.Add(new Article { ArticleId = 1, Reference = "X200", Nom = "VTT Fox", Description = "Velo tout terrain de la marque FOX", Prix = 2500, Poids = 3, QteStock = 3, Annee = 2024, DispoEnLigne = true, CategorieId = 1 });
            _context.Articles.Add(new Article { ArticleId = 2, Reference = "X220", Nom = "VTT Rossignol", Description = "Velo tout terrain de la marque Rossignol", Prix = 2000, Poids = 4, QteStock = 5, Annee = 2020, DispoEnLigne = true, CategorieId = 1 });
            await _context.SaveChangesAsync();

            //ACT
            var result = await _manager.GetAllAsync();

            //ASSERT
            Assert.IsNotNull(result);
            var resultList = result.ToList();
            Assert.AreEqual(2, resultList.Count);
            Assert.AreEqual("VTT Fox", resultList[0].Nom);
        }

        // GET: api/Articlesz/5
        [TestMethod]
        public async Task GetArticles_ExistingId_ReturnCadre()
        {
            //ARANGE
            var newArticle = new Article { ArticleId = 1, Reference = "X200", Nom = "VTT Fox", Description = "Velo tout terrain de la marque FOX", Prix = 2500, Poids = 3, QteStock = 3, Annee = 2024, DispoEnLigne = true, CategorieId = 1 };
            _context.Articles.Add(newArticle);
            await _context.SaveChangesAsync();
            var id = newArticle.ArticleId;
            //ACT
            var result = await _manager.GetByIdAsync(id);

            //ASSERT
            Assert.AreEqual("VTT Fox", result.Nom);
        }

        [TestMethod]
        public async Task GetArticles_UnknownId_ReturnsNotFound()
        {
            // ARRANGE
            _context.Articles.Add(new Article { ArticleId = 1, Reference = "X200", Nom = "VTT Fox", Description = "Velo tout terrain de la marque FOX", Prix = 2500, Poids = 3, QteStock = 3, Annee = 2024, DispoEnLigne = true, CategorieId = 1 });
            await _context.SaveChangesAsync();


            // ACT
            var result = await _manager.GetByIdAsync(99);

            // ASSERT
            Assert.AreEqual(null, result);
        }

        // POST: api/Articles
        [TestMethod]
        public async Task PostArticles_ValidModel_ReturnsCreatedAtAction()
        {
            //ARANGE
            var newArticles = new Article { ArticleId = 1, Reference = "X200", Nom = "VTT Fox", Description = "Velo tout terrain de la marque FOX", Prix = 2500, Poids = 3, QteStock = 3, Annee = 2024, DispoEnLigne = true, CategorieId = 1 };

            //ACT
            await _manager.AddAsync(newArticles);

            //ASSERT
            var articleInDB = await _context.Articles.FindAsync(1);
            Assert.IsNotNull(articleInDB, "L'article devrait être présent dans la base de données.");
            Assert.AreEqual("VTT Fox", articleInDB.Nom);
        }

        [TestMethod]
        public async Task PostArticle_InvalidModel_ReturnsBadRequest()
        {
            //ARANGE
            var newArticle = new Article { ArticleId = 1, Nom = "VTT Fox", Description = "Velo tout terrain de la marque FOX", Prix = 2500, Poids = 3, QteStock = 3, Annee = 2024, DispoEnLigne = true, CategorieId = 1 };

            //ASSERT
            await Assert.ThrowsExceptionAsync<DbUpdateException>(async () => { await _manager.AddAsync(newArticle); });
        }

        // PUT: api/Velos/5
        [TestMethod]
        public async Task PutArticles_ValidUpdate_ReturnsNoContent()
        {
            //ARANGE
            var articleExisting = new Article { ArticleId = 1, Reference = "X200", Nom = "VTT Fox", Description = "Velo tout terrain de la marque FOX", Prix = 2500, Poids = 3, QteStock = 3, Annee = 2024, DispoEnLigne = true, CategorieId = 1 };
            var articleToUpdate = new Article { ArticleId = 1, Reference = "X200", Nom = "VTT Rossignol", Description = "Velo tout terrain de la marque Rossignol", Prix = 2500, Poids = 3, QteStock = 3, Annee = 2024, DispoEnLigne = true, CategorieId = 1 };
            _context.Articles.Add(articleToUpdate);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
            //ACT
            await _manager.UpdateAsync(articleToUpdate, articleExisting);

            //ASSERT
            _context.ChangeTracker.Clear();

            var newArticle = await _context.Articles.FindAsync(articleExisting.ArticleId);
            Assert.AreEqual("VTT Fox", newArticle.Nom);
        }

        [TestMethod]
        public async Task PutArticles_IdMismatch_ReturnsBadRequest()
        {
            // Arrange
            var articleExisting = new Article { ArticleId = 1, Reference = "X200", Nom = "VTT Fox", Description = "Velo tout terrain de la marque FOX", Prix = 2500, Poids = 3, QteStock = 3, Annee = 2024, DispoEnLigne = true, CategorieId = 1 };
            var articleToUpdate = new Article { ArticleId = 2, Reference = "X200", Nom = "VTT Rossignol", Description = "Velo tout terrain de la marque Rossignol", Prix = 2500, Poids = 3, QteStock = 3, Annee = 2024, DispoEnLigne = true, CategorieId = 1 };

            // Assert
            await Assert.ThrowsExceptionAsync<DbUpdateConcurrencyException>(async () => { await _manager.UpdateAsync(articleToUpdate, articleExisting); });
        }

        // DELETE: api/Articles/5
        [TestMethod]
        public async Task DeleteVelos_ValidId_ReturnsNoContent()
        {
            // Arrange
            var articleExisting = new Article { ArticleId = 1, Reference = "X200", Nom = "VTT Fox", Description = "Velo tout terrain de la marque FOX", Prix = 2500, Poids = 3, QteStock = 3, Annee = 2024, DispoEnLigne = true, CategorieId = 1 };
            _context.Articles.Add(articleExisting);
            await _context.SaveChangesAsync();

            // ACT
            await _manager.DeleteAsync(articleExisting);

            //  ASSERT
            var articles = await _manager.GetAllAsync();

            Assert.IsNotNull(articles);
        }
    }
}
