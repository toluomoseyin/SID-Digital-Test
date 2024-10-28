using System.ComponentModel.DataAnnotations;

namespace AccountMgt.Models.Models
{
    public class Bank
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
