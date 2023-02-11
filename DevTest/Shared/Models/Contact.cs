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
        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter a surname")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "An email address is required"), EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string EmailAddress { get; set; }
    }
}
