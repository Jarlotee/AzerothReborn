using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AzerothReborn.Data.Auth.Models;

[Comment("Uptime system")]
public class RealmUptime
{
    [Key]
    [Comment("The realm identifer")]
    public required uint RealmId { get; set; }

    [Comment("The timestamp of the realm starting.")]
    public required DateTime StartTimeUtc { get; set; }

    [Comment("The human readable description of the realm start timestamp.")]
    public required string StartTimeReadable { get; set; }

    [Comment("The uptime of the realm in seconds.")]
    public required ulong Uptime { get; set; }

    public required Realm Realm { get; set; }
}