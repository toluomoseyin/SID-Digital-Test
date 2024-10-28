namespace UserMgtService.API.Configuration
{
    public static class LoggingExtensions
    {
        public static void AddAppLogging(this IServiceCollection services)
        {
            services.AddLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                logging.AddDebug();
                logging.SetMinimumLevel(LogLevel.Information);
            });
        }
    }
}
