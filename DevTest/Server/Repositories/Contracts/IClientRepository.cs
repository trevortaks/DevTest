
using DevTest.Shared.Models;

namespace DevTest.Server.Repositories.Contracts
{
    public interface IClientRepository
    {
        Task<List<Shared.Models.Client>> GetAllClients();
        Task<Shared.Models.Client> GetClientById(int clientId);
        Task<Shared.Models.Client> GetClientByName(string name);
        Task<Shared.Models.Client> GetClientByCode(string code);
        Task<List<Shared.Models.Client>> GetClientsByContactId(int contactId);
        Task<int> SaveClient(Shared.Models.Client client);
        Task<int> SaveClients(List<Shared.Models.Client> clients);
        Task<bool> UpdateClient(Shared.Models.Client client);
        Task<bool> updateClients(List<Shared.Models.Client> clients);

    }
}
