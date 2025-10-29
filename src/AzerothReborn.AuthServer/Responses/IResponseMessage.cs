namespace AzerothReborn.AuthServer.Responses;

internal interface IResponseMessage
{
    ValueTask WriteAsync(Network.SocketWriter writer);
}
