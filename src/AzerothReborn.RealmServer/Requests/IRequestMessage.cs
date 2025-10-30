namespace AzerothReborn.RealmServer.Requests;

internal interface IRequestMessage<T> where T : IRequestMessage<T>
{
    static abstract Network.Opcodes Opcode { get; }

    static abstract T Read(Network.PacketReader reader);
}
