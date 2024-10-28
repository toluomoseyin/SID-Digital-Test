using System.ComponentModel.DataAnnotations;
using AccountMgt.Models.Enums;

namespace AccountMgt.Models.Models
{

    public class Account
    {
        [Key]
        public int Id { get; set; }
        public string AccountNumber { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }
        public AccountType Type { get; set; }

        public string Email { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public AccountStatus Status { get; set; }

        private string GenerateAccountNumber()
        {
            return $"{new Random().Next(1000000000, 999999999)}";
        }

        public Account()
        {
            CreatedDate = DateTime.UtcNow;
            Status = AccountStatus.Active;
            AccountNumber = GenerateAccountNumber();
        }
    }
}
