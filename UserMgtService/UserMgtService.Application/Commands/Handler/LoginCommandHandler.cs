using Ardalis.GuardClauses;
using UserMgtService.Application.Abstraction.Repositories;
using UserMgtService.Application.Abstraction.Services;
using UserMgtService.Application.DTOs;
using UserMgtService.Application.Extensions;
using Mediator;
using Validot;

namespace UserMgtService.Application.Commands.Handler
{
    public class LoginCommandHandler
        : ICommandHandler<LoginCommand, AuthResponse<LoggedInUserDTO>>
    {
       // private readonly IValidator<LoginDTO> _validator;
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly ITokenService _tokenService;

        public LoginCommandHandler(
           // IValidator<LoginDTO> validator,
            IUserCommandRepository userCommandRepository,
            ITokenService tokenService)
        {
          //  _validator = Guard.Against.Null(validator, nameof(validator));
            _userCommandRepository = Guard.Against.Null(userCommandRepository,
                nameof(userCommandRepository));
            _tokenService = Guard.Against.Null(tokenService, nameof(tokenService));
        }


        public async ValueTask<AuthResponse<LoggedInUserDTO>> Handle(
            LoginCommand command, CancellationToken cancellationToken)
        {
            //var vResult = _validator.Validate(command.Data);

            //if (vResult.AnyErrors)
            //    return AuthResponse<LoggedInUserDTO>.Failure("Validation failed");

            var user = await _userCommandRepository
                .GetByUsernameAsync(command.Data.Username);

            if (user is null)
                return AuthResponse<LoggedInUserDTO>
                    .Failure("Username or password is invalid");

            var signInResult = await _userCommandRepository
                .SignInAsync(user, command.Data.Password);

            if (!signInResult.Succeeded)
                return AuthResponse<LoggedInUserDTO>
                    .Failure("Username or password is invalid");

            var userRole = await _userCommandRepository.GetRoleAsync(user);

            var token = _tokenService
                .GenerateToken(command.Data.ToEntity(userRole ?? "User"));

            return AuthResponse<LoggedInUserDTO>.Failure("Success",
                new LoggedInUserDTO(command.Data.Username, userRole, token));
        }
    }
}
