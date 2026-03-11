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
    public class ClientsController : ControllerBase
    {
        private readonly IDataRepository<Client> dataRepository;

        public ClientsController(IDataRepository<Client> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        [ActionName("GetClientById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var client = await dataRepository.GetByIdAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutClient(int id, Client client)
        {
            if (id != client.Idclient)
            {
                return BadRequest();
            }
            var clientToUpdate = await dataRepository.GetByIdAsync(id);
            if (clientToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(clientToUpdate.Value, client);
                return NoContent();
            }
        }

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(client);
            return CreatedAtAction("GetClientById", new { id = client.Idclient}, client);
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await dataRepository.GetByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(client.Value);
            return NoContent();
        }
    }
}
