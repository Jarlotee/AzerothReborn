namespace AzerothReborn.RealmServer.Requests;

internal sealed class CMSG_PING : IRequestMessage<CMSG_PING>
{
    public required uint Payload { get; init; }

    public static Network.Opcodes Opcode => Network.Opcodes.CMSG_PING;

    public static CMSG_PING Read(Network.PacketReader reader)
    {
        return new CMSG_PING()
        {
            Payload = reader.UInt32()
        };
    }
}
