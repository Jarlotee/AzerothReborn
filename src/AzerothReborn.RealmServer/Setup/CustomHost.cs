using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace AzerothReborn.RealmServer.Setup;


internal static class CustomHost
{
    public static HostApplicationBuilder CreateApplicationBuilderWithAppsettings(string[]? args)
    {
        return Host.CreateApplicationBuilder(new HostApplicationBuilderSettings
        {
            Args = args,
            ContentRootPath = Directory.GetCurrentDirectory(),
            Configuration = CreateConfiguration()
        });
    }

    private static ConfigurationManager CreateConfiguration()
    {
        var configuration = new ConfigurationManager();
        configuration.AddJsonFile("appsettings.json", optional: false);
        configuration.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}.json", optional: true, reloadOnChange: true);
        configuration.AddEnvironmentVariables();

        return configuration;
    }
}