namespace AzerothReborn.AuthServer.Requests;

internal interface IRequestMessage<T> where T : IRequestMessage<T>
{
    static abstract ValueTask<T> ReadAsync(Network.SocketReader reader);
}
