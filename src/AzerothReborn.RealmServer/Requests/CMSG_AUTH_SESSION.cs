namespace AzerothReborn.RealmServer.Requests;

internal class CMSG_AUTH_SESSION
{
    public int ClientVersion { get; private set; }
    public int ClientSessionId { get; private set; }
    public string ClientAccount { get; private set; }
    public int ClientSeed { get; set; }
    public byte[] ClientHash { get; private set; }
    public int ClientAddonSize { get; private set; }

    public CMSG_AUTH_SESSION(Network.PacketReader reader)
    {
        ClientVersion = reader.GetInt32();
        ClientSessionId = reader.GetInt32();
        ClientAccount = reader.GetString();
        ClientSeed = reader.GetInt32();
        ClientHash = reader.GetBytes(20);
        ClientAddonSize = reader.GetInt32();
    }
}
