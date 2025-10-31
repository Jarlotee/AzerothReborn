using AzerothReborn.RealmServer.Reflection;
using AzerothReborn.RealmServer.Responses;

namespace AzerothReborn.RealmServer.Handlers;

[Opcode(Network.Opcodes.CMSG_QUERY_TIME)]
internal class CMSG_QUERY_TIME : IHandler
{
    public Task<List<IResponse>> HandleAsync(Network.PacketReader reader, Domain.Client client)
    {
        var response = new SMSG_QUERY_TIME_RESPONSE();
        return Task.FromResult(new List<IResponse> { response });
    }
}
