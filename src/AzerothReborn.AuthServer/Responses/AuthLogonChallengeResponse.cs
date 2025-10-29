namespace AzerothReborn.AuthServer.Responses;

internal sealed class AuthLogonChallengeResponse : IResponseMessage
{
    public required byte[] PublicB { get; init; }
    public required byte[] G { get; init; }
    public required byte[] N { get; init; }
    public required byte[] Salt { get; init; }
    public required byte[] CrcSalt { get; init; }

    public async ValueTask WriteAsync(Network.SocketWriter writer)
    {
        await writer.WriteByteAsync((byte)Network.MessageOpcode.CMD_AUTH_LOGON_CHALLENGE);
        await writer.WriteByteAsync((byte)Domain.AccountStates.LOGIN_OK);
        await writer.WriteZeroBytesAsync(1);
        await writer.WriteByteArrayAsync(PublicB);
        await writer.WriteByteAsync((byte)G.Length);
        await writer.WriteByteAsync(G[0]);
        await writer.WriteByteAsync(32);
        await writer.WriteByteArrayAsync(N);
        await writer.WriteByteArrayAsync(Salt);
        await writer.WriteByteArrayAsync(CrcSalt);
        await writer.WriteZeroBytesAsync(1);
    }
}
