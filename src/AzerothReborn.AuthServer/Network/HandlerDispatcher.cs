namespace AzerothReborn.AuthServer.Network;

internal sealed class HandlerDispatcher<THandler, TRequest> : IHandlerDispatcher
    where THandler : Handlers.IHandler<TRequest>
    where TRequest : Requests.IRequestMessage<TRequest>
{
    private readonly THandler handler;

    public HandlerDispatcher(THandler handler)
    {
        this.handler = handler;
    }

    public MessageOpcode Opcode => handler.MessageOpcode;

    public async Task ExectueAsync(SocketReader reader, SocketWriter writer)
    {
        var request = await TRequest.ReadAsync(reader);
        var response = await handler.ExectueAsync(request);
        await response.WriteAsync(writer);
    }
}
