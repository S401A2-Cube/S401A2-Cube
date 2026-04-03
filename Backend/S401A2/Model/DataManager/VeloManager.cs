using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APICube.Models.EntityFramework;
using S401A2.Models.Repository;
using S401A2.Model.EntityFramework;

namespace S401A2.Model.DataManager
{
    public class VeloManager : IVeloRepository
    {
        private readonly CubeDBContext? _context;
        public VeloManager() { }

        public VeloManager(CubeDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Velo>> GetAllAsync()
        {
            if (_context == null)
            {
                return null;
            }

            var Velos = await _context.Velos
                .AsNoTracking()
                .AsSplitQuery()
                .Select(v => new Velo
                {
                    IdVelo = v.IdVelo,
                    IdArticle = v.IdArticle,
                    LienVue360 = v.LienVue360,
                    IdModele = v.IdModele,

                    Article = new Article
                    {
                        ArticleId = v.Article.ArticleId,
                        Reference = v.Article.Reference,
                        Nom = v.Article.Nom,
                        Prix = v.Article.Prix,
                        Description = v.Article.Description,
                        Poids = v.Article.Poids,
                        QteStock = v.Article.QteStock,
                        Annee = v.Article.Annee,
                        DispoEnLigne = v.Article.DispoEnLigne,
                        CategorieId = v.Article.CategorieId,

                        CategorieArticle = new Categorie
                        {
                            CategorieId = v.Article.CategorieArticle.CategorieId,
                            Nom = v.Article.CategorieArticle.Nom
                        },

                        MotsCles = v.Article.MotsCles == null ? null : v.Article.MotsCles.Select(m => new MotCle
                        {
                            MotCleId = m.MotCleId,
                            Nom = m.Nom
                        }).ToList(),

                        Images = v.Article.Images.Take(1).Select(img => new Image
                        {
                            ImageId = img.ImageId,
                            Chemin = img.Chemin,
                            ArticleId = img.ArticleId
                        }).ToList(),

                        Velos = null
                    },

                    ModeleVelo = new Modele
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
                    }).ToList(),

                    Millesimes = v.Millesimes == null ? null : v.Millesimes.Select(m => new Millesime
                    {
                        IdMillesime = m.IdMillesime,
                        Annee = m.Annee,
                        Description = m.Description,
                        Velos = null
                    }).ToList()
                })
                .ToListAsync();

            return Velos;
        }

        public async Task<IEnumerable<Velo>> GetChunkAsync(int skip, int take)
        {
            if (_context == null) return null;

            return await _context.Velos
                .AsNoTracking()
                .AsSplitQuery()
                .OrderBy(v => v.IdVelo)
                .Skip(skip)
                .Take(take)
                .Select(v => new Velo
                {
                    IdVelo = v.IdVelo,
                    IdArticle = v.IdArticle,
                    LienVue360 = v.LienVue360,
                    IdModele = v.IdModele,

                    Article = new Article
                    {
                        ArticleId = v.Article.ArticleId,
                        Reference = v.Article.Reference,
                        Nom = v.Article.Nom,
                        Prix = v.Article.Prix,
                        Description = v.Article.Description,
                        Poids = v.Article.Poids,
                        QteStock = v.Article.QteStock,
                        Annee = v.Article.Annee,
                        DispoEnLigne = v.Article.DispoEnLigne,
                        CategorieId = v.Article.CategorieId,

                        CategorieArticle = new Categorie
                        {
                            CategorieId = v.Article.CategorieArticle.CategorieId,
                            Nom = v.Article.CategorieArticle.Nom
                        },

                        MotsCles = v.Article.MotsCles == null ? null : v.Article.MotsCles.Select(m => new MotCle
                        {
                            MotCleId = m.MotCleId,
                            Nom = m.Nom
                        }).ToList(),

                        Images = v.Article.Images.Take(1).Select(img => new Image
                        {
                            ImageId = img.ImageId,
                            Chemin = img.Chemin,
                            ArticleId = img.ArticleId
                        }).ToList(),

                        Velos = null
                    },

                    ModeleVelo = new Modele
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
                    }).ToList(),

                    Millesimes = v.Millesimes == null ? null : v.Millesimes.Select(m => new Millesime
                    {
                        IdMillesime = m.IdMillesime,
                        Annee = m.Annee,
                        Description = m.Description,
                        Velos = null
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<Velo> GetByIdAsync(int id)
        {
            if (_context == null)
            {
                return null;
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

                    Article = new Article
                    {
                        ArticleId = v.Article.ArticleId,
                        Reference = v.Article.Reference,
                        Nom = v.Article.Nom,
                        Prix = v.Article.Prix,
                        Description = v.Article.Description,
                        Poids = v.Article.Poids,
                        QteStock = v.Article.QteStock,
                        Annee = v.Article.Annee,
                        DispoEnLigne = v.Article.DispoEnLigne,
                        CategorieId = v.Article.CategorieId,

                        CategorieArticle = new Categorie
                        {
                            CategorieId = v.Article.CategorieArticle.CategorieId,
                            Nom = v.Article.CategorieArticle.Nom
                        },

                        MotsCles = v.Article.MotsCles == null ? null : v.Article.MotsCles.Select(m => new MotCle
                        {
                            MotCleId = m.MotCleId,
                            Nom = m.Nom
                        }).ToList(),

                        Images = v.Article.Images.Take(1).Select(img => new Image
                        {
                            ImageId = img.ImageId,
                            Chemin = img.Chemin,
                            ArticleId = img.ArticleId
                        }).ToList(),

                        Velos = null,
                        ArticleLignePanier = null,
                        ArticleCommande = null
                    },

                    ModeleVelo = new Modele
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
                    }).ToList(),

                    Millesimes = v.Millesimes == null ? null : v.Millesimes.Select(m => new Millesime
                    {
                        IdMillesime = m.IdMillesime,
                        Annee = m.Annee,
                        Description = m.Description,
                        Velos = null
                    }).ToList()
                })
                .FirstOrDefaultAsync();

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

        public async Task UpdateAsync(Velo entityToUpdate, Velo entity)
        {
            if (_context != null)
            {
                entityToUpdate.LienVue360 = entity.LienVue360;
                entityToUpdate.IdArticle = entity.IdArticle;
                entityToUpdate.IdModele = entity.IdModele;
                if (entity.Couleurs != null)
                {
                    entityToUpdate.Couleurs?.Clear();
                    var ids = entity.Couleurs.Select(c => c.IdCouleur).ToList();
                    var items = await _context.Couleurs.Where(c => ids.Contains(c.IdCouleur)).ToListAsync();
                    foreach (var item in items) entityToUpdate.Couleurs?.Add(item);
                }

                if (entity.Tailles != null)
                {
                    entityToUpdate.Tailles?.Clear();
                    var ids = entity.Tailles.Select(t => t.IdTaille).ToList();
                    var items = await _context.Tailles.Where(t => ids.Contains(t.IdTaille)).ToListAsync();
                    foreach (var item in items) entityToUpdate.Tailles?.Add(item);
                }

                if (entity.Cadres != null)
                {
                    entityToUpdate.Cadres?.Clear();
                    var ids = entity.Cadres.Select(c => c.IdMateriau).ToList();
                    var items = await _context.Cadres.Where(c => ids.Contains(c.IdMateriau)).ToListAsync();
                    foreach (var item in items) entityToUpdate.Cadres?.Add(item);
                }

                if (entity.Geometries != null)
                {
                    entityToUpdate.Geometries?.Clear();
                    var ids = entity.Geometries.Select(g => g.IdGeometrie).ToList();
                    var items = await _context.Geometries.Where(g => ids.Contains(g.IdGeometrie)).ToListAsync();
                    foreach (var item in items) entityToUpdate.Geometries?.Add(item);
                }
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Database context is not available.");
            }
        }
    }
}