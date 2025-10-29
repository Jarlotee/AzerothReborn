using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AzerothReborn.Data.Auth.Models;

[Comment("Banned Account List")]
[Index(nameof(AccountId), nameof(Active), IsUnique = false)]
public class AccountBanned
{
    [Key]
    [Comment("The account identifier.")]
    public required uint AccountId { get; set; }

    [Comment("The date when the account was banned")]
    public required DateTime BanDateUtc { get; set; }

    [Comment("The date when the account will be automatically unbanned.")]
    public required DateTime UnBanDateUtc { get; set; }

    [MaxLength(50)]
    [Comment("The character that banned the account.")]
    public required string BannedBy { get; set; }

    [MaxLength(255)]
    [Comment("The reason for the ban.")]
    public required string BannedReason { get; set; }

    [Comment("Is the ban is currently active or not.")]
    public required bool Active { get; set; } = false;

    public required Account Account { get; set; }
}