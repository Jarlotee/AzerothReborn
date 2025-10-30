using AzerothReborn.RealmServer.Domain;
using AzerothReborn.RealmServer.Handlers;
using AzerothReborn.RealmServer.Network;
using AzerothReborn.RealmServer.Requests;
using AzerothReborn.Tcp;
using Microsoft.Extensions.DependencyInjection;

namespace AzerothReborn.RealmServer.Setup;

internal static class Services
{
    public static void RegisterNetworkComponents(this IServiceCollection collection)
    {
        collection.AddSingleton<TcpServer>();
        collection.AddScoped<ITcpConnection, RealmTcpConnection>();
    }

    public static void RegisterNetworkHandlers(this IServiceCollection collection)
    {
        collection.AddScoped<CMSG_PING_Handler>();
    }

    public static void RegisterNetworkDispatchers(this IServiceCollection collection)
    {
        collection.AddScoped<IHandlerDispatcher, HandlerDispatcher<CMSG_PING, CMSG_PING_Handler>>();
    }

    public static void RegisterServices(this IServiceCollection collection)
    {
        collection.AddHostedService<RealmServer.Services.Realm>();
    }

    public static void RegisterRealmComponents(this IServiceCollection collection)
    {
        collection.AddScoped<IGameState, GameState>(); // does  nothing?
        collection.AddSingleton<ClientData.Loader>();
        collection.AddScoped<Client>();
        collection.AddSingleton<Realm>();
        collection.AddSingleton<Cluster>(); // was WorldServerClass, kind of just a helper
    }
}

//         builder.RegisterType<Globals.Functions>().As<Globals.Functions>().SingleInstance();
//         builder.RegisterType<Packets>().As<Packets>().SingleInstance();
//         builder.RegisterType<WcGuild>().As<WcGuild>().SingleInstance();
//         builder.RegisterType<WcNetwork>().As<WcNetwork>().SingleInstance();
//         builder.RegisterType<WcHandlers>().As<WcHandlers>().SingleInstance();
//         builder.RegisterType<WcHandlersAuth>().As<WcHandlersAuth>().SingleInstance();
//         builder.RegisterType<WcHandlersBattleground>().As<WcHandlersBattleground>().SingleInstance();
//         builder.RegisterType<WcHandlersChat>().As<WcHandlersChat>().SingleInstance();
//         builder.RegisterType<WcHandlersGroup>().As<WcHandlersGroup>().SingleInstance();
//         builder.RegisterType<WcHandlersGuild>().As<WcHandlersGuild>().SingleInstance();
//         builder.RegisterType<WcHandlersMisc>().As<WcHandlersMisc>().SingleInstance();
//         builder.RegisterType<WcHandlersMovement>().As<WcHandlersMovement>().SingleInstance();
//         builder.RegisterType<WcHandlersSocial>().As<WcHandlersSocial>().SingleInstance();
//         builder.RegisterType<WcHandlersTickets>().As<WcHandlersTickets>().SingleInstance();
//         builder.RegisterType<WsHandlerChannels>().As<WsHandlerChannels>().SingleInstance();
//         builder.RegisterType<WcHandlerCharacter>().As<WcHandlerCharacter>().SingleInstance();
//     }
// }