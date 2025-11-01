using Microsoft.EntityFrameworkCore;

namespace AzerothReborn.Data.Character.Models;

[Comment("Spell Tracking System")]
public class Spell
{
    [Comment("The character identifier.")]
    public required uint CharacterId { get; set; }

    [Comment("The spell identifier.")]
    public required uint SpellId { get; set; }

    [Comment("Indicates if the spell is active.")]
    public required bool Active { get; set; }

    [Comment("The cooldown of the spell.")]
    public required uint Cooldown { get; set; }

    [Comment("The item identifier used to cast the spell.")]
    public long? ItemId { get; set; }

    public required Character Character { get; set; }

    public Inventory? Item { get; set; }
}


