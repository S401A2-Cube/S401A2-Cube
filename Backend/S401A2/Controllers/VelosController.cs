using APICube.Models.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S401A2.Model;
using S401A2.Models.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace S401A2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VelosController : ControllerBase
    {
        private readonly IVeloRepository _repository;

        public VelosController(IVeloRepository dataRepository)
        {
            _repository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
        }

        // GET: api/Velos
        [HttpGet]
        [ActionName("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<Velo>> GetVelos()
        {
            return await _repository.GetAllAsync();
        }

        // GET: api/Velos/chunked?skip=${skip}&take=${take}
        [HttpGet]
        [Route("chunked")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<Velo>> GetVelosChunked([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            return await _repository.GetChunkAsync(skip, take);
        }

        // GET: api/Velos/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Velo>> GetVelo(int id)
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
        [Authorize(Policy = Policies.Admin)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // PUT: api/Velos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Policy = Policies.Admin)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutVelo(int id, Velo velo)
        {
            try
            {
                if (id != velo.IdVelo)
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
                velo.Article = null;
                velo.ModeleVelo = null;

                await _repository.UpdateAsync(veloToUpdate, velo);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        // POST: api/Velos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Policy = Policies.Admin)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Velo>> PostVelo(Velo velo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                velo.Article = null;
                velo.ModeleVelo = null;

                await _repository.AddAsync(velo);

                return CreatedAtAction(null, new { id = velo.IdVelo }, velo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        // DELETE: api/Velos/5
        [Authorize(Policy = Policies.Admin)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVelo(int id)
        {
            try
            {
                var velo = await _repository.GetByIdAsync(id);
                if (velo == null)
                {
                    return NotFound();
                }

                await _repository.DeleteAsync(velo);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}