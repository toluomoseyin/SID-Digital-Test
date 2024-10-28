using AccountMgt.Infrastructure.Options;
using MassTransit;

namespace AccountMgt.API.Configuration
{
    public static class MassTransitExtensions
    {
        public static IServiceCollection AddMassTransitRabbitMq(this IServiceCollection services, IConfiguration configuration,
            Action<IBusRegistrationConfigurator>? busConfig = null, Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator>? setup = null)
        {
            services.AddMassTransit(opts =>
            {
                busConfig?.Invoke(opts);

                opts.UsingRabbitMq((ctx, cfg) =>
                {
                    var config = configuration.GetSection(nameof(RabbitMqConfig)).Get<RabbitMqConfig>();
                    cfg.Host(config.Host, host =>
                    {
                        host.Username(config.Username);
                        host.Password(config.Password);
                    });

                    setup?.Invoke(ctx, cfg);
                });
                opts.AddPublishMessageScheduler();
            });

            return services;
        }
    }
}
