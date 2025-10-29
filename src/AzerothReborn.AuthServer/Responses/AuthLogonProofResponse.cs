namespace AzerothReborn.AuthServer.Responses;

internal sealed class AuthLogonProofResponse : IResponseMessage
{
    public required Domain.AccountStates AccountState { get; init; }
    public byte[]? M2 { get; init; }

    public async ValueTask WriteAsync(Network.SocketWriter writer)
    {
        await writer.WriteByteAsync((byte)Network.MessageOpcode.CMD_AUTH_LOGON_PROOF);
        await writer.WriteByteAsync((byte)AccountState);

        if (M2 != null)
        {
            await writer.WriteByteArrayAsync(M2);
            await writer.WriteZeroBytesAsync(4);
        }
    }
}
