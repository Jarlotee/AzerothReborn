namespace AzerothReborn.RealmServer.Responses;

internal class SMSG_WHOIS : IResponse
{
    public Network.Opcodes GetOpcode()
    {
        return Network.Opcodes.SMSG_WHOIS;
    }

    public void Write(Network.PacketWriter writer)
    {
        writer.AddString("This feature is not available yet.");
    }
}
