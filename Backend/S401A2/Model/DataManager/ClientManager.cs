using APICube.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
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
                    AdresseId = c.AdresseId,

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

                AdresseClient = c.AdresseClient == null ? null : new Adresse
                {
                    Id = c.AdresseClient.Id,
                    Rue = c.AdresseClient.Rue,
                    Ville = c.AdresseClient.Ville,
                    Pays = c.AdresseClient.Pays,
                },

                CiviliteClient = c.CiviliteClient == null ? null : new Civilite
                {
                    Id = c.CiviliteClient.Id,
                    Nom = c.CiviliteClient.Nom,
                },

                ClientCommande = c.ClientCommande == null ? null : c.ClientCommande.Select(mc => new Commande
                {
                    Id = mc.Id,
                    AdresseCommandeLivr = mc.AdresseIdLivr == null ? null : new Adresse
                    {
                        Id = mc.AdresseCommandeLivr.Id,
                        Rue = mc.AdresseCommandeLivr.Rue,
                        Ville = mc.AdresseCommandeLivr.Ville,
                        CodePostale = mc.AdresseCommandeLivr.CodePostale,
                        Pays = mc.AdresseCommandeLivr.Pays,
                    },
                    AdresseCommandeFact = mc.AdresseIdFact == null ? null : new Adresse
                    {
                        Id = mc.AdresseCommandeFact.Id,
                        Rue = mc.AdresseCommandeFact.Rue,
                        Ville = mc.AdresseCommandeFact.Ville,
                        CodePostale = mc.AdresseCommandeFact.CodePostale,
                        Pays = mc.AdresseCommandeFact.Pays,
                    },
                    ArticleCommande = (ICollection<Article>)(mc.ArticleCommande == null ? null : mc.ArticleCommande.Select(ma => new Article
                    {
                        ArticleId = ma.ArticleId,
                        Reference = ma.Reference,
                        Nom = ma.Reference,
                        Description = ma.Description,
                        Prix = ma.Prix,
                        Poids = ma.Poids,
                        QteStock = ma.QteStock,
                        Annee = ma.Annee,
                        DispoEnLigne = ma.DispoEnLigne,
                        CategorieId = ma.CategorieId
                    }
                   ))
                }).ToList()


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
