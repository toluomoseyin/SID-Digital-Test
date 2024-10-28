using Hangfire;
using Hangfire.MemoryStorage;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using UserMgtService.API.Configuration;
using UserMgtService.Application.Abstraction.Repositories;
using UserMgtService.Application.Abstraction.Services;
using UserMgtService.Application.Extensions;
using UserMgtService.Application.Middlewares;
using UserMgtService.Infrastructure;
using UserMgtService.Infrastructure.Database.Repositories;
using UserMgtService.Infrastructure.HostedService;
using UserMgtService.Infrastructure.Options;
using UserMgtService.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.AddDebug();
    logging.SetMinimumLevel(LogLevel.Information);
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureSwagger();


builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureAuthorization();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 4;
    options.Password.RequiredUniqueChars = 1;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IRateLimiterService, RateLimiterService>();

builder.Services.AddHostedService<SeedUserRolesHostedService>();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddHangfire(config => config.UseMemoryStorage());
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseInMemoryDatabase("InMemoryDb"));
}
else
{
    builder.Services.AddHangfire(config =>
        config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
              .UseSimpleAssemblyNameTypeSerializer()
              .UseRecommendedSerializerSettings()
              .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
              {
                  CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                  SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                  QueuePollInterval = TimeSpan.Zero,
                  UseRecommendedIsolationLevel = true,
                  DisableGlobalLocks = true
              }));

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));
}

builder.Services.AddMediatorApp();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserCommandRepository, UserCommandRepository>();
builder.Services.Configure<UserMgtService
    .Infrastructure
    .Options
    .TokenOptions>(builder.Configuration.GetSection("Jwt"));
builder.Services.Configure<RateLimitingOption>(builder.Configuration.GetSection("RateLimitingOption"));




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();


app.UseMiddleware<ExceptionMiddleware>();

app.UseMiddleware<RateLimitingMiddleware>();

app.UseHangfireDashboard();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
