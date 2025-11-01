using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzerothReborn.Data.Character.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auctions",
                columns: table => new
                {
                    AuctionId = table.Column<long>(type: "bigint", nullable: false, comment: "The auction identifier.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BidPrice = table.Column<long>(type: "bigint", nullable: false, comment: "The current bid price."),
                    BuyoutPrice = table.Column<long>(type: "bigint", nullable: false, comment: "The buyout price."),
                    TimeLeft = table.Column<long>(type: "bigint", nullable: false, comment: "The time before the auction expires in seconds."),
                    BidderId = table.Column<long>(type: "bigint", nullable: false, comment: "The identifier of character who bid on the item."),
                    OwnerId = table.Column<long>(type: "bigint", nullable: false, comment: "The character who created the auction."),
                    TemplateId = table.Column<int>(type: "int", nullable: false, comment: "The items template id."),
                    ItemCount = table.Column<byte>(type: "tinyint", nullable: false, comment: "The count of items in the auction."),
                    ItemId = table.Column<long>(type: "bigint", nullable: false, comment: "The guid of the item.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auctions", x => x.AuctionId);
                },
                comment: "Auction System");

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    CharacterId = table.Column<long>(type: "bigint", nullable: false, comment: "The character identifier.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<long>(type: "bigint", nullable: false, comment: "The account identifier owning the character."),
                    Name = table.Column<long>(type: "bigint", maxLength: 21, nullable: false, comment: "The name of the character."),
                    Level = table.Column<byte>(type: "tinyint", nullable: false, comment: "The level of the character."),
                    Experience = table.Column<int>(type: "int", nullable: false, comment: "The experience of the character."),
                    RestedExperience = table.Column<byte>(type: "tinyint", nullable: false, comment: "The amount of rested experience the character has accrued."),
                    Online = table.Column<bool>(type: "bit", nullable: false, comment: "Indicates the character is online."),
                    LogoutTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Indicates when the character last logged out."),
                    PositionX = table.Column<float>(type: "real", nullable: false, comment: "The x position of the character."),
                    PositionY = table.Column<float>(type: "real", nullable: false, comment: "The y position of the character."),
                    PositionZ = table.Column<float>(type: "real", nullable: false, comment: "The z position of the character."),
                    Orientation = table.Column<float>(type: "real", nullable: false, comment: "The orientation of the character. (e.g. which way its facing)"),
                    MapId = table.Column<byte>(type: "tinyint", nullable: false, comment: "The map idenfier for the position of the character."),
                    ZoneId = table.Column<byte>(type: "tinyint", nullable: false, comment: "The zone idenfier for the position of the character."),
                    MoviePlayed = table.Column<bool>(type: "bit", nullable: false, comment: "Indicated whether the  into movie has been played for a new character."),
                    HearthPositionX = table.Column<float>(type: "real", nullable: false, comment: "The x position of the character's hearthstone."),
                    HearthPositionY = table.Column<float>(type: "real", nullable: false, comment: "The y position of the character's hearthstone."),
                    HearthPositionZ = table.Column<float>(type: "real", nullable: false, comment: "The z position of the character's hearthstone."),
                    HearthMapId = table.Column<byte>(type: "tinyint", nullable: false, comment: "The map idenfier for the position of the character's hearthstone."),
                    HearthZoneId = table.Column<byte>(type: "tinyint", nullable: false, comment: "The zone idenfier for the position of the character's hearthstone."),
                    GuiildId = table.Column<int>(type: "int", nullable: true, comment: "The guild idenfier for the character's guild."),
                    GuildRank = table.Column<byte>(type: "tinyint", nullable: true, comment: "The guild idenfier for the character's guild."),
                    GuildPersonalNote = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "The personal note displayed on the guild interface."),
                    GuildOfficerNote = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "The officer note displayed on the guild interface."),
                    Race = table.Column<byte>(type: "tinyint", nullable: false, comment: "The race of the characer."),
                    Class = table.Column<byte>(type: "tinyint", nullable: false, comment: "The class of the characer."),
                    Gender = table.Column<byte>(type: "tinyint", nullable: false, comment: "The gender of the characer."),
                    Skin = table.Column<byte>(type: "tinyint", nullable: false, comment: "The skin color of the characer."),
                    Face = table.Column<byte>(type: "tinyint", nullable: false, comment: "The face of the characer."),
                    HairStyle = table.Column<byte>(type: "tinyint", nullable: false, comment: "The hair style of the characer."),
                    HairColor = table.Column<byte>(type: "tinyint", nullable: false, comment: "The hair color of the characer."),
                    FacialHair = table.Column<byte>(type: "tinyint", nullable: false, comment: "The facial hair of the characer."),
                    RestState = table.Column<byte>(type: "tinyint", nullable: false, comment: "The indenfier of the state of rest of the characer."),
                    Mana = table.Column<short>(type: "smallint", nullable: false, comment: "The current mana of the characer."),
                    Energy = table.Column<byte>(type: "tinyint", nullable: false, comment: "The current energy of the characer."),
                    Rage = table.Column<byte>(type: "tinyint", nullable: false, comment: "The current rage of the characer."),
                    Health = table.Column<short>(type: "smallint", nullable: false, comment: "The current health of the characer."),
                    ManaType = table.Column<byte>(type: "tinyint", nullable: false, comment: "The current mana type of the characer."),
                    Strength = table.Column<byte>(type: "tinyint", nullable: false, comment: "The current strength of the characer."),
                    Agility = table.Column<byte>(type: "tinyint", nullable: false, comment: "The current agility of the characer."),
                    Stamina = table.Column<byte>(type: "tinyint", nullable: false, comment: "The current stamina of the characer."),
                    Intellect = table.Column<byte>(type: "tinyint", nullable: false, comment: "The current intellect of the characer."),
                    Spirit = table.Column<byte>(type: "tinyint", nullable: false, comment: "The current spirit of the characer."),
                    Copper = table.Column<long>(type: "bigint", nullable: false, comment: "The current money in total copper pieces of the characer."),
                    WatchedFactionIndex = table.Column<byte>(type: "tinyint", nullable: false, comment: "The faction relationship of the characer."),
                    Reputation = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "The encoded values of reputation of the characer."),
                    Skills = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "The encoded values of skills of the characer."),
                    Auras = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "The encoded values of auras of the characer."),
                    TutorialFlags = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "The encoded values of tutorials seen of the characer."),
                    TaxiFlags = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "The encoded values of related taxis of the characer."),
                    ActionBar = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "The encoded values of action bar mapped of the characer."),
                    MapsExplored = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "The encoded values of maps that have been explored by the characer."),
                    Restrictions = table.Column<byte>(type: "tinyint", nullable: false, comment: "The restriction mask for the characer."),
                    TalentPoints = table.Column<byte>(type: "tinyint", nullable: false, comment: "The available talent points for the characer."),
                    BankSlots = table.Column<byte>(type: "tinyint", nullable: false, comment: "The number of bank slots unlocked for the characer."),
                    TransportId = table.Column<decimal>(type: "decimal(20,0)", nullable: false, comment: "The assigned transport identifier for the characer.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.CharacterId);
                },
                comment: "Character System");

            migrationBuilder.CreateTable(
                name: "Corpses",
                columns: table => new
                {
                    CorpseId = table.Column<long>(type: "bigint", nullable: false, comment: "The corpse identifier.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterId = table.Column<long>(type: "bigint", nullable: false, comment: "The character identifier."),
                    PositionX = table.Column<float>(type: "real", nullable: false, comment: "The x position of the corpse."),
                    PositionY = table.Column<float>(type: "real", nullable: false, comment: "The y position of the corpse."),
                    PositionZ = table.Column<float>(type: "real", nullable: false, comment: "The z position of the corpse."),
                    Orientation = table.Column<float>(type: "real", nullable: false, comment: "The orientation of the corpse. (e.g. which way its facing)"),
                    MapId = table.Column<byte>(type: "tinyint", nullable: false, comment: "The map idenfier for the position of the corpse."),
                    TimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The timestamp of death."),
                    Type = table.Column<byte>(type: "tinyint", nullable: false, comment: "The type of corpse."),
                    InstanceId = table.Column<byte>(type: "tinyint", nullable: true, comment: "The instance id where the corpse resides.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corpses", x => x.CorpseId);
                    table.ForeignKey(
                        name: "FK_Corpses_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId");
                },
                comment: "Death System");

            migrationBuilder.CreateTable(
                name: "Friendships",
                columns: table => new
                {
                    CharacterId = table.Column<long>(type: "bigint", nullable: false, comment: "The character identifier."),
                    FriendId = table.Column<long>(type: "bigint", nullable: false, comment: "The friend identifier."),
                    Flags = table.Column<byte>(type: "tinyint", nullable: false, comment: "The flags describing the friend.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendships", x => new { x.CharacterId, x.FriendId });
                    table.ForeignKey(
                        name: "FK_Friendships_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId");
                    table.ForeignKey(
                        name: "FK_Friendships_Characters_FriendId",
                        column: x => x.FriendId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Friend Social System");

            migrationBuilder.CreateTable(
                name: "Guilds",
                columns: table => new
                {
                    GuildId = table.Column<long>(type: "bigint", nullable: false, comment: "The guild identifier.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "The name of the guild."),
                    LeaderId = table.Column<long>(type: "bigint", nullable: false, comment: "The character identifier of the character is the guild leader."),
                    MessageOfTheDay = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "The message of the day for the guild."),
                    Info = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "The informational message for the guild."),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The timestamp of the creation of the guild."),
                    EmblemStyle = table.Column<byte>(type: "tinyint", nullable: false, comment: "The style of the emblem of the guild."),
                    EmblemColor = table.Column<byte>(type: "tinyint", nullable: false, comment: "The color of the emblem of the guild."),
                    BorderStyle = table.Column<byte>(type: "tinyint", nullable: false, comment: "The style of the border of the guild."),
                    BorderColor = table.Column<byte>(type: "tinyint", nullable: false, comment: "The color of the border of the guild."),
                    BackgroundColor = table.Column<byte>(type: "tinyint", nullable: false, comment: "The color of the background of the guild."),
                    Rank0 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "The name of rank 0 within the guild."),
                    Rank0Rights = table.Column<int>(type: "int", nullable: false, comment: "The rights of rank 0 within the guild."),
                    Rank1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "The name of rank 1 within the guild."),
                    Rank1Rights = table.Column<int>(type: "int", nullable: false, comment: "The rights of rank 1 within the guild."),
                    Rank2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "The name of rank 2 within the guild."),
                    Rank2Rights = table.Column<int>(type: "int", nullable: false, comment: "The rights of rank 2 within the guild."),
                    Rank3 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "The name of rank 3 within the guild."),
                    Rank3Rights = table.Column<int>(type: "int", nullable: false, comment: "The rights of rank 3 within the guild."),
                    Rank4 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "The name of rank 4 within the guild."),
                    Rank4Rights = table.Column<int>(type: "int", nullable: false, comment: "The rights of rank 4 within the guild."),
                    Rank5 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "The name of rank 5 within the guild."),
                    Rank5Rights = table.Column<int>(type: "int", nullable: false, comment: "The rights of rank 5 within the guild."),
                    Rank6 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "The name of rank 6 within the guild."),
                    Rank6Rights = table.Column<int>(type: "int", nullable: false, comment: "The rights of rank 6 within the guild."),
                    Rank7 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "The name of rank 7 within the guild."),
                    Rank7Rights = table.Column<int>(type: "int", nullable: false, comment: "The rights of rank 7 within the guild."),
                    Rank8 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "The name of rank 8 within the guild."),
                    Rank8Rights = table.Column<int>(type: "int", nullable: false, comment: "The rights of rank 8 within the guild."),
                    Rank9 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "The name of rank 9 within the guild."),
                    Rank9Rights = table.Column<int>(type: "int", nullable: false, comment: "The rights of rank 9 within the guild.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guilds", x => x.GuildId);
                    table.ForeignKey(
                        name: "FK_Guilds_Characters_LeaderId",
                        column: x => x.LeaderId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId");
                },
                comment: "Guild System");

            migrationBuilder.CreateTable(
                name: "Honors",
                columns: table => new
                {
                    CharacterId = table.Column<long>(type: "bigint", nullable: false, comment: "The character identifier."),
                    Points = table.Column<int>(type: "int", nullable: false, comment: "The honor points for the character."),
                    Rank = table.Column<byte>(type: "tinyint", nullable: false, comment: "The current honor rank for the character."),
                    HighestRank = table.Column<byte>(type: "tinyint", nullable: false, comment: "The highest honor rank for the character."),
                    HonorableKills = table.Column<int>(type: "int", nullable: false, comment: "The total honorable kills for the character."),
                    DishonorableKills = table.Column<int>(type: "int", nullable: false, comment: "The total dishonorable kills for the character."),
                    PointsThisWeek = table.Column<short>(type: "smallint", nullable: false, comment: "The honor points for this week for the character."),
                    PointsLastWeek = table.Column<int>(type: "int", nullable: false, comment: "The honor points for the prior week for the character."),
                    PointsYesterday = table.Column<int>(type: "int", nullable: false, comment: "The honor points accrued yesterday for the character."),
                    KillsThisWeek = table.Column<int>(type: "int", nullable: false, comment: "The total kills for this week for the character."),
                    KillsLastWeek = table.Column<int>(type: "int", nullable: false, comment: "The total kills for the prior week for the character."),
                    KillsYesterday = table.Column<int>(type: "int", nullable: false, comment: "The total kills accrued yesterday for the character."),
                    HonrableKillsToday = table.Column<short>(type: "smallint", nullable: false, comment: "The total honorable kills accrued today for the character."),
                    DishonorableKillsToday = table.Column<short>(type: "smallint", nullable: false, comment: "The total honorable kills accrued today for the character.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Honors", x => x.CharacterId);
                    table.ForeignKey(
                        name: "FK_Honors_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId");
                },
                comment: "Honor System");

            migrationBuilder.CreateTable(
                name: "InstanceLocks",
                columns: table => new
                {
                    InstanceLockId = table.Column<long>(type: "bigint", nullable: false, comment: "The instance lock identifier.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterId = table.Column<long>(type: "bigint", nullable: true, comment: "The character identifier."),
                    GroupId = table.Column<long>(type: "bigint", nullable: true, comment: "The group identifier."),
                    MapId = table.Column<int>(type: "int", nullable: false, comment: "The identifier of the map that is locked."),
                    InstanceId = table.Column<long>(type: "bigint", nullable: false, comment: "The identifier of the instance that the group or characer is locked."),
                    ExpirationUtc = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The timestamp  of indicating when the lock will expire.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstanceLocks", x => x.InstanceLockId);
                    table.ForeignKey(
                        name: "FK_InstanceLocks_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId");
                },
                comment: "Instance Lock System");

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    ItemId = table.Column<long>(type: "bigint", nullable: false, comment: "The item identifier.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateId = table.Column<int>(type: "int", nullable: false, comment: "The items template identifier."),
                    Slot = table.Column<byte>(type: "tinyint", nullable: false, comment: "The slot to which the item is assigned."),
                    Bag = table.Column<long>(type: "bigint", nullable: false, comment: "The bag to which the item is assigned."),
                    OwnerId = table.Column<long>(type: "bigint", nullable: false, comment: "The character identifier."),
                    CreatorId = table.Column<long>(type: "bigint", nullable: true, comment: "The character identifier indicating who created the item."),
                    GifterId = table.Column<long>(type: "bigint", nullable: true, comment: "The character identifier indicating who gifted the item."),
                    Count = table.Column<byte>(type: "tinyint", nullable: false, comment: "The count of items in the stack."),
                    Durability = table.Column<short>(type: "smallint", nullable: false, comment: "The current durability of the item."),
                    Flags = table.Column<short>(type: "smallint", nullable: false, comment: "The flags set of the item."),
                    Charges = table.Column<byte>(type: "tinyint", nullable: false, comment: "The current charges available on the item."),
                    TextId = table.Column<short>(type: "smallint", nullable: false, comment: "The text identifier for the item."),
                    Enchantment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "The enchantment name for the item."),
                    RandomProperties = table.Column<short>(type: "smallint", nullable: false, comment: "The random properties for the item.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Inventories_Characters_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId");
                    table.ForeignKey(
                        name: "FK_Inventories_Characters_GifterId",
                        column: x => x.GifterId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId");
                    table.ForeignKey(
                        name: "FK_Inventories_Characters_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId");
                },
                comment: "Inventory System");

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    PetId = table.Column<long>(type: "bigint", nullable: false, comment: "The pet identifier.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterId = table.Column<long>(type: "bigint", nullable: false, comment: "The identifer of the owning character."),
                    Entry = table.Column<long>(type: "bigint", nullable: false, comment: "No idea. Maybe and index for when you have multiple pets?"),
                    ModelId = table.Column<long>(type: "bigint", nullable: false, comment: "The model identifer"),
                    CreatedBySpellId = table.Column<long>(type: "bigint", nullable: false, comment: "The spell identifer used to create the pet."),
                    PetType = table.Column<byte>(type: "tinyint", nullable: false, comment: "The category of the pet."),
                    Level = table.Column<long>(type: "bigint", nullable: false, comment: "The level of the pet."),
                    Experience = table.Column<long>(type: "bigint", nullable: false, comment: "The current experience of the pet."),
                    ReactState = table.Column<byte>(type: "tinyint", nullable: false, comment: "The current behavior of the pet."),
                    LoyaltyPoints = table.Column<long>(type: "bigint", nullable: false, comment: "The current loyalty points of the pet."),
                    Loyalty = table.Column<int>(type: "int", nullable: false, comment: "The current loyalty level of the pet."),
                    TrainingPoints = table.Column<int>(type: "int", nullable: false, comment: "The current training points of the pet."),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "The current training points of the pet."),
                    Renamed = table.Column<bool>(type: "bit", nullable: false, comment: "Indicates the pet was renamed."),
                    Slot = table.Column<long>(type: "bigint", nullable: false, comment: "Which slot the pet is equiped to."),
                    Health = table.Column<long>(type: "bigint", nullable: false, comment: "The current health of the pet."),
                    Mana = table.Column<long>(type: "bigint", nullable: false, comment: "The current mana of the pet."),
                    Hapiness = table.Column<int>(type: "int", nullable: false, comment: "The current hapiness of the pet."),
                    LastSaveUtc = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The timestamp of the pets last save."),
                    ResetTalentsCost = table.Column<int>(type: "int", nullable: false, comment: "The cost required to reset the pets talents."),
                    ResetTalentsTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The cooldown required to reset the pets talents."),
                    AB = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Who knows?"),
                    TeachSpellData = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Who knows?")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.PetId);
                    table.ForeignKey(
                        name: "FK_Pets_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId");
                },
                comment: "Pet System");

            migrationBuilder.CreateTable(
                name: "Quests",
                columns: table => new
                {
                    CharacterId = table.Column<long>(type: "bigint", nullable: false, comment: "The character identifier."),
                    QuestId = table.Column<long>(type: "bigint", nullable: false, comment: "The quest identifier."),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "The status of the quest.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quests", x => new { x.CharacterId, x.QuestId });
                    table.ForeignKey(
                        name: "FK_Quests_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId");
                },
                comment: "Quests Tracking System");

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    CharacterId = table.Column<long>(type: "bigint", nullable: false, comment: "The character identifier."),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "The text of the ticket."),
                    PositionX = table.Column<float>(type: "real", nullable: false, comment: "The x position of the character."),
                    PositionY = table.Column<float>(type: "real", nullable: false, comment: "The y position of the character."),
                    PositionZ = table.Column<float>(type: "real", nullable: false, comment: "The z position of the character."),
                    MapId = table.Column<byte>(type: "tinyint", nullable: false, comment: "The map idenfier for the position of the character.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.CharacterId);
                    table.ForeignKey(
                        name: "FK_Tickets_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId");
                },
                comment: "Friend Social System");

            migrationBuilder.CreateTable(
                name: "Mail",
                columns: table => new
                {
                    MailId = table.Column<int>(type: "int", nullable: false, comment: "The mail identifier.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<long>(type: "bigint", nullable: true, comment: "The character identifier for the sender."),
                    RecieverId = table.Column<long>(type: "bigint", nullable: true, comment: "The character identifier for the reciever."),
                    Type = table.Column<byte>(type: "tinyint", nullable: false, comment: "The type of mail sent."),
                    Stationary = table.Column<short>(type: "smallint", nullable: false, comment: "The type of stationary used."),
                    Subject = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "The subject of the mail."),
                    Body = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "The body of the mail."),
                    CopperSent = table.Column<long>(type: "bigint", nullable: false, comment: "The amount of money sent in the mail."),
                    CopperRequred = table.Column<long>(type: "bigint", nullable: false, comment: "The amount of money requuired to open the mail (COD)."),
                    SentUtc = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The timestamp when the mail was sent."),
                    Read = table.Column<byte>(type: "tinyint", nullable: false, comment: "The state of the mail being read."),
                    ItemId = table.Column<long>(type: "bigint", nullable: true, comment: "The item id being sent.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mail", x => x.MailId);
                    table.ForeignKey(
                        name: "FK_Mail_Characters_RecieverId",
                        column: x => x.RecieverId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId");
                    table.ForeignKey(
                        name: "FK_Mail_Characters_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId");
                    table.ForeignKey(
                        name: "FK_Mail_Inventories_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Inventories",
                        principalColumn: "ItemId");
                },
                comment: "Mail System");

            migrationBuilder.CreateTable(
                name: "Petitions",
                columns: table => new
                {
                    PetitionId = table.Column<long>(type: "bigint", nullable: false, comment: "The petition identifier.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<long>(type: "bigint", nullable: false, comment: "The item identifier."),
                    OwnerId = table.Column<long>(type: "bigint", nullable: false, comment: "The character identifier of the character who owns the petition."),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "The petition name."),
                    Type = table.Column<byte>(type: "tinyint", nullable: false, comment: "The petition type."),
                    Signatures = table.Column<byte>(type: "tinyint", nullable: false, comment: "The number of signatures gathered."),
                    Signer1 = table.Column<long>(type: "bigint", nullable: true, comment: "The character identifier of the signature gathered."),
                    Signer2 = table.Column<long>(type: "bigint", nullable: true, comment: "The character identifier of the signature gathered."),
                    Signer3 = table.Column<long>(type: "bigint", nullable: true, comment: "The character identifier of the signature gathered."),
                    Signer4 = table.Column<long>(type: "bigint", nullable: true, comment: "The character identifier of the signature gathered."),
                    Signer5 = table.Column<long>(type: "bigint", nullable: true, comment: "The character identifier of the signature gathered."),
                    Signer6 = table.Column<long>(type: "bigint", nullable: true, comment: "The character identifier of the signature gathered."),
                    Signer7 = table.Column<long>(type: "bigint", nullable: true, comment: "The character identifier of the signature gathered."),
                    Signer8 = table.Column<long>(type: "bigint", nullable: true, comment: "The character identifier of the signature gathered."),
                    Signer9 = table.Column<long>(type: "bigint", nullable: true, comment: "The character identifier of the signature gathered.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Petitions", x => x.PetitionId);
                    table.ForeignKey(
                        name: "FK_Petitions_Characters_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId");
                    table.ForeignKey(
                        name: "FK_Petitions_Inventories_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Inventories",
                        principalColumn: "ItemId");
                },
                comment: "Guild System");

            migrationBuilder.CreateTable(
                name: "Spells",
                columns: table => new
                {
                    CharacterId = table.Column<long>(type: "bigint", nullable: false, comment: "The character identifier."),
                    SpellId = table.Column<long>(type: "bigint", nullable: false, comment: "The spell identifier."),
                    Active = table.Column<bool>(type: "bit", nullable: false, comment: "Indicates if the spell is active."),
                    Cooldown = table.Column<long>(type: "bigint", nullable: false, comment: "The cooldown of the spell."),
                    ItemId = table.Column<long>(type: "bigint", nullable: true, comment: "The item identifier used to cast the spell.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spells", x => new { x.CharacterId, x.SpellId });
                    table.ForeignKey(
                        name: "FK_Spells_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId");
                    table.ForeignKey(
                        name: "FK_Spells_Inventories_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Inventories",
                        principalColumn: "ItemId");
                },
                comment: "Spell Tracking System");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_TemplateId",
                table: "Auctions",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_Name",
                table: "Characters",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Corpses_CharacterId",
                table: "Corpses",
                column: "CharacterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Corpses_InstanceId",
                table: "Corpses",
                column: "InstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Corpses_TimeUtc",
                table: "Corpses",
                column: "TimeUtc");

            migrationBuilder.CreateIndex(
                name: "IX_Corpses_Type",
                table: "Corpses",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_FriendId",
                table: "Friendships",
                column: "FriendId");

            migrationBuilder.CreateIndex(
                name: "IX_Guilds_LeaderId",
                table: "Guilds",
                column: "LeaderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InstanceLocks_CharacterId",
                table: "InstanceLocks",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_InstanceLocks_MapId_GroupId_CharacterId",
                table: "InstanceLocks",
                columns: new[] { "MapId", "GroupId", "CharacterId" },
                unique: true,
                filter: "[GroupId] IS NOT NULL AND [CharacterId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_CreatorId",
                table: "Inventories",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_GifterId",
                table: "Inventories",
                column: "GifterId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_OwnerId",
                table: "Inventories",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Mail_ItemId",
                table: "Mail",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Mail_RecieverId",
                table: "Mail",
                column: "RecieverId");

            migrationBuilder.CreateIndex(
                name: "IX_Mail_SenderId",
                table: "Mail",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Petitions_ItemId",
                table: "Petitions",
                column: "ItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Petitions_OwnerId",
                table: "Petitions",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_CharacterId",
                table: "Pets",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_Spells_ItemId",
                table: "Spells",
                column: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auctions");

            migrationBuilder.DropTable(
                name: "Corpses");

            migrationBuilder.DropTable(
                name: "Friendships");

            migrationBuilder.DropTable(
                name: "Guilds");

            migrationBuilder.DropTable(
                name: "Honors");

            migrationBuilder.DropTable(
                name: "InstanceLocks");

            migrationBuilder.DropTable(
                name: "Mail");

            migrationBuilder.DropTable(
                name: "Petitions");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "Quests");

            migrationBuilder.DropTable(
                name: "Spells");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Characters");
        }
    }
}
