using Microsoft.EntityFrameworkCore;

namespace AzerothReborn.Data.Character.Models;

[Comment("Friend Social System")]
public class Friendship
{
    [Comment("The character identifier.")]
    public required uint CharacterId { get; set; }

    [Comment("The friend identifier.")]
    public required uint FriendId { get; set; }

    [Comment("The flags describing the friend.")]
    public required byte Flags { get; set; }

    public required Character Character { get; set; }

    public required Character Friend { get; set; }
}


