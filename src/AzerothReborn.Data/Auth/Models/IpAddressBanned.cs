using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AzerothReborn.Data.Auth.Models;

[Comment("Banned IP Addresses")]
[Index(nameof(IpAddress), nameof(BanDateUtc), IsUnique = false)]
public class IpAddressBanned
{
    [Key]
    [MaxLength(30)]
    [Comment("The IP address that is banned.")]
    public required string IpAddress { get; set; }

    [Comment("The date when the ip address was banned")]
    public required DateTime BanDateUtc { get; set; }

    [Comment("The date when the ip address will be automatically unbanned.")]
    public required DateTime UnBanDateUtc { get; set; }

    [MaxLength(50)]
    [Comment("The character that banned the account.")]
    public required string BannedBy { get; set; }

    [MaxLength(255)]
    [Comment("The reason for the ban.")]
    public required string BannedReason { get; set; }
}