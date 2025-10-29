namespace AzerothReborn.AuthServer.Handlers;

internal sealed class AuthReconnectChallengeHandler : IHandler<Requests.RsLogonChallengeRequest>
{
    private readonly RsLogonChallengeHandler rsLogonChallengeHandler;

    public AuthReconnectChallengeHandler(RsLogonChallengeHandler rsLogonChallengeHandler)
    {
        this.rsLogonChallengeHandler = rsLogonChallengeHandler;
    }

    public Network.MessageOpcode MessageOpcode => Network.MessageOpcode.CMD_AUTH_RECONNECT_CHALLENGE;

    public async Task<Responses.IResponseMessage> ExectueAsync(Requests.RsLogonChallengeRequest request)
    {
        return await rsLogonChallengeHandler.ExectueAsync(request);
    }
}
