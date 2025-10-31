namespace AzerothReborn.RealmServer.Requests;

internal class CMSG_UPDATE_ACCOUNT_DATA
{
    public uint DataId { get; private set; }

    public CMSG_UPDATE_ACCOUNT_DATA(Network.PacketReader reader)
    {
        DataId = reader.GetUInt32();
    }
}
