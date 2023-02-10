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

        [HttpGet]
        public async Task<IActionResult> GetAllContacts()
        {
            var contacts = await _contactsRepository.GetAllContacts();
            return Ok(contacts);
        }

        [HttpPost]
        public async Task<IActionResult> SaveContact(Contact contact)
        {
            var result = await _contactsRepository.SaveContact(contact);

            if (result < 1) return StatusCode(500);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContact(Contact contact)
        {
            //var result = await _contactsRepository.Up
            return Ok();
        }
    }
}
