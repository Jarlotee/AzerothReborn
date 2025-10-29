using System.Net;
using System.Net.Sockets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzerothReborn.Tcp;

public sealed class TcpServer
{
    private readonly ILogger _logger;
    private readonly IServiceScopeFactory _factory;

    public TcpServer(ILogger<TcpServer> logger, IServiceScopeFactory factory)
    {
        _logger = logger;
        _factory = factory;
    }

    public async Task RunAsync(string endpoint, CancellationToken cancellationToken = default)
    {
        var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Bind(IPEndPoint.Parse(endpoint));
        socket.Listen(10);

        _logger.LogInformation($"Tcp server was started on {endpoint}");

        while (!cancellationToken.IsCancellationRequested)
        {
            HandleClientConnection(await socket.AcceptAsync(cancellationToken), cancellationToken);
        }
    }

    private async void HandleClientConnection(Socket socket, CancellationToken cancellationToken)
    {
        if (socket.RemoteEndPoint is not IPEndPoint endpoint)
        {
            _logger.LogError("Unable to get remote endpoint");
            return;
        }

        _logger.LogInformation($"Tcp client was conntected {endpoint}");
        try
        {
            using var scope = _factory.CreateScope();
            var tcpConnection = scope.ServiceProvider.GetRequiredService<ITcpConnection>();
            await tcpConnection.ExecuteAsync(socket, cancellationToken);
        }
        catch (SocketException exception) when (exception.SocketErrorCode == SocketError.ConnectionAborted)
        {
            _logger.LogInformation("Connection aborted");
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Unhandled exception");
        }
        finally
        {
            socket.Dispose();
        }

        _logger.LogInformation($"Tcp client {endpoint} was disconected");
    }
}
