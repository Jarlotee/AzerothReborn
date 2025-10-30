namespace AzerothReborn.RealmServer.Network;

internal sealed class HandlerDispatcher<TRequest, THandler> : IHandlerDispatcher
    where TRequest : Requests.IRequestMessage<TRequest>
    where THandler : Handlers.IHandler<TRequest>
{
    private readonly THandler handler;

    public HandlerDispatcher(THandler handler)
    {
        this.handler = handler;
    }

    public Opcodes Opcode => TRequest.Opcode;

    public Task<HandlerResult> ExectueAsync(PacketReader reader)
    {
        return handler.ExectueAsync(TRequest.Read(reader));
    }
}
