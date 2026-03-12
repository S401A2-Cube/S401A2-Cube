using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S401A2.Model.EntityFramework;
using S401A2.Models.Repository;

namespace S401A2.Model.DataManager
{
    public class ArticleManager : IDataRepository<Article>
    {
        private readonly CubeDBContext? _context;
        public ArticleManager() { }

        public ArticleManager(CubeDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ActionResult<IEnumerable<Article>>> GetAllAsync()
        {
            if (_context == null)
            {
                return new NotFoundResult();
            }

            var articles = await _context.Articles
                    .AsNoTracking()
                    .Select(a => new Article
                    {
                        ArticleId = a.ArticleId,
                        Reference = a.Reference,
                        Nom = a.Nom,
                        Description = a.Description,
                        Prix = a.Prix,
                        Poids = a.Poids,
                        QteStock = a.QteStock,
                        Annee = a.Annee,
                        DispoEnLigne = a.DispoEnLigne,
                        CategorieId = a.CategorieId,

                        CategorieArticle = a.CategorieArticle == null ? null : new Categorie
                        {
                            CategorieId = a.CategorieArticle.CategorieId,
                            Nom = a.CategorieArticle.Nom,
                            Articles = null
                        },

                        MotsCles = a.MotsCles == null ? null : a.MotsCles.Select(mc => new MotCle
                        {
                            MotCleId = mc.MotCleId,
                            Nom = mc.Nom,
                            Articles = null
                        }).ToList()
                    })
                    .ToListAsync();

            return articles;
        }

        public async Task<ActionResult<Article>> GetByIdAsync(int id)
        {
            if (_context == null)
            {
                return new NotFoundResult();
            }

            var article = await _context.Articles
                .AsNoTracking()
                .Where(a => a.ArticleId == id)
                .Select(a => new Article
                {
                    ArticleId = a.ArticleId,
                    Reference = a.Reference,
                    Nom = a.Nom,
                    Description = a.Description,
                    Prix = a.Prix,
                    Poids = a.Poids,
                    QteStock = a.QteStock,
                    Annee = a.Annee,
                    DispoEnLigne = a.DispoEnLigne,
                    CategorieId = a.CategorieId,

                    CategorieArticle = a.CategorieArticle == null ? null : new Categorie
                    {
                        CategorieId = a.CategorieArticle.CategorieId,
                        Nom = a.CategorieArticle.Nom,
                        Articles = null
                    },

                    MotsCles = a.MotsCles == null ? null : a.MotsCles.Select(mc => new MotCle
                    {
                        MotCleId = mc.MotCleId,
                        Nom = mc.Nom,
                        Articles = null
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (article == null)
            {
                return new NotFoundResult();
            }

            return article;
        }

        public async Task AddAsync(Article entity)
        {
            if (_context != null)
            {
                await _context.Articles.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Database context is not available.");
            }
        }

        public async Task DeleteAsync(Article entity)
        {
            if (_context != null)
            {
                _context.Articles.Remove(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Database context is not available.");
            }
        }

        public Task UpdateAsync(Article entityToUpdate, Article entity)
        {
            if (_context != null)
            {
                entityToUpdate.Nom = entity.Nom;
                entityToUpdate.Description = entity.Description;
                entityToUpdate.Prix = entity.Prix;
                entityToUpdate.Poids = entity.Poids;
                entityToUpdate.QteStock = entity.QteStock;
                entityToUpdate.Annee = entity.Annee;
                entityToUpdate.DispoEnLigne = entity.DispoEnLigne;
                // TODO: dont forget to update upcoming classes (categorie, model3d, ...)

                _context.Articles.Update(entityToUpdate);
                return _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Database context is not available.");
            }
        }
    }
}
