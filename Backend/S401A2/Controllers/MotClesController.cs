using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S401A2.Model.EntityFramework;
using S401A2.Models.Repository;

namespace S401A2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotClesController : ControllerBase
    {
        private readonly CubeDBContext _context;
        private readonly IDataRepository<MotCle> _repository;

        public MotClesController(IDataRepository<MotCle> dataRepository)
        {
            _repository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
        }

        // GET: api/MotCles
        [HttpGet]
        [ActionName("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<MotCle?>> GetMotCles()
        {
            return await _repository.GetAllAsync();
        }

        // GET: api/MotCles/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MotCle>> GetMotCle(int id)
        {
            var MotCle = await _repository.GetByIdAsync(id);

            if (MotCle == null)
            {
                return NotFound();
            }

            return MotCle;
        }

        // PUT: api/MotCles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutMotCle(int id, MotCle motCle)
        {
            if (id != motCle.MotCleId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var MotCleToUpdate = await _repository.GetByIdAsync(id);
            if (MotCleToUpdate == null || MotCleToUpdate == null)
            {
                return NotFound();
            }

            await _repository.UpdateAsync(MotCleToUpdate, motCle);

            return NoContent();
        }

        // POST: api/MotCles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MotCle>> PostMotCle(MotCle motCle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.AddAsync(motCle);

            return CreatedAtAction("GetMotCle", new { id = motCle.MotCleId }, motCle);
        }

        // DELETE: api/MotCles/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMotCle(int id)
        {
            var motCle = await _repository.GetByIdAsync(id);
            if (motCle == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(motCle);

            return NoContent();
        }
    }
}
