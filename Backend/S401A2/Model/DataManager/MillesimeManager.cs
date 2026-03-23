using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S401A2.Models.Repository;
using S401A2.Model.EntityFramework;
using APICube.Models.EntityFramework;

namespace S401A2.Model.DataManager
{
    public class MillesimeManager : IDataRepository<Millesime>
    {
        private readonly CubeDBContext? _context;
        public MillesimeManager() { }

        public MillesimeManager(CubeDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Millesime>> GetAllAsync()
        {
            if (_context == null) return null;

            return await _context.Millesimes
                .AsNoTracking()
                .Select(m => new Millesime
                {
                    IdMillesime = m.IdMillesime,
                    Annee = m.Annee,
                    Description = m.Description,
                    Velos = null 
                })
                .ToListAsync();
        }

        public async Task<Millesime> GetByIdAsync(int id)
        {
            if (_context == null) return null;

            var millesime = await _context.Millesimes
                .AsNoTracking()
                .Where(m => m.IdMillesime == id)
                .Select(m => new Millesime
                {
                    IdMillesime = m.IdMillesime,
                    Annee = m.Annee,
                    Description = m.Description,
                    Velos = null
                })
                .FirstOrDefaultAsync();

            if (millesime == null) return null;

            return millesime;
        }

        public async Task AddAsync(Millesime entity)
        {
            if (_context == null) throw new InvalidOperationException("Database context is not available.");
            await _context.Millesimes.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Millesime entityToUpdate, Millesime entity)
        {
            if (_context == null) throw new InvalidOperationException("Database context is not available.");
            entityToUpdate.Annee = entity.Annee;
            _context.Millesimes.Update(entityToUpdate);
            entityToUpdate.Description = entity.Description;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Millesime entity)
        {
            if (_context == null) throw new InvalidOperationException("Database context is not available.");
            _context.Millesimes.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}