namespace AzerothReborn.RealmServer.Domain;

public class Realm
{    
    // public long ClietniDs;

    public Dictionary<uint, Client> ClienTs = new();
    public ReaderWriterLock CharacteRsLock = new();
    public Dictionary<ulong, Character> CharacteRs = new();

    // public Random Rnd = new();
}
