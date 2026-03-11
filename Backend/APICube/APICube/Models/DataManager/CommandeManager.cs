using APICube.Models.EntityFramework;
using APICube.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static APICube.Models.Repository.IDataRepository;

namespace APICube.Models.DataManager
{
    public class CommandeManager : IDataRepositoryCommande
    {
        readonly S401Context? context;

        public CommandeManager() { }

        public CommandeManager  (S401Context context)
        {
            this.context = context;
        }


        public async Task<ActionResult<IEnumerable<Commande>>> GetAllAsync()
        {
            return await context.Commandes.ToListAsync();
        }

        public async Task<ActionResult<Commande>> GetByIdAsync(int id)
        {
            return await context.Commandes.FirstOrDefaultAsync(u => u.Idcommande == id);
        }

        public async Task<ActionResult<Commande>> GetByStringAsync(string Etat)
        {
            return await context.Commandes.FirstOrDefaultAsync(u => u.Etat.ToUpper() == Etat.ToUpper());
        }

        public async Task AddAsync(Commande entity)
        {
            await context.Commandes.AddAsync(entity);
            await context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Commande commande, Commande entity)
        {
            context.Entry(commande).State = EntityState.Modified;

            commande.Idcommande = entity.Idcommande;
            commande.Idnommagasin = entity.Idnommagasin;
            commande.Idmodelivraison = entity.Idmodelivraison;
            commande.Idadresse = entity.Idadresse;
            commande.AdrIdadresse = entity.AdrIdadresse;
            commande.Idclient = entity.Idclient;
            commande.Idcodepromo = entity.Idcodepromo;
            commande.Fraisdeport = entity.Fraisdeport;
            commande.Etat = entity.Etat;


            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Commande commande)
        {
            context.Commandes.Remove(commande);
            await context.SaveChangesAsync();
        }
    }
}
