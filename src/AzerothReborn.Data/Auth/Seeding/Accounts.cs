using Microsoft.EntityFrameworkCore;

namespace AzerothReborn.Data.Auth.Seeding;

public static class Accounts
{
    /// <summary>
    /// Seeds accounts if the code is being run in debug mode AND there are no accounts already in the database
    /// </summary>
    public static async Task SeedAccounts(this DbContext context, CancellationToken cancellationToken)
    {
#if DEBUG
        if (await context.Set<Models.Account>().CountAsync() == 0)
        {
            context.Set<Models.Account>().AddRange([
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

            await context.SaveChangesAsync();
        }
#endif
    }
}