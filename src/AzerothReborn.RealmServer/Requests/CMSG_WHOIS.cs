namespace AzerothReborn.RealmServer.Requests;

internal class CMSG_WHOIS
{
    public string Name { get; private set; }

    public CMSG_WHOIS(Network.PacketReader reader)
    {
        Name = reader.GetString();
    }
}
