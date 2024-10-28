using AccountMgt.Application.DTOs;

namespace AccountMgt.Application.Abstraction.Services
{
    public interface IUserServiceClient
    {
        Task<UserDto> GetUserByIdAsync(string userId);
    }
}
