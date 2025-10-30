namespace AzerothReborn.RealmServer.Responses;

internal interface IResponseMessage
{
    Network.Opcodes Opcode { get; }

    void Write(Network.PacketWriter writer);
}
