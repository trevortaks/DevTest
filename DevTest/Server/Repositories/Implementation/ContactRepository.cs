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
            string result = await ExecuteQueryFirstOrDefault<string>(sql, new {EmailAddress = email});

            return String.IsNullOrEmpty(result);
        }

        public async Task<List<Contact>> GetAllContacts()
        {
            return (List<Contact>)await GetAll();
        }

        Task<List<ClientDto>> IContactRepository.GetClientsByContactId(int contactId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ContactDto>> GetAllContactsWithClientCount()
        {
            throw new NotImplementedException();
        }
    }
}
