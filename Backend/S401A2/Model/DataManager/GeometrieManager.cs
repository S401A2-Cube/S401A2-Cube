using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S401A2.Models.Repository;
using S401A2.Model.EntityFramework;

namespace S401A2.Model.DataManager
{
    public class GeometrieManager : IDataRepository<Geometrie>
    {
        private readonly CubeDBContext? _context;
        public GeometrieManager() { }

        public GeometrieManager(CubeDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ActionResult<IEnumerable<Geometrie>>> GetAllAsync()
        {
            if (_context == null) return new NotFoundResult();

            return await _context.Geometries
                .AsNoTracking()
                .Select(g => new Geometrie
                {
                    IdGeometrie = g.IdGeometrie,
                    NomPiece = g.NomPiece,
                    TaillePiece = g.TaillePiece,
                    Velos = null 
                })
                .ToListAsync();
        }

        public async Task<ActionResult<Geometrie>> GetByIdAsync(int id)
        {
            if (_context == null) return new NotFoundResult();

            var geometrie = await _context.Geometries
                .AsNoTracking()
                .Where(g => g.IdGeometrie == id)
                .Select(g => new Geometrie
                {
                    IdGeometrie = g.IdGeometrie,
                    NomPiece = g.NomPiece,
                    TaillePiece = g.TaillePiece,
                    Velos = null
                })
                .FirstOrDefaultAsync();

            if (geometrie == null) return new NotFoundResult();

            return geometrie;
        }

        public async Task AddAsync(Geometrie entity)
        {
            if (_context == null) throw new InvalidOperationException("Database context is not available.");
            await _context.Geometries.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Geometrie entityToUpdate, Geometrie entity)
        {
            if (_context == null) throw new InvalidOperationException("Database context is not available.");
            entityToUpdate.NomPiece = entity.NomPiece;
            _context.Geometries.Update(entityToUpdate);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Geometrie entity)
        {
            if (_context == null) throw new InvalidOperationException("Database context is not available.");
            _context.Geometries.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}