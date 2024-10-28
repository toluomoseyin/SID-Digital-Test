using System.ComponentModel.DataAnnotations;

namespace AccountMgt.Models.Models
{
    public class TransactionStatus
    {
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }
    }
}
