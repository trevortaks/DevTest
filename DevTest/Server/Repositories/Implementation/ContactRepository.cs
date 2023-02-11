using DevTest.Server.Repositories.Contracts;
using DevTest.Shared.Dtos;
using DevTest.Shared.Models;

namespace DevTest.Server.Repositories.Implementation
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        public ContactRepository(IConfiguration config) : base(config)
        {
        }

        public async Task<List<ClientDto>> GetClientsByContactId(int contactId)
        {
            var sql = "SELECT C.* from tblClients C" +
                            " INNER JOIN tblClientContacts CC ON CC.ClientID = C.ClientID" +
                            " INNER JOIN tblContacts CO ON CC.ContactID = CO.ContactID" +
                            " WHERE CO.ContactID = @ContactId ORDER BY C.Name";
            var clients = await ExecuteQuery<ClientDto>(sql, new { ContactId = contactId });
            return clients.ToList();
        }

        public async Task<Contact> GetContactById(int contactId)
        {
            return await GetById(contactId);
        }


        public async Task<int> SaveContact(Contact contact)
        {
            if (await CheckEmailIsUnique(contact.EmailAddress))
            {
                throw new Exception("Email already exists");
            }

            return await Create(contact);
        }

        public async Task<bool> CheckEmailIsUnique(string email)
        {
            var sql = "SELECT EmailAddress from tblContacts where EmailAddress = @EmailAddress";
            string? result = await ExecuteQueryFirstOrDefault<string>(sql, new {EmailAddress = email});

            return !String.IsNullOrEmpty(result);
        }

        public async Task<List<Contact>> GetAllContacts()
        {
            return (List<Contact>)await GetAll();
        }

        public async Task<List<ContactDto>> GetAllContactsWithClientCount()
        {
            var sql = "SELECT * FROM (SELECT CO.ContactID, CO.Name, CO.Surname, CONCAT(CO.Surname, ' ', CO.Name) As FullName, CO.EmailAddress, Count(C.ClientCode) As LinkedClients from tblContacts CO " +
                " LEFT OUTER JOIN tblClientContacts CC ON CO.ContactID = CC.ContactID " +
                " LEFT OUTER JOIN tblClients C ON CC.ClientID = C.ClientID" +
                " GROUP BY Co.ContactID, CO.Surname, CO.Name, Co.EmailAddress) Contacts" +
                " ORDER BY Fullname";

            return (List<ContactDto>)await ExecuteQuery<ContactDto>(sql);
        }

        public async Task<List<ClientDto>> GetUnlinkedClientsByContactId(int contactId)
        {
            var sql = "SELECT * FROM tblClients" +
                        " WHERE ClientID NOT IN (SELECT ClientID FROM tblClientContacts WHERE ContactID = @ContactID)";
            return (List<ClientDto>)await ExecuteQuery<ClientDto>(sql, new { ContactID = contactId });
        }
    }
}
