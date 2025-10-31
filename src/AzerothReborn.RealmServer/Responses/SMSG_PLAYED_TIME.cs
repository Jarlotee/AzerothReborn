namespace AzerothReborn.RealmServer.Responses;

internal class SMSG_PLAYED_TIME : IResponse
{
    public Network.Opcodes GetOpcode()
    {
        return Network.Opcodes.SMSG_PLAYED_TIME;
    }

    public void Write(Network.PacketWriter writer)
    {
        writer.AddUInt32(1);
        writer.AddUInt32(1);
    }
}
