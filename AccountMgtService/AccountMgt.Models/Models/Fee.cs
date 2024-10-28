using System.ComponentModel.DataAnnotations;

namespace AccountMgt.Models.Models
{
    public class Fee
    {
        [Key]
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int TransactionId { get; set; }
        public Transaction BankTransferTransaction { get; set; }
    }
}
