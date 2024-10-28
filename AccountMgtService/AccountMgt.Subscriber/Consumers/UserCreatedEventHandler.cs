using AccountMgt.Application.Abstraction.Repositories;
using AccountMgt.Models.Enums;
using AccountMgt.Models.Events;
using AccountMgt.Models.Models;
using MassTransit;

namespace AccountMgt.Subscriber.Consumers
{

    public class UserCreatedEventHandler : IConsumer<UserCreatedEvent>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public UserCreatedEventHandler(
            IAccountRepository accountRepository,
            IPublishEndpoint publishEndpoint)
        {
            _accountRepository = accountRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            try
            {
                var account = new Account
                {
                    UserId = context.Message.UserId,
                    Name = context.Message.FullName,
                    Email = context.Message.Email,
                    Type = AccountType.Saving,
                    CreatedDate = DateTime.UtcNow,
                    Status = AccountStatus.Active
                };
                var createdAccount = await _accountRepository
                      .CreateAccountAsync(account);

                await _publishEndpoint.Publish(new AccountCreatedEvent
                {
                    AccountName = account.Name,
                    AccountNumber = account.Name,
                    AccountType = AccountType.Saving,
                    UserId = context.Message.UserId,
                });

            }
            catch (Exception ex)
            {

                throw;
            }

        }

    }
}
