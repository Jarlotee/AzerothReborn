using System.Net;
using System.Net.Sockets;

namespace AzerothReborn.AuthServer.Network;

internal sealed class TcpConnection : Tcp.ITcpConnection
{
    private readonly IHandlerDispatcher[] dispatchers;
    private readonly Domain.ClientState clientState;

    public TcpConnection(
        IEnumerable<IHandlerDispatcher> dispatchers,
        Domain.ClientState clientState)
    {
        this.dispatchers = dispatchers.ToArray();
        this.clientState = clientState;
    }

    public async Task ExecuteAsync(Socket socket, CancellationToken cancellationToken)
    {
        if (socket.RemoteEndPoint is not IPEndPoint endpoint)
        {
            throw new Exception("Unable to get remote endponit");
        }
        clientState.IPAddress = endpoint.Address;

        var socketReader = new SocketReader(socket, cancellationToken);
        var socketWriter = new SocketWriter(socket, cancellationToken);
        while (!cancellationToken.IsCancellationRequested)
        {
            await ExecuteMessageAsync(socketReader, socketWriter);
        }
    }

    private async Task ExecuteMessageAsync(SocketReader reader, SocketWriter writer)
    {
        if (!reader.HasData()) { return; }

        var opcode = (MessageOpcode)await reader.ReadByteAsync();

        var dispatcher = dispatchers.FirstOrDefault(x => x.Opcode == opcode);
        if (dispatcher == null)
        {
            throw new NotImplementedException($"Unsupported opcode {opcode}");
        }
        else
        {
            await dispatcher.ExectueAsync(reader, writer);
        }
    }
}
