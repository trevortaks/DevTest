using DevTest.Server.Repositories.Contracts;
using DevTest.Shared.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DevTest.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository _clients;

        public ClientsController(IClientRepository clients)
        {
            _clients = clients;
        }

        [HttpGet("GetAllClients")]
        public async Task<IActionResult> GetAllClients()
        {
            var clients = await _clients.GetAllClients();

            return Ok(clients);
        }

        [HttpGet("GetClient/{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var client = await _clients.GetClientById(id);

            if (client == null) return NotFound();

            return Ok(client);
        }

        [HttpGet("/GetClientContacts/{id}")]
        public async Task<IActionResult> GetClientContacts(int id)
        {
            var contacts = await _clients.GetContactsByClientId(id);

            if (contacts == null) return NotFound();

            return Ok(contacts);
        }

        [HttpPost("SaveClient")]
        public async void SaveClient([FromBody] ClientDb client)
        {
            await _clients.SaveClient(client);
        }


        [HttpPut("UpdateClient")]
        public async void UpdateClient([FromBody] ClientDb client)
        {
            var result = await _clients.UpdateClient(client);
        }
    }
}
