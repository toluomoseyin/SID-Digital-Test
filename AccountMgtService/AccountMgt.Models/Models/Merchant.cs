using System.ComponentModel.DataAnnotations;

namespace AccountMgt.Models.Models
{
    public class Merchant
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Transaction> BankTransferTransactions { get; set; }
    }
}
