using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AzerothReborn.Data.Character.Models;

[Comment("Guild System")]
public class Guild
{
    [Key]
    [Comment("The guild identifier.")]
    public required uint GuildId { get; set; }

    [MaxLength(255)]
    [Comment("The name of the guild.")]
    public required string Name { get; set; }

    [Comment("The character identifier of the character is the guild leader.")]
    public required uint LeaderId { get; set; }

    [MaxLength(255)]
    [Comment("The message of the day for the guild.")]
    public required string MessageOfTheDay { get; set; }

    [MaxLength(255)]
    [Comment("The informational message for the guild.")]
    public required string Info { get; set; }

    [Comment("The timestamp of the creation of the guild.")]
    public required DateTime CreatedUtc { get; set; }

    [Comment("The style of the emblem of the guild.")]
    public required byte EmblemStyle { get; set; }

    [Comment("The color of the emblem of the guild.")]
    public required byte EmblemColor { get; set; }

    [Comment("The style of the border of the guild.")]
    public required byte BorderStyle { get; set; }

    [Comment("The color of the border of the guild.")]
    public required byte BorderColor { get; set; }

    [Comment("The color of the background of the guild.")]
    public required byte BackgroundColor { get; set; }

    [MaxLength(255)]
    [Comment("The name of rank 0 within the guild.")]
    public required string Rank0 { get; set; } = "Guild Master";

    [Comment("The rights of rank 0 within the guild.")]
    public required int Rank0Rights { get; set; } = 61951;

    [MaxLength(255)]
    [Comment("The name of rank 1 within the guild.")]
    public required string Rank1 { get; set; } = "Officer";

    [Comment("The rights of rank 1 within the guild.")]
    public required int Rank1Rights { get; set; } = 67;

    [MaxLength(255)]
    [Comment("The name of rank 2 within the guild.")]
    public required string Rank2 { get; set; } = "Veteran";

    [Comment("The rights of rank 2 within the guild.")]
    public required int Rank2Rights { get; set; } = 67;

    [MaxLength(255)]
    [Comment("The name of rank 3 within the guild.")]
    public required string Rank3 { get; set; } = "Member";

    [Comment("The rights of rank 3 within the guild.")]
    public required int Rank3Rights { get; set; } = 67;

    [MaxLength(255)]
    [Comment("The name of rank 4 within the guild.")]
    public required string Rank4 { get; set; } = "Initiate";

    [Comment("The rights of rank 4 within the guild.")]
    public required int Rank4Rights { get; set; } = 67;

    [MaxLength(255)]
    [Comment("The name of rank 5 within the guild.")]
    public required string Rank5 { get; set; } = "";

    [Comment("The rights of rank 5 within the guild.")]
    public required int Rank5Rights { get; set; } = 0;

    [MaxLength(255)]
    [Comment("The name of rank 6 within the guild.")]
    public required string Rank6 { get; set; } = "";

    [Comment("The rights of rank 6 within the guild.")]
    public required int Rank6Rights { get; set; } = 0;

    [MaxLength(255)]
    [Comment("The name of rank 7 within the guild.")]
    public required string Rank7 { get; set; } = "";

    [Comment("The rights of rank 7 within the guild.")]
    public required int Rank7Rights { get; set; } = 0;

    [MaxLength(255)]
    [Comment("The name of rank 8 within the guild.")]
    public required string Rank8 { get; set; } = "";

    [Comment("The rights of rank 8 within the guild.")]
    public required int Rank8Rights { get; set; } = 0;

    [MaxLength(255)]
    [Comment("The name of rank 9 within the guild.")]
    public required string Rank9 { get; set; } = "";

    [Comment("The rights of rank 9 within the guild.")]
    public required int Rank9Rights { get; set; } = 0;

    public required Character Leader { get; set; }
}