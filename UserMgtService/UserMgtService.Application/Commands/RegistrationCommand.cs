using UserMgtService.Application.DTOs;
using Mediator;

namespace UserMgtService.Application.Commands
{
    public sealed record  class RegistrationCommand(RegistrationDTO Data) : ICommand<AuthResponse>;
}
