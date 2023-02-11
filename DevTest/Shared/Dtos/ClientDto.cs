using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTest.Shared.Dtos
{
    public class ClientDto
    {
        public int ClientID { get; set; }
        public string ClientCode { get; set; }
        public string Name { get; set; }
        public int LinkedContacts { get; set; }
    }
}
