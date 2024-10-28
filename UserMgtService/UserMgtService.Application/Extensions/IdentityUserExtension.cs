using UserMgtService.Application.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace UserMgtService.Application.Extensions
{
    public static class IdentityUserExtension
    {
        public static IdentityUser ToEntity(this RegistrationDTO user)
        {
            return new IdentityUser
            {
                Email = user.Email,
                UserName = user.Username,
            };
        }

       
    }
}
