using AccountMgt.Application.Extensions;
using Hangfire;
using Hangfire.MemoryStorage;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AccountMgt.Infrastructure
{
    public static class DependencyInjection
    {

        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatorApp();
#pragma warning disable CS8604 // Possible null reference argument.
            if (services.BuildServiceProvider().GetService<IHostEnvironment>().IsDevelopment())
            {
                services.AddHangfire(config => config.UseMemoryStorage());
                services.AddDbContext<AppDbContext>(options =>
                    options.UseInMemoryDatabase("InMemoryDb"));
            }
            else
            {
                services.AddHangfire(config =>
                    config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                          .UseSimpleAssemblyNameTypeSerializer()
                          .UseRecommendedSerializerSettings()
                          .UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
                          {
                              CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                              SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                              QueuePollInterval = TimeSpan.Zero,
                              UseRecommendedIsolationLevel = true,
                              DisableGlobalLocks = true
                          }));

                services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection")));
            }
#pragma warning restore CS8604 // Possible null reference argument.
        }
    }
}

