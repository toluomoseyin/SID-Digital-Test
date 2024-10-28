using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AccountMgt.Application.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }



        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected Error Occurred.");
                await WriteResponseAsync(ex, context);
            }
        }

        protected virtual async Task WriteResponseAsync(Exception generalEx, HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;
            var dataResponse = new { status = false, Messgage = "Generic error occurred on server. Check logs for more info." };
            await context.Response.WriteAsync(JsonSerializer.Serialize(dataResponse));
        }

    }
}
