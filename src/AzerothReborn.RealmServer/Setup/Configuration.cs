using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AzerothReborn.RealmServer.Setup;


internal static class Configuration
{
    public static void HandleConfiguration(this HostApplicationBuilder builder)
    {
        builder.Services.Configure<RealmServer.Configuration.Server>(
            builder.Configuration.GetSection("Server"));
    }
}