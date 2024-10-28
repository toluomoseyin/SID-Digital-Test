using System.ComponentModel.DataAnnotations;

namespace AccountMgt.Models.Models
{
    public class TransactionType
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
    }
}
