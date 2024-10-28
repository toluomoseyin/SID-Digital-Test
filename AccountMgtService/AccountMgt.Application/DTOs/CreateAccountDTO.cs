using AccountMgt.Models.Enums;

namespace AccountMgt.Application.DTOs
{
    public class CreateAccountDTO
    {
        public AccountType AccountType { get; set; }
        public string UserId { get; set; }
    }
}
