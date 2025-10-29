namespace AzerothReborn.AuthServer.Handlers;

internal interface IHandler<TRequest> where TRequest : Requests.IRequestMessage<TRequest>
{
    Network.MessageOpcode MessageOpcode { get; }

    Task<Responses.IResponseMessage> ExectueAsync(TRequest request);
}
