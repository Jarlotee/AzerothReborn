using AzerothReborn.RealmServer.Reflection;
using AzerothReborn.RealmServer.Responses;
using Microsoft.Extensions.Logging;

namespace AzerothReborn.RealmServer.Handlers;

[Opcode(Network.Opcodes.CMSG_WHOIS)]
internal class CMSG_WHOIS : IHandler
{
    private readonly ILogger _logger;

    public CMSG_WHOIS(ILogger<CMSG_WHOIS> logger)
    {
        _logger = logger;
    }

    public Task<List<IResponse>> HandleAsync(Network.PacketReader reader, Domain.Client client)
    {
        _logger.LogWarning("PACKET RECIEVED BUT PARTIALLY HANDLED [CMSG_WHOIS]");
        // var request = new Requests.CMSG_WHOIS(reader);
        var response = new SMSG_WHOIS();
        return Task.FromResult(new List<IResponse> { response });
    }
}
