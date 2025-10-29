namespace AzerothReborn.AuthServer.Responses;

internal sealed class EmptyResponse : IResponseMessage
{
    public ValueTask WriteAsync(Network.SocketWriter writer)
    {
        return ValueTask.CompletedTask;
    }
}
