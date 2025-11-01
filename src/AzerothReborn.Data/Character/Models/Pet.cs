using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AzerothReborn.Data.Character.Models;

[Index(nameof(CharacterId), IsUnique = false)]
[Comment("Pet System")]
public class Pet
{
    [Key]
    [Comment("The pet identifier.")]
    public required uint PetId { get; set; }

    [Comment("The identifer of the owning character.")]
    public required uint CharacterId { get; set; }

    [Comment("No idea. Maybe and index for when you have multiple pets?")]
    public required uint Entry { get; set; }

    [Comment("The model identifer")]
    public required uint ModelId { get; set; }

    [Comment("The spell identifer used to create the pet.")]
    public required uint CreatedBySpellId { get; set; }

    [Comment("The category of the pet.")]
    public required byte PetType { get; set; }

    [Comment("The level of the pet.")]
    public required uint Level { get; set; }

    [Comment("The current experience of the pet.")]
    public required uint Experience { get; set; }

    [Comment("The current behavior of the pet.")]
    public required byte ReactState { get; set; }

    [Comment("The current loyalty points of the pet.")]
    public required uint LoyaltyPoints { get; set; }

    [Comment("The current loyalty level of the pet.")]
    public required int Loyalty { get; set; }

    [Comment("The current training points of the pet.")]
    public required int TrainingPoints { get; set; }

    [MaxLength(100)]
    [Comment("The current training points of the pet.")]
    public required string Name { get; set; }

    [Comment("Indicates the pet was renamed.")]
    public required bool Renamed { get; set; }

    [Comment("Which slot the pet is equiped to.")]
    public required uint Slot { get; set; }

    [Comment("The current health of the pet.")]
    public required uint Health { get; set; }

    [Comment("The current mana of the pet.")]
    public required uint Mana { get; set; }

    [Comment("The current hapiness of the pet.")]
    public required int Hapiness { get; set; }

    [Comment("The timestamp of the pets last save.")]
    public required DateTime LastSaveUtc { get; set; }

    [Comment("The cost required to reset the pets talents.")]
    public required int ResetTalentsCost { get; set; }

    [Comment("The cooldown required to reset the pets talents.")]
    public required DateTime ResetTalentsTimeUtc { get; set; }

    [Comment("Who knows?")]
    public string? AB { get; set; }

    [Comment("Who knows?")]
    public string? TeachSpellData { get; set; }

    public required Character Character { get; set; }
}