using DevTest.Client.Pages;
using DevTest.Server.Repositories.Contracts;
using DevTest.Shared.Dtos;
using DevTest.Shared.Models;

namespace DevTest.Server.Repositories.Implementation
{
    public class ClientRepository : GenericRepository<Shared.Models.ClientDb>, IClientRepository
    {
        public ClientRepository(IConfiguration config) : base(config)
        {
        }

        public async Task<ClientDb> GetClientByCode(string code)
        {
            var sql = "SELECT * FROM tblClients WHERE ClientCode = @ClientCode";
            return await ExecuteQueryFirstOrDefault<Shared.Models.ClientDb>(sql, new { ClientCode = code });
        }


        public Task<ClientDb> GetClientById(int clientId)
        {
            return GetById(clientId);
        }

        public async Task<ClientDb> GetClientByName(string name)
        {
            var sql = "SELECT * FROM tblClients WHERE Name = @Name";
            return await ExecuteQueryFirstOrDefault<Shared.Models.ClientDb>(sql, new { Name = name });
        }

        public async Task<List<ContactDto>> GetContactsByClientId(int clientId)
        {
            var sql = "SELECT * FROM (SELECT CONCAT(C.Surname, C.Firstname) As FullName C.* FROM tblContacts CO " +
                            " INNER JOIN tblClientContacts CC ON CC.ContactID = CO.ContactID" +
                            " INNER JOIN tblClients C ON C.ClientID = CC.ClientID" +
                            " WHERE C.ClientID = @ClientId)" +
                            " ORDER BY FullName";
            var contacts = await ExecuteQuery<ContactDto>(sql, new { ClientId = clientId });
            return contacts.ToList();
        }

        public async Task<List<ClientDb>> GetAllClientsWithContactCount()
        {
            var sql = "SELECT C.ClientID, C.ClientCode, C.Name, COUNT(CO.ContactID) from tblClients C" +
                            " INNER JOIN tblClientContacts CC ON CC.ClientID = C.ClientID" +
                            " INNER JOIN tblContacts CO ON CC.ContactID = CO.ContactID" +
                            " GROUP BY C.ClientID, C.Name, C.Code" +
                            " ORDER BY C.Name";
            var contacts = await ExecuteQuery<ClientDb>(sql);
            return contacts.ToList();
        }

        public async Task<int> SaveClient(ClientDb client)
        {
            string prefix = GenerateClientPrefix(client.Name);
            int code = await GetNextNumberInSeuence(prefix);
            client.ClientNo = prefix + code.ToString("000");

            return await Create(client);
        }

        public async Task<int> SaveClients(List<ClientDb> clients)
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

        public async Task<bool> UpdateClient(ClientDb client)
        {
            return await Update(client);
        }

        public async Task<bool> updateClients(List<ClientDb> clients)
        {
            return await UpdateRange(clients);
        }

        public async Task<List<ClientDb>> GetAllClients()
        {
            List<ClientDb> clients = (List<ClientDb>)await GetAll();
            return clients;
        }

       
    }
}
