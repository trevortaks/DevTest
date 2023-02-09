
using DevTest.Shared.Models;

namespace DevTest.Server.Repositories.Contracts
{
    public interface IClientRepository
    {
        public Shared.Models.Client GetClientById(int clientId);
        public Shared.Models.Client GetClientByName(string name);
        public Shared.Models.Client GetClientByCode(string code);
        public List<Contact> GetContactsByClientId(int clientId);
        public int SaveClient(Shared.Models.Client client);
        public int SaveClients(List<Shared.Models.Client> clients);
        public int UpdateClient(Shared.Models.Client client);
        public int updateClients(List<Shared.Models.Client> clients);

    }
}
