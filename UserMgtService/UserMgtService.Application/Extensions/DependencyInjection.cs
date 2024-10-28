using Microsoft.Extensions.DependencyInjection;

namespace UserMgtService.Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMediatorApp(this IServiceCollection services)
        {
            return services.AddMediator(options =>
            {
                options.Namespace = "UserMgtService.Application.Mediator";
                options.ServiceLifetime = ServiceLifetime.Scoped;
            });
        }



    }
}
