using System.Net;

namespace AzerothReborn.AuthServer.Domain;

internal sealed class ClientState
{
    public AuthEngine AuthEngine { get; } = new AuthEngine();
    public uint? AccountId { get; set; }
    public string? AccountName { get; set; }
    public IPAddress? IPAddress { get; set; }
}
