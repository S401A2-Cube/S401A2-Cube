using APICube.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S401A2.Model.EntityFramework;
using S401A2.Models.Repository;

namespace S401A2.Model.DataManager
{
    public class LignePanierManager : IDataRepository<LignePanier>
    {
        private readonly CubeDBContext? _context;
        public LignePanierManager() { }

        public LignePanierManager(CubeDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task AddAsync(LignePanier entity)
        {
            if (_context != null)
            {
                await _context.LignePaniers.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Database context is not available.");
            }
        }

        public async Task DeleteAsync(LignePanier entity)
        {
            if (_context != null)
            {
                _context.LignePaniers.Remove(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Database context is not available.");
            }
        }

        public async Task<IEnumerable<LignePanier>> GetAllAsync()
        {
            if (_context == null)
            {
                return null;
            }

            var lignes = await _context.LignePaniers
                    .AsNoTracking()
                    .Select(l => new LignePanier
                    {
                        Id = l.Id,
                        QtePanier = l.QtePanier,
                        ArticleId = l.ArticleId,
                        ClientId = l.ClientId,
                        CommandeId = l.CommandeId,
                        TailleId = l.TailleId,
                        CouleurId = l.CouleurId,

                        ArticleLignePanier = l.ArticleLignePanier == null ? null : new Article
                        {
                            ArticleId = l.ArticleLignePanier.ArticleId,
                            Nom = l.ArticleLignePanier.Nom,
                            Reference = l.ArticleLignePanier.Reference,
                            Prix = l.ArticleLignePanier.Prix,
                            ArticleLignePanier = null,

                            Images = l.ArticleLignePanier.Images == null ? null : l.ArticleLignePanier.Images.Select(img => new Image
                            {
                                ImageId = img.ImageId,
                                Chemin = img.Chemin
                            }).ToList()
                        },

                        CouleurChoisie = l.CouleurChoisie == null ? null : new Couleur
                        {
                            IdCouleur = l.CouleurChoisie.IdCouleur,
                            NomCouleur = l.CouleurChoisie.NomCouleur,
                            Velos = null
                        },

                        TailleChoisie = l.TailleChoisie == null ? null : new Taille
                        {
                            IdTaille = l.TailleChoisie.IdTaille,
                            LibelleTaille = l.TailleChoisie.LibelleTaille,
                            Velos = null
                        },
                    })
                    .ToListAsync();

            return lignes;

        }

        public async Task<LignePanier?> GetByIdAsync(int id)
        {
            if (_context == null)
            {
                return null;
            }

            var ligne = await _context.LignePaniers
                    .AsNoTracking()
                    .Where(l => l.Id == id)
                    .Select(l => new LignePanier
                    {
                        Id = l.Id,
                        QtePanier = l.QtePanier,
                        ArticleId = l.ArticleId,
                        ClientId = l.ClientId,
                        CommandeId = l.CommandeId,
                        TailleId = l.TailleId,
                        CouleurId = l.CouleurId,

                        ArticleLignePanier = l.ArticleLignePanier == null ? null : new Article
                        {
                            ArticleId = l.ArticleLignePanier.ArticleId,
                            Nom = l.ArticleLignePanier.Nom,
                            Reference = l.ArticleLignePanier.Reference,
                            Prix = l.ArticleLignePanier.Prix,
                            ArticleLignePanier = null,

                            Images = l.ArticleLignePanier.Images == null ? null : l.ArticleLignePanier.Images.Select(img => new Image
                            {
                                ImageId = img.ImageId,
                                Chemin = img.Chemin
                            }).ToList()
                        },

                        CouleurChoisie = l.CouleurChoisie == null ? null : new Couleur
                        {
                            IdCouleur = l.CouleurChoisie.IdCouleur,
                            NomCouleur = l.CouleurChoisie.NomCouleur,
                            Velos = null
                        },

                        TailleChoisie = l.TailleChoisie == null ? null : new Taille
                        {
                            IdTaille = l.TailleChoisie.IdTaille,
                            LibelleTaille = l.TailleChoisie.LibelleTaille,
                            Velos = null
                        },
                    })
                    .FirstOrDefaultAsync();

            return ligne;
        }

        public Task UpdateAsync(LignePanier entityToUpdate, LignePanier entity)
        {
            if (_context != null)
            {
                entityToUpdate.QtePanier = entity.QtePanier;

                _context.LignePaniers.Update(entityToUpdate);
                return _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Database context is not available.");
            }
        }
    }
}
