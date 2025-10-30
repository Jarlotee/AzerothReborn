using AzerothReborn.RealmServer.Reference;

namespace AzerothReborn.RealmServer.Domain;

public class Map
{
    public int Id;
    public MapTypes Type = MapTypes.MAP_COMMON;
    public required string Name;
    public int ParentMap = -1;
    public int ResetTime;

    public bool IsDungeon => Type is MapTypes.MAP_INSTANCE or MapTypes.MAP_RAID;

    public bool IsRaid => Type == MapTypes.MAP_RAID;

    public bool IsBattleGround => Type == MapTypes.MAP_BATTLEGROUND;

    public bool HasResetTime => ResetTime != 0;
}