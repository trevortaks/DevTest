using DevTest.Server.Repositories.Contracts;
using DevTest.Shared;
using DevTest.Shared.Dtos;
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

            return Ok(new ResponseModel<List<ClientDb>>(clients));
        }

        [HttpGet("GetAllClientsWithContactCount")]
        public async Task<IActionResult> GetAllClientsWithContactCount()
        {
            var clients = await _clients.GetAllClientsWithContactCount();

            return Ok(new ResponseModel<List<ClientDto>>(clients));
        }

        [HttpGet("GetClient/{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var client = await _clients.GetClientById(id);

            if (client == null) return NotFound();

            return Ok(new ResponseModel<ClientDb>(client));
        }

        [HttpGet("/GetClientContacts/{id}")]
        public async Task<IActionResult> GetClientContacts(int id)
        {
            var contacts = await _clients.GetContactsByClientId(id);

            if (contacts == null) return NotFound();

            return Ok(new ResponseModel<List<ContactDto>>(contacts));
        }

        [HttpPost("SaveClient")]
        public async Task<IActionResult> SaveClient([FromBody] ClientDb client)
        {
            var id = await _clients.SaveClient(client);
            var newClient = await _clients.GetClientById(id);
            return Ok(new ResponseModel<ClientDb>(newClient));
        }


        [HttpPut("UpdateClient")]
        public async Task<IActionResult> UpdateClient([FromBody] ClientDb client)
        {
            var result = await _clients.UpdateClient(client);
            return Ok(new ResponseModel<bool>(result));
        }

        [HttpGet("GetUnlinkedContactsByClientId/{clientId}")]
        public async Task<IActionResult> GetUnlinkedContactsByClientId(int clientId)
        {
            var result = await _clients.GetUnlinkedContactsByClientId(clientId);
            return Ok(new ResponseModel<List<ContactDto>>(result));
        }
    }
}
