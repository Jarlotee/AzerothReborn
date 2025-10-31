namespace AzerothReborn.RealmServer.Responses;

internal class SMSG_PONG : IResponse
{
    private readonly uint _payload;

    public SMSG_PONG(uint payload)
    {
        _payload = payload;
    }

    public Network.Opcodes GetOpcode()
    {
        return Network.Opcodes.SMSG_PONG;
    }

    public void Write(Network.PacketWriter writer)
    {
        writer.AddUInt32(_payload);
    }
}
