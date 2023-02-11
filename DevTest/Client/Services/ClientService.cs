using DevTest.Shared;
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

        public async Task<ResponseModel<ClientDb>?> GetClientById(int id)
        {
            return await _client.GetJsonAsync<ClientDb>(ApiRoutes.GetClientById + id.ToString());
        }

        public async Task<ResponseModel<List<ClientDb>>?> GetAllClients()
        {
            return await _client.GetJsonAsync<List<ClientDb>>(ApiRoutes.GetAllClients);
        }

        public async Task<ResponseModel<List<ClientDto>>?> GetAllClientsWithContactCount()
        {
            return await _client.GetJsonAsync<List<ClientDto>>(ApiRoutes.GetAllClientsWithContactCount);
        }

        public async Task<ResponseModel<ClientDb>?> SaveClient(ClientDb client)
        {
            return await _client.PostJsonAsync(ApiRoutes.SaveClient, client);
        }

        public async Task<ResponseModel<List<Contact>>?> GetClientContacts(int clientId)
        {
            return await _client.GetJsonAsync<List<Contact>>(ApiRoutes.GetClientContacts + clientId.ToString());
        }
    }
}
