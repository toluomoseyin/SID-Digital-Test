namespace AccountMgt.API.Configuration
{
    public static class AuthorizationConfiguration
    {
        public static readonly string AdminOrFinancePolicy = "AdminOrFinancePolicy";


        public static IServiceCollection ConfigureAuthorization(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAuthorization(options =>
            {
                options.AddPolicy(AdminOrFinancePolicy, policy => policy.RequireRole("Admin", "Finance"));
            });

            return serviceCollection;
        }
    }
}
