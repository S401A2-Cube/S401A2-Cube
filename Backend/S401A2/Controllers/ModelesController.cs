using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S401A2.Model.EntityFramework;
using S401A2.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S401A2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelesController : ControllerBase
    {
        private readonly IDataRepository<Modele> _repository;

        public ModelesController(IDataRepository<Modele> dataRepository)
        {
            _repository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
        }

        // GET: api/Modeles
        [HttpGet]
        [ActionName("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<Modele>> GetModeles()
        {
            return await _repository.GetAllAsync();
        }

        // GET: api/Modeles/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Modele>> GetModele(int id)
        {
            var modele = await _repository.GetByIdAsync(id);

            if (modele == null)
            {
                return NotFound();
            }

            return modele;
        }

        // PUT: api/Modeles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutModele(int id, Modele modele)
        {
            if (id != modele.IdModele)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var modeleToUpdate = await _repository.GetByIdAsync(id);
            if (modeleToUpdate == null || modeleToUpdate == null)
            {
                return NotFound();
            }

            await _repository.UpdateAsync(modeleToUpdate, modele);

            return NoContent();
        }

        // POST: api/Modeles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Modele>> PostModele(Modele modele)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.AddAsync(modele);

            return CreatedAtAction("GetModele", new { id = modele.IdModele }, modele);
        }

        // DELETE: api/Modeles/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteModele(int id)
        {
            var modele = await _repository.GetByIdAsync(id);
            if (modele == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(modele);

            return NoContent();
        }
    }
}
