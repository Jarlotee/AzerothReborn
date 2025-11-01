using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AzerothReborn.Data.Character.Models;

[Comment("Mail System")]
[Index(nameof(RecieverId), IsUnique = false)]
public class Mail
{
    [Key]
    [Comment("The mail identifier.")]
    public required int MailId { get; set; }

    [Comment("The character identifier for the sender.")]
    public uint? SenderId { get; set; }

    [Comment("The character identifier for the reciever.")]
    public uint? RecieverId { get; set; }

    [Comment("The type of mail sent.")]
    public required byte Type { get; set; }

    [Comment("The type of stationary used.")]
    public required short Stationary { get; set; } = 41;

    [MaxLength(255)]
    [Comment("The subject of the mail.")]
    public required string Subject { get; set; }

    [MaxLength(255)]
    [Comment("The body of the mail.")]
    public required string Body { get; set; }

    [Comment("The amount of money sent in the mail.")]
    public required uint CopperSent { get; set; }

    [Comment("The amount of money requuired to open the mail (COD).")]
    public required uint CopperRequred { get; set; }

    [Comment("The timestamp when the mail was sent.")]
    public required DateTime SentUtc { get; set; }

    [Comment("The state of the mail being read.")]
    public required byte Read { get; set; }

    [Comment("The item id being sent.")]
    public long? ItemId { get; set; }

    public Character? Sender { get; set; }

    public Character? Reciever { get; set; }

    public Inventory? Item { get; set; }
}