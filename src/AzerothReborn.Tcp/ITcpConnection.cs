using System.Net.Sockets;

namespace AzerothReborn.Tcp;

public interface ITcpConnection
{
    Task ExecuteAsync(Socket socket, CancellationToken cancellationToken);
}
