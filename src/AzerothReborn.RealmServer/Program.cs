using AzerothReborn.RealmServer.Setup;
using Microsoft.Extensions.Hosting;

var builder = CustomHost.CreateApplicationBuilderWithAppsettings(args);
builder.HandleConfiguration();
builder.HandleDatabase();
builder.HandleLogging();

// services
builder.Services.RegisterNetworkHandlers();
builder.Services.RegisterNetworkComponents();
builder.Services.RegisterServices();
builder.Services.RegisterRealmComponents();

Console.Title = "AzerothReborn RealmServer";

using IHost host = builder.Build();

await host.RunAsync();