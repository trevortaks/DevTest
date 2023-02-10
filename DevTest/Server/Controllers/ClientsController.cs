using DevTest.Server.Repositories.Contracts;
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

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            var clients = await _clients.GetAllClients();

            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var client = await _clients.GetClientById(id);

            if (client == null) return NotFound();

            return Ok(client);
        }

        [HttpPost]
        public async void SaveClient([FromBody] Shared.Models.Client client)
        {
            
        }


        [HttpPut]
        public async void UpdateClient([FromBody]Shared.Models.Client client)
        {
            var result = await _clients.UpdateClient(client);
        }
    }
}
