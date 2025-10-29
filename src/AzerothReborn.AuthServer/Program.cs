using AzerothReborn.AuthServer.Configuration;
using AzerothReborn.AuthServer.Domain;
using AzerothReborn.AuthServer.Handlers;
using AzerothReborn.AuthServer.Network;
using AzerothReborn.AuthServer.Requests;
using AzerothReborn.Tcp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var configuration = new ConfigurationManager();
configuration.AddJsonFile("appsettings.json", optional: false);
configuration.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}.json", optional: true, reloadOnChange: true);
configuration.AddEnvironmentVariables();

var builder = Host.CreateApplicationBuilder(new HostApplicationBuilderSettings
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory(),
    Configuration = configuration
});

builder.Services.Configure<Server>(builder.Configuration.GetSection("Server"));
builder.Services.AddDbContext<AzerothReborn.Data.Auth.Context>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("Auth")));

// logging
builder.Services.AddSerilog(c => c.ReadFrom.Configuration(builder.Configuration));
// services
builder.Services.AddHostedService<AzerothReborn.AuthServer.Services.Auth>();
builder.Services.AddScoped<ITcpConnection, TcpConnection>();
builder.Services.AddScoped<ClientState>();
builder.Services.AddScoped<RsLogonChallengeHandler>();
builder.Services.AddScoped<RsLogonProofHandler>();
builder.Services.AddScoped<AuthReconnectChallengeHandler>();
builder.Services.AddScoped<AuthRealmlistHandler>();
builder.Services.AddScoped<IHandlerDispatcher, HandlerDispatcher<RsLogonChallengeHandler, RsLogonChallengeRequest>>();
builder.Services.AddScoped<IHandlerDispatcher, HandlerDispatcher<RsLogonProofHandler, RsLogonProofRequest>>();
builder.Services.AddScoped<IHandlerDispatcher, HandlerDispatcher<AuthReconnectChallengeHandler, RsLogonChallengeRequest>>();
builder.Services.AddScoped<IHandlerDispatcher, HandlerDispatcher<AuthRealmlistHandler, AuthRealmlistRequest>>();
builder.Services.AddSingleton<TcpServer>();

Console.Title = "AzerothReborn AuthServer";

using IHost host = builder.Build();

await host.RunAsync();
