using UserMgtService.Application.DTOs;
using Mediator;

namespace UserMgtService.Application.Commands
{
    public sealed record class LoginCommand(LoginDTO Data) : ICommand<AuthResponse<LoggedInUserDTO>>;
}
