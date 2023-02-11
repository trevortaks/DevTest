namespace DevTest.Client.Services
{
    public class ApiRoutes
    {
        public static string GetClientById = "/api/Clients/GetClientById/";
        public static string GetAllClients = "/api/Clients/GetAllClients";
        public static string GetAllClientsWithContactCount = "/api/Clients/GetAllClientsWithContactCount";
        public static string GetClientContacts = "/api/Clients/GetClientContacts/";
        public static string SaveClient = "/api/Clients/SaveClient";
        public static string UpdateClient = "/api/Clients/UpdateClient";

        public static string GetContactById = "/api/Contacts/GetContactById/";
        public static string GetAllContacts = "/api/Contacts/GetAllContacts";
        public static string GetAllContactsWithClientCount = "/api/Contacts/GetAllContactsWithClientCount";
        public static string GetContactClients = "/api/Contacts/GetContactClients/";
        public static string SaveContact = "/api/Contacts/SaveContact";
        public static string UpdateContact = "/api/Contacts/UpdateContact";
        internal static string GetUnlinkedClientsByContactId = "/api/Contacts/GetUnlinkedClientsByContactId";
        internal static string LinkClient = "/api/Links";
        internal static string Unlinkclient = "/api/Links";
        internal static string GetUnlinkedContactsByClientId = "/api/Clients/GetUnlinkedContactsByClientId";
    }
}
