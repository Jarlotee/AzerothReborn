namespace AzerothReborn.AuthServer.Requests;

internal sealed class RsLogonProofRequest : IRequestMessage<RsLogonProofRequest>
{
    public required byte[] A { get; init; }
    public required byte[] M1 { get; init; }

    public static async ValueTask<RsLogonProofRequest> ReadAsync(Network.SocketReader reader)
    {
        var a = await reader.ReadByteArrayAsync(32);
        var m1 = await reader.ReadByteArrayAsync(20);
        await reader.ReadVoidAsync(22);
        return new RsLogonProofRequest { A = a, M1 = m1 };
    }
}
