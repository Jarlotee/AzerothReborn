using Microsoft.EntityFrameworkCore;

namespace AzerothReborn.Data.Character.Models;

[Comment("Quests Tracking System")]
public class Quest
{
    [Comment("The character identifier.")]
    public required uint CharacterId { get; set; }

    [Comment("The quest identifier.")]
    public required uint QuestId { get; set; }

    [Comment("The status of the quest.")]
    public required int Status { get; set; } // is there a reference somewhere? seems like a big value

    public required Character Character { get; set; }
}


