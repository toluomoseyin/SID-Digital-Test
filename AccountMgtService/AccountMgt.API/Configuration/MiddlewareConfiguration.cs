using AccountMgt.Application.Middlewares;

namespace AccountMgt.API.Configuration
{
    public static class MiddlewareConfiguration
    {
        public static void ConfigureMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<RateLimitingMiddleware>();
            app.UseHttpsRedirection();
        }
    }
}
