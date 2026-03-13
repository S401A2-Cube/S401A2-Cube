using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S401A2.Model.EntityFramework;
using S401A2.Models.Repository;

namespace S401A2.Model.DataManager
{
    public class MotCleManager : IDataRepository<MotCle>
    {
        private readonly CubeDBContext? _context;
        public MotCleManager() { }

        public MotCleManager(CubeDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<MotCle?>> GetAllAsync()
        {
            if (_context == null)
            {
                return null;
            }

            var motCles = await _context.MotCles
                .AsNoTracking()
                .Select(c => new MotCle
                {
                    MotCleId = c.MotCleId,
                    Nom = c.Nom,
                    Articles = c.Articles.Select(a => new Article
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
                        CategorieArticle = a.CategorieArticle,
                        MotsCles = a.MotsCles.Select(mc => new MotCle
                        {
                            MotCleId = mc.MotCleId,
                            Nom = mc.Nom,
                            Articles = null
                        }).ToList()
                    }).ToList()
                })
                .ToListAsync();

            return motCles;
        }

        public async Task<MotCle?> GetByIdAsync(int id)
        {
            if (_context == null)
            {
                return null;
            }

            var motCle = await _context.MotCles
                .AsNoTracking()
                .Where(a => a.MotCleId == id)
                .Select(c => new MotCle
                {
                    MotCleId = c.MotCleId,
                    Nom = c.Nom,

                    Articles = c.Articles.Select(a => new Article
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
                        CategorieArticle = a.CategorieArticle,
                        MotsCles = a.MotsCles.Select(mc => new MotCle
                        {
                            MotCleId = mc.MotCleId,
                            Nom = mc.Nom,
                            Articles = null
                        }).ToList()
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (motCle == null)
            {
                return null;
            }

            return motCle;
        }

        public async Task AddAsync(MotCle entity)
        {
            if (_context != null)
            {
                await _context.MotCles.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Database context is not available.");
            }
        }

        public async Task DeleteAsync(MotCle entity)
        {
            if (_context != null)
            {
                _context.MotCles.Remove(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Database context is not available.");
            }
        }

        public Task UpdateAsync(MotCle entityToUpdate, MotCle entity)
        {
            if (_context != null)
            {
                entityToUpdate.Nom = entity.Nom;

                _context.MotCles.Update(entityToUpdate);
                return _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Database context is not available.");
            }
        }
    }
}
