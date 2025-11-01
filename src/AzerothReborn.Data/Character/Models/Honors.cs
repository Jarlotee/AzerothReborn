using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AzerothReborn.Data.Character.Models;

[Comment("Honor System")]
public class Honors
{
    [Key]
    [Comment("The character identifier.")]
    public required uint CharacterId { get; set; }

    [Comment("The honor points for the character.")]
    public required ushort Points { get; set; }

    [Comment("The current honor rank for the character.")]
    public required byte Rank { get; set; }

    [Comment("The highest honor rank for the character.")]
    public required byte HighestRank { get; set; }

    [Comment("The total honorable kills for the character.")]
    public required int HonorableKills { get; set; }

    [Comment("The total dishonorable kills for the character.")]
    public required int DishonorableKills { get; set; }

    [Comment("The honor points for this week for the character.")]
    public required short PointsThisWeek { get; set; }

    [Comment("The honor points for the prior week for the character.")]
    public required ushort PointsLastWeek { get; set; }

    [Comment("The honor points accrued yesterday for the character.")]
    public required ushort PointsYesterday { get; set; }

    [Comment("The total kills for this week for the character.")]
    public required int KillsThisWeek { get; set; }

    [Comment("The total kills for the prior week for the character.")]
    public required int KillsLastWeek { get; set; }

    [Comment("The total kills accrued yesterday for the character.")]
    public required int KillsYesterday { get; set; }

    [Comment("The total honorable kills accrued today for the character.")]
    public required short HonrableKillsToday { get; set; }

    [Comment("The total honorable kills accrued today for the character.")]
    public required short DishonorableKillsToday { get; set; }

    public required Character Character { get; set; }
}


