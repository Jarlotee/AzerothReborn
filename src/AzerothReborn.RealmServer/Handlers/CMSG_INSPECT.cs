using AzerothReborn.RealmServer.Reflection;
using AzerothReborn.RealmServer.Responses;
using Microsoft.Extensions.Logging;

namespace AzerothReborn.RealmServer.Handlers;

[Opcode(Network.Opcodes.CMSG_COMPLETE_CINEMATIC)]
internal class CMSG_INSPECT : IHandler
{
    private readonly ILogger _logger;

    public CMSG_INSPECT(ILogger<CMSG_INSPECT> logger)
    {
        _logger = logger;
    }

    public Task<List<IResponse>> HandleAsync(Network.PacketReader reader, Domain.Client client)
    {
        // var request = new Requests.CMSG_INSPECT(reader);
        _logger.LogWarning("PACKET RECIEVED BUT NOT HANDLED [CMSG_INSPECT]");
        
        return Task.FromResult(new List<IResponse> { });
    }
}
