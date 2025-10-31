namespace AzerothReborn.RealmServer.Requests;

internal class CMSG_REQUEST_ACCOUNT_DATA
{
    public uint DataId { get; private set; }

    public CMSG_REQUEST_ACCOUNT_DATA(Network.PacketReader reader)
    {
        DataId = reader.GetUInt32();
        // var size = reader.GetUInt32();
    }
}
