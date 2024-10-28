namespace UserMgtService.Application.DTOs
{
    public sealed class LoggedInUserDTO(string userName, string role, string token) : UserDTO(userName, role)
    {
        public string Token { get; init; } = token;
    }
}
