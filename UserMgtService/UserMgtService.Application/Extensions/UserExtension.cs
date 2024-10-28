using UserMgtService.Application.DTOs;

namespace UserMgtService.Application.Extensions
{
    public static class UserExtension
    {
        public static UserDTO ToEntity(this LoginDTO login, string role)
        {
            return new UserDTO
            {
                Username = login.Username,
                Role = role,
            };
        }
    }
}
