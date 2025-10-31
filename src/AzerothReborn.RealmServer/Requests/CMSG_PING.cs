namespace AzerothReborn.RealmServer.Requests;

internal class CMSG_PING
{
    public uint Payload { get; private set; }

    public CMSG_PING(Network.PacketReader reader)
    {
        Payload = reader.GetUInt32();
    }
}
