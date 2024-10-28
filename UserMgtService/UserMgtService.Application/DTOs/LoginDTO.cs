namespace UserMgtService.Application.DTOs
{

    public sealed class LoginDTO(string username, string password)
    {
        public string Username { get; init; } = username;
        public string Password { get; init; } = password;
    }
}
