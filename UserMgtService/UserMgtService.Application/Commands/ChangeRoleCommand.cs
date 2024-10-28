using UserMgtService.Application.DTOs;
using Mediator;

namespace UserMgtService.Application.Commands
{
    public sealed record class ChangeRoleCommand(ChangeRoleDTO Data) : ICommand<AuthResponse>;
}
