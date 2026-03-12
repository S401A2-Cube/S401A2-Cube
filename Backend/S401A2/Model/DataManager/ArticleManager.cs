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

            return await _context.Articles.ToListAsync();
        }

        public async Task<ActionResult<Article>> GetByIdAsync(int id)
        {
            if (_context == null)
            {
                return new NotFoundResult();
            }

            var velo = await _context.Articles.FirstOrDefaultAsync(u => u.ArticleId == id);

            if (velo == null)
            {
                return new NotFoundResult();
            }

            return velo;
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
