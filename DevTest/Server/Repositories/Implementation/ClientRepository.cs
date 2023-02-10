using DevTest.Server.Repositories.Contracts;

namespace DevTest.Server.Repositories.Implementation
{
    public class ClientRepository : GenericRepository<Shared.Models.Client>, IClientRepository
    {
        public ClientRepository(IConfiguration config) : base(config)
        {
        }

        public async Task<Shared.Models.Client> GetClientByCode(string code)
        {
            var sql = "SELECT * FROM tblClients WHERE ClientCode = @ClientCode";
            return await ExecuteQueryFirstOrDefault<Shared.Models.Client>(sql, new { ClientCode = code });
        }


        public Task<Shared.Models.Client> GetClientById(int clientId)
        {
            return GetById(clientId);
        }

        public async Task<Shared.Models.Client> GetClientByName(string name)
        {
            var sql = "SELECT * FROM tblClients WHERE Name = @Name";
            return await ExecuteQueryFirstOrDefault<Shared.Models.Client>(sql, new { Name = name });
        }

        public async Task<List<Shared.Models.Client>> GetClientsByContactId(int contactId)
        {
            var sql = "";
            var clients = await ExecuteQuery<Shared.Models.Client>(sql, new { ContactId = contactId});
            return clients.ToList();
        }

        public async Task<int> SaveClient(Shared.Models.Client client)
        {
            return await Create(client);
        }

        public async Task<int> SaveClients(List<Shared.Models.Client> clients)
        {
            return await CreateRange(clients);
        }

        public async Task<int> GetNextNumberInSeuence(string prefix)
        {
            string sql = $"SELECT ISNULL(MAX(CAST(REPLACE(ClientCode, {prefix}, '') AS INT)) ,0) + 1 from tblClients WHERE ClientCode IS LIKE '{prefix}%'";
            return await ExecuteQueryFirstOrDefault<int>(sql);
        }

        public string GenerateClientPrefix(string clientName)
        {
            string[] tokens = clientName.Split(' ');
            string prefix = "";
            if(tokens.Length == 1)
            {
                prefix = tokens[0].Substring(0,3);
                if (prefix.Length == 3) return prefix;
            }
            else
            {
                foreach (var token in tokens)
                {
                    prefix += token.ElementAt(0);
                    if (prefix.Length == 3) return prefix;
                }
            }

            string fillers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int i = 0;

            while (prefix.Length < 3)
            {
                prefix.Append(fillers[i]);
                i++;
            }

            return prefix;
        }

        public async Task<bool> UpdateClient(Shared.Models.Client client)
        {
            return await Update(client);
        }

        public async Task<bool> updateClients(List<Shared.Models.Client> clients)
        {
            return await UpdateRange(clients);
        }

        public async Task<List<Shared.Models.Client>> GetAllClients()
        {
            List<Shared.Models.Client> clients = (List<Shared.Models.Client>)await GetAll();
            return clients;
        }
    }
}
