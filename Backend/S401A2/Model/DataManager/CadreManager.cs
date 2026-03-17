using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S401A2.Models.Repository;
using S401A2.Model.EntityFramework;

namespace S401A2.Model.DataManager
{
    public class CadreManager : IDataRepository<Cadre>
    {
        private readonly CubeDBContext? _context;
        public CadreManager() { }

        public CadreManager(CubeDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ActionResult<IEnumerable<Cadre>>> GetAllAsync()
        {
            if (_context == null) return new NotFoundResult();

            return await _context.Cadres
                .AsNoTracking()
                .Select(m => new Cadre
                {
                    IdMateriau = m.IdMateriau,
                    NomMat = m.NomMat,
                    FormeCadre = m.FormeCadre,
                    Velos = null 
                })
                .ToListAsync();
        }

        public async Task<ActionResult<Cadre>> GetByIdAsync(int id)
        {
            if (_context == null) return new NotFoundResult();

            var cadre = await _context.Cadres
                .AsNoTracking()
                .Where(m => m.IdMateriau == id)
                .Select(m => new Cadre
                {
                    IdMateriau = m.IdMateriau,
                    NomMat = m.NomMat,
                    FormeCadre = m.FormeCadre,
                    Velos = null
                })
                .FirstOrDefaultAsync();

            if (cadre == null) return new NotFoundResult();

            return cadre;
        }

        public async Task AddAsync(Cadre entity)
        {
            if (_context == null) throw new InvalidOperationException("Database context is not available.");
            await _context.Cadres.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cadre entityToUpdate, Cadre entity)
        {
            if (_context == null) throw new InvalidOperationException("Database context is not available.");
            entityToUpdate.NomMat = entity.NomMat;
            _context.Cadres.Update(entityToUpdate);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Cadre entity)
        {
            if (_context == null) throw new InvalidOperationException("Database context is not available.");
            _context.Cadres.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}