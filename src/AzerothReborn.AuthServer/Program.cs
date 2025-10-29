using AzerothReborn.AuthServer.Setup;
using Microsoft.Extensions.Hosting;

var builder = CustomHost.CreateApplicationBuilderWithAppsettings(args);
builder.HandleConfiguration();
builder.HandleDatabase();
builder.HandleLogging();

// services
builder.Services.RegisterNetworkHandlers();
builder.Services.RegisterNetworkDispatchers();
builder.Services.RegisterNetworkComponents();
builder.Services.RegisterServices();

Console.Title = "AzerothReborn AuthServer";

using IHost host = builder.Build();

await host.RunAsync();
