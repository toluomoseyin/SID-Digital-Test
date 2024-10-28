using UserMgtService.Application.Abstraction.Repositories;
using UserMgtService.Application.DTOs;
using Mediator;
using Microsoft.AspNetCore.Routing;

namespace UserMgtService.Application.Commands.Handler
{
    public sealed class EmailConfirmationCommandHandler
        : ICommandHandler<EmailConfirmationCommand, AuthResponse>
    {
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly LinkGenerator _linkGenerator;

        public EmailConfirmationCommandHandler(
            IUserCommandRepository userCommandRepository,
            LinkGenerator linkGenerator)
        {
            _userCommandRepository = userCommandRepository;
            _linkGenerator = linkGenerator;
        }

        public async ValueTask<AuthResponse> Handle(
            EmailConfirmationCommand command, CancellationToken cancellationToken)
        {
            //var user = await _userCommandRepository.GetByUserIdAsync(command.userId);

            //if (user is null)
            //    return AuthResponse.Fail("User not found");

            //var token = await _userCommandRepository.GenerateEmailConfirmationTokenAsync(user);

            //var confirmationLink = _linkGenerator.GetUriByAction(
            //    action: "ConfirmEmail",
            //    controller: "Account",
            //    values: new { command.userId, token },
            //    scheme: command.HttpRequest.Scheme,
            //    host: command.HttpRequest.Host);
            throw new NotImplementedException();


        }
    }
}
