using System.Buffers;

namespace AzerothReborn.RealmServer.Network;

internal sealed class HandlerResult : IDisposable
{
    private readonly Responses.IResponseMessage[] messages;
    private readonly int length;

    private HandlerResult(Responses.IResponseMessage[] messages, int length)
    {
        this.messages = messages;
        this.length = length;
    }

    public static HandlerResult From(Responses.IResponseMessage responseMessage)
    {
        var memoryOwner = ArrayPool<Responses.IResponseMessage>.Shared.Rent(1);
        memoryOwner[0] = responseMessage;
        return new HandlerResult(memoryOwner, 1);
    }

    public static Task<HandlerResult> FromTask(Responses.IResponseMessage responseMessage)
    {
        return Task.FromResult(From(responseMessage));
    }

    public IEnumerable<Responses.IResponseMessage> GetResponseMessages()
    {
        return messages.Take(length);
    }

    public void Dispose()
    {
        ArrayPool<Responses.IResponseMessage>.Shared.Return(messages);
    }
}
