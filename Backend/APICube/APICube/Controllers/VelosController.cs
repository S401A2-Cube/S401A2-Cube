using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APICube.Models.EntityFramework;

namespace APICube.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VelosController : ControllerBase
    {
        private readonly S401Context _context;

        public VelosController(S401Context context)
        {
            _context = context;
        }

        // GET: api/Velos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Velo>>> GetVelos()
        {
            return await _context.Velos.ToListAsync();
        }

        // GET: api/Velos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Velo>> GetVelo(int id)
        {
            var velo = await _context.Velos.FindAsync(id);

            if (velo == null)
            {
                return NotFound();
            }

            return velo;
        }

        // PUT: api/Velos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVelo(int id, Velo velo)
        {
            if (id != velo.Idmateriau)
            {
                return BadRequest();
            }

            _context.Entry(velo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VeloExists(id))
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

        // POST: api/Velos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Velo>> PostVelo(Velo velo)
        {
            _context.Velos.Add(velo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VeloExists(velo.Idmateriau))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetVelo", new { id = velo.Idmateriau }, velo);
        }

        // DELETE: api/Velos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVelo(int id)
        {
            var velo = await _context.Velos.FindAsync(id);
            if (velo == null)
            {
                return NotFound();
            }

            _context.Velos.Remove(velo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VeloExists(int id)
        {
            return _context.Velos.Any(e => e.Idmateriau == id);
        }
    }
}
