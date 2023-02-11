using DevTest.Client.Pages.Contacts;
using DevTest.Shared;
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

        public async Task<ResponseModel<List<Contact>>?> GetAllContacts()
        {
            return await _client.GetJsonAsync<List<Contact>>(ApiRoutes.GetAllContacts);
        }

        public async Task<ResponseModel<List<ContactDto>>?> GetAllContactsWithClientCount()
        {
            return await _client.GetJsonAsync<List<ContactDto>>(ApiRoutes.GetAllContactsWithClientCount);
        }

        public async Task<ResponseModel<Contact>?> GetContactById(int id)
        {
            return await _client.GetJsonAsync<Contact>(ApiRoutes.GetContactById + id.ToString());
        }

        public async Task<ResponseModel<List<ClientDto>>?> GetContactClients(int contactId)
        {
            return await _client.GetJsonAsync<List<ClientDto>>(ApiRoutes.GetContactClients + contactId.ToString());
        }

        public async Task<ResponseModel<List<ContactDto>>?> GetUnlinkedContactsByClientId(int clientId)
        {
            return await _client.GetJsonAsync<List<ContactDto>>(ApiRoutes.GetUnlinkedContactsByClientId + clientId.ToString());
        }
        public async Task<ResponseModel<Contact>?> SaveContact(Contact contact)
        {
            return await _client.PostJsonAsync(ApiRoutes.SaveContact, contact);
        }

        public async Task<ResponseModel<bool>?> UpdateContact(Contact contact)
        {
            return await _client.PutJsonAsync(ApiRoutes.UpdateContact, contact);
        }

        public async Task<ResponseModel<ContactClient>?> LinkClient(ContactClient link)
        {
            return await _client.PostJsonAsync(ApiRoutes.LinkClient, link);
        }

        public async Task<ResponseModel<bool>?> UnlinkClient(ContactClient link)
        {
            return await _client.DeleteJsonAsync<ContactClient>(ApiRoutes.Unlinkclient + "?contactId = " + link.ContactId + "clientId=" + link.ClientId);
        }

    }
}
