namespace AzerothReborn.RealmServer.Domain;

public class Server
{
    public int Latency;
    public DateTime Started = DateTime.Now;
    public float CpuUsage;
    public ulong MemoryUsage;
}
