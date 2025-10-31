using AzerothReborn.RealmServer.Reflection;
using AzerothReborn.RealmServer.Responses;

namespace AzerothReborn.RealmServer.Handlers;

[Opcode(Network.Opcodes.CMSG_PING)]
internal class CMSG_PING : IHandler
{
    public Task<List<IResponse>> HandleAsync(Network.PacketReader reader, Domain.Client client)
    {
        var request = new Requests.CMSG_PING(reader);
        var response = new SMSG_PONG(request.Payload);

        return Task.FromResult(new List<IResponse> { response });
    }
}
