namespace AzerothReborn.RealmServer.Domain;

public class CharacterRace
{
    public int RaceId { get; set; }
    public short FactionId { get; set; }
    public int ModelMale { get; set; }
    public int ModelFemale { get; set; }
    public byte TeamId { get; set; }
    public int CinematicId { get; set; }
}