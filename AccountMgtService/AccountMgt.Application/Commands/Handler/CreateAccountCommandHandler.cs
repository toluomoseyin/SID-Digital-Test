using AccountMgt.Application.Abstraction.Repositories;
using AccountMgt.Application.Abstraction.Services;
using AccountMgt.Application.DTOs;
using AccountMgt.Application.Messages;
using AccountMgt.Models.Enums;
using AccountMgt.Models.Events;
using AccountMgt.Models.Models;
using AccountMgt.Application.DTOs;
using Ardalis.GuardClauses;
using MassTransit;
using Mediator;

namespace AccountMgt.Application.Commands.Handler
{
    public class CreateAccountCommandHandler
      : ICommandHandler<CreateAccountCommand, AuthResponse<CreateAccountResponse>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserServiceClient _userServiceClient;
        private readonly IPublishEndpoint _publishEndpoint;

        public CreateAccountCommandHandler(
            IAccountRepository accountRepository,
            IUserServiceClient userServiceClient)
        {
            _accountRepository = Guard.Against.Null(accountRepository,
                nameof(accountRepository));
            _userServiceClient = userServiceClient;
        }

        /// <summary>
        /// Handles the command to create an account.
        /// </summary>
        /// <param name="command">The command containing account creation data, including the user ID and account type.</param>
        /// <param name="cancellationToken">Cancellation token for the operation, allowing for the operation to be cancelled.</param>
        /// <returns>A response containing the result of the account creation operation, including success status and created account number.</returns>
        public async ValueTask<AuthResponse<CreateAccountResponse>> Handle(
            CreateAccountCommand command,
            CancellationToken cancellationToken)
        {
            UserDto user;
            try
            {
                // Attempt to get user information
                // from Usermangement service
                // using the provided UserId
                user = await _userServiceClient
                    .GetUserByIdAsync(command.Data.UserId);
            }
            catch (Exception ex)
            {
                return AuthResponse<CreateAccountResponse>
                    .Failure($"{ErrorMessages
                    .FAILED_TO_RETRIEVE_DATA} {ex.Message}");
            }

            var account = new Account
            {
                AccountNumber = Guid.NewGuid().ToString(),
                UserId = command.Data.UserId,
                Name = user.FullName,
                Email = user.Email,
                Type = command.Data.AccountType,
                CreatedDate = DateTime.UtcNow,
                Status = AccountStatus.Active
            };

            try
            {
                var createdAccount = await _accountRepository
                    .CreateAccountAsync(account);

                // Publish an event to other services
                // e.g Notification service indicating
                // that a new account has been created
                await _publishEndpoint.Publish(new AccountCreatedEvent
                {
                    AccountName = account.Name,
                    AccountNumber = account.AccountNumber,
                    AccountType = command.Data.AccountType,
                    UserId = command.Data.UserId,
                });

                var responseData = new CreateAccountResponse
                {
                    AccountNumber = createdAccount.AccountNumber,
                };

                return AuthResponse<CreateAccountResponse>
                    .Successful(SuccessMessages
                    .ACCOUNT_CREATED_SUCCESSFULLY, responseData);
            }
            catch (Exception ex)
            {
                return AuthResponse<CreateAccountResponse>
                    .Failure($"Account creation failed: {ex.Message}");
            }
        }

    }
}
