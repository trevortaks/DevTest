using DevTest.Shared.Dtos;
using DevTest.Shared.Models;

namespace DevTest.Server.Repositories.Contracts
{
    public interface IContactRepository
    {
        Task<List<ClientDto>> GetClientsByContactId(int contactId);
        Task<Contact> GetContactById(int contactId);
        Task<List<Contact>> GetAllContacts();
        Task<List<ContactDto>> GetAllContactsWithClientCount();
        Task<List<ClientDto>> GetUnlinkedClientsByContactId(int contactId);
        Task<int> SaveContact(Contact contact);

    }
}
