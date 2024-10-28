using System.ComponentModel.DataAnnotations;

namespace AccountMgt.Models.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public int UserId { get; set; }
        public int MerchantId { get; set; }
        public int CurrencyId { get; set; }
        public int StatusId { get; set; }
        public int BankId { get; set; }
        public int TransactionTypeId { get; set; }
        public User User { get; set; }
        public Merchant Merchant { get; set; }
        public Currency Currency { get; set; }
        public TransactionStatus Status { get; set; }
        public Bank Bank { get; set; }
        public TransactionType TransactionType { get; set; }
        public ICollection<Fee> Fees { get; set; }
        public ICollection<AuditTrail> AuditTrails { get; set; }
    }
}
