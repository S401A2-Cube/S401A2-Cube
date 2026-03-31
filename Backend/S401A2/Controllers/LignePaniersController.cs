using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S401A2.Model.EntityFramework;

namespace S401A2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LignePaniersController : ControllerBase
    {
        private readonly CubeDBContext _context;

        public LignePaniersController(CubeDBContext context)
        {
            _context = context;
        }

        // GET: api/LignePaniers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LignePanier>>> GetLignePaniers()
        {
            return await _context.LignePaniers.ToListAsync();
        }

        // GET: api/LignePaniers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LignePanier>> GetLignePanier(int id)
        {
            var lignePanier = await _context.LignePaniers.FindAsync(id);

            if (lignePanier == null)
            {
                return NotFound();
            }

            return lignePanier;
        }

        // PUT: api/LignePaniers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLignePanier(int id, LignePanier lignePanier)
        {
            if (id != lignePanier.Id)
            {
                return BadRequest();
            }

            _context.Entry(lignePanier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LignePanierExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/LignePaniers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LignePanier>> PostLignePanier(LignePanier lignePanier)
        {
            _context.LignePaniers.Add(lignePanier);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLignePanier", new { id = lignePanier.Id }, lignePanier);
        }

        // DELETE: api/LignePaniers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLignePanier(int id)
        {
            var lignePanier = await _context.LignePaniers.FindAsync(id);
            if (lignePanier == null)
            {
                return NotFound();
            }

            _context.LignePaniers.Remove(lignePanier);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LignePanierExists(int id)
        {
            return _context.LignePaniers.Any(e => e.Id == id);
        }
    }
}
