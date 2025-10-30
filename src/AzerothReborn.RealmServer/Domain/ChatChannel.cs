namespace AzerothReborn.RealmServer.Domain;

public class ChatChannel
{
    public required int Index { get; set; }
    public required int Flags { get; set; }
    public required string Name { get; set; }
}
