using Microsoft.AspNetCore.Identity;

namespace UserMgtService.Application.Abstraction.Repositories
{
    public interface IUserCommandRepository
    {
        Task<List<IdentityUser>> GetAsync();
        Task<IdentityUser> GetByUserIdAsync(string userId);
        Task<IdentityUser> GetByUsernameAsync(string username);
        Task<IdentityResult> AddToRoleAsync(IdentityUser user, string role);
        Task<IdentityResult> AddAsync(IdentityUser user, string password);
        Task<IdentityUser?> GetByUsernameEmailAsync(string username, string email);
        Task<SignInResult> SignInAsync(IdentityUser user, string password);
        Task<string?> GetRoleAsync(IdentityUser user);
        Task<string?> GenerateEmailConfirmationTokenAsync(IdentityUser user);
        Task<bool> IsInRoleAsync(IdentityUser user, string role);

    }
}
