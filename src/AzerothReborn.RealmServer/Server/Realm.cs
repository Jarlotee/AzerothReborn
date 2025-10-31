using AzerothReborn.RealmServer.Domain;

namespace AzerothReborn.RealmServer.Server;

public class Realm
{    
    public Dictionary<uint, Client> ClienTs = new();
    public ReaderWriterLock CharacteRsLock = new();
    public Dictionary<ulong, Character> CharacteRs = new();
}
