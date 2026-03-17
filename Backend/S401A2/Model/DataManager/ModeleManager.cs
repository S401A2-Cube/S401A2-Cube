using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S401A2.Models.Repository;
using S401A2.Model.EntityFramework;

namespace S401A2.Model.DataManager
{
    public class ModeleManager : IDataRepository<Modele>
    {
        private readonly CubeDBContext? _context;
        public ModeleManager() { }

        public ModeleManager(CubeDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Modele>> GetAllAsync()
        {
            if (_context == null) return null;

            return await _context.Modeles
                .AsNoTracking()
                .Select(m => new Modele
                {
                    IdModele = m.IdModele,
                    NomModele = m.NomModele,
                    Velos = null 
                })
                .ToListAsync();
        }

        public async Task<Modele> GetByIdAsync(int id)
        {
            if (_context == null) return null;

            var modele = await _context.Modeles
                .AsNoTracking()
                .Where(m => m.IdModele == id)
                .Select(m => new Modele
                {
                    IdModele = m.IdModele,
                    NomModele = m.NomModele,
                    Velos = null
                })
                .FirstOrDefaultAsync();

            if (modele == null) return null;

            return modele;
        }

        public async Task AddAsync(Modele entity)
        {
            if (_context == null) throw new InvalidOperationException("Database context is not available.");
            await _context.Modeles.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Modele entityToUpdate, Modele entity)
        {
            if (_context == null) throw new InvalidOperationException("Database context is not available.");
            entityToUpdate.NomModele = entity.NomModele;
            _context.Modeles.Update(entityToUpdate);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Modele entity)
        {
            if (_context == null) throw new InvalidOperationException("Database context is not available.");
            _context.Modeles.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}