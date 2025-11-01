using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AzerothReborn.Data.Character.Models;

[Index(nameof(MapId), nameof(GroupId), nameof(CharacterId), IsUnique = true)]
[Comment("Instance Lock System")]
public class InstanceLock
{
    [Key]
    [Comment("The instance lock identifier.")]
    public required long InstanceLockId { get; set; }

    [Comment("The character identifier.")]
    public uint? CharacterId { get; set; }

    [Comment("The group identifier.")]
    public uint? GroupId { get; set; } // is this necessary?

    [Comment("The identifier of the map that is locked.")]
    public required ushort MapId { get; set; }

    [Comment("The identifier of the instance that the group or characer is locked.")]
    public required uint InstanceId { get; set; }

    [Comment("The timestamp  of indicating when the lock will expire.")]
    public required DateTime ExpirationUtc { get; set; }

    public Character? Character { get; set; }
}


