using AzerothReborn.RealmServer.Domain;
using AzerothReborn.RealmServer.Network;
using AzerothReborn.RealmServer.Server;
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
        collection.AddSingleton<Handlers.HandlerProvider>();
        collection.AddSingleton<Handlers.CMSG_PING>();
        collection.AddSingleton<Handlers.CMSG_NEXT_CINEMATIC_CAMERA>();
        collection.AddSingleton<Handlers.CMSG_QUERY_TIME>();
        collection.AddSingleton<Handlers.CMSG_PLAYED_TIME>();
        collection.AddSingleton<Handlers.CMSG_INSPECT>();
        collection.AddSingleton<Handlers.CMSG_UPDATE_ACCOUNT_DATA>();
        collection.AddSingleton<Handlers.CMSG_REQUEST_ACCOUNT_DATA>();
        collection.AddSingleton<Handlers.CMSG_LOGOUT_CANCEL>();
        collection.AddSingleton<Handlers.CMSG_CANCEL_TRADE>();
        collection.AddSingleton<Handlers.CMSG_NAME_QUERY>();
        collection.AddSingleton<Handlers.CMSG_WHOIS>();
        collection.AddSingleton<Handlers.CMSG_AUTH_SESSION>();
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
        collection.AddSingleton<Realm>(); // was WorldCluster
        collection.AddSingleton<Cluster>(); // was WorldServerClass, kind of just a helper
    }
}


//         // NOTE: These opcodes below must be exluded form WorldServer
//         _[Opcodes.CMSG_CHAR_ENUM] = WcHandlersAuth.On_CMSG_CHAR_ENUM;
//         _[Opcodes.CMSG_CHAR_CREATE] = WcHandlersAuth.On_CMSG_CHAR_CREATE;
//         _[Opcodes.CMSG_CHAR_DELETE] = WcHandlersAuth.On_CMSG_CHAR_DELETE;
//         _[Opcodes.CMSG_CHAR_RENAME] = WcHandlersAuth.On_CMSG_CHAR_RENAME;
//         _[Opcodes.CMSG_PLAYER_LOGIN] = WcHandlersAuth.On_CMSG_PLAYER_LOGIN;
//         _[Opcodes.CMSG_PLAYER_LOGOUT] = WcHandlersAuth.On_CMSG_PLAYER_LOGOUT;
//         _[Opcodes.MSG_MOVE_WORLDPORT_ACK] = WcHandlersAuth.On_MSG_MOVE_WORLDPORT_ACK;

//         _[Opcodes.CMSG_WHO] = WcHandlersSocial.On_CMSG_WHO;
//         _[Opcodes.CMSG_BUG] = WcHandlersTickets.On_CMSG_BUG;
//         _[Opcodes.CMSG_GMTICKET_GETTICKET] = WcHandlersTickets.On_CMSG_GMTICKET_GETTICKET;
//         _[Opcodes.CMSG_GMTICKET_CREATE] = WcHandlersTickets.On_CMSG_GMTICKET_CREATE;
//         _[Opcodes.CMSG_GMTICKET_SYSTEMSTATUS] = WcHandlersTickets.On_CMSG_GMTICKET_SYSTEMSTATUS;
//         _[Opcodes.CMSG_GMTICKET_DELETETICKET] = WcHandlersTickets.On_CMSG_GMTICKET_DELETETICKET;
//         _[Opcodes.CMSG_GMTICKET_UPDATETEXT] = WcHandlersTickets.On_CMSG_GMTICKET_UPDATETEXT;

//         _[Opcodes.CMSG_BATTLEMASTER_JOIN] = WcHandlersBattleground.On_CMSG_BATTLEMASTER_JOIN;
//         _[Opcodes.CMSG_BATTLEFIELD_PORT] = WcHandlersBattleground.On_CMSG_BATTLEFIELD_PORT;
//         _[Opcodes.CMSG_LEAVE_BATTLEFIELD] = WcHandlersBattleground.On_CMSG_LEAVE_BATTLEFIELD;
//         _[Opcodes.MSG_BATTLEGROUND_PLAYER_POSITIONS] = WcHandlersBattleground.On_MSG_BATTLEGROUND_PLAYER_POSITIONS;

//         _[Opcodes.CMSG_FRIEND_LIST] = WcHandlersSocial.On_CMSG_FRIEND_LIST;
//         _[Opcodes.CMSG_ADD_FRIEND] = WcHandlersSocial.On_CMSG_ADD_FRIEND;
//         _[Opcodes.CMSG_ADD_IGNORE] = WcHandlersSocial.On_CMSG_ADD_IGNORE;
//         _[Opcodes.CMSG_DEL_FRIEND] = WcHandlersSocial.On_CMSG_DEL_FRIEND;
//         _[Opcodes.CMSG_DEL_IGNORE] = WcHandlersSocial.On_CMSG_DEL_IGNORE;

//         _[Opcodes.CMSG_REQUEST_RAID_INFO] = WcHandlersGroup.On_CMSG_REQUEST_RAID_INFO;
//         _[Opcodes.CMSG_GROUP_INVITE] = WcHandlersGroup.On_CMSG_GROUP_INVITE;
//         _[Opcodes.CMSG_GROUP_CANCEL] = WcHandlersGroup.On_CMSG_GROUP_CANCEL;
//         _[Opcodes.CMSG_GROUP_ACCEPT] = WcHandlersGroup.On_CMSG_GROUP_ACCEPT;
//         _[Opcodes.CMSG_GROUP_DECLINE] = WcHandlersGroup.On_CMSG_GROUP_DECLINE;
//         _[Opcodes.CMSG_GROUP_UNINVITE] = WcHandlersGroup.On_CMSG_GROUP_UNINVITE;
//         _[Opcodes.CMSG_GROUP_UNINVITE_GUID] = WcHandlersGroup.On_CMSG_GROUP_UNINVITE_GUID;
//         _[Opcodes.CMSG_GROUP_DISBAND] = WcHandlersGroup.On_CMSG_GROUP_DISBAND;
//         _[Opcodes.CMSG_GROUP_RAID_CONVERT] = WcHandlersGroup.On_CMSG_GROUP_RAID_CONVERT;
//         _[Opcodes.CMSG_GROUP_SET_LEADER] = WcHandlersGroup.On_CMSG_GROUP_SET_LEADER;
//         _[Opcodes.CMSG_GROUP_CHANGE_SUB_GROUP] = WcHandlersGroup.On_CMSG_GROUP_CHANGE_SUB_GROUP;
//         _[Opcodes.CMSG_GROUP_SWAP_SUB_GROUP] = WcHandlersGroup.On_CMSG_GROUP_SWAP_SUB_GROUP;
//         _[Opcodes.CMSG_LOOT_METHOD] = WcHandlersGroup.On_CMSG_LOOT_METHOD;
//         _[Opcodes.MSG_MINIMAP_PING] = WcHandlersGroup.On_MSG_MINIMAP_PING;
//         _[Opcodes.MSG_RANDOM_ROLL] = WcHandlersGroup.On_MSG_RANDOM_ROLL;
//         _[Opcodes.MSG_RAID_READY_CHECK] = WcHandlersGroup.On_MSG_RAID_READY_CHECK;
//         _[Opcodes.MSG_RAID_ICON_TARGET] = WcHandlersGroup.On_MSG_RAID_ICON_TARGET;
//         _[Opcodes.CMSG_REQUEST_PARTY_MEMBER_STATS] = WcHandlersGroup.On_CMSG_REQUEST_PARTY_MEMBER_STATS;
//         _[Opcodes.CMSG_TURN_IN_PETITION] = WcHandlersGuild.On_CMSG_TURN_IN_PETITION;
//         _[Opcodes.CMSG_GUILD_QUERY] = WcHandlersGuild.On_CMSG_GUILD_QUERY;
//         _[Opcodes.CMSG_GUILD_CREATE] = WcHandlersGuild.On_CMSG_GUILD_CREATE;
//         _[Opcodes.CMSG_GUILD_DISBAND] = WcHandlersGuild.On_CMSG_GUILD_DISBAND;
//         _[Opcodes.CMSG_GUILD_ROSTER] = WcHandlersGuild.On_CMSG_GUILD_ROSTER;
//         _[Opcodes.CMSG_GUILD_INFO] = WcHandlersGuild.On_CMSG_GUILD_INFO;
//         _[Opcodes.CMSG_GUILD_RANK] = WcHandlersGuild.On_CMSG_GUILD_RANK;
//         _[Opcodes.CMSG_GUILD_ADD_RANK] = WcHandlersGuild.On_CMSG_GUILD_ADD_RANK;
//         _[Opcodes.CMSG_GUILD_DEL_RANK] = WcHandlersGuild.On_CMSG_GUILD_DEL_RANK;
//         _[Opcodes.CMSG_GUILD_PROMOTE] = WcHandlersGuild.On_CMSG_GUILD_PROMOTE;
//         _[Opcodes.CMSG_GUILD_DEMOTE] = WcHandlersGuild.On_CMSG_GUILD_DEMOTE;
//         _[Opcodes.CMSG_GUILD_LEADER] = WcHandlersGuild.On_CMSG_GUILD_LEADER;
//         _[Opcodes.MSG_SAVE_GUILD_EMBLEM] = WcHandlersGuild.On_MSG_SAVE_GUILD_EMBLEM;
//         _[Opcodes.CMSG_GUILD_SET_OFFICER_NOTE] = WcHandlersGuild.On_CMSG_GUILD_SET_OFFICER_NOTE;
//         _[Opcodes.CMSG_GUILD_SET_PUBLIC_NOTE] = WcHandlersGuild.On_CMSG_GUILD_SET_PUBLIC_NOTE;
//         _[Opcodes.CMSG_GUILD_MOTD] = WcHandlersGuild.On_CMSG_GUILD_MOTD;
//         _[Opcodes.CMSG_GUILD_INVITE] = WcHandlersGuild.On_CMSG_GUILD_INVITE;
//         _[Opcodes.CMSG_GUILD_ACCEPT] = WcHandlersGuild.On_CMSG_GUILD_ACCEPT;
//         _[Opcodes.CMSG_GUILD_DECLINE] = WcHandlersGuild.On_CMSG_GUILD_DECLINE;
//         _[Opcodes.CMSG_GUILD_REMOVE] = WcHandlersGuild.On_CMSG_GUILD_REMOVE;
//         _[Opcodes.CMSG_GUILD_LEAVE] = WcHandlersGuild.On_CMSG_GUILD_LEAVE;

//         _[Opcodes.CMSG_CHAT_IGNORED] = WcHandlersChat.On_CMSG_CHAT_IGNORED;
//         _[Opcodes.CMSG_MESSAGECHAT] = WcHandlersChat.On_CMSG_MESSAGECHAT;
//         _[Opcodes.CMSG_JOIN_CHANNEL] = WcHandlersChat.On_CMSG_JOIN_CHANNEL;
//         _[Opcodes.CMSG_LEAVE_CHANNEL] = WcHandlersChat.On_CMSG_LEAVE_CHANNEL;
//         _[Opcodes.CMSG_CHANNEL_LIST] = WcHandlersChat.On_CMSG_CHANNEL_LIST;
//         _[Opcodes.CMSG_CHANNEL_PASSWORD] = WcHandlersChat.On_CMSG_CHANNEL_PASSWORD;
//         _[Opcodes.CMSG_CHANNEL_SET_OWNER] = WcHandlersChat.On_CMSG_CHANNEL_SET_OWNER;
//         _[Opcodes.CMSG_CHANNEL_OWNER] = WcHandlersChat.On_CMSG_CHANNEL_OWNER;
//         _[Opcodes.CMSG_CHANNEL_MODERATOR] = WcHandlersChat.On_CMSG_CHANNEL_MODERATOR;
//         _[Opcodes.CMSG_CHANNEL_UNMODERATOR] = WcHandlersChat.On_CMSG_CHANNEL_UNMODERATOR;
//         _[Opcodes.CMSG_CHANNEL_MUTE] = WcHandlersChat.On_CMSG_CHANNEL_MUTE;
//         _[Opcodes.CMSG_CHANNEL_UNMUTE] = WcHandlersChat.On_CMSG_CHANNEL_UNMUTE;
//         _[Opcodes.CMSG_CHANNEL_KICK] = WcHandlersChat.On_CMSG_CHANNEL_KICK;
//         _[Opcodes.CMSG_CHANNEL_INVITE] = WcHandlersChat.On_CMSG_CHANNEL_INVITE;
//         _[Opcodes.CMSG_CHANNEL_BAN] = WcHandlersChat.On_CMSG_CHANNEL_BAN;
//         _[Opcodes.CMSG_CHANNEL_UNBAN] = WcHandlersChat.On_CMSG_CHANNEL_UNBAN;
//         _[Opcodes.CMSG_CHANNEL_ANNOUNCEMENTS] = WcHandlersChat.On_CMSG_CHANNEL_ANNOUNCEMENTS;
//         _[Opcodes.CMSG_CHANNEL_MODERATE] = WcHandlersChat.On_CMSG_CHANNEL_MODERATE;

//         // NOTE: These opcodes are only partialy handled by Cluster and must be handled by WorldServer
//         _[Opcodes.MSG_MOVE_HEARTBEAT] = WcHandlersMovement.On_MSG_MOVE_HEARTBEAT;
//         _[Opcodes.MSG_MOVE_START_BACKWARD] = WcHandlersMovement.On_MSG_START_BACKWARD;
//         _[Opcodes.MSG_MOVE_START_FORWARD] = WcHandlersMovement.On_MSG_MOVE_START_FORWARD;
//         _[Opcodes.MSG_MOVE_START_PITCH_DOWN] = WcHandlersMovement.On_MSG_MOVE_START_PITCH_DOWN;
//         _[Opcodes.MSG_MOVE_START_PITCH_UP] = WcHandlersMovement.On_MSG_MOVE_START_PITCH_UP;
//         _[Opcodes.MSG_MOVE_START_STRAFE_LEFT] = WcHandlersMovement.On_MSG_MOVE_STRAFE_LEFT;
//         _[Opcodes.MSG_MOVE_START_STRAFE_RIGHT] = WcHandlersMovement.On_MSG_MOVE_STRAFE_LEFT;
//         _[Opcodes.MSG_MOVE_START_SWIM] = WcHandlersMovement.On_MSG_MOVE_START_SWIM;
//         _[Opcodes.MSG_MOVE_START_TURN_LEFT] = WcHandlersMovement.On_MSG_MOVE_START_TURN_LEFT;
//         _[Opcodes.MSG_MOVE_START_TURN_RIGHT] = WcHandlersMovement.On_MSG_MOVE_START_TURN_RIGHT;
//         _[Opcodes.MSG_MOVE_STOP] = WcHandlersMovement.On_MSG_MOVE_STOP;
//         _[Opcodes.MSG_MOVE_STOP_PITCH] = WcHandlersMovement.On_MSG_MOVE_STOP_PITCH;
//         _[Opcodes.MSG_MOVE_STOP_STRAFE] = WcHandlersMovement.On_MSG_MOVE_STOP_STRAFE;
//         _[Opcodes.MSG_MOVE_STOP_SWIM] = WcHandlersMovement.On_MSG_MOVE_STOP_SWIM;
//         _[Opcodes.MSG_MOVE_STOP_TURN] = WcHandlersMovement.On_MSG_MOVE_STOP_TURN;
//         _[Opcodes.MSG_MOVE_SET_FACING] = WcHandlersMovement.On_MSG_MOVE_SET_FACING;

//         // Opcodes redirected from the WorldServer
//         _[Opcodes.CMSG_CREATURE_QUERY] = OnClusterPacket;
//         _[Opcodes.CMSG_GAMEOBJECT_QUERY] = OnClusterPacket;

// builder.RegisterModule<LegacyWorldModule>();
// var worldServer = container.Resolve<WorldServer>();
// await worldServer.StartAsync();