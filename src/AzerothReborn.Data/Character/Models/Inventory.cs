using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AzerothReborn.Data.Character.Models;

[Index(nameof(OwnerId), IsUnique = false)]
[Comment("Inventory System")]
public class Inventory
{
    [Key]
    [Comment("The item identifier.")]
    public required long ItemId { get; set; }

    [Comment("The items template identifier.")]
    public required ushort TemplateId { get; set; }

    [Comment("The slot to which the item is assigned.")]
    public required byte Slot { get; set; }

    [Comment("The bag to which the item is assigned.")]
    public required long Bag { get; set; }

    [Comment("The character identifier.")]
    public required uint OwnerId { get; set; }

    [Comment("The character identifier indicating who created the item.")]
    public uint? CreatorId { get; set; }

    [Comment("The character identifier indicating who gifted the item.")]
    public uint? GifterId { get; set; }

    [Comment("The count of items in the stack.")]
    public required byte Count { get; set; }

    [Comment("The current durability of the item.")]
    public required short Durability { get; set; }

    [Comment("The flags set of the item.")]
    public required short Flags { get; set; }

    [Comment("The current charges available on the item.")]
    public required byte Charges { get; set; }

    [Comment("The text identifier for the item.")]
    public required short TextId { get; set; }

    [MaxLength(255)]
    [Comment("The enchantment name for the item.")]
    public string? Enchantment { get; set; }

    [Comment("The random properties for the item.")]
    public required short RandomProperties { get; set; }

    public Character? Owner { get; set; }
    public Character? Creator { get; set; }
    public Character? Gifter { get; set; }
    public Petition? Petition { get; set; }
}
