namespace AzerothReborn.AuthServer.Requests;

internal sealed class AuthRealmlistRequest : IRequestMessage<AuthRealmlistRequest>
{
    public required byte[] Unk { get; init; }

    public static async ValueTask<AuthRealmlistRequest> ReadAsync(Network.SocketReader reader)
    {
        return new AuthRealmlistRequest
        {
            Unk = await reader.ReadByteArrayAsync(4)
        };
    }
}
