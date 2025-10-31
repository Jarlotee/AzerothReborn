namespace AzerothReborn.RealmServer.Requests;

internal class CMSG_NAME_QUERY
{
    public ulong Guid { get; private set; }

    public CMSG_NAME_QUERY(Network.PacketReader reader)
    {
        Guid = reader.GetUInt64();
    }
}
