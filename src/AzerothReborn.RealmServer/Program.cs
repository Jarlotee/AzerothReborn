using AzerothReborn.RealmServer.Setup;
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
builder.Services.RegisterRealmComponents();

Console.Title = "AzerothReborn RealmServer";

using IHost host = builder.Build();

await host.RunAsync();


// builder.RegisterModule<LegacyWorldModule>();

// var container = builder.Build();
// var legacyWorldCluster = container.Resolve<LegacyWorldCluster>();
// WorldServiceLocator.Container = container;
// var worldServer = container.Resolve<WorldServer>();

// await legacyWorldCluster.StartAsync();
// await worldServer.StartAsync();