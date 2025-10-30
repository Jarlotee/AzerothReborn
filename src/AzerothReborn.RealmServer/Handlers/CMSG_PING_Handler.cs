namespace AzerothReborn.RealmServer.Handlers;

internal sealed class CMSG_PING_Handler : IHandler<Requests.CMSG_PING>
{
    public Task<Network.HandlerResult> ExectueAsync(Requests.CMSG_PING request)
    {
        var response = new Responses.SMSG_PONG
        {
            Payload = request.Payload
        };

        return Network.HandlerResult.FromTask(response);
    }
}
