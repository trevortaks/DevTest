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
            var sql = "SELECT * FROM (SELECT CONCAT(CO.Surname, ' ', CO.Name) As FullName, CO.* FROM tblContacts CO " +
                            " INNER JOIN tblClientContacts CC ON CC.ContactID = CO.ContactID" +
                            " INNER JOIN tblClients C ON C.ClientID = CC.ClientID" +
                            " WHERE C.ClientID = @ClientId) Contacts" +
                            " ORDER BY FullName";
            var contacts = await ExecuteQuery<ContactDto>(sql, new { ClientId = clientId });
            return contacts.ToList();
        }

        public async Task<List<ClientDto>> GetAllClientsWithContactCount()
        {
            var sql = "SELECT C.ClientID, C.ClientCode, C.Name, ISNULL(COUNT(CO.ContactID),0) As LinkedContacts from tblClients C" +
                            " LEFT OUTER JOIN tblClientContacts CC ON CC.ClientID = C.ClientID" +
                            " LEFT OUTER JOIN tblContacts CO ON CC.ContactID = CO.ContactID" +
                            " GROUP BY C.ClientID, C.Name, C.ClientCode" +
                            " ORDER BY C.Name";
            var contacts = await ExecuteQuery<ClientDto>(sql);
            return contacts.ToList();
        }

        public async Task<int> SaveClient(ClientDb client)
        {
            string prefix = GenerateClientPrefix(client.Name);
            int code = await GetNextNumberInSeuence(prefix);
            client.ClientCode = prefix + code.ToString("000");

            return await Create(client);
        }

        public async Task<int> SaveClients(List<ClientDb> clients)
        {
            return await CreateRange(clients);
        }

        public async Task<int> GetNextNumberInSeuence(string prefix)
        {
            string sql = $"SELECT ISNULL(MAX(CAST(REPLACE(ClientCode, '{prefix}', '') AS INT)) ,0) + 1 from tblClients WHERE ClientCode LIKE '{prefix}%'";
            return await ExecuteQueryFirstOrDefault<int>(sql);
        }

        public string GenerateClientPrefix(string clientName)
        {
            string[] tokens = clientName.Split(' ');
            string prefix = "";
            if(tokens.Length == 1)
            {
                prefix = tokens[0].Substring(0, tokens[0].Length > 2 ? 3 : tokens[0].Length);
                if (prefix.Length == 3) return prefix.ToUpper();
            }
            else
            {
                foreach (var token in tokens)
                {
                    prefix += token.ElementAt(0);
                    if (prefix.Length == 3) return prefix.ToUpper();
                }
            }

            string fillers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int i = 1;

            while (prefix.Length < 3)
            {
                prefix += fillers[i];
                i++;
            }

            return prefix.ToUpper();
        }

        public async Task<bool> UpdateClient(ClientDb client)
        {
            return await Update(client);
        }

        public async Task<bool> UpdateClients(List<ClientDb> clients)
        {
            return await UpdateRange(clients);
        }

        public async Task<List<ClientDb>> GetAllClients()
        {
            List<ClientDb> clients = (List<ClientDb>)await GetAll();
            return clients;
        }

        public async Task<List<ContactDto>> GetUnlinkedContactsByClientId(int clientId)
        {
            var sql = "SELECT * FROM tblContacts " +
                " WHERE ContactID NOT IN (SELECT ContactID FROM tblClientContacts WHERE ClientID = @ClientId)";
            return (List<ContactDto>)await ExecuteQuery<ContactDto>(sql, new { ClientId = clientId });
        }

        public async Task<ContactClient> CreateLink(ContactClient link)
        {
            var sql = "INSERT INTO tblClientContacts(ClientID, ContactID) VALUES (@ClientID, @ContactID)";
            await ExecuteNonQuery(sql, new { ClientID = link.ClientId, ContactID = link.ContactId });
            return link;
        }

        public async Task<bool> DeleteLink(ContactClient link)
        {
            var sql = "DELETE FROM tblClientContacts WHERE ContactID = @ContactID AND ClientID = @ClientID";
            var result = await ExecuteNonQuery(sql, new { ContactID = link.ContactId, ClientID = link.ClientId });
            return result > 0;
        }

    }
}
