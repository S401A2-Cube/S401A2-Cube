using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S401A2.Model.EntityFramework;
using S401A2.Models.Repository;

namespace S401A2.Model.DataManager
{
    public class CategorieManager : IDataRepository<Categorie>
    {
        private readonly CubeDBContext? _context;
        public CategorieManager() { }

        public CategorieManager(CubeDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ActionResult<IEnumerable<Categorie>>> GetAllAsync()
        {
            if (_context == null)
            {
                return new NotFoundResult();
            }

            return await _context.Categories.ToListAsync();
        }

        public async Task<ActionResult<Categorie>> GetByIdAsync(int id)
        {
            if (_context == null)
            {
                return new NotFoundResult();
            }

            var velo = await _context.Categories.FirstOrDefaultAsync(u => u.CategorieId == id);

            if (velo == null)
            {
                return new NotFoundResult();
            }

            return velo;
        }

        public async Task AddAsync(Categorie entity)
        {
            if (_context != null)
            {
                await _context.Categories.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Database context is not available.");
            }
        }

        public async Task DeleteAsync(Categorie entity)
        {
            if (_context != null)
            {
                _context.Categories.Remove(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Database context is not available.");
            }
        }

        public Task UpdateAsync(Categorie entityToUpdate, Categorie entity)
        {
            if (_context != null)
            {
                entityToUpdate.Nom = entity.Nom;

                _context.Categories.Update(entityToUpdate);
                return _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Database context is not available.");
            }
        }
    }
}
