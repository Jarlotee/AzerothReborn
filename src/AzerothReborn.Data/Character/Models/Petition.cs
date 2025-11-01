using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AzerothReborn.Data.Character.Models;

[Comment("Guild System")]
public class Petition
{
    [Key]
    [Comment("The petition identifier.")]
    public required uint PetitionId { get; set; }

    [Comment("The item identifier.")]
    public required long ItemId { get; set; }

    [Comment("The character identifier of the character who owns the petition.")]
    public required uint OwnerId { get; set; }

    [MaxLength(255)]
    [Comment("The petition name.")]
    public required string Name { get; set; }

    [Comment("The petition type.")]
    public required byte Type { get; set; }

    [Comment("The number of signatures gathered.")]
    public required byte Signatures { get; set; }

    [Comment("The character identifier of the signature gathered.")]
    public uint? Signer1 { get; set; }

    [Comment("The character identifier of the signature gathered.")]
    public uint? Signer2 { get; set; }

    [Comment("The character identifier of the signature gathered.")]
    public uint? Signer3 { get; set; }

    [Comment("The character identifier of the signature gathered.")]
    public uint? Signer4 { get; set; }

    [Comment("The character identifier of the signature gathered.")]
    public uint? Signer5 { get; set; }

    [Comment("The character identifier of the signature gathered.")]
    public uint? Signer6 { get; set; }

    [Comment("The character identifier of the signature gathered.")]
    public uint? Signer7 { get; set; }

    [Comment("The character identifier of the signature gathered.")]
    public uint? Signer8 { get; set; }

    [Comment("The character identifier of the signature gathered.")]
    public uint? Signer9 { get; set; }

    public required Character Owner { get; set; }

    public required Inventory Item { get; set; }
}
