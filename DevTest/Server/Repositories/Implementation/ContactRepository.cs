using DevTest.Server.Repositories.Contracts;
using DevTest.Shared.Models;

namespace DevTest.Server.Repositories.Implementation
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        public ContactRepository(IConfiguration config) : base(config)
        {
        }

        public async Task<List<Contact>> GetContactsByClientId(int contactId)
        {
            var sql = "SELECT C.* FROM tblContacts CO " +
                            "INNER JOIN tblClientContacts CC ON CC.ContactID = CO.ContactID" +
                            "INNER JOIN tblClients C ON C.ClientID = CC.ClientID" +
                            "WHERE CO.ContactID = @ContactID";
            var contacts = await ExecuteQuery<Contact>(sql, new { ContactID = contactId });
            return contacts.ToList();
        }

        public async Task<Contact> GetContactById(int contactId)
        {
            return await GetById(contactId);
        }

        public async Task<int> SaveContact(Contact contact)
        {
            return await Create(contact);
        }

        public async Task<List<Contact>> GetAllContacts()
        {
            return (List<Contact>)await GetAll();
        }
    }
}
