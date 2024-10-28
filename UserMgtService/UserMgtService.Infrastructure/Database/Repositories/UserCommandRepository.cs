using UserMgtService.Application.Abstraction.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace UserMgtService.Infrastructure.Database.Repositories
{
    public class UserCommandRepository(
        UserManager<IdentityUser> userManager, 
        SignInManager<IdentityUser> signInManager):IUserCommandRepository
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;


        public async Task<IdentityResult> AddAsync(IdentityUser user,string password)
        {
            return await _userManager.CreateAsync(user,password);
        }

        public async Task<IdentityResult> AddToRoleAsync(IdentityUser user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<IdentityUser?> GetByUsernameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<bool> IsInRoleAsync(IdentityUser user,string role)
        {
            return await _userManager.IsInRoleAsync(user, role);
        }
        public async Task<string?> GetRoleAsync(IdentityUser user)
        {
            return (await _userManager.GetRolesAsync(user)).FirstOrDefault();
        }

        public async Task<string?> GenerateEmailConfirmationTokenAsync(IdentityUser user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<IdentityUser?> GetByUserIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<List<IdentityUser>> GetAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<SignInResult> SignInAsync(IdentityUser user, string password)
        {
            return await _signInManager.PasswordSignInAsync(user,password,isPersistent:true,lockoutOnFailure:false);
        }
        public async Task<IdentityUser?> GetByUsernameEmailAsync(string username, string email)
        {
            return await _userManager.Users.FirstOrDefaultAsync(UserExists(username, email));
        }

        private static Expression<Func<IdentityUser, bool>> UserExists(string username, string email)
        {
            return x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
                                                                        || x.Email.Equals(email, StringComparison.OrdinalIgnoreCase);
        }

    }
}
