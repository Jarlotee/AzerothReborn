using AzerothReborn.RealmServer.Reflection;
using AzerothReborn.RealmServer.Responses;
using Microsoft.Extensions.Logging;

namespace AzerothReborn.RealmServer.Handlers;

[Opcode(Network.Opcodes.CMSG_PLAYED_TIME)]
internal class CMSG_PLAYED_TIME : IHandler
{
    private readonly ILogger _logger;

    public CMSG_PLAYED_TIME(ILogger<CMSG_PLAYED_TIME> logger)
    {
        _logger = logger;
    }

    public Task<List<IResponse>> HandleAsync(Network.PacketReader reader, Domain.Client client)
    {
        _logger.LogWarning("PACKET RECIEVED BUT PARTIALLY HANDLED [CMSG_PLAYED_TIME]");

        var response = new SMSG_PLAYED_TIME();
        return Task.FromResult(new List<IResponse> { response });
    }
}
