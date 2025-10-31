using AzerothReborn.RealmServer.Reflection;
using AzerothReborn.RealmServer.Responses;
using Microsoft.Extensions.Logging;

namespace AzerothReborn.RealmServer.Handlers;

[Opcode(Network.Opcodes.CMSG_UPDATE_ACCOUNT_DATA)]
internal class CMSG_UPDATE_ACCOUNT_DATA : IHandler
{
    private readonly ILogger _logger;

    public CMSG_UPDATE_ACCOUNT_DATA(ILogger<CMSG_NEXT_CINEMATIC_CAMERA> logger)
    {
        _logger = logger;
    }

    public Task<List<IResponse>> HandleAsync(Network.PacketReader reader, Domain.Client client)
    {
        _logger.LogWarning("PACKET RECIEVED BUT NOT HANDLED [CMSG_UPDATE_ACCOUNT_DATA]");
        // var request = new Requests.CMSG_UPDATE_ACCOUNT_DATA(reader);

        return Task.FromResult(new List<IResponse> { });
    }
}