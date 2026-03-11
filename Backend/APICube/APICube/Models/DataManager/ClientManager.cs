using APICube.Models.EntityFramework;
using APICube.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;


namespace APICube.Models.DataManager
{
    public class ClientManager : IDataRepository<Client>
    {
        readonly S401Context? context;

        public ClientManager() { }

        public ClientManager(S401Context context) 
        {
            this.context = context;
        }
        public async Task AddAsync(Client client)
        {
            await context.Clients.AddAsync(client);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Client client)
        {

            context?.Clients.Remove(client);
            await context.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<Client>>> GetAllAsync()
        {
            return await context.Clients.ToListAsync();
        }

        public async Task<ActionResult<Client>> GetByIdAsync(int id)
        {
            return await context.Clients.FirstOrDefaultAsync(u => u.Idclient == id);
        }

        public async Task UpdateAsync(Client clientToUpdate, Client client)
        {
            context.Entry(clientToUpdate).State = EntityState.Modified;
            clientToUpdate.Idclient = client.Idclient;
            clientToUpdate.Idcivilite = client.Idcivilite;
            clientToUpdate.Nomclient = client.Nomclient;
            clientToUpdate.Prenomclient = client.Prenomclient;
            clientToUpdate.Email = client.Email;
            clientToUpdate.Motdepasse = client.Motdepasse;
            clientToUpdate.Datenaissance = client.Datenaissance;
            clientToUpdate.Newsletter = client.Newsletter;
            clientToUpdate.Datederniereactivite = client.Datederniereactivite;
            clientToUpdate.Role = client.Role;
            clientToUpdate.RememberToken = client.RememberToken;
            clientToUpdate.GoogleId = client.GoogleId;
            clientToUpdate.TwoFactorCode = client.TwoFactorCode;
            clientToUpdate.TwoFactorExpiresAt = client.TwoFactorExpiresAt;
            clientToUpdate.Mobile = client.Mobile;
            clientToUpdate.Is2faEnabled = client.Is2faEnabled;
            await context.SaveChangesAsync();
        }
    }
}
