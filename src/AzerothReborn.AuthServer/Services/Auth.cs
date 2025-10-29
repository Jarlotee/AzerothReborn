using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AzerothReborn.AuthServer.Services;

internal class Auth : IHostedService
{
    private readonly ILogger _logger;
    private readonly Tcp.TcpServer _tcpServer;
    private readonly Data.Auth.Context _context;
    private readonly Configuration.Server _config;
    private readonly CancellationTokenSource _cancellationSource;

    public Auth(
        ILogger<Auth> logger,
        Tcp.TcpServer tcpServer,
        Data.Auth.Context context,
        IOptions<Configuration.Server> config)
    {
        _logger = logger;
        _tcpServer = tcpServer;
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

        _logger.LogInformation($"Updating Auth Database");
        await _context.Database.MigrateAsync();

        _logger.LogInformation("Starting TCP server");
        await _tcpServer.RunAsync(_config.Endpoint, _cancellationSource.Token);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping AuthServer");
        _cancellationSource.Cancel();

        return Task.CompletedTask;
    }
}