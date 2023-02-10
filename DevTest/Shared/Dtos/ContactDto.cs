using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTest.Shared.Dtos
{
    public class ContactDto
    {
        public int ContactId { get; set; }
        public string FullName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public int LinkedClients { get; set; }
    }
}
