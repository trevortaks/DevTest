using DevTest.Shared.Models;

namespace DevTest.Server.Repositories.Contracts
{
    public interface IContactRepository
    {
        public Contact GetContactById(int contactId);
        public int SaveContact(Contact contact);
        public List<Shared.Models.Client> GetClientsByContactId(int contactId);

    }
}
