namespace AzerothReborn.RealmServer.Network;

internal interface IHandlerDispatcher
{
    Opcodes Opcode { get; }

    Task<HandlerResult> ExectueAsync(PacketReader reader);
}
