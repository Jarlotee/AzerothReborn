namespace AzerothReborn.AuthServer.Network;

internal interface IHandlerDispatcher
{
    MessageOpcode Opcode { get; }

    Task ExectueAsync(SocketReader reader, SocketWriter writer);
}
