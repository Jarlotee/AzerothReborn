using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AzerothReborn.Data.Character.Models;

[Index(nameof(Name), IsUnique = true)]
[Comment("Character System")]
public class Character
{
    [Key]
    [Comment("The character identifier.")]
    public required uint CharacterId { get; set; }

    [Comment("The account identifier owning the character.")]
    public required uint AccountId { get; set; }

    [MaxLength(21)]
    [Comment("The name of the character.")]
    public required uint Name { get; set; }

    [Comment("The level of the character.")]
    public required byte Level { get; set; }

    [Comment("The experience of the character.")]
    public required ushort Experience { get; set; }

    [Comment("The amount of rested experience the character has accrued.")]
    public required byte RestedExperience { get; set; }

    [Comment("Indicates the character is online.")]
    public required bool Online { get; set; }

    [Comment("Indicates when the character last logged out.")]
    public DateTime? LogoutTimeUtc { get; set; }

    [Comment("The x position of the character.")]
    public required float PositionX { get; set; }

    [Comment("The y position of the character.")]
    public required float PositionY { get; set; }

    [Comment("The z position of the character.")]
    public required float PositionZ { get; set; }

    [Comment("The orientation of the character. (e.g. which way its facing)")]
    public required float Orientation { get; set; }

    [Comment("The map idenfier for the position of the character.")]
    public required byte MapId { get; set; }

    [Comment("The zone idenfier for the position of the character.")]
    public required byte ZoneId { get; set; }

    [Comment("Indicated whether the  into movie has been played for a new character.")]
    public required bool MoviePlayed { get; set; }

    [Comment("The x position of the character's hearthstone.")]
    public required float HearthPositionX { get; set; }

    [Comment("The y position of the character's hearthstone.")]
    public required float HearthPositionY { get; set; }

    [Comment("The z position of the character's hearthstone.")]
    public required float HearthPositionZ { get; set; }

    [Comment("The map idenfier for the position of the character's hearthstone.")]
    public required byte HearthMapId { get; set; }

    [Comment("The zone idenfier for the position of the character's hearthstone.")]
    public required byte HearthZoneId { get; set; }

    [Comment("The guild idenfier for the character's guild.")]
    public int? GuiildId { get; set; }

    [Comment("The guild idenfier for the character's guild.")]
    public byte? GuildRank { get; set; }

    [MaxLength(255)]
    [Comment("The personal note displayed on the guild interface.")]
    public string? GuildPersonalNote { get; set; }

    [MaxLength(255)]
    [Comment("The officer note displayed on the guild interface.")]
    public string? GuildOfficerNote { get; set; }

    [Comment("The race of the characer.")]
    public required byte Race { get; set; }

    [Comment("The class of the characer.")]
    public required byte Class { get; set; }

    [Comment("The gender of the characer.")]
    public required byte Gender { get; set; }

    [Comment("The skin color of the characer.")]
    public required byte Skin { get; set; }

    [Comment("The face of the characer.")]
    public required byte Face { get; set; }

    [Comment("The hair style of the characer.")]
    public required byte HairStyle { get; set; }

    [Comment("The hair color of the characer.")]
    public required byte HairColor { get; set; }

    [Comment("The facial hair of the characer.")]
    public required byte FacialHair { get; set; }

    [Comment("The indenfier of the state of rest of the characer.")]
    public required byte RestState { get; set; }

    [Comment("The current mana of the characer.")]
    public required short Mana { get; set; }

    [Comment("The current energy of the characer.")]
    public required byte Energy { get; set; }

    [Comment("The current rage of the characer.")]
    public required byte Rage { get; set; }

    [Comment("The current health of the characer.")]
    public required short Health { get; set; }

    [Comment("The current mana type of the characer.")]
    public required byte ManaType { get; set; }

    [Comment("The current strength of the characer.")]
    public required byte Strength { get; set; }

    [Comment("The current agility of the characer.")]
    public required byte Agility { get; set; }

    [Comment("The current stamina of the characer.")]
    public required byte Stamina { get; set; }

    [Comment("The current intellect of the characer.")]
    public required byte Intellect { get; set; }

    [Comment("The current spirit of the characer.")]
    public required byte Spirit { get; set; }

    [Comment("The current money in total copper pieces of the characer.")]
    public required uint Copper { get; set; }

    [Comment("The faction relationship of the characer.")]
    public required byte WatchedFactionIndex { get; set; }

    [Comment("The encoded values of reputation of the characer.")]
    public required string Reputation { get; set; }

    [Comment("The encoded values of skills of the characer.")]
    public required string Skills { get; set; }

    [Comment("The encoded values of auras of the characer.")]
    public required string Auras { get; set; }

    [MaxLength(255)]
    [Comment("The encoded values of tutorials seen of the characer.")]
    public required string TutorialFlags { get; set; }

    [MaxLength(255)]
    [Comment("The encoded values of related taxis of the characer.")]
    public required string TaxiFlags { get; set; }

    [Comment("The encoded values of action bar mapped of the characer.")]
    public required string ActionBar { get; set; }

    [Comment("The encoded values of maps that have been explored by the characer.")]
    public required string MapsExplored { get; set; }

    [Comment("The restriction mask for the characer.")]
    public required byte Restrictions { get; set; }

    [Comment("The available talent points for the characer.")]
    public required byte TalentPoints { get; set; }

    [Comment("The number of bank slots unlocked for the characer.")]
    public required byte BankSlots { get; set; }

    [Comment("The assigned transport identifier for the characer.")]
    public required ulong TransportId { get; set; }

    public Honors? Honors { get; set; }

    public Corpse? Corpse { get; set; }

    public Ticket? Ticket { get; set; }

    public Guild? OwnedGuild { get; set; }

    public List<Pet> Pets { get; set; }

    public List<Friendship> Friendships { get; set; }

    public List<InstanceLock> InstanceLocks { get; set; }

    public List<Inventory> Inventory { get; set; }

    public List<Inventory> Creations { get; set; }

    public List<Inventory> Gifts { get; set; }

    public List<Mail> MailSent { get; set; }

    public List<Mail> MailRecieved { get; set; }

    public List<Quest> Quests { get; set; }

    public List<Spell> Spells { get; set; }

    public List<Petition> Petitions { get; set; }


    public Character()
    {
        Pets = [];
        Friendships = [];
        InstanceLocks = [];
        Inventory = [];
        Creations = [];
        Gifts = [];
        MailSent = [];
        MailRecieved = [];
        Quests = [];
        Spells = [];
        Petitions = [];
    }
}

