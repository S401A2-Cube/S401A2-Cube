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
    public class TaillesController : ControllerBase
    {
        private readonly IDataRepository<Taille> _repository;

        public TaillesController(IDataRepository<Taille> dataRepository)
        {
            _repository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
        }

        // GET: api/Tailles
        [HttpGet]
        [ActionName("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Taille>>> GetTailles()
        {
            return await _repository.GetAllAsync();
        }

        // GET: api/Tailles/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Taille>> GetTaille(int id)
        {
            var Taille = await _repository.GetByIdAsync(id);

            if (Taille == null)
            {
                return NotFound();
            }

            return Taille;
        }

        // PUT: api/Tailles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutTaille(int id, Taille Taille)
        {
            if (id != Taille.IdTaille)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var TailleToUpdate = await _repository.GetByIdAsync(id);
            if (TailleToUpdate == null || TailleToUpdate.Value == null)
            {
                return NotFound();
            }

            await _repository.UpdateAsync(TailleToUpdate.Value, Taille);

            return NoContent();
        }

        // POST: api/Tailles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Taille>> PostTaille(Taille Taille)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.AddAsync(Taille);

            return CreatedAtAction("GetTaille", new { id = Taille.IdTaille }, Taille);
        }

        // DELETE: api/Tailles/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTaille(int id)
        {
            var Taille = await _repository.GetByIdAsync(id);
            if (Taille == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(Taille.Value);

            return NoContent();
        }
    }
}
