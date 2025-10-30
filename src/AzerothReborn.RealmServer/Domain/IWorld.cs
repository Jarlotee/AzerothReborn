using System.ComponentModel;

namespace AzerothReborn.RealmServer.Domain;

public interface IWorld
{
    [Description("Initialize client object.")]
    void ClientConnect(uint id, Client client);

    [Description("Destroy client object.")]
    void ClientDisconnect(uint id);

    [Description("Assing particular client to this world server (Use client ID).")]
    void ClientLogin(uint id, ulong guid);

    [Description("Remove particular client from this world server (Use client ID).")]
    void ClientLogout(uint id);

    [Description("Transfer packet from Realm to World using client's ID.")]
    void ClientPacket(uint id, byte[] data);

    [Description("Create CharacterObject.")]
    int ClientCreateCharacter(string account, string name, byte race, byte classe, byte gender, byte skin, byte face, byte hairStyle, byte hairColor, byte facialHair, byte outfitId);

    [Description("Respond to world server if still alive.")]
    int Ping(int timestamp, int latency);

    [Description("Tell the cluster about your CPU & Memory Usage")]
    Server GetServerInfo();

    [Description("Make world create specific map.")]
    Task InstanceCreateAsync(uint Map);

    [Description("Make world destroy specific map.")]
    void InstanceDestroy(uint Map);

    [Description("Check world configuration.")]
    bool InstanceCanCreate(int Type);

    [Description("Set client's group.")]
    void ClientSetGroup(uint ID, long GroupID);

    [Description("Update group information.")]
    void GroupUpdate(long GroupID, byte GroupType, ulong GroupLeader, ulong[] Members);

    [Description("Update group information about looting.")]
    void GroupUpdateLoot(long GroupID, byte Difficulty, byte Method, byte Threshold, ulong Master);

    [Description("Request party member stats.")]
    byte[] GroupMemberStats(ulong GUID, int Flag);

    [Description("Update guild information.")]
    void GuildUpdate(ulong GUID, uint GuildID, byte GuildRank);

    void BattlefieldCreate(int BattlefieldID, byte BattlefieldMapType, uint Map);

    void BattlefieldDelete(int BattlefieldID);

    void BattlefieldJoin(int BattlefieldID, ulong GUID);

    void BattlefieldLeave(int BattlefieldID, ulong GUID);
}
