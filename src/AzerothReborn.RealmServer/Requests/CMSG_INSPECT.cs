namespace AzerothReborn.RealmServer.Requests;

internal class CMSG_INSPECT
{
    public ulong Guid { get; private set; }

    public CMSG_INSPECT(Network.PacketReader reader)
    {
        Guid = reader.GetUInt64();
    }
}
