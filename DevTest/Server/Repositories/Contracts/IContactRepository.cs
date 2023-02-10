using DevTest.Shared.Models;

namespace DevTest.Server.Repositories.Contracts
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetContactsByClientId(int contactId);
        Task<Contact> GetContactById(int contactId);

        Task<List<Contact>> GetAllContacts();
        Task<int> SaveContact(Contact contact);

    }
}
