using AccountMgt.Application.Abstraction.Services;
using AccountMgt.Application.DTOs;
using System.Net.Http.Json;

namespace AccountMgt.Infrastructure.Services
{
    public class UserServiceClient : IUserServiceClient
    {
        private readonly HttpClient _httpClient;

        public UserServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserDto> GetUserByIdAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"/api/users/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to retrieve user information. Status: {response.StatusCode}");
            }

            return await response.Content.ReadFromJsonAsync<UserDto>();
        }
    }
}
