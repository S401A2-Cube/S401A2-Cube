using Microsoft.EntityFrameworkCore;
using S401A2.Model.EntityFramework;
using S401A2.Models.Repository;

namespace S401A2.Model.DataManager
{
    public class AccessoireManager : IDataRepository<Accessoire>
    {
        private readonly CubeDBContext? _context;

        public AccessoireManager() { }

        public AccessoireManager(CubeDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Accessoire?>> GetAllAsync()
        {
            if (_context == null)
            {
                return null;
            }

            return await _context.Set<Accessoire>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Accessoire?> GetByIdAsync(int id)
        {
            if (_context == null)
            {
                return null;
            }

            return await _context.Set<Accessoire>()
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.ArticleId == id);
        }

        public async Task AddAsync(Accessoire entity)
        {
            if (_context != null)
            {
                await _context.Set<Accessoire>().AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Database context is not available.");
            }
        }

        public async Task DeleteAsync(Accessoire entity)
        {
            if (_context != null)
            {
                _context.Set<Accessoire>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Database context is not available.");
            }
        }

        public Task UpdateAsync(Accessoire entityToUpdate, Accessoire entity)
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

                entityToUpdate.TypeAccessoire = entity.TypeAccessoire;
                entityToUpdate.TailleUnique = entity.TailleUnique;
                entityToUpdate.Materiaux = entity.Materiaux;
                entityToUpdate.Dimensions = entity.Dimensions;
                entityToUpdate.Caracteristiques = entity.Caracteristiques;

                _context.Set<Accessoire>().Update(entityToUpdate);
                return _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Database context is not available.");
            }
        }
    }
}