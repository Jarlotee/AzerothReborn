using AzerothReborn.RealmServer.Reflection;
using AzerothReborn.RealmServer.Responses;
using Microsoft.Extensions.Logging;

namespace AzerothReborn.RealmServer.Handlers;

[Opcode(Network.Opcodes.CMSG_NEXT_CINEMATIC_CAMERA)]
internal class CMSG_NEXT_CINEMATIC_CAMERA : IHandler
{
    private readonly ILogger _logger;

    public CMSG_NEXT_CINEMATIC_CAMERA(ILogger<CMSG_NEXT_CINEMATIC_CAMERA> logger)
    {
        _logger = logger;
    }

    public Task<List<IResponse>> HandleAsync(Network.PacketReader reader, Domain.Client client)
    {
        _logger.LogWarning("PACKET RECIEVED BUT NOT HANDLED [CMSG_NEXT_CINEMATIC_CAMERA]");

        return Task.FromResult(new List<IResponse> { });
    }
}
