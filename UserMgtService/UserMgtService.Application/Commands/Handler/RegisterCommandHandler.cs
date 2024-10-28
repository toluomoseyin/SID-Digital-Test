using Ardalis.GuardClauses;
using UserMgtService.Application.Abstraction.Repositories;
using UserMgtService.Application.DTOs;
using UserMgtService.Application.Extensions;
using Mediator;
using Validot;

namespace UserMgtService.Application.Commands.Handler
{
    public sealed class RegisterCommandHandler
        : ICommandHandler<RegistrationCommand, AuthResponse>
    {
       // private readonly IValidator<RegistrationDTO> _validator;
        private readonly IUserCommandRepository _userCommandRepository;

        public RegisterCommandHandler(
         //   IValidator<RegistrationDTO> validator,
            IUserCommandRepository userCommandRepository)
        {
           // _validator = Guard.Against.Null(validator, nameof(validator));
            _userCommandRepository = Guard.Against.Null(userCommandRepository, nameof(userCommandRepository));
        }

        public async ValueTask<AuthResponse> Handle(
            RegistrationCommand command, CancellationToken cancellationToken)
        {
            //var validationResult = _validator.Validate(command.Data, failFast: true);
            //if (validationResult.AnyErrors)
            //    return AuthResponse.Fail("Validation failed.");

            var existingUser = await _userCommandRepository
                .GetByUsernameEmailAsync(command.Data.Username, command.Data.Email);

            if (existingUser != null)
                return AuthResponse.Fail("Username or email already in use.");

            var creationResult = await _userCommandRepository
                .AddAsync(command.Data.ToEntity(), command.Data.Password);

            if (!creationResult.Succeeded)
                return AuthResponse.Fail(string.Join(", ",
                    creationResult.Errors.Select(e => e.Description)));

            var roleAssignmentResult = await _userCommandRepository
                .AddToRoleAsync(command.Data.ToEntity(), "User");

            if (!roleAssignmentResult.Succeeded)
                return AuthResponse.Fail(string.Join(", ",
                    roleAssignmentResult.Errors.Select(e => e.Description)));

            return AuthResponse.Successful(
                $"Account successfully created for {command.Data.Username}.");
        }
    }
}
