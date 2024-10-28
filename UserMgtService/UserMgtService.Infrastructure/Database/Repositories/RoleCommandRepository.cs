using Microsoft.AspNetCore.Identity;

namespace UserMgtService.Infrastructure.Database.Repositories
{
    internal class RoleCommandRepository(RoleManager<IdentityRole> roleManager)
    {
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;


        public async Task<IdentityResult> AddAsync(IdentityRole role)
        {
            return await _roleManager.CreateAsync(role);
        }
    }
}
