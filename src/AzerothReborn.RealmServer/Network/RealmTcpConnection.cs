using System.Buffers;
using System.Buffers.Binary;
using System.Net.Sockets;
using AzerothReborn.RealmServer.Domain;
using AzerothReborn.Tcp;

namespace AzerothReborn.RealmServer.Network;

internal sealed class RealmTcpConnection : ITcpConnection
{
    private const int MAX_PACKET_LENGTH = 10000;

    private readonly Domain.Client legacyClientClass;
    private readonly IHandlerDispatcher[] dispatchers;

    private readonly MemoryPool<byte> memoryPool = MemoryPool<byte>.Shared;

    public RealmTcpConnection(Domain.Client legacyClientClass, IEnumerable<IHandlerDispatcher> dispatchers)
    {
        this.legacyClientClass = legacyClientClass;

        this.dispatchers = dispatchers.ToArray();
    }

    public async Task ExecuteAsync(Socket socket, CancellationToken cancellationToken)
    {
        legacyClientClass.Socket = socket;
        await legacyClientClass.OnConnectAsync();

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

        var dispatcher = dispatchers.FirstOrDefault(x => x.Opcode == opcode);
        if (dispatcher != null)
        {
            await ExecuteHandlerAsync(dispatcher, body, socket, cancellationToken);
        }
        else
        {
            ExecuteLegacyHandler(memoryOwner.Memory.Slice(0, header.Length + body.Length));
        }
    }

    private async Task ExecuteHandlerAsync(IHandlerDispatcher dispatcher, Memory<byte> body, Socket socket, CancellationToken cancellationToken)
    {
        using var result = await dispatcher.ExectueAsync(new PacketReader(body));
        using var memoryOwner = memoryPool.Rent(MAX_PACKET_LENGTH);
        foreach (var response in result.GetResponseMessages())
        {
            await SendAsync(socket, memoryOwner.Memory, response, cancellationToken);
        }
    }

    private void ExecuteLegacyHandler(ReadOnlyMemory<byte> packet)
    {
        var legacyPacket = new PacketClass(packet.ToArray());
        legacyClientClass.OnPacket(legacyPacket);
    }

    private async ValueTask WaitForNextPacket(Socket socket, CancellationToken cancellationToken)
    {
        await socket.ReceiveAsync(Array.Empty<byte>(), cancellationToken);
    }

    private async ValueTask<Memory<byte>> ReadPacketHeaderAsync(Socket socket, Memory<byte> buffer, CancellationToken cancellationToken)
    {
        var header = buffer.Slice(0, 6);
        await ReadAsync(socket, header, cancellationToken);
        legacyClientClass.DecodePacketHeader(header.Span);
        return header;
    }

    private async ValueTask<Memory<byte>> ReadPacketBodyAsync(Socket socket, Memory<byte> buffer, CancellationToken cancellationToken)
    {
        var length = BinaryPrimitives.ReadUInt16BigEndian(buffer.Span) - 4;
        var body = buffer.Slice(6, length);
        await ReadAsync(socket, body, cancellationToken);
        return body;
    }

    private async ValueTask SendAsync(Socket socket, Memory<byte> buffer, Responses.IResponseMessage response, CancellationToken cancellationToken)
    {
        var packetWriter = new PacketWriter(buffer, response.Opcode);
        response.Write(packetWriter);
        var packet = packetWriter.ToPacket();
        legacyClientClass.EncodePacketHeader(packet.Span);
        await SendAsync(socket, packet, cancellationToken);
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
