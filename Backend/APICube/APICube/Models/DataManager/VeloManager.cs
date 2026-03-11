using APICube.Models.DTOs;
using APICube.Models.EntityFramework;
using APICube.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.DataManager
{
    public class VeloManager : IDataRepositoryVelo
    {
        private readonly S401Context? _context;

        public VeloManager(S401Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ActionResult<Velo>> GetByIdAsync(int idmateriau, int idcouleur, int idtaille, int idarticle)
        {
            if (_context == null)
            {
                return new NotFoundResult();
            }

            var velo = await _context.Velos.FirstOrDefaultAsync(u =>
                u.Idmateriau == idmateriau &&
                u.Idcouleur == idcouleur &&
                u.Idtaille == idtaille &&
                u.Idarticle == idarticle);

            if (velo == null)
            {
                return new NotFoundResult();
            }

            return velo;
        }

        public async Task<ActionResult<IEnumerable<Velo>>> GetAllAsync()
        {
            if (_context == null)
            {
                return new NotFoundResult();
            }

            return await _context.Velos.ToListAsync();
        }
        public async Task<ActionResult<IEnumerable<VeloDetailDTO>>> GetAllDTOAsync()
        {
            if (_context == null)
            {
                return new NotFoundResult();
            }

            var velos = await _context.Velos
                .Select(v => new VeloDetailDTO
                {
                    // Main properties
                    Reference = v.Reference,
                    Nomarticle = v.Nomarticle,
                    Prix = v.Prix,

                    // Nested DTOs
                    Article = new VeloArticleDTO
                    {
                        Idarticle = v.IdarticleNavigation.Idarticle,
                        IdCategorie = v.IdarticleNavigation.Idcategorie,
                        Reference = v.IdarticleNavigation.Reference,
                        Prix = v.IdarticleNavigation.Prix,
                        Nomarticle = v.IdarticleNavigation.Nomarticle,
                        Description = v.IdarticleNavigation.Description,
                        Poids = v.IdarticleNavigation.Poids,
                        Disponibiliteenligne = v.IdarticleNavigation.Disponibiliteenligne,
                        Resume = v.IdarticleNavigation.Resume,
                        Pourcentpromotion = v.IdarticleNavigation.Pourcentpromotion,
                        Qtestock = v.IdarticleNavigation.Qtestock
                    },
                    Couleur = new VeloCouleurDTO
                    {
                        IdCouleur = v.IdcouleurNavigation.Idcouleur,
                        Codehexacouleur = v.IdcouleurNavigation.Codehexacouleur,
                        Effetpeinture = v.IdcouleurNavigation.Effetpeinture,
                        Nomcouleur = v.IdcouleurNavigation.Nomcouleur
                    },
                    Carde = new VeloCadreDTO
                    {
                        Idmateriau = v.IdmateriauNavigation.Idmateriau,
                        Nommateriau = v.IdmateriauNavigation.Nommateriau,
                        Formecadre = v.IdmateriauNavigation.Formecadre
                    },
                    Millesime = new VeloMillesimeDTO
                    {
                        Idmillesime = v.IdmillesimeNavigation.Idmillesime,
                        Annee = v.IdmillesimeNavigation.Annee
                    },
                    Modele = new VeloModeleDTO
                    {
                        Idmodele = v.IdmodeleNavigation.Idmodele,
                        Nommodele = v.IdmodeleNavigation.Nommodele
                    },
                    Taille = new VeloTailleDTO
                    {
                        Idtaille = v.IdtailleNavigation.Idtaille,
                        Libelletaille = v.IdtailleNavigation.Libelletaille
                    },
                    Usage = v.IdusageNavigation != null ? new VeloUsageDTO
                    {
                        Idusage = v.IdusageNavigation.Idusage,
                        Nomusage = v.IdusageNavigation.Nomusage
                    } : null // Usage is optional
                })
                .ToListAsync();

            return velos;
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
                _context.Entry(entityToUpdate).State = EntityState.Modified;
            
                entityToUpdate.Idmateriau = entity.Idmateriau;
                entityToUpdate.Idcouleur = entity.Idcouleur;
                entityToUpdate.Idtaille = entity.Idtaille;
                entityToUpdate.Idarticle = entity.Idarticle;
                entityToUpdate.Idmodele = entity.Idmodele;
                entityToUpdate.Idmillesime = entity.Idmillesime;
                entityToUpdate.Idusage = entity.Idusage;
                entityToUpdate.Idcategorie = entity.Idcategorie;
                entityToUpdate.Reference = entity.Reference;
                entityToUpdate.Prix = entity.Prix;
                entityToUpdate.Nomarticle = entity.Nomarticle;
                entityToUpdate.Description = entity.Description;
                entityToUpdate.Poids = entity.Poids;
                entityToUpdate.Disponibiliteenligne = entity.Disponibiliteenligne;
                entityToUpdate.Resume = entity.Resume;
                entityToUpdate.Pourcentpromotion = entity.Pourcentpromotion;
                entityToUpdate.Lienvue360 = entity.Lienvue360;
                entityToUpdate.Qtestock = entity.Qtestock;

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Database context is not available.");
            }
        }

        public async Task<ActionResult<IEnumerable<Velo>>> GetByCouleurID(int id)
        {
            if (_context == null)
            {
                return new NotFoundResult();
            }

            return await _context.Velos.Where(v => v.Idcouleur == id).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Velo>>> GetByMateriauID(int id)
        {
            if (_context == null)
            {
                return new NotFoundResult();
            }

            return await _context.Velos.Where(v => v.Idmateriau == id).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Velo>>> GetByMillesimeID(int id)
        {
            if (_context == null)
            {
                return new NotFoundResult();
            }

            return await _context.Velos.Where(v => v.Idmillesime == id).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Velo>>> GetByModeleID(int id)
        {
            if (_context == null)
            {
                return new NotFoundResult();
            }

            return await _context.Velos.Where(v => v.Idmodele == id).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Velo>>> GetByTailleID(int id)
        {
            if (_context == null)
            {
                return new NotFoundResult();
            }

            return await _context.Velos.Where(v => v.Idtaille == id).ToListAsync();
        }
    }
}