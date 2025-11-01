using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzerothReborn.Data.Auth.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<long>(type: "bigint", nullable: false, comment: "The account identifier.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false, comment: "The account user name."),
                    ShaPasswordHash = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false, comment: "This field contains the encrypted SHA1 password."),
                    SecurityLevel = table.Column<byte>(type: "tinyint", nullable: false, comment: "The account security level."),
                    SessionKey = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "The Session Key."),
                    ValidationHash = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "The validated hash value."),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Password salt value."),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "Email address associated with the account"),
                    CreatedDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The date when the account was created."),
                    LastIpAddress = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, comment: "The IP used in the last login attempt."),
                    FailedLogins = table.Column<byte>(type: "tinyint", nullable: false, comment: "The number of failed logins attempted on the account."),
                    Locked = table.Column<bool>(type: "bit", nullable: false, comment: "Indicates whether the account has been locked or not."),
                    LastLoginUtc = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "The date when the account was last logged into."),
                    Expansion = table.Column<byte>(type: "tinyint", nullable: false, comment: "Which maximum expansion content a user has access to."),
                    Locale = table.Column<byte>(type: "tinyint", nullable: true, comment: "The locale used by the client logged into this account."),
                    OperatingSystem = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true, comment: "The Operating System of the connected client"),
                    MuteTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "The time when the account will be unmuted.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                },
                comment: "Account System");

            migrationBuilder.CreateTable(
                name: "IpAddressesBanned",
                columns: table => new
                {
                    IpAddress = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "The IP address that is banned."),
                    BanDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The date when the ip address was banned"),
                    UnBanDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The date when the ip address will be automatically unbanned."),
                    BannedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "The character that banned the account."),
                    BannedReason = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "The reason for the ban.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IpAddressesBanned", x => x.IpAddress);
                },
                comment: "Banned IP Addresses");

            migrationBuilder.CreateTable(
                name: "Realms",
                columns: table => new
                {
                    RealmId = table.Column<long>(type: "bigint", nullable: false, comment: "The realm identifer")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false, comment: "The name of the realm"),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "The public IP address of the realm server."),
                    LocalAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "The local IP address of the realm server."),
                    LocalSubnetMask = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "The subnet mask used for the local network."),
                    Port = table.Column<int>(type: "int", nullable: false, comment: "The port where the realm server is running."),
                    Icon = table.Column<byte>(type: "tinyint", nullable: false, comment: "The icon of the realm."),
                    RealmFlags = table.Column<byte>(type: "tinyint", nullable: false, comment: "Supported masks for the realm."),
                    TimeZone = table.Column<byte>(type: "tinyint", nullable: false, comment: "The realm timezone."),
                    AllowedSecurityLevel = table.Column<byte>(type: "tinyint", nullable: false, comment: "Minimum security level (see account) for realm visibility."),
                    Population = table.Column<decimal>(type: "decimal(20,0)", nullable: false, comment: "The current realm population."),
                    SupportedClients = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false, comment: "The supported clients that the realm will accept.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Realms", x => x.RealmId);
                },
                comment: "Realm System");

            migrationBuilder.CreateTable(
                name: "WardenLog",
                columns: table => new
                {
                    WardenLogId = table.Column<decimal>(type: "decimal(20,0)", nullable: false, comment: "The warden log identifer")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FailedCheckId = table.Column<short>(type: "smallint", nullable: false, comment: "The warden failed check inentifier"),
                    Action = table.Column<byte>(type: "tinyint", nullable: false, comment: "Action taken (see enum WardenActions)"),
                    AccountId = table.Column<long>(type: "bigint", nullable: false, comment: "The account identifier."),
                    PlayerGuid = table.Column<int>(type: "int", nullable: false, comment: "The player identifier."),
                    MapId = table.Column<int>(type: "int", nullable: false, comment: "The map identifier. (See map.dbc)"),
                    PositionX = table.Column<float>(type: "real", nullable: false, comment: "The x location of the character."),
                    PositionY = table.Column<float>(type: "real", nullable: false, comment: "The y location of the character."),
                    PositionZ = table.Column<float>(type: "real", nullable: false, comment: "The z location of the character."),
                    TimestampUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WardenLog", x => x.WardenLogId);
                },
                comment: "Warden log of failed checks");

            migrationBuilder.CreateTable(
                name: "AccountsBanned",
                columns: table => new
                {
                    AccountId = table.Column<long>(type: "bigint", nullable: false, comment: "The account identifier."),
                    BanDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The date when the account was banned"),
                    UnBanDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The date when the account will be automatically unbanned."),
                    BannedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "The character that banned the account."),
                    BannedReason = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "The reason for the ban."),
                    Active = table.Column<bool>(type: "bit", nullable: false, comment: "Is the ban is currently active or not.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountsBanned", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_AccountsBanned_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId");
                },
                comment: "Banned Account List");

            migrationBuilder.CreateTable(
                name: "RealmCharacters",
                columns: table => new
                {
                    RealmId = table.Column<long>(type: "bigint", nullable: false, comment: "The realm identifer"),
                    AccountId = table.Column<long>(type: "bigint", nullable: false, comment: "The account identifier."),
                    CharacterCount = table.Column<byte>(type: "tinyint", nullable: false, comment: "The number of characters the account has on the realm.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealmCharacters", x => new { x.AccountId, x.RealmId });
                    table.ForeignKey(
                        name: "FK_RealmCharacters_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId");
                    table.ForeignKey(
                        name: "FK_RealmCharacters_Realms_RealmId",
                        column: x => x.RealmId,
                        principalTable: "Realms",
                        principalColumn: "RealmId");
                },
                comment: "Realm Character Tracker");

            migrationBuilder.CreateTable(
                name: "RealmUptime",
                columns: table => new
                {
                    RealmId = table.Column<long>(type: "bigint", nullable: false, comment: "The realm identifer"),
                    StartTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The timestamp of the realm starting."),
                    StartTimeReadable = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "The human readable description of the realm start timestamp."),
                    Uptime = table.Column<decimal>(type: "decimal(20,0)", nullable: false, comment: "The uptime of the realm in seconds.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealmUptime", x => x.RealmId);
                    table.ForeignKey(
                        name: "FK_RealmUptime_Realms_RealmId",
                        column: x => x.RealmId,
                        principalTable: "Realms",
                        principalColumn: "RealmId");
                },
                comment: "Uptime system");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_SecurityLevel",
                table: "Accounts",
                column: "SecurityLevel");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserName",
                table: "Accounts",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountsBanned_AccountId_Active",
                table: "AccountsBanned",
                columns: new[] { "AccountId", "Active" });

            migrationBuilder.CreateIndex(
                name: "IX_IpAddressesBanned_IpAddress_BanDateUtc",
                table: "IpAddressesBanned",
                columns: new[] { "IpAddress", "BanDateUtc" });

            migrationBuilder.CreateIndex(
                name: "IX_RealmCharacters_RealmId",
                table: "RealmCharacters",
                column: "RealmId");

            migrationBuilder.CreateIndex(
                name: "IX_Realms_Name",
                table: "Realms",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountsBanned");

            migrationBuilder.DropTable(
                name: "IpAddressesBanned");

            migrationBuilder.DropTable(
                name: "RealmCharacters");

            migrationBuilder.DropTable(
                name: "RealmUptime");

            migrationBuilder.DropTable(
                name: "WardenLog");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Realms");
        }
    }
}
