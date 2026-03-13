using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APICube.Models.EntityFramework; 
using S401A2.Models.Repository;
using S401A2.Model.EntityFramework;

namespace S401A2.Model.DataManager
{
    public class VeloManager : IDataRepository<Velo>
    {
        private readonly CubeDBContext? _context;
        public VeloManager() { }

        public VeloManager(CubeDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ActionResult<IEnumerable<Velo>>> GetAllAsync()
        {
            if (_context == null)
            {
                return new NotFoundResult();
            }

            var Velos = await _context.Velos
                    .AsNoTracking()
                    .Select(v => new Velo
                    {
                        IdVelo = v.IdVelo, 
                        IdArticle = v.IdArticle,
                        LienVue360 = v.LienVue360,
                        IdModele = v.IdModele,

                        ModeleVelo = v.ModeleVelo == null ? null : new Modele
                        {
                            IdModele = v.ModeleVelo.IdModele,
                            NomModele = v.ModeleVelo.NomModele,
                            Velos = null
                        },

                        Couleurs = v.Couleurs == null ? null : v.Couleurs.Select(c => new Couleur
                        {
                            IdCouleur = c.IdCouleur,
                            NomCouleur = c.NomCouleur,
                            EffetPeinture = c.EffetPeinture,
                            Velos = null
                        }).ToList(),

                        Tailles = v.Tailles == null ? null : v.Tailles.Select(t => new Taille
                        {
                            IdTaille = t.IdTaille,
                            LibelleTaille = t.LibelleTaille,
                            Velos = null
                        }).ToList(),

                        Cadres = v.Cadres == null ? null : v.Cadres.Select(cad => new Cadre
                        {
                            IdMateriau = cad.IdMateriau,
                            NomMat = cad.NomMat,
                            FormeCadre = cad.FormeCadre,
                            Velos = null
                        }).ToList(),

                        Geometries = v.Geometries == null ? null : v.Geometries.Select(g => new Geometrie
                        {
                            IdGeometrie = g.IdGeometrie,
                            NomPiece = g.NomPiece,
                            TaillePiece = g.TaillePiece,
                            Velos = null
                        }).ToList()
                    })
                    .ToListAsync();

            return Velos;
        }

        public async Task<ActionResult<Velo>> GetByIdAsync(int id)
        {
            if (_context == null)
            {
                return new NotFoundResult();
            }

            var Velo = await _context.Velos
                .AsNoTracking()
                .Where(v => v.IdVelo == id) 
                .Select(v => new Velo 
                {
                    IdVelo = v.IdVelo,
                    IdArticle = v.IdArticle,
                    LienVue360 = v.LienVue360,
                    IdModele = v.IdModele,

                    ModeleVelo = v.ModeleVelo == null ? null : new Modele
                    {
                        IdModele = v.ModeleVelo.IdModele,
                        NomModele = v.ModeleVelo.NomModele,
                        Velos = null
                    },

                    Couleurs = v.Couleurs == null ? null : v.Couleurs.Select(c => new Couleur
                    {
                        IdCouleur = c.IdCouleur,
                        NomCouleur = c.NomCouleur,
                        EffetPeinture = c.EffetPeinture,
                        Velos = null
                    }).ToList(),

                    Tailles = v.Tailles == null ? null : v.Tailles.Select(t => new Taille
                    {
                        IdTaille = t.IdTaille,
                        LibelleTaille = t.LibelleTaille,
                        Velos = null
                    }).ToList(),

                    Cadres = v.Cadres == null ? null : v.Cadres.Select(cad => new Cadre
                    {
                        IdMateriau = cad.IdMateriau,
                        NomMat = cad.NomMat,
                        FormeCadre = cad.FormeCadre,
                        Velos = null
                    }).ToList(),

                    Geometries = v.Geometries == null ? null : v.Geometries.Select(g => new Geometrie
                    {
                        IdGeometrie = g.IdGeometrie,
                        NomPiece = g.NomPiece,
                        TaillePiece = g.TaillePiece,
                        Velos = null
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (Velo == null)
            {
                return new NotFoundResult();
            }

            return Velo;
        }

        public async Task AddAsync(Velo entity)
        {
            if (_context != null)
            {
                await _context.Velos.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Database context is not available.");
            }
        }

        public async Task DeleteAsync(Velo entity)
        {
            if (_context != null)
            {
                _context.Velos.Remove(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Database context is not available.");
            }
        }

        public Task UpdateAsync(Velo entityToUpdate, Velo entity)
        {
            if (_context != null)
            {
                entityToUpdate.LienVue360 = entity.LienVue360;
                entityToUpdate.IdArticle = entity.IdArticle; 
                entityToUpdate.IdModele = entity.IdModele;   

                _context.Velos.Update(entityToUpdate);
                return _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Database context is not available.");
            }
        }
    }
}