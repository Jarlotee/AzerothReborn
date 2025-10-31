using AzerothReborn.RealmServer.Reflection;
using Microsoft.Extensions.Logging;

namespace AzerothReborn.RealmServer.Handlers;

[Opcode(Network.Opcodes.CMSG_REQUEST_ACCOUNT_DATA)]
internal class CMSG_REQUEST_ACCOUNT_DATA : IHandler
{
    private readonly ILogger _logger;

    public CMSG_REQUEST_ACCOUNT_DATA(ILogger<CMSG_NEXT_CINEMATIC_CAMERA> logger)
    {
        _logger = logger;
    }

    public Task<List<Responses.IResponse>> HandleAsync(Network.PacketReader reader, Domain.Client client)
    {
        var request = new Requests.CMSG_REQUEST_ACCOUNT_DATA(reader);
        var response = new Responses.SMSG_UPDATE_ACCOUNT_DATA(request.DataId);

        _logger.LogWarning("PACKET RECIEVED BUT ONLY PARTIALLY HANDLED [CMSG_REQUEST_ACCOUNT_DATA]");

        return Task.FromResult(new List<Responses.IResponse> { response });
    }
}
