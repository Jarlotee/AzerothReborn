using System.ComponentModel;

namespace AzerothReborn.RealmServer.Domain;

public interface ICluster
{
    [Description("Signal realm server for new world server.")]
    bool Connect(string uri, List<uint> maps, IWorld world);

    [Description("Signal realm server for disconected world server.")]
    void Disconnect(string uri, List<uint> maps);

    [Description("Send data packet to client.")]
    void ClientSend(uint id, byte[] data);

    [Description("Notify client drop.")]
    void ClientDrop(uint id);

    [Description("Notify client transfer.")]
    void ClientTransfer(uint id, float posX, float posY, float posZ, float ori, uint map);

    [Description("Notify client update.")]
    void ClientUpdate(uint id, uint zone, byte level);

    [Description("Set client chat flag.")]
    void ClientSetChatFlag(uint id, byte flag);

    [Description("Get client crypt key.")]
    byte[] ClientGetCryptKey(uint id);

    List<int> BattlefieldList(byte type);

    void BattlefieldFinish(int battlefieldId);

    [Description("Send data packet to all clients online.")]
    void Broadcast(byte[] data);

    [Description("Send data packet to all clients in specified client's group.")]
    void BroadcastGroup(long groupId, byte[] data);

    [Description("Send data packet to all clients in specified client's raid.")]
    void BroadcastRaid(long groupId, byte[] data);

    [Description("Send data packet to all clients in specified client's guild.")]
    void BroadcastGuild(long guildId, byte[] data);

    [Description("Send data packet to all clients in specified client's guild officers.")]
    void BroadcastGuildOfficers(long guildId, byte[] data);

    [Description("Send update for the requested group.")]
    void GroupRequestUpdate(uint id);
}
