using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AzerothReborn.Data.Character.Models;

[Comment("Friend Social System")]
public class Ticket
{
    [Key]
    [Comment("The character identifier.")]
    public required uint CharacterId { get; set; }

    [Comment("The text of the ticket.")]
    public required string Text { get; set; }

    [Comment("The x position of the character.")]
    public required float PositionX { get; set; }

    [Comment("The y position of the character.")]
    public required float PositionY { get; set; }

    [Comment("The z position of the character.")]
    public required float PositionZ { get; set; }

    [Comment("The map idenfier for the position of the character.")]
    public required byte MapId { get; set; }

    public required Character Character { get; set; }
}


