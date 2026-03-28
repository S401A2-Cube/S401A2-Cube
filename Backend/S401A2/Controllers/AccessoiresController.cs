using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using S401A2.Model.EntityFramework;
using S401A2.Models.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace S401A2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessoiresController : ControllerBase
    {
        private readonly IDataRepository<Accessoire> _repository;

        public AccessoiresController(IDataRepository<Accessoire> dataRepository)
        {
            _repository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
        }

        // GET: api/Accessoires
        [HttpGet]
        [ActionName("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<Accessoire?>> GetAccessoires()
        {
            return await _repository.GetAllAsync();
        }

        // GET: api/Accessoires/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Accessoire>> GetAccessoire(int id)
        {
            var accessoire = await _repository.GetByIdAsync(id);

            if (accessoire == null)
            {
                return NotFound();
            }

            return accessoire;
        }

        // PUT: api/Accessoires/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutAccessoire(int id, Accessoire accessoire)
        {
            if (id != accessoire.ArticleId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accessoireToUpdate = await _repository.GetByIdAsync(id);
            if (accessoireToUpdate == null)
            {
                return NotFound();
            }

            await _repository.UpdateAsync(accessoireToUpdate, accessoire);

            return NoContent();
        }

        // POST: api/Accessoires
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Accessoire>> PostAccessoire(Accessoire accessoire)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.AddAsync(accessoire);

            return CreatedAtAction("GetAccessoire", new { id = accessoire.ArticleId }, accessoire);
        }

        // DELETE: api/Accessoires/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAccessoire(int id)
        {
            var accessoire = await _repository.GetByIdAsync(id);
            if (accessoire == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(accessoire);

            return NoContent();
        }
    }
}