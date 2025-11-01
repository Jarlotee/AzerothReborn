using Microsoft.EntityFrameworkCore;

namespace AzerothReborn.Data.Character;

public class Context : DbContext
{
    public DbSet<Models.Auction> Auctions { get; set; }
    public DbSet<Models.Pet> Pets { get; set; }
    public DbSet<Models.Character> Characters { get; set; }
    public DbSet<Models.Honors> Honors { get; set; }
    public DbSet<Models.Friendship> Friendships { get; set; }
    public DbSet<Models.InstanceLock> InstanceLocks { get; set; }
    public DbSet<Models.Inventory> Inventories { get; set; }
    public DbSet<Models.Mail> Mail { get; set; }
    public DbSet<Models.Quest> Quests { get; set; }
    public DbSet<Models.Spell> Spells { get; set; }
    public DbSet<Models.Ticket> Tickets { get; set; }
    public DbSet<Models.Corpse> Corpses { get; set; }
    public DbSet<Models.Petition> Petitions { get; set; }
    public DbSet<Models.Guild> Guilds { get; set; }

    public Context() : base() { }

    public Context(DbContextOptions<Context> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        /* uncomment for migration support */
        // optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=Character;User Id=sa;Password=5@_password;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.Pet>()
            .HasOne(e => e.Character)
            .WithMany(e => e.Pets)
            .OnDelete(DeleteBehavior.ClientCascade);

        modelBuilder.Entity<Models.Friendship>()
            .HasKey(e => new { e.CharacterId, e.FriendId });

        modelBuilder.Entity<Models.Friendship>()
            .HasOne(e => e.Character)
            .WithMany(e => e.Friendships)
            .OnDelete(DeleteBehavior.ClientCascade);

        modelBuilder.Entity<Models.Honors>()
            .HasOne(e => e.Character)
            .WithOne(e => e.Honors)
            .OnDelete(DeleteBehavior.ClientCascade);

        modelBuilder.Entity<Models.InstanceLock>()
            .HasOne(e => e.Character)
            .WithMany(e => e.InstanceLocks)
            .OnDelete(DeleteBehavior.ClientCascade);

        modelBuilder.Entity<Models.Inventory>()
            .HasOne(e => e.Owner)
            .WithMany(e => e.Inventory)
            .OnDelete(DeleteBehavior.ClientCascade);

        modelBuilder.Entity<Models.Inventory>()
            .HasOne(e => e.Creator)
            .WithMany(e => e.Creations)
            .OnDelete(DeleteBehavior.ClientSetNull);

        modelBuilder.Entity<Models.Inventory>()
            .HasOne(e => e.Gifter)
            .WithMany(e => e.Gifts)
            .OnDelete(DeleteBehavior.ClientSetNull);

        modelBuilder.Entity<Models.Mail>()
            .HasOne(e => e.Sender)
            .WithMany(e => e.MailSent)
            .OnDelete(DeleteBehavior.ClientSetNull);

        modelBuilder.Entity<Models.Mail>()
            .HasOne(e => e.Reciever)
            .WithMany(e => e.MailRecieved)
            .OnDelete(DeleteBehavior.ClientSetNull);

        modelBuilder.Entity<Models.Quest>()
            .HasKey(e => new { e.CharacterId, e.QuestId });

        modelBuilder.Entity<Models.Quest>()
            .HasOne(e => e.Character)
            .WithMany(e => e.Quests)
            .OnDelete(DeleteBehavior.ClientCascade);

        modelBuilder.Entity<Models.Spell>()
            .HasKey(e => new { e.CharacterId, e.SpellId });

        modelBuilder.Entity<Models.Spell>()
            .HasOne(e => e.Character)
            .WithMany(e => e.Spells)
            .OnDelete(DeleteBehavior.ClientCascade);

        modelBuilder.Entity<Models.Ticket>()
            .HasOne(e => e.Character)
            .WithOne(e => e.Ticket)
            .OnDelete(DeleteBehavior.ClientCascade);

        modelBuilder.Entity<Models.Corpse>()
            .HasOne(e => e.Character)
            .WithOne(e => e.Corpse)
            .OnDelete(DeleteBehavior.ClientCascade);

        modelBuilder.Entity<Models.Petition>()
            .HasOne(e => e.Owner)
            .WithMany(e => e.Petitions)
            .OnDelete(DeleteBehavior.ClientCascade);

        modelBuilder.Entity<Models.Petition>()
            .HasOne(e => e.Item)
            .WithOne(e => e.Petition)
            .OnDelete(DeleteBehavior.ClientCascade);

        modelBuilder.Entity<Models.Guild>()
            .HasOne(e => e.Leader)
            .WithOne(e => e.OwnedGuild)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}
