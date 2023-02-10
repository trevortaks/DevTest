
using DevTest.Shared.Dtos;
using DevTest.Shared.Models;

namespace DevTest.Server.Repositories.Contracts
{
    public interface IClientRepository
    {
        Task<List<ClientDb>> GetAllClients();
        Task<List<ClientDb>> GetAllClientsWithContactCount();
        Task<ClientDb> GetClientById(int clientId);
        Task<ClientDb> GetClientByName(string name);
        Task<ClientDb> GetClientByCode(string code);
        Task<List<ContactDto>> GetContactsByClientId(int clientId);
        Task<int> SaveClient(ClientDb client);
        Task<int> SaveClients(List<ClientDb> clients);
        Task<bool> UpdateClient(ClientDb client);
        Task<bool> updateClients(List<ClientDb> clients);

    }
}
