namespace AzerothReborn.RealmServer.Responses;

internal sealed class SMSG_PONG : IResponseMessage
{
    public uint Payload { get; init; }

    public Network.Opcodes Opcode => Network.Opcodes.SMSG_PONG;

    public void Write(Network.PacketWriter writer)
    {
        writer.UInt32(Payload);
    }
}
