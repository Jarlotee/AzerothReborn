using System.Buffers;
using System.Buffers.Binary;
using System.Net.Sockets;
using AzerothReborn.RealmServer.Handlers;
using AzerothReborn.Tcp;

namespace AzerothReborn.RealmServer.Network;

internal sealed class RealmTcpConnection : ITcpConnection
{

    private const int MAX_PACKET_LENGTH = 10000;

    private readonly HandlerProvider _provider;

    private readonly Domain.Client _clientClass;

    private readonly MemoryPool<byte> memoryPool = MemoryPool<byte>.Shared;

    public RealmTcpConnection(Domain.Client legacyClientClass, HandlerProvider provider)
    {
        _clientClass = legacyClientClass;
        _provider = provider;
    }

    public async Task ExecuteAsync(Socket socket, CancellationToken cancellationToken)
    {
        _clientClass.Socket = socket;
        await _clientClass.OnConnectAsync();

        while (!cancellationToken.IsCancellationRequested)
        {
            await WaitForNextPacket(socket, cancellationToken);
            await HandlePacketAsync(socket, cancellationToken);
        }
    }

    private async Task HandlePacketAsync(Socket socket, CancellationToken cancellationToken)
    {
        using var memoryOwner = memoryPool.Rent(MAX_PACKET_LENGTH);
        var header = await ReadPacketHeaderAsync(socket, memoryOwner.Memory, cancellationToken);
        var body = await ReadPacketBodyAsync(socket, memoryOwner.Memory, cancellationToken);

        var opcode = (Opcodes)BinaryPrimitives.ReadUInt32LittleEndian(header.Span.Slice(2));

        var handler = _provider.GetHandlerForOpcode(opcode);
        if (handler != null)
        {
            await ExecuteHandlerAsync(handler, body, socket, cancellationToken);
        }
        else
        {
            throw new NotImplementedException("Should not happen");
        }
    }

    private async Task ExecuteHandlerAsync(IHandler handler, Memory<byte> body, Socket socket, CancellationToken cancellationToken)
    {
        var responses = await handler.HandleAsync(new PacketReader(body), _clientClass);
        using var memoryOwner = memoryPool.Rent(MAX_PACKET_LENGTH);
        foreach (var response in responses)
        {
            var packetWriter = new PacketWriter(memoryOwner.Memory, response.GetOpcode());
            response.Write(packetWriter);
            var packet = packetWriter.ToPacket();
            _clientClass.EncodePacketHeader(packet.Span);
            await SendAsync(socket, packet, cancellationToken);
        }
    }

    private async ValueTask WaitForNextPacket(Socket socket, CancellationToken cancellationToken)
    {
        await socket.ReceiveAsync(Array.Empty<byte>(), cancellationToken);
    }

    private async ValueTask<Memory<byte>> ReadPacketHeaderAsync(Socket socket, Memory<byte> buffer, CancellationToken cancellationToken)
    {
        var header = buffer.Slice(0, 6);
        await ReadAsync(socket, header, cancellationToken);
        _clientClass.DecodePacketHeader(header.Span);
        return header;
    }

    private async ValueTask<Memory<byte>> ReadPacketBodyAsync(Socket socket, Memory<byte> buffer, CancellationToken cancellationToken)
    {
        var length = BinaryPrimitives.ReadUInt16BigEndian(buffer.Span) - 4;
        var body = buffer.Slice(6, length);
        await ReadAsync(socket, body, cancellationToken);
        return body;
    }

    private async ValueTask ReadAsync(Socket socket, Memory<byte> buffer, CancellationToken cancellationToken)
    {
        if (buffer.Length == 0)
        {
            return;
        }

        var length = await socket.ReceiveAsync(buffer, cancellationToken);
        if (length != buffer.Length)
        {
            throw new NotImplementedException("Invalid number of bytes was readed from socket");
        }
    }

    private async ValueTask SendAsync(Socket socket, ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken)
    {
        var length = await socket.SendAsync(buffer, cancellationToken);
        if (length != buffer.Length)
        {
            throw new NotImplementedException("Invalid number of bytes was sended to socket");
        }
    }
}
