using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AzerothReborn.Data.Auth.Models;

[Comment("Realm Character Tracker")]
public class RealmCharacters
{
    [Comment("The realm identifer")]
    public required uint RealmId { get; set; }

    [Comment("The account identifier.")]
    public required uint AccountId { get; set; }

    [Comment("The number of characters the account has on the realm.")]
    public required byte CharacterCount { get; set; } = 0;

    public required Realm Realm { get; set; }
    public required Account Account { get; set; }
}