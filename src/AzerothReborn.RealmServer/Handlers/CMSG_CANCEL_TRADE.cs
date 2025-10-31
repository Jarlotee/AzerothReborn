using AzerothReborn.RealmServer.Reflection;
using AzerothReborn.RealmServer.Responses;
using Microsoft.Extensions.Logging;

namespace AzerothReborn.RealmServer.Handlers;

[Opcode(Network.Opcodes.CMSG_CANCEL_TRADE)]
internal class CMSG_CANCEL_TRADE : IHandler
{
    private readonly ILogger _logger;

    public CMSG_CANCEL_TRADE(ILogger<CMSG_CANCEL_TRADE> logger)
    {
        _logger = logger;
    }

    public Task<List<IResponse>> HandleAsync(Network.PacketReader reader, Domain.Client client)
    {
        _logger.LogWarning("PACKET RECIEVED BUT NOT HANDLED [CMSG_CANCEL_TRADE]");

        // if (client.Character is not null && client.Character.IsInWorld)
        // {
        //     try
        //     {
        //         client.Character.GetWorld.ClientPacket(client.Index, packet.Data);
        //     }
        //     catch
        //     {
        //         _clusterServiceLocator.WcNetwork.WorldServer.Disconnect("NULL", new List<uint> { client.Character.Map });
        //     }
        // }
        // else
        // {
        //     _clusterServiceLocator.WorldCluster.Log.WriteLine(LogType.DEBUG, "[{0}:{1}] CMSG_CANCEL_TRADE", client.IP, client.Port);
        // }

        return Task.FromResult(new List<IResponse> { });
    }
}
