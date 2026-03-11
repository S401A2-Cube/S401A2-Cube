using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APICube.Models.EntityFramework;
using APICube.Models.Repository;
using NuGet.Protocol.Core.Types;

namespace APICube.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandesController : ControllerBase
    {
        private readonly IDataRepositoryCommande _repository;

        public CommandesController(IDataRepositoryCommande commandeManager)
        {
            _repository = commandeManager;
        }

        // GET: api/Commandes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Commande>>> GetCommandes()
        {
            return await _repository.GetAllAsync();
        }

        // GET: api/Commandes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Commande>> GetCommande(int id)
        {
            var commande = await _repository.GetByIdAsync(id);

            if (commande.Value == null)
            {
                return NotFound();
            }

            return commande;
        }

        // PUT: api/Commandes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommande(int id, Commande commande)
        {
            if (id != commande.Idcommande)
            {
                return BadRequest();
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var CommandeToUpdate = await _repository.GetByIdAsync(id);
            if (CommandeToUpdate == null)

            {
                return NotFound();
            }
            else
            {


                await _repository.UpdateAsync(CommandeToUpdate.Value, commande);
            }

            return NoContent();
        }

        // POST: api/Commandes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Commande>> PostCommande(Commande commande)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _repository.AddAsync(commande);
            return CreatedAtAction("GetCommande", new { id = commande.Idcommande }, commande);

        }


            // DELETE: api/Commandes/5
            [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommande(int id)
        {
            var commande = await _repository.GetByIdAsync(id);
            if (commande == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(commande.Value);

            return NoContent();
        }

       
    }
}
