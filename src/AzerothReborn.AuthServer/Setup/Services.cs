using AzerothReborn.AuthServer.Domain;
using AzerothReborn.AuthServer.Handlers;
using AzerothReborn.AuthServer.Network;
using AzerothReborn.AuthServer.Requests;
using AzerothReborn.AuthServer.Services;
using AzerothReborn.Tcp;
using Microsoft.Extensions.DependencyInjection;

namespace AzerothReborn.AuthServer.Setup;


internal static class Services
{
    public static void RegisterNetworkComponents(this IServiceCollection collection)
    {
        collection.AddScoped<ITcpConnection, TcpConnection>();
        collection.AddScoped<ClientState>();
        collection.AddSingleton<TcpServer>();
    }

    public static void RegisterNetworkHandlers(this IServiceCollection collection)
    {
        collection.AddScoped<RsLogonChallengeHandler>();
        collection.AddScoped<RsLogonProofHandler>();
        collection.AddScoped<AuthReconnectChallengeHandler>();
        collection.AddScoped<AuthRealmlistHandler>();
    }

    public static void RegisterNetworkDispatchers(this IServiceCollection collection)
    {
        collection.AddScoped<IHandlerDispatcher, HandlerDispatcher<RsLogonChallengeHandler, RsLogonChallengeRequest>>();
        collection.AddScoped<IHandlerDispatcher, HandlerDispatcher<RsLogonProofHandler, RsLogonProofRequest>>();
        collection.AddScoped<IHandlerDispatcher, HandlerDispatcher<AuthReconnectChallengeHandler, RsLogonChallengeRequest>>();
        collection.AddScoped<IHandlerDispatcher, HandlerDispatcher<AuthRealmlistHandler, AuthRealmlistRequest>>();
    }

    public static void RegisterServices(this IServiceCollection collection)
    {
        collection.AddHostedService<Auth>();
    }
}