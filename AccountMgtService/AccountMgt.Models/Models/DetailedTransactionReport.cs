using System.ComponentModel.DataAnnotations;

namespace AccountMgt.Models.Models
{

    public class DetailedTransactionReport
    {
        [Key]
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? MerchantName { get; set; }
        public string? CurrencyCode { get; set; }
        public string? TransactionType { get; set; }
        public decimal TotalAmount { get; set; }
        public int TransactionCount { get; set; }
        public decimal AverageAmount { get; set; }
        public decimal TotalFees { get; set; }
        public string? LatestChangeDescription { get; set; }
        public DateTime LastTransactionDate { get; set; }
        public string? TimeInterval { get; set; }
    }
}
