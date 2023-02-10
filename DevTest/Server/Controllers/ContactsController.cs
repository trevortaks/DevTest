using DevTest.Server.Repositories.Contracts;
using DevTest.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevTest.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactRepository _contactsRepository;

        public ContactsController(IContactRepository contactsRepository)
        {
            _contactsRepository = contactsRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactById(int id)
        {
            var contact = await _contactsRepository.GetContactById(id);
            if (contact == null) return NotFound();
            return Ok(contact);
        }

        [HttpGet("GetAllContacts")]
        public async Task<IActionResult> GetAllContacts()
        {
            var contacts = await _contactsRepository.GetAllContacts();
            return Ok(contacts);
        }

        [HttpGet("GetAllContactsWithClientCount")]
        public async Task<IActionResult> GetAllContactsWithClientCount()
        {
            var contacts = await _contactsRepository.GetAllContactsWithClientCount();
            return Ok(contacts);
        }

        [HttpGet("GetContactClients/{contactId}")]
        public async Task<IActionResult> GetContactClients(int contactId)
        {
            var clients = await _contactsRepository.GetClientsByContactId(contactId);
            if (clients == null) return NotFound();
            return Ok(clients);
        }

        [HttpPost("SaveContact")]
        public async Task<IActionResult> SaveContact([FromBody]Contact contact)
        {
            var result = await _contactsRepository.SaveContact(contact);

            return Ok(result);
        }

        [HttpPut("UpdateContact")]
        public async Task<IActionResult> UpdateContact([FromBody]Contact contact)
        {
            //var result = await _contactsRepository.Up
            return Ok();
        }
    }
}
