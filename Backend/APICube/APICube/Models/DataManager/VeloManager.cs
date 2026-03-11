using APICube.Models.EntityFramework;
using APICube.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICube.Models.DataManager
{
    public class VeloManager : IDataRepositoryVelo
    {
        private readonly S401Context? _context;

        public VeloManager(S401Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ActionResult<Velo>> GetByIdAsync(int id)
        {
            return await _context.Velos.FirstOrDefaultAsync(u => u.Idarticle == id);
        }

        public async Task AddAsync(Velo entity)
        {
            await _context.Velos.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Velo entity)
        {
            _context.Velos.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Velo entityToUpdate, Velo entity)
        {
            _context.Entry(entityToUpdate).State = EntityState.Modified;
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

            await _context.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<Velo>>> GetAllAsync()
        {
            return await _context.Velos.ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Velo>>> GetByCouleurID(int id)
        {
            return await _context.Velos.Where(v => v.Idcouleur == id).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Velo>>> GetByMateriauID(int id)
        {
            return await _context.Velos.Where(v => v.Idmateriau == id).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Velo>>> GetByMillesimeID(int id)
        {
            return await _context.Velos.Where(v => v.Idmillesime == id).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Velo>>> GetByModeleID(int id)
        {
            return await _context.Velos.Where(v => v.Idmodele == id).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Velo>>> GetByTailleID(int id)
        {
            return await _context.Velos.Where(v => v.Idtaille == id).ToListAsync();
        }
    }
}
