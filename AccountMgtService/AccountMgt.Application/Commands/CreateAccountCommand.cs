using AccountMgt.Application.DTOs;
using Mediator;

namespace AccountMgt.Application.Commands
{
    public sealed record class CreateAccountCommand(CreateAccountDTO Data, string UserId) : ICommand<AuthResponse<CreateAccountResponse>>;
}
