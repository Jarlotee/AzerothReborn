using Microsoft.EntityFrameworkCore;

namespace AzerothReborn.Data.Auth;

public class Context : DbContext
{
    public DbSet<Models.Account> Accounts { get; set; }
    public DbSet<Models.AccountBanned> AccountsBanned { get; set; }
    public DbSet<Models.IpAddressBanned> IpAddressesBanned { get; set; }
    public DbSet<Models.Realm> Realms { get; set; }
    public DbSet<Models.RealmCharacters> RealmCharacters { get; set; }
    public DbSet<Models.RealmUptime> RealmUptime { get; set; }
    public DbSet<Models.WardenLog> WardenLog { get; set; }

    public Context() : base() { }

    public Context(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        /* uncomment for migration support */
        //optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=Auth;User Id=sa;Password=5@_password;TrustServerCertificate=True");
        optionsBuilder.UseAsyncSeeding(async (_context, _, cancellationToken) =>
        {
#if DEBUG
            if (await _context.Set<Models.Account>().CountAsync() == 0)
            {
                _context.Set<Models.Account>().AddRange([
                    new Models.Account {
                        UserName = "ADMINISTRATOR" ,
                        ShaPasswordHash = "a34b29541b87b7e4823683ce6c7bf6ae68beaaac",
                        SecurityLevel = 3,
                        Locked = false,
                        FailedLogins = 0,
                        Expansion = 0,
                        CreatedDateUtc = DateTime.UtcNow,
                    },
                    new Models.Account {
                        UserName = "GAMEMASTER" ,
                        ShaPasswordHash = "7841e21831d7c6bc0b57fbe7151eb82bd65ea1f9",
                        SecurityLevel = 2,
                        Locked = false,
                        FailedLogins = 0,
                        Expansion = 0,
                        CreatedDateUtc = DateTime.UtcNow,
                    },
                    new Models.Account {
                        UserName = "MODERATOR" ,
                        ShaPasswordHash = "a7f5fbff0b4eec2d6b6e78e38e8312e64d700008",
                        SecurityLevel = 1,
                        Locked = false,
                        FailedLogins = 0,
                        Expansion = 0,
                        CreatedDateUtc = DateTime.UtcNow,
                    },
                    new Models.Account {
                        UserName = "PLAYER" ,
                        ShaPasswordHash = "3ce8a96d17c5ae88a30681024e86279f1a38c041",
                        SecurityLevel = 0,
                        Locked = false,
                        FailedLogins = 0,
                        Expansion = 0,
                        CreatedDateUtc = DateTime.UtcNow,
                    },
                ]);

                await _context.SaveChangesAsync();
            }
#endif
        });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.AccountBanned>()
            .HasOne(e => e.Account)
            .WithOne(e => e.Ban)
            .OnDelete(DeleteBehavior.ClientCascade);

        modelBuilder.Entity<Models.RealmCharacters>()
            .HasOne(e => e.Realm)
            .WithMany(e => e.Characters)
            .OnDelete(DeleteBehavior.ClientCascade);

        modelBuilder.Entity<Models.RealmCharacters>()
            .HasOne(e => e.Account)
            .WithMany(e => e.Characters)
            .OnDelete(DeleteBehavior.ClientCascade);

        modelBuilder.Entity<Models.RealmCharacters>()
            .HasKey(o => new { o.AccountId, o.RealmId });

        modelBuilder.Entity<Models.RealmUptime>()
            .HasOne(e => e.Realm)
            .WithOne(e => e.Uptime)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}
