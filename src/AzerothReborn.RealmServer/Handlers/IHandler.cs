namespace AzerothReborn.RealmServer.Handlers;

internal interface IHandler
{
    Task<List<Responses.IResponse>> HandleAsync(Network.PacketReader reader, Domain.Client client);
}
