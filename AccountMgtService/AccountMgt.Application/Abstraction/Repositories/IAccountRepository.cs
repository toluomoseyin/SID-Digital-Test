using AccountMgt.Models.Models;

namespace AccountMgt.Application.Abstraction.Repositories
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<Account> GetAccountByIdAsync(int id);
        Task<Account> GetAccountByAccountNumberAsync(string accountNumber);
        Task<Account> CreateAccountAsync(Account account);
        Task UpdateAccountAsync(Account account);
        Task<bool> DeleteAccountAsync(int id);
    }
}
