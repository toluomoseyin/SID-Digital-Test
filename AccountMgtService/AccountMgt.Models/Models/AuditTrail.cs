using System.ComponentModel.DataAnnotations;

namespace AccountMgt.Models.Models
{
    public class AuditTrail
    {
        [Key]
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public string ChangeDescription { get; set; }
        public DateTime ChangeDate { get; set; }
        public Transaction BankTransferTransaction { get; set; }
    }
}
