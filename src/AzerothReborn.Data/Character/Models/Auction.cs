using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AzerothReborn.Data.Character.Models;

[Index(nameof(TemplateId), IsUnique = false)]
[Comment("Auction System")]
public class Auction
{
    [Key]
    [Comment("The auction identifier.")]
    public required uint AuctionId { get; set; }

    [Comment("The current bid price.")]
    public required uint BidPrice { get; set; }

    [Comment("The buyout price.")]
    public required uint BuyoutPrice { get; set; }

    [Comment("The time before the auction expires in seconds.")]
    public required uint TimeLeft { get; set; }

    [Comment("The identifier of character who bid on the item.")]
    public required uint BidderId { get; set; }

    [Comment("The character who created the auction.")]
    public required uint OwnerId { get; set; }

    [Comment("The items template id.")]
    public required ushort TemplateId { get; set; }

    [Comment("The count of items in the auction.")]
    public required byte ItemCount { get; set; }

    [Comment("The guid of the item.")]
    public required uint ItemId { get; set; }

    // TODO consider mapping to Inventory and Character objects with cascades like mail
}


