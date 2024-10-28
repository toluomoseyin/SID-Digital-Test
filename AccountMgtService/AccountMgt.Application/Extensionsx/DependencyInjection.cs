using Microsoft.Extensions.DependencyInjection;

namespace AccountMgt.Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMediatorApp(this IServiceCollection services)
        {
            return services.AddMediator(options =>
            {
                options.Namespace = "AccountMgt.Application.Mediator";
                options.ServiceLifetime = ServiceLifetime.Scoped;
            });
        }



    }
}
