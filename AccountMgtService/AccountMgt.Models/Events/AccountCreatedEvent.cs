using AccountMgt.Models.Enums;

namespace AccountMgt.Models.Events
{
    public class AccountCreatedEvent
    {
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public AccountType AccountType { get; set; }
        public string UserId { get; set; }
    }
}
