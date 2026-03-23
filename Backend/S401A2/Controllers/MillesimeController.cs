using APICube.Models.EntityFramework;
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
    public class MillesimesController : ControllerBase
    {
        private readonly IDataRepository<Millesime> _repository;

        public MillesimesController(IDataRepository<Millesime> dataRepository)
        {
            _repository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
        }

        // GET: api/Millesimes
        [HttpGet]
        [ActionName("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<Millesime>> GetMillesimes()
        {
            return await _repository.GetAllAsync();
        }

        // GET: api/Millesimes/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Millesime>> GetMillesime(int id)
        {
            var millesime = await _repository.GetByIdAsync(id);

            if (millesime == null)
            {
                return NotFound();
            }

            return millesime;
        }

        // PUT: api/Millesimes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutMillesime(int id, Millesime millesime)
        {
            if (id != millesime.IdMillesime)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var millesimeToUpdate = await _repository.GetByIdAsync(id);
            if (millesimeToUpdate == null || millesimeToUpdate == null)
            {
                return NotFound();
            }

            await _repository.UpdateAsync(millesimeToUpdate, millesime);

            return NoContent();
        }

        // POST: api/Millesimes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Millesime>> PostMillesime(Millesime millesime)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.AddAsync(millesime);

            return CreatedAtAction("GetMillesime", new { id = millesime.IdMillesime }, millesime);
        }

        // DELETE: api/Millesimes/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMillesime(int id)
        {
            var millesime = await _repository.GetByIdAsync(id);
            if (millesime == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(millesime);

            return NoContent();
        }
    }
}
