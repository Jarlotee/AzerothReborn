namespace AzerothReborn.RealmServer.Responses;

internal class SMSG_UPDATE_ACCOUNT_DATA : IResponse
{
    private readonly UInt32 _dataId;

    public SMSG_UPDATE_ACCOUNT_DATA(UInt32 dataId)
    {
        _dataId = dataId;
    }

    public Network.Opcodes GetOpcode()
    {
        return Network.Opcodes.SMSG_UPDATE_ACCOUNT_DATA;
    }

    public void Write(Network.PacketWriter writer)
    {
        writer.AddUInt32(_dataId);
        writer.AddUInt32(0);
    }
}
