using APICube.Models.EntityFramework;
using APICube.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICube.Models.DataManager
{
    public class VeloManager : IDataRepositoryVelo
    {
        readonly S401Context? context;

        public VeloManager() { }

        public VeloManager(S401Context context)
        {
            this.context = context;
        }

        public async Task<ActionResult<Velo>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(Velo entity)
        {
            await context.Velos.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Velo entity)
        {
            context.Velos.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Velo entityToUpdate, Velo entity)
        {
            context.Entry(entityToUpdate).State = EntityState.Modified;
            entityToUpdate.Idmateriau = entity.Idmateriau;
            entityToUpdate.Idcouleur  = entity.Idcouleur;
            entityToUpdate.Idtaille  = entity.Idtaille;
            entityToUpdate.Idarticle  = entity.Idarticle;
            entityToUpdate.Idmodele  = entity.Idmodele;
            entityToUpdate.Idmillesime  = entity.Idmillesime;
            entityToUpdate.Idusage  = entity.Idusage;
            entityToUpdate.Idcategorie  = entity.Idcategorie;
            entityToUpdate.Reference = entity.Reference;
            entityToUpdate.Prix  = entity.Prix;
            entityToUpdate.Nomarticle = entity.Nomarticle;
            entityToUpdate.Description  = entity.Description;
            entityToUpdate.Poids  = entity.Poids;
            entityToUpdate.Disponibiliteenligne  = entity.Disponibiliteenligne;
            entityToUpdate.Resume  = entity.Resume;
            entityToUpdate.Pourcentpromotion  = entity.Pourcentpromotion;
            entityToUpdate.Lienvue360  = entity.Lienvue360;
            entityToUpdate.Qtestock  = entity.Qtestock;

            entityToUpdate.IdarticleNavigation = entity.IdarticleNavigation;
            entityToUpdate.IdcouleurNavigation = entity.IdcouleurNavigation;
            entityToUpdate.IdmateriauNavigation = entity.IdmateriauNavigation;
            entityToUpdate.IdmillesimeNavigation = entity.IdmillesimeNavigation;
            entityToUpdate.IdmodeleNavigation = entity.IdmodeleNavigation;
            entityToUpdate.IdtailleNavigation = entity.IdtailleNavigation;
            entityToUpdate.IdusageNavigation = entity.IdusageNavigation;
            entityToUpdate.Corresponda = entity.Corresponda;
            entityToUpdate.Estenlienavecs = entity.Estenlienavecs;
            entityToUpdate.Peutavoir3s = entity.Peutavoir3s;
            entityToUpdate.Peutavoir5s = entity.Peutavoir5s;
            entityToUpdate.Veloelectriques = entity.Veloelectriques;

            await context.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<Velo>>> GetAllAsync()
        {
            return await context.Velos.ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Velo>>> GetByArticleID(int id)
        {
            return await context.Velos.Where(v => v.Idarticle == id).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Velo>>> GetByCouleurID(int id)
        {
            return await context.Velos.Where(v => v.Idcouleur == id).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Velo>>> GetByMateriauID(int id)
        {
            return await context.Velos.Where(v => v.Idmateriau== id).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Velo>>> GetByMillesimeID(int id)
        {
            return await context.Velos.Where(v => v.Idmillesime == id).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Velo>>> GetByModeleID(int id)
        {
            return await context.Velos.Where(v => v.Idmodele == id).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Velo>>> GetByTailleID(int id)
        {
            return await context.Velos.Where(v => v.Idtaille== id).ToListAsync();
        }
    }
}
