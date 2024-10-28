using System.ComponentModel.DataAnnotations;

namespace AccountMgt.Models.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Transaction> BankTransferTransactions { get; set; }
    }
}
