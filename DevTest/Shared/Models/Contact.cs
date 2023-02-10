using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations;
using KeyAttribute = Dapper.Contrib.Extensions.KeyAttribute;

namespace DevTest.Shared.Models
{
    [Table("tblContacts")]
    public class Contact
    {
        [KeyAttribute]
        public int ContactId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [EmailAddress,Required]
        public string EmailAddress { get; set; }
    }
}
