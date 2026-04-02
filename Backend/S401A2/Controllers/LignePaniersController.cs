using Microsoft.AspNetCore.Authorization;
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
    public class LignePaniersController : ControllerBase
    {
        private readonly IDataRepository<LignePanier> _repository;

        public LignePaniersController(IDataRepository<LignePanier> dataRepository)
        {
            _repository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
        }

        // GET: api/LignePaniers
        [HttpGet]
        [ActionName("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<LignePanier>> GetLignePaniers()
        {
            return await _repository.GetAllAsync();
        }

        // GET: api/LignePaniers/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LignePanier>> GetLignePanier(int id)
        {
            var lignePanier = await _repository.GetByIdAsync(id);

            if (lignePanier == null)
            {
                return NotFound();
            }

            return lignePanier;
        }

        // PUT: api/LignePaniers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutLignePanier(int id, LignePanier lignePanier)
        {
            if (id != lignePanier.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lignePanierToUpdate = await _repository.GetByIdAsync(id);
            if (lignePanierToUpdate == null || lignePanierToUpdate.Id == null)
            {
                return NotFound();
            }

            await _repository.UpdateAsync(lignePanierToUpdate, lignePanier);

            return NoContent();
        }

        // POST: api/LignePaniers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LignePanier>> PostLignePanier(LignePanier lignePanier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.AddAsync(lignePanier);

            return CreatedAtAction("GetLignePanier", new { id = lignePanier.Id }, lignePanier);
        }

        // DELETE: api/LignePaniers/5
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteLignePanier(int id)
        {
            var lignePanier = await _repository.GetByIdAsync(id);
            if (lignePanier == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(lignePanier);

            return NoContent();
        }

        //private bool LignePanierExists(int id)
        //{
        //    return _repository.GetAllAsync().Any(e => e.Id == id);
        //}
    }
}
