using AzerothReborn.RealmServer.Network;

namespace AzerothReborn.RealmServer.Responses;

internal interface IResponse
{
    public Opcodes GetOpcode();

    public void Write(Network.PacketWriter writer);
}
