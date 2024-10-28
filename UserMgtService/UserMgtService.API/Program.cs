using Hangfire;
using UserMgtService.API.Configuration;
using UserMgtService.Application.Abstraction.Repositories;
using UserMgtService.Application.Abstraction.Services;
using UserMgtService.Application.Extensions;
using UserMgtService.Application.Middlewares;
using UserMgtService.Infrastructure.Database.Repositories;
using UserMgtService.Infrastructure.HostedService;
using UserMgtService.Infrastructure.Options;
using UserMgtService.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAppLogging();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureSwagger();


builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureAuthorization();

builder.Services.AddAppIdentity();

builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IRateLimiterService, RateLimiterService>();

builder.Services.AddHostedService<SeedUserRolesHostedService>();
builder.Services.AddAppDatabase(builder.Configuration, builder.Environment);
builder.Services.AddHangfireConfig(builder.Configuration, builder.Environment);

builder.Services.AddMediatorApp();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserCommandRepository, UserCommandRepository>();
builder.Services.Configure<TokenOptions>(builder.Configuration.GetSection("Jwt"));
builder.Services
    .Configure<RateLimitingOption>(builder
    .Configuration.GetSection("RateLimitingOption"));

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

app.UseHttpsRedirection();

app.UseHangfireDashboard();

app.MapControllers();

app.Run();
