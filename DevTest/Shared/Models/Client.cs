using Dapper.Contrib.Extensions;

namespace DevTest.Shared.Models
{
    [Table("tblClients")]
    public class ClientDb
    {
        [Key]
        public int ClientID { get; set; }
        public string ClientCode { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
    }
}
