using AccountMgt.Application.Abstraction.Repositories;
using AccountMgt.Application.Abstraction.Services;
using AccountMgt.Infrastructure.Database.Repositories;
using AccountMgt.Infrastructure.Jobs;
using AccountMgt.Infrastructure.Options;
using AccountMgt.Infrastructure.Services;
using Polly;
using Polly.Extensions.Http;

namespace AccountMgt.API.Configuration
{
    public static class ServiceConfiguration
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IUserServiceClient, UserServiceClient>(client =>
            {
                client.BaseAddress = new Uri("https://userservice.example.com");
                client.Timeout = TimeSpan.FromSeconds(10);
            })
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetCircuitBreakerPolicy());

            services.Configure<TokenOptions>(configuration.GetSection("Jwt"));
            services.Configure<RateLimitingOption>(configuration
                .GetSection("RateLimitingOption"));
            services.AddSingleton<IRateLimiterService, RateLimiterService>();

            services.AddScoped<IReportGenerator, LinqReportGeneratorRepository>();
            services.AddScoped<IPdfReportService, PdfReportService>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddSingleton<ReportScheduler>();

        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(
                    retryCount: 3,
                    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                );
        }

        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: 2,
                    durationOfBreak: TimeSpan.FromSeconds(30)
                );
        }
    }
}
