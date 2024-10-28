using Microsoft.EntityFrameworkCore;
using UserMgtService.Infrastructure;

namespace UserMgtService.API.Configuration
{
    public static class DatabaseExtensions
    {
        public static void AddAppDatabase(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                services.AddDbContext<AppDbContext>(options =>
                    options.UseInMemoryDatabase("InMemoryDb"));
            }
            else
            {
                services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection")));
            }
        }
    }
}
