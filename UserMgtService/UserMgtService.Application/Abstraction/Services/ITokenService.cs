using UserMgtService.Application.DTOs;

namespace UserMgtService.Application.Abstraction.Services
{
    public interface ITokenService
    {
        public string GenerateToken(UserDTO user);
    }
}
