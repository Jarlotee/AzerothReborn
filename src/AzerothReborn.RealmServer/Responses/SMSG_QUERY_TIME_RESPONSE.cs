namespace AzerothReborn.RealmServer.Responses;

internal class SMSG_QUERY_TIME_RESPONSE : IResponse
{
    public Network.Opcodes GetOpcode()
    {
        return Network.Opcodes.SMSG_QUERY_TIME_RESPONSE;
    }

    public void Write(Network.PacketWriter writer)
    {
        var unixTime = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds();
        writer.AddUInt32((uint)unixTime);
    }
}
