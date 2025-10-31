using AzerothReborn.RealmServer.Reflection;
using AzerothReborn.RealmServer.Responses;
using Microsoft.Extensions.Logging;

namespace AzerothReborn.RealmServer.Handlers;

[Opcode(Network.Opcodes.CMSG_COMPLETE_CINEMATIC)]
internal class CMSG_COMPLETE_CINEMATIC : IHandler
{
    private readonly ILogger _logger;

    public CMSG_COMPLETE_CINEMATIC(ILogger<CMSG_NEXT_CINEMATIC_CAMERA> logger)
    {
        _logger = logger;
    }

    public Task<List<IResponse>> HandleAsync(Network.PacketReader reader, Domain.Client client)
    {
        _logger.LogWarning("PACKET RECIEVED BUT NOT HANDLED [CMSG_COMPLETE_CINEMATIC]");

        return Task.FromResult(new List<IResponse> { });
    }
}
