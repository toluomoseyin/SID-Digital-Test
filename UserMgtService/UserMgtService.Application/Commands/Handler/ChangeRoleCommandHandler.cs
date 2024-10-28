using Ardalis.GuardClauses;
using Mediator;
using UserMgtService.Application.Abstraction.Repositories;
using UserMgtService.Application.DTOs;

namespace UserMgtService.Application.Commands.Handler
{
    public class ChangeRoleCommandHandler
      : ICommandHandler<ChangeRoleCommand, AuthResponse>
    {
        private readonly IUserCommandRepository _userCommandRepository;

        public ChangeRoleCommandHandler(
            IUserCommandRepository userCommandRepository
            )
        {
            _userCommandRepository = Guard.Against.Null(userCommandRepository,
                nameof(userCommandRepository));
        }

        public async ValueTask<AuthResponse> Handle(ChangeRoleCommand command, CancellationToken cancellationToken)
        {
            var user = await _userCommandRepository.GetByUsernameAsync(command.Data.userName);

            if (user is null)
                return AuthResponse.Fail("user not found");

            var isInRole = await _userCommandRepository.IsInRoleAsync(user, command.Data.roleName);

            if (isInRole)
                return AuthResponse.Fail($"User is already in role {command.Data.roleName}");

            var roleAssignmentResult = await _userCommandRepository.AddToRoleAsync(user, command.Data.roleName);

            if (!roleAssignmentResult.Succeeded)
                return AuthResponse.Fail(string.Join(", ",
                   roleAssignmentResult.Errors.Select(e => e.Description)));

            return AuthResponse.Successful(
               $"Account successfully added to role {command.Data.roleName}.");
        }
    }
}
