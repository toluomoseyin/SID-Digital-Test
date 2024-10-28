using Hangfire;
using Hangfire.MemoryStorage;
using Hangfire.SqlServer;

namespace UserMgtService.API.Configuration
{
    public static class HangfireExtensions
    {
        public static void AddHangfireConfig(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                services.AddHangfire(config => config.UseMemoryStorage());
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
            }
        }

        public static void UseAppHangfireDashboard(this WebApplication app)
        {
            app.UseHangfireDashboard();
        }
    }
}
