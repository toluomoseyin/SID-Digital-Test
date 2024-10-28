using UserMgtService.Application.Abstraction.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace UserMgtService.Application.Middlewares
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IRateLimiterService _rateLimiter;
        private readonly ILogger<RateLimitingMiddleware> _logger;

        public RateLimitingMiddleware(
            RequestDelegate next,
            IRateLimiterService rateLimiter,
            ILogger<RateLimitingMiddleware> logger)
        {
            _next = next;
            _rateLimiter = rateLimiter;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();
            var apiKey = context.Request.Headers["X-Api-Key"].FirstOrDefault();
            var rateLimitKey = !string.IsNullOrEmpty(apiKey) ? apiKey : ipAddress;

            if (_rateLimiter.IsRateLimited(rateLimitKey))
            {
                _logger.LogInformation("Rate limit exceeded for key: {RateLimitKey}", rateLimitKey);
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                context.Response.Headers["Retry-After"] = "60";
                await context.Response.WriteAsync("Rate limit exceeded. Try again later.");
                return;
            }

            await _next(context);
        }
    }
}
