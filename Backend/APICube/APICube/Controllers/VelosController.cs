using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APICube.Models.EntityFramework;
using APICube.Models.Repository;

namespace APICube.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VelosController : ControllerBase
    {
        private readonly IDataRepositoryVelo _repository;

        public VelosController(IDataRepositoryVelo veloManager)
        {
            _repository = veloManager;
        }

        // GET: api/Velos
        [HttpGet]
        [ActionName("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<IEnumerable<Velo>>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        // GET: api/Velos/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Velo>> GetById(int id)
        {
            var velo = await _repository.GetByIdAsync(id);

            if (velo == null)
            {
                return NotFound();
            }

            return velo;
        }

        // PUT: api/Velos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutVelo(int id, Velo velo)
        {
            if (id != velo.Idarticle)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var veloToUpdate = await _repository.GetByIdAsync(id);
            if (veloToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _repository.UpdateAsync(veloToUpdate.Value, velo);
            }

            return NoContent();
        }

        // POST: api/Velos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Velo>> PostVelo(Velo velo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.AddAsync(velo);

            return CreatedAtAction("GetUtilisateur", new { id = velo.Idarticle }, velo);
        }

        // DELETE: api/Velos/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVelo(int id)
        {
            var velo = await _repository.GetByIdAsync(id);
            if (velo == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(velo.Value);

            return NoContent();
        }

        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetByMateriauID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Velo>>> GetByMateriauID(int id)
        {
            return await _repository.GetByMateriauID(id);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetByCouleurID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Velo>>> GetByCouleurID(int id)
        {
            return await _repository.GetByCouleurID(id);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetByTailleID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Velo>>> GetByTailleID(int id)
        {
            return await _repository.GetByTailleID(id);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetByModeleID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Velo>>> GetByModeleID(int id)
        {
            return await _repository.GetByModeleID(id);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetByMillesimeID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Velo>>> GetByMillesimeID(int id)
        {
            return await _repository.GetByMillesimeID(id);
        }
    }
}
