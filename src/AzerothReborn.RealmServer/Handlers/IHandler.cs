namespace AzerothReborn.RealmServer.Handlers;

internal interface IHandler<TRequest> where TRequest : Requests.IRequestMessage<TRequest>
{
    Task<Network.HandlerResult> ExectueAsync(TRequest request);
}
