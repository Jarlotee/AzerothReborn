using AzerothReborn.Data.Auth.Seeding;
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

    public Context(DbContextOptions<Context> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        /* uncomment for migration support */
        // optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=Auth;User Id=sa;Password=5@_password;TrustServerCertificate=True");
        optionsBuilder.UseAsyncSeeding(async (_context, _, cancellationToken) =>
        {
            await _context.SeedAccounts(cancellationToken);
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
