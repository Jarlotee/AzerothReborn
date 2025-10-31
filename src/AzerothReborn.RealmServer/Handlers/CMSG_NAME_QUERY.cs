using AzerothReborn.RealmServer.Reflection;
using Microsoft.Extensions.Logging;

namespace AzerothReborn.RealmServer.Handlers;

[Opcode(Network.Opcodes.CMSG_NAME_QUERY)]
internal class CMSG_NAME_QUERY : IHandler
{
    private readonly ILogger _logger;
    private readonly Server.Realm _realm;

    public CMSG_NAME_QUERY(ILogger<CMSG_NAME_QUERY> logger, Server.Realm realm)
    {
        _logger = logger;
        _realm = realm;
    }

    public Task<List<Responses.IResponse>> HandleAsync(Network.PacketReader reader, Domain.Client client)
    {
        var request = new Requests.CMSG_NAME_QUERY(reader);

        if (Extensions.Functions.GuidIsPlayer(request.Guid) && _realm.CharacteRs.ContainsKey(request.Guid))
        {
            var response = new Responses.SMSG_NAME_QUERY_RESPONSE(
                request.Guid,
                _realm.CharacteRs[request.Guid].Name ?? "",
                (byte)_realm.CharacteRs[request.Guid].Race,
                _realm.CharacteRs[request.Guid].Gender,
                (byte)_realm.CharacteRs[request.Guid].Class
            );

            return Task.FromResult(new List<Responses.IResponse> { response });
        }

        return Task.FromResult(new List<Responses.IResponse> { });
    }
}
