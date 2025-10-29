using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AzerothReborn.Data.Auth.Models;

[Index(nameof(Name), IsUnique = true)]
[Comment("Realm System")]
public class Realm
{
    [Key]
    [Comment("The realm identifer")]
    public required uint RealmId { get; set; }

    [MaxLength(32)]
    [Comment("The name of the realm")]
    public required string Name { get; set; }

    [MaxLength(255)]
    [Comment("The public IP address of the realm server.")]
    public required string Address { get; set; } = "127.0.0.1";

    [MaxLength(255)]
    [Comment("The local IP address of the realm server.")]
    public required string LocalAddress { get; set; } = "127.0.0.1";

    [MaxLength(30)]
    [Comment("The subnet mask used for the local network.")]
    public required string LocalSubnetMask { get; set; } = "255.255.255.0";

    [Comment("The port where the realm server is running.")]
    public required ushort Port { get; set; } = 8085;

    [Comment("The icon of the realm.")]
    public required byte Icon { get; set; } = 0;

    [Comment("Supported masks for the realm.")]
    public required byte RealmFlags { get; set; } = 2;

    [Comment("The realm timezone.")]
    public required byte TimeZone { get; set; } = 0;

    [Comment("Minimum security level (see account) for realm visibility.")]
    public required byte AllowedSecurityLevel { get; set; } = 0;

    [Comment("The current realm population.")]
    public required ulong Population { get; set; } = 0;


    [MaxLength(64)]
    [Comment("The supported clients that the realm will accept.")]
    public required string SupportedClients { get; set; } = "5875";

    public RealmUptime? Uptime { get; set; }
    public required IEnumerable<RealmCharacters> Characters { get; set; }
}