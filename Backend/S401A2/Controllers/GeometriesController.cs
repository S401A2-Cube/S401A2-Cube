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
        private readonly IDataRepository<Cadre> _repository;

        public GeometriesController(IDataRepository<Cadre> dataRepository)
        {
            _repository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
        }

        // GET: api/Geometries
        [HttpGet]
        [ActionName("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<Cadre>> GetGeometries()
        {
            return await _repository.GetAllAsync();
        }

        // GET: api/Geometries/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Cadre>> GetCadre(int id)
        {
            var cadre = await _repository.GetByIdAsync(id);

            if (cadre == null)
            {
                return NotFound();
            }

            return cadre;
        }

        // PUT: api/Geometries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutCadre(int id, Cadre cadre)
        {
            if (id != cadre.IdMateriau)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cadreToUpdate = await _repository.GetByIdAsync(id);
            if (cadreToUpdate == null || cadreToUpdate == null)
            {
                return NotFound();
            }

            await _repository.UpdateAsync(cadreToUpdate, cadre);

            return NoContent();
        }

        // POST: api/Geometries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Cadre>> PostCadre(Cadre cadre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.AddAsync(cadre);

            return CreatedAtAction("GetCadre", new { id = cadre.IdMateriau }, cadre);
        }

        // DELETE: api/Geometries/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCadre(int id)
        {
            var cadre = await _repository.GetByIdAsync(id);
            if (cadre == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(cadre);

            return NoContent();
        }
    }
}
