using Microsoft.EntityFrameworkCore;
using S401A2.Model.EntityFramework;
using S401A2.Models.Repository;

namespace S401A2.Model.DataManager
{
    public class ImageManager : IDataRepository<Image>
    {
        private readonly CubeDBContext? _context;

        public ImageManager() { }

        public ImageManager(CubeDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Image?>> GetAllAsync()
        {
            if (_context == null)
            {
                return null;
            }

            return await _context.Set<Image>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Image?> GetByIdAsync(int id)
        {
            if (_context == null)
            {
                return null;
            }

            return await _context.Set<Image>()
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.ImageId == id);
        }

        public async Task AddAsync(Image entity)
        {
            if (_context != null)
            {
                await _context.Set<Image>().AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Database context is not available.");
            }
        }

        public async Task DeleteAsync(Image entity)
        {
            if (_context != null)
            {
                _context.Set<Image>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Database context is not available.");
            }
        }

        public Task UpdateAsync(Image entityToUpdate, Image entity)
        {
            if (_context != null)
            {
                entityToUpdate.Chemin = entity.Chemin;
                entityToUpdate.ArticleId = entity.ArticleId;

                _context.Set<Image>().Update(entityToUpdate);
                return _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Database context is not available.");
            }
        }
    }
}