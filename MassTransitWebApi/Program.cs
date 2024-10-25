using MassTransitWebApi.Consumers;
using MassTransit;
using Serilog;
using Microsoft.Extensions.Configuration; // Required for configuration access

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug() // Set the minimum log level
    .WriteTo.Console() // Log to console
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day) // Log to file with daily rotation
    .CreateLogger();

builder.Host.UseSerilog(); // Use Serilog for logging


// Configure MassTransit with In-Memory Transport
builder.Services.AddMassTransit(x =>
{
    // Adding the consumer
    x.AddConsumer<SampleMessageConsumer>();

    // Configure the in-memory transport
    x.UsingInMemory((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});

// This line adds the hosted service that will manage the bus lifecycle
builder.Services.AddMassTransitHostedService();

// Other service registrations
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();

app.Run();