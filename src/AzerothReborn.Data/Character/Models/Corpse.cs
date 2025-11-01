using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AzerothReborn.Data.Character.Models;

[Index(nameof(CharacterId), IsUnique = true)]
[Index(nameof(Type), IsUnique = false)]
[Index(nameof(TimeUtc), IsUnique = false)]
[Index(nameof(InstanceId), IsUnique = false)]
[Comment("Death System")]
public class Corpse
{
    [Key]
    [Comment("The corpse identifier.")]
    public required uint CorpseId { get; set; }

    [Comment("The character identifier.")]
    public required uint CharacterId { get; set; }

    [Comment("The x position of the corpse.")]
    public required float PositionX { get; set; }

    [Comment("The y position of the corpse.")]
    public required float PositionY { get; set; }

    [Comment("The z position of the corpse.")]
    public required float PositionZ { get; set; }

    [Comment("The orientation of the corpse. (e.g. which way its facing)")]
    public required float Orientation { get; set; }

    [Comment("The map idenfier for the position of the corpse.")]
    public required byte MapId { get; set; }

    [Comment("The timestamp of death.")]
    public required DateTime TimeUtc { get; set; }

    [Comment("The type of corpse.")]
    public required byte Type { get; set; }

    [Comment("The instance id where the corpse resides.")]
    public byte? InstanceId { get; set; }

    public required Character Character { get; set; }
}