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
    public class CategoriesController : ControllerBase
    {
        private readonly IDataRepository<Categorie> dataRepository;

        public CategoriesController(IDataRepository<Categorie> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categorie>>> GetCategories()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        [ActionName("GetCategorieById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Categorie>> GetCategorie(int id)
        {
            var categorie = await dataRepository.GetByIdAsync(id);

            if (categorie == null)
            {
                return NotFound();
            }

            return categorie;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutCategorie(int id, Categorie categorie)
        {
            if (id != categorie.Idcategorie)
            {
                return BadRequest();
            }

            var categorieToUpdate = await dataRepository.GetByIdAsync(id);
            if (categorieToUpdate == null)
            {
                return NotFound();
            }

            else
            {
                await dataRepository.UpdateAsync(categorieToUpdate.Value, categorie);
                return NoContent();
            }
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Categorie>> PostCategorie(Categorie categorie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(categorie);
            return CreatedAtAction("GetClientById", new { id = categorie.Idcategorie }, categorie);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCategorie(int id)
        {
            var categorie = await dataRepository.GetByIdAsync(id);
            if (categorie == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(categorie.Value);
            return NoContent();
        }
    }
}
