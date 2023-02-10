using DevTest.Shared.Dtos;
using DevTest.Shared.Models;

namespace DevTest.Client.Services
{
    public class ContactService
    {
        private readonly RequestClient _client;

        public ContactService(RequestClient client)
        {
            _client = client;
        }

        public async Task<List<Contact>> GetAllContacts()
        {
            return await _client.GetJsonAsync<List<Contact>>(ApiRoutes.GetAllContacts);
        }

        public async Task<List<ContactDto>> GetAllContactsWithClientCount()
        {
            return await _client.GetJsonAsync<List<ContactDto>>(ApiRoutes.GetAllContactsWithClientCount);
        }

        public async Task<Contact> GetContactById(int id)
        {
            return await _client.GetJsonAsync<Contact>(ApiRoutes.GetContactById + id.ToString());
        }

        public async Task<List<ClientDto>> GetContactClients(int contactId)
        {
            return await _client.GetJsonAsync<List<ClientDto>>(ApiRoutes.GetContactClients + contactId.ToString());
        }

        public async Task SaveContact(Contact contact)
        {
            await _client.PostJsonAsync(ApiRoutes.SaveContact, contact);
        }

        public async Task UpdateContact(Contact contact)
        {
            await _client.PutJsonAsync(ApiRoutes.UpdateContact, contact);
        }
    }
}
