using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S401A2.Model.EntityFramework;   
using S401A2.Models.Repository;
using S401A2.Model.EntityFramework;

namespace S401A2.Model.DataManager
{
    public class TailleManager : IDataRepository<Taille>
    {
        private readonly CubeDBContext? _context;
        public TailleManager() { }

        public TailleManager(CubeDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ActionResult<IEnumerable<Taille>>> GetAllAsync()
        {
            if (_context == null)
            {
                return new NotFoundResult();
            }

            var Tailles = await _context.Tailles
                    .AsNoTracking()
                    .Select(t => new Taille
                    {
                        IdTaille = t.IdTaille, 
                        LibelleTaille = t.LibelleTaille,
                        Velos = null

                    })
                    .ToListAsync();

            return Tailles;
        }

        public async Task<ActionResult<Taille>> GetByIdAsync(int id)
        {
            if (_context == null)
            {
                return new NotFoundResult();
            }

            var Taille = await _context.Tailles
                .AsNoTracking()
                .Where(t => t.IdTaille == id) 
                .Select(t => new Taille 
                {
                    IdTaille = t.IdTaille,
                    LibelleTaille = t.LibelleTaille,
                    Velos = null

                })
                .FirstOrDefaultAsync();

            if (Taille == null)
            {
                return new NotFoundResult();
            }

            return Taille;
        }

        public async Task AddAsync(Taille entity)
        {
            if (_context != null)
            {
                await _context.Tailles.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Database context is not available.");
            }
        }

        public async Task DeleteAsync(Taille entity)
        {
            if (_context != null)
            {
                _context.Tailles.Remove(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Database context is not available.");
            }
        }

        public Task UpdateAsync(Taille entityToUpdate, Taille entity)
        {
            if (_context != null)
            {
                entityToUpdate.LibelleTaille = entity.LibelleTaille;   

                _context.Tailles.Update(entityToUpdate);
                return _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Database context is not available.");
            }
        }
    }
}