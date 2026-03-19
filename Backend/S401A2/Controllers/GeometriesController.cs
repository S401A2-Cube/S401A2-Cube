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
    public class GeometriesController : ControllerBase
    {
        private readonly IDataRepository<Geometrie> _repository;

        public GeometriesController(IDataRepository<Geometrie> dataRepository)
        {
            _repository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
        }

        // GET: api/Geometries
        [HttpGet]
        [ActionName("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<Geometrie>> GetGeometrie()
        {
            return await _repository.GetAllAsync();
        }

        // GET: api/Geometries/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Geometrie>> GetGeometrie(int id)
        {
            var geometrie = await _repository.GetByIdAsync(id);

            if (geometrie == null)
            {
                return NotFound();
            }

            return geometrie;
        }

        // PUT: api/Geometries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutGeometrie(int id, Geometrie geometrie)
        {
            if (id != geometrie.IdGeometrie)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var geometrieToUpdate = await _repository.GetByIdAsync(id);
            if (geometrieToUpdate == null || geometrieToUpdate == null)
            {
                return NotFound();
            }

            await _repository.UpdateAsync(geometrieToUpdate, geometrie);

            return NoContent();
        }

        // POST: api/Geometries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Geometrie>> PostGeometrie(Geometrie geometrie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.AddAsync(geometrie);

            return CreatedAtAction("GetGeometrie", new { id = geometrie.IdGeometrie }, geometrie);
        }

        // DELETE: api/Geometries/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteGeometrie(int id)
        {
            var geometrie = await _repository.GetByIdAsync(id);
            if (geometrie == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(geometrie);

            return NoContent();
        }
    }
}
