using AccountMgt.Application.DTOs;
using AccountMgt.Application.DTOs;
using Mediator;

namespace AccountMgt.Application.Commands
{
    public sealed record class CreateAccountCommand(CreateAccountDTO Data) : ICommand<AuthResponse<CreateAccountResponse>>;
}
