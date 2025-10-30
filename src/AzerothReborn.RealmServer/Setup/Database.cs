using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AzerothReborn.RealmServer.Setup;


internal static class Database
{
    public static void HandleDatabase(this HostApplicationBuilder builder)
    {
        builder.Services.AddDbContext<Data.Auth.Context>(o =>
            o.UseSqlServer(builder.Configuration.GetConnectionString("Auth")));
    }
}