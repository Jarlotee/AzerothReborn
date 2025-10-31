using AzerothReborn.RealmServer.Reflection;
using AzerothReborn.RealmServer.Responses;
using Microsoft.Extensions.Logging;

namespace AzerothReborn.RealmServer.Handlers;

[Opcode(Network.Opcodes.CMSG_NEXT_CINEMATIC_CAMERA)]
internal class CMSG_LOGOUT_CANCEL : IHandler
{
    private readonly ILogger _logger;

    public CMSG_LOGOUT_CANCEL(ILogger<CMSG_LOGOUT_CANCEL> logger)
    {
        _logger = logger;
    }

    public Task<List<IResponse>> HandleAsync(Network.PacketReader reader, Domain.Client client)
    {
        _logger.LogWarning("PACKET RECIEVED BUT NOT HANDLED [CMSG_LOGOUT_CANCEL]");

        return Task.FromResult(new List<IResponse> { });
    }
}
