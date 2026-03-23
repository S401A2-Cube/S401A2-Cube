using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S401A2.Models.Repository;
using S401A2.Model.EntityFramework;

namespace S401A2.Model.DataManager
{
    public class CouleurManager : IDataRepository<Couleur>
    {
        private readonly CubeDBContext? _context;
        public CouleurManager() { }

        public CouleurManager(CubeDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Couleur>> GetAllAsync()
        {
            if (_context == null) return null;

            return await _context.Couleurs
                .AsNoTracking()
                .Select(c => new Couleur
                {
                    IdCouleur = c.IdCouleur,
                    NomCouleur = c.NomCouleur,
                    EffetPeinture = c.EffetPeinture,
                    Velos = null 
                })
                .ToListAsync();
        }

        public async Task<Couleur> GetByIdAsync(int id)
        {
            if (_context == null) return null;

            var couleur = await _context.Couleurs
                .AsNoTracking()
                .Where(c => c.IdCouleur == id)
                .Select(c => new Couleur
                {
                    IdCouleur = c.IdCouleur,
                    NomCouleur = c.NomCouleur,
                    EffetPeinture = c.EffetPeinture,
                    Velos = null
                })
                .FirstOrDefaultAsync();

            if (couleur == null) return null;

            return couleur;
        }

        public async Task AddAsync(Couleur entity)
        {
            if (_context == null) throw new InvalidOperationException("Database context is not available.");
            await _context.Couleurs.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Couleur entityToUpdate, Couleur entity)
        {
            if (_context == null) throw new InvalidOperationException("Database context is not available.");
            entityToUpdate.NomCouleur = entity.NomCouleur;
            entityToUpdate.EffetPeinture = entity.EffetPeinture;
            _context.Couleurs.Update(entityToUpdate);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Couleur entity)
        {
            if (_context == null) throw new InvalidOperationException("Database context is not available.");
            _context.Couleurs.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}