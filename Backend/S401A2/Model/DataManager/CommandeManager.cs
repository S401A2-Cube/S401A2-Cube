using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S401A2.Model.EntityFramework;
using S401A2.Models.Repository;
using APICube.Models.EntityFramework;

namespace S401A2.Model.DataManager
{
    public class CommandeManager : IDataRepository<Commande>
    {
        private readonly CubeDBContext? _context;

        public CommandeManager() { }

        public CommandeManager(CubeDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Commande?>> GetAllAsync()
        {
            if (_context == null)
            {
                return null;
            }

            var commandes = await _context.Commandes
                    .AsNoTracking()
                    .Select(c => new Commande
                    {
                        Id = c.Id,
                        Livraison = c.Livraison,
                        AdresseIdLivr = c.AdresseIdLivr,
                        AdresseIdFact = c.AdresseIdFact,
                        ClientId = c.ClientId,

                        AdresseCommandeLivr = c.AdresseCommandeLivr == null ? null : new Adresse(),

                        AdresseCommandeFact = c.AdresseCommandeFact == null ? null : new Adresse(),

                        ClientCommande = c.ClientCommande == null ? null : new Client()
                    })
                    .ToListAsync();

            return commandes;
        }

        public async Task<Commande?> GetByIdAsync(int id)
        {
            if (_context == null)
            {
                return null;
            }

            var commande = await _context.Commandes
                .AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => new Commande
                {
                    Id = c.Id,
                    Livraison = c.Livraison,
                    AdresseIdLivr = c.AdresseIdLivr,
                    AdresseIdFact = c.AdresseIdFact,
                    ClientId = c.ClientId,

                    AdresseCommandeLivr = c.AdresseCommandeLivr == null ? null : new Adresse(),

                    AdresseCommandeFact = c.AdresseCommandeFact == null ? null : new Adresse(),

                    ClientCommande = c.ClientCommande == null ? null : new Client()
                })
                .FirstOrDefaultAsync();

            if (commande == null)
            {
                return null;
            }

            return commande;
        }

        public async Task AddAsync(Commande entity)
        {
            if (_context != null)
            {
                await _context.Commandes.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Database context is not available.");
            }
        }

        public async Task DeleteAsync(Commande entity)
        {
            if (_context != null)
            {
                _context.Commandes.Remove(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Database context is not available.");
            }
        }

        public Task UpdateAsync(Commande entityToUpdate, Commande entity)
        {
            if (_context != null)
            {
                entityToUpdate.Livraison = entity.Livraison;
                entityToUpdate.AdresseIdLivr = entity.AdresseIdLivr;
                entityToUpdate.AdresseIdFact = entity.AdresseIdFact;
                entityToUpdate.ClientId = entity.ClientId;

                _context.Commandes.Update(entityToUpdate);
                return _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Database context is not available.");
            }
        }
    }
}

