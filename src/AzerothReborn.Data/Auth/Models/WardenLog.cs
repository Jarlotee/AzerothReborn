using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AzerothReborn.Data.Auth.Models;

[Comment("Warden log of failed checks")]
public class WardenLog
{
    [Key]
    [Comment("The warden log identifer")]
    public required ulong WardenLogId { get; set; }

    [Comment("The warden failed check inentifier")]
    public required short FailedCheckId { get; set; }

    [Comment("Action taken (see enum WardenActions)")]
    public required byte Action { get; set; }

    [Comment("The account identifier.")]
    public required uint AccountId { get; set; }

    [Comment("The player identifier.")]
    public required int PlayerGuid { get; set; }

    [Comment("The map identifier. (See map.dbc)")]
    public required int MapId { get; set; }

    [Comment("The x location of the character.")]
    public float PositionX { get; set; }

    [Comment("The y location of the character.")]
    public float PositionY { get; set; }

    [Comment("The z location of the character.")]
    public float PositionZ { get; set; }

    public required DateTime TimestampUtc { get; set; } = DateTime.UtcNow;
}