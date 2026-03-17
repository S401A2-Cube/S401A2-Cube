using APICube.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S401A2.Model.EntityFramework;
using S401A2.Models.Repository;

namespace S401A2.Model.DataManager
{
    public class ClientManager : IDataRepository<Client>
    {
        private readonly CubeDBContext? _context;
        public ClientManager() { }

        public ClientManager(CubeDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task AddAsync(Client entity)
        {
            if (_context != null)
            {
                await _context.Clients.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Database context is not available.");
            }
        }

        public async Task DeleteAsync(Client entity)
        {
            if (_context != null)
            {
                _context.Clients.Remove(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Database context is not available.");
            }
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            if (_context == null)
            {
                return null;
            }

            var clients = await _context.Clients
                .AsNoTracking()
                .Select(c => new Client
                {
                    Id = c.Id,
                    CiviliteId = c.CiviliteId,
                    Nom = c.Nom,
                    Prenom = c.Prenom,
                    Email = c.Email,
                    Mdp = c.Mdp,
                    DateNaissance = c.DateNaissance,
                    Role = c.Role,

                    ClientCommande = c.ClientCommande == null ? null : c.ClientCommande.Select(mc => new Commande
                    {
                        Id = mc.Id,
                        Livraison = mc.Livraison,
                        AdresseIdFact = mc.AdresseIdFact,
                        AdresseIdLivr = mc.AdresseIdLivr,
                        ClientId = mc.ClientId,
                    }).ToList(),
                    ClientLignePanier = c.ClientLignePanier == null ? null : c.ClientLignePanier.Select(mc => new LignePanier
                    {
                        Id = mc.Id,
                        ArticleId = mc.ArticleId,
                        CommandeId = mc.CommandeId,
                        QtePanier = mc.QtePanier,
                        ClientId = mc.ClientId,
                    }).ToList(),

                }).ToListAsync();
            return clients;
        }

        public async Task<Client?> GetByIdAsync(int id)
        {
            if (_context == null)
            {
                return null;
            }

            var client = await _context.Clients
                .AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => new Client
                {
                    Id = c.Id,
                    CiviliteId = c.CiviliteId,
                    Nom = c.Nom,
                    Prenom = c.Prenom,
                    Email = c.Email,
                    Mdp = c.Mdp,
                    DateNaissance = c.DateNaissance,
                    Role = c.Role,

                    ClientCommande = c.ClientCommande == null ? null : c.ClientCommande.Select(mc => new Commande
                    {
                        Id = mc.Id,
                        Livraison = mc.Livraison,
                        AdresseIdFact = mc.AdresseIdFact,
                        AdresseIdLivr = mc.AdresseIdLivr,
                        ClientId = mc.ClientId,
                    }).ToList(),
                    ClientLignePanier = c.ClientLignePanier == null ? null : c.ClientLignePanier.Select(mc => new LignePanier
                    {
                        Id = mc.Id,
                        ArticleId = mc.ArticleId,
                        CommandeId = mc.CommandeId,
                        QtePanier = mc.QtePanier,
                        ClientId = mc.ClientId,
                    }).ToList(),

                }).FirstOrDefaultAsync();

            return client;
        }

        public Task UpdateAsync(Client entityToUpdate, Client entity)
        {
            if (_context != null)
            {
                entityToUpdate.Nom = entity.Nom;
                entityToUpdate.Prenom = entity.Prenom;
                entityToUpdate.Email = entity.Email;
                entityToUpdate.Mdp = entity.Mdp;
                entityToUpdate.DateNaissance = entity.DateNaissance;
                entityToUpdate.Role = entity.Role;
                // TODO: dont forget to update upcoming classes (civilite,adresse, model3d, ...)

                _context.Clients.Update(entityToUpdate);
                return _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Database context is not available.");
            }
        }
    }
}
