using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AzerothReborn.Data.Auth.Models;

[Index(nameof(UserName), IsUnique = true)]
[Index(nameof(SecurityLevel), IsUnique = false)]
[Comment("Account System")]
public class Account
{
    [Key]
    [Comment("The account identifier.")]
    public uint AccountId { get; set; }

    [MaxLength(32)]
    [Comment("The account user name.")]
    public required string UserName { get; set; }

    [MaxLength(40)]
    [Comment("This field contains the encrypted SHA1 password.")]
    public required string ShaPasswordHash { get; set; }

    [Comment("The account security level.")]
    public required byte SecurityLevel { get; set; } = 0;

    [Comment("The Session Key.")]
    public string? SessionKey { get; set; }

    [Comment("The validated hash value.")]
    public string? ValidationHash { get; set; }

    [Comment("Password salt value.")]
    public string? Salt { get; set; }

    [MaxLength(255)]
    [Comment("Email address associated with the account")]
    public string? Email { get; set; }

    [Comment("The date when the account was created.")]
    public required DateTime CreatedDateUtc { get; set; } = DateTime.UtcNow;

    [MaxLength(30)]
    [Comment("The IP used in the last login attempt.")]
    public string? LastIpAddress { get; set; } = "0.0.0.0";

    [Comment("The number of failed logins attempted on the account.")]
    public required byte FailedLogins { get; set; } = 0;

    [Comment("Indicates whether the account has been locked or not.")]
    public required bool Locked { get; set; } = false;

    [Comment("The date when the account was last logged into.")]
    public DateTime? LastLoginUtc { get; set; }

    [Comment("Which maximum expansion content a user has access to.")]
    public required byte Expansion { get; set; } = 0;

    [Comment("The locale used by the client logged into this account.")]
    public byte? Locale { get; set; }

    [MaxLength(3)]
    [Comment("The Operating System of the connected client")]
    public string? OperatingSystem { get; set; }

    [Comment("The time when the account will be unmuted.")]
    public DateTime? MuteTime { get; set; }

    public AccountBanned? Ban { get; set; }

    public IEnumerable<RealmCharacters>? Characters { get; set; }
}