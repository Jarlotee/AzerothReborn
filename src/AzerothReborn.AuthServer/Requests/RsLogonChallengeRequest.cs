namespace AzerothReborn.AuthServer.Requests;

internal sealed class RsLogonChallengeRequest : IRequestMessage<RsLogonChallengeRequest>
{
    public required string AccountName { get; init; }
    public required Domain.WowClientBuildVersions ClientBuild { get; init; }

    public static async ValueTask<RsLogonChallengeRequest> ReadAsync(Network.SocketReader reader)
    {
        await reader.ReadVoidAsync(10);
        var clientBuild = await reader.ReadInt16Async();
        await reader.ReadVoidAsync(20);
        var accountLength = await reader.ReadByteAsync();
        var accountName = await reader.ReadStringAsync(accountLength);

        return new()
        {
            AccountName = accountName,
            ClientBuild = (Domain.WowClientBuildVersions)clientBuild
        };
    }
}
