namespace UserMgtService.Application.DTOs
{
    public class UserDTO
    {
        public string Username { get; init; } = string.Empty;
        public string Role { get; init; } = string.Empty;

        public UserDTO()
        {

        }
        public UserDTO(string userName, string role)
        {
            Username = userName;
            Role = role;
        }
    }
}
