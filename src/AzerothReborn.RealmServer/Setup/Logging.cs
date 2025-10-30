using Microsoft.Extensions.Hosting;
using Serilog;

namespace AzerothReborn.RealmServer.Setup;


internal static class Logging
{
    public static void HandleLogging(this HostApplicationBuilder builder)
    {
        builder.Services.AddSerilog(c => c.ReadFrom.Configuration(builder.Configuration));
    }
}