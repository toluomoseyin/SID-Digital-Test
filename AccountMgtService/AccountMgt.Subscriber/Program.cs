using AccountMgt.Application.Abstraction.Repositories;
using AccountMgt.Infrastructure.Database.Repositories;
using AccountMgt.Subscriber.Consumers;
using AccountMgt.Subscriber.Extensions;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransitRabbitMq(builder.Configuration,
    (busConfig) =>
    {
        busConfig.AddConsumer<UserCreatedEventHandler>();
    },
    (ctx, cfg) => cfg.ReceiveEndpoint("aml", e =>
    {
        e.ConfigureConsumer<UserCreatedEventHandler>(ctx);
        e.UseMessageRetry(r => r.Interval(5, TimeSpan.FromSeconds(10)));
    }));

builder.Services.AddSingleton<IAccountRepository, AccountRepository>();
var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
