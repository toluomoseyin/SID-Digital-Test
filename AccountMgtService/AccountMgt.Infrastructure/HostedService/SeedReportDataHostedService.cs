using AccountMgt.Models.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AccountMgt.Infrastructure.HostedService
{

    public sealed class SeedReportDataHostedService
        : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<SeedReportDataHostedService> _logger;

        public SeedReportDataHostedService(
            IServiceScopeFactory scopeFactory,
            ILogger<SeedReportDataHostedService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            await SeedComplextQueryDataAsync();
        }




        private static List<User> GenerateUsers(int count)
        {
            var users = new List<User>();
            for (int i = 1; i <= count; i++)
            {
                users.Add(
                    new User
                    {
                        Id = i,
                        Name = $"User {i}",
                        Email = $"user{i}@example.com"
                    });
            }
            return users;
        }

        private static List<Merchant> GenerateMerchants(int count)
        {
            var merchants = new List<Merchant>();
            for (int i = 1; i <= count; i++)
            {
                merchants.Add(
                    new Merchant
                    {
                        Id = i,
                        Name = $"Merchant {i}"
                    });
            }
            return merchants;
        }

        private static List<Currency> GenerateCurrencies(int count)
        {
            var currencies = new List<Currency>();

            var currencyNames = new[] { "USD", "EUR", "GBP" };

            for (int i = 0; i < count; i++)
            {
                currencies.Add(
                    new Currency
                    {
                        Id = i + 1,
                        Code = currencyNames[i],
                        Name = $"{currencyNames[i]} Currency"
                    });
            }
            return currencies;
        }

        private static List<TransactionStatus> GenerateTransactionStatuses(int count)
        {
            var statuses = new List<TransactionStatus>();
            var statusNames = new[]
            {
                "Completed",
                "Pending",
                "Failed",
                "Refunded",
                "Cancelled"
            };

            for (int i = 0; i < count; i++)
            {
                statuses.Add(
                    new TransactionStatus
                    {
                        Id = i + 1,
                        Status = statusNames[i]
                    });
            }
            return statuses;
        }

        private static List<Bank> GenerateBanks(int count)
        {
            var banks = new List<Bank>();
            for (int i = 1; i <= count; i++)
            {
                banks.Add(new Bank { Id = i, Name = $"Bank {i}" });
            }
            return banks;
        }

        private static List<TransactionType> GenerateTransactionTypes(int count)
        {
            var types = new List<TransactionType>();

            var typeNames = new[]
            {
                "Intra-bank Transfer",
                "Inter-bank Transfer",
                "Bill Payment",
                "Airtime Purchase"
            };

            for (int i = 0; i < count; i++)
            {
                types.Add(
                    new TransactionType
                    {
                        Id = i + 1,
                        Type = typeNames[i]
                    });
            }
            return types;
        }

        private static List<Transaction> GenerateTransactions(
            int totalCount,
            int userCount,
            int merchantCount,
            int currencyCount,
            int statusCount,
            int bankCount,
            int typeCount)
        {
            var transactions = new List<Transaction>();
            var random = new Random();
            for (int i = 1; i <= totalCount; i++)
            {
                transactions.Add(new Transaction
                {
                    Id = i,
                    Amount = Convert.ToDecimal(
                        Math.Round(random.NextDouble() * 1000, 2)),
                    TransactionDate = DateTime
                    .UtcNow.AddDays(-random.Next(1, 30)),
                    UserId = random.Next(1, userCount + 1),
                    MerchantId = random.Next(1, merchantCount + 1),
                    CurrencyId = random.Next(1, currencyCount + 1),
                    StatusId = random.Next(1, statusCount + 1),
                    BankId = random.Next(1, bankCount + 1),
                    TransactionTypeId = random.Next(1, typeCount + 1)
                });
            }
            return transactions;
        }

        private async Task SeedComplextQueryDataAsync()
        {
            using var scope = _scopeFactory.CreateScope();

            var dbContext = scope
                .ServiceProvider
                .GetRequiredService<AppDbContext>();

            // await dbContext.Database.MigrateAsync(cancellationToken);

            var users = GenerateUsers(10);

            var merchants = GenerateMerchants(5);

            var currencies = GenerateCurrencies(3);

            var transactionStatuses = GenerateTransactionStatuses(5);

            var banks = GenerateBanks(3);

            var transactionTypes = GenerateTransactionTypes(4);

            var transactions = GenerateTransactions(
                1000,
                users.Count,
                merchants.Count,
                currencies.Count,
                transactionStatuses.Count,
                banks.Count,
                transactionTypes.Count);

            dbContext.Users.AddRange(users);
            dbContext.Merchants.AddRange(merchants);
            dbContext.Currencies.AddRange(currencies);
            dbContext.TransactionStatuses.AddRange(transactionStatuses);
            dbContext.Banks.AddRange(banks);
            dbContext.TransactionTypes.AddRange(transactionTypes);
            dbContext.Transactions.AddRange(transactions);

            await dbContext.SaveChangesAsync();
        }
    }

}
