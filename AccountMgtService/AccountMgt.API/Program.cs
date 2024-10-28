using AccountMgt.API.Configuration;
using AccountMgt.Infrastructure;
using AccountMgt.Infrastructure.HostedService;
using AccountMgt.Infrastructure.Jobs;
using Hangfire;

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

builder.Services.AddMassTransitRabbitMq(builder.Configuration);

builder.Services.ConfigureServices(builder.Configuration);

builder.Services.ConfigureDatabase(builder.Configuration);

builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureAuthorization();

builder.Services.AddMemoryCache();

builder.Services.AddHostedService<SeedReportDataHostedService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.ConfigureMiddlewares();

// Schedule Daily Report Generation
var reportScheduler = app.Services.GetRequiredService<ReportScheduler>();
reportScheduler.ScheduleDailyReportGeneration();

app.UseHangfireDashboard();
app.MapControllers();
app.Run();
