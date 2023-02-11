using DevTest.Server.Repositories.Contracts;
using DevTest.Shared;
using DevTest.Shared.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DevTest.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinksController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public LinksController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpPost("CreateLink")]
        public async Task<IActionResult> Post([FromBody] ContactClient contactClient)
        {
            var result = await _clientRepository.CreateLink(contactClient);
            return Ok(new ResponseModel<ContactClient>(contactClient));
        }

        [HttpDelete("DeleteLink")]
        public async Task<IActionResult> Delete(int clientId, int contactId)
        {
            var result = await _clientRepository.DeleteLink(new ContactClient() { ClientId = clientId, ContactId = contactId});
            return Ok(new ResponseModel<bool>(result));
        }
    }
}
