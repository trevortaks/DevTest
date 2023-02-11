using DevTest.Shared.Dtos;
using DevTest.Shared.Models;

namespace DevTest.Client.Services
{
    public class ClientService
    {
        private readonly RequestClient _client;

        public ClientService(RequestClient client)
        {
            _client = client;
        }

        public async Task<ClientDb> GetClientById(int id)
        {
            return await _client.GetJsonAsync<ClientDb>(ApiRoutes.GetClientById + id.ToString());
        }

        public async Task<List<ClientDb>> GetAllClients()
        {
            return await _client.GetJsonAsync<List<ClientDb>>(ApiRoutes.GetAllClients);
        }

        public async Task<List<ClientDto>> GetAllClientsWithContactCount()
        {
            return await _client.GetJsonAsync<List<ClientDto>>(ApiRoutes.GetAllClientsWithContactCount);
        }

        public async Task<int> SaveClient(ClientDb client)
        {
            return await _client.PostJsonAsync(ApiRoutes.SaveClient, client);
        }

        public async Task<List<Contact>> GetClientContacts(int clientId)
        {
            return await _client.GetJsonAsync<List<Contact>>(ApiRoutes.GetClientContacts + clientId.ToString());
        }
    }
}
