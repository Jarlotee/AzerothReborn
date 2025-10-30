using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AzerothReborn.RealmServer.Services;

internal class Realm : IHostedService
{
    private readonly ILogger _logger;
    private readonly Tcp.TcpServer _tcpServer;
    private readonly ClientData.Loader _loader;
    private readonly Data.Auth.Context _context;
    private readonly Configuration.Server _config;
    private readonly CancellationTokenSource _cancellationSource;

    public Realm(
        ILogger<Realm> logger,
        Tcp.TcpServer tcpServer,
        ClientData.Loader loader,
        Data.Auth.Context context,
        IOptions<Configuration.Server> config)
    {
        _logger = logger;
        _tcpServer = tcpServer;
        _loader = loader;
        _context = context;
        _config = config.Value;
        _cancellationSource = new CancellationTokenSource();
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        // logger.Trace(@" __  __      _  _  ___  ___  ___               ");
        // logger.Trace(@"|  \/  |__ _| \| |/ __|/ _ \/ __|   We Love    ");
        // logger.Trace(@"| |\/| / _` | .` | (_ | (_) \__ \   Vanilla Wow");
        // logger.Trace(@"|_|  |_\__,_|_|\_|\___|\___/|___/              ");
        // logger.Trace("                                                ");

        // _logger.LogInformation($"Updating Auth Database");
        // await _context.Database.MigrateAsync();

        _logger.LogInformation("Loading DBC files");
        await _loader.LoadAsync();

        _logger.LogInformation("Starting TCP server");
        await _tcpServer.RunAsync(_config.Endpoint, _cancellationSource.Token);

        // try
        // {
        //     // Set all characters offline
        //     WorldCluster.GetCharacterDatabase().Update("UPDATE characters SET char_online = 0;");
        // }
        // catch (Exception e)
        // {
        //     WorldCluster.Log.WriteLine(LogType.FAILED, "Internal database initialization failed! [{0}]{1}{2}", e.Message, Constants.vbCrLf, e.ToString());
        // }
        

    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping RealmServer");
        _cancellationSource.Cancel();

        return Task.CompletedTask;
    }
}

// public async Task StartAsync()
//     {
//         _clusterServiceLocator.WcHandlers.IntializePacketHandlers();
//         _clusterServiceLocator.WorldServerClass.Start(); // renamed to Cluster

//         Log.WriteLine(LogType.INFORMATION, "Load Time: {0}", Strings.Format(DateAndTime.DateDiff(DateInterval.Second, DateAndTime.Now, DateAndTime.Now), "0 seconds"));
//         Log.WriteLine(LogType.INFORMATION, "Used memory: {0}", Strings.Format(GC.GetTotalMemory(false), "### ### ##0 bytes"));
//     }

//     public void WaitConsoleCommand()
//     {
//         var tmp = "";
//         string[] commandList;
//         string[] cmds;
//         var cmd = Array.Empty<string>();
//         int varList;
//         while (!_clusterServiceLocator.WcNetwork.WorldServer.MFlagStopListen)
//         {
//             try
//             {
//                 tmp = Log.ReadLine();
//                 commandList = tmp.Split(";");
//                 var loopTo = Information.UBound(commandList);
//                 for (varList = Information.LBound(commandList); varList <= loopTo; varList++)
//                 {
//                     cmds = Strings.Split(commandList[varList], " ", 2);
//                     if (commandList[varList].Length > 0)
//                     {
//                         // <<<<<<<<<<<COMMAND STRUCTURE>>>>>>>>>>
//                         switch (cmds[0].ToLower() ?? "")
//                         {
//                             case "shutdown":
//                                 {
//                                     Log.WriteLine(LogType.WARNING, "Server shutting down...");
//                                     _clusterServiceLocator.WcNetwork.WorldServer.MFlagStopListen = true;
//                                     break;
//                                 }

//                             case "info":
//                                 {
//                                     Log.WriteLine(LogType.INFORMATION, "Used memory: {0}", Strings.Format(GC.GetTotalMemory(false), "### ### ##0 bytes"));
//                                     break;
//                                 }

//                             case "help":
//                                 {
//                                     Console.ForegroundColor = ConsoleColor.Blue;
//                                     Console.WriteLine("'WorldCluster' Command list:");
//                                     Console.ForegroundColor = ConsoleColor.White;
//                                     Console.WriteLine("---------------------------------");
//                                     Console.WriteLine("");
//                                     Console.WriteLine("'help' - Brings up the 'WorldCluster' Command list (this).");
//                                     Console.WriteLine("");
//                                     Console.WriteLine("'info' - Displays used memory.");
//                                     Console.WriteLine("");
//                                     Console.WriteLine("'shutdown' - Shuts down WorldCluster.");
//                                     break;
//                                 }

//                             default:
//                                 {
//                                     Console.ForegroundColor = ConsoleColor.Red;
//                                     Console.WriteLine("Error! Cannot find specified command. Please type 'help' for information on console for commands.");
//                                     Console.ForegroundColor = ConsoleColor.Gray;
//                                     break;
//                                 }
//                         }
//                         // <<<<<<<<<<</END COMMAND STRUCTURE>>>>>>>>>>>>
//                     }
//                 }
//             }
//             catch (Exception e)
//             {
//                 Log.WriteLine(LogType.FAILED, "Error executing command [{0}]. {2}{1}", Strings.Format(DateAndTime.TimeOfDay, "hh:mm:ss"), tmp, e.ToString(), Constants.vbCrLf);
//             }
//         }