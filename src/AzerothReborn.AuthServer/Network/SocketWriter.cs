using System.Buffers;
using System.Net.Sockets;

namespace AzerothReborn.AuthServer.Network;

internal sealed class SocketWriter
{
    private readonly Socket socket;
    private readonly CancellationToken cancellationToken;
    private readonly ArrayPool<byte> arrayPool = ArrayPool<byte>.Shared;

    public SocketWriter(Socket socket, CancellationToken cancellationToken)
    {
        this.socket = socket;
        this.cancellationToken = cancellationToken;
    }

    public async ValueTask WriteByteArrayAsync(byte[] value)
    {
        await WriteAsync(value, value.Length);
    }

    public async ValueTask WriteByteAsync(byte value)
    {
        var buffer = arrayPool.Rent(sizeof(byte));
        try
        {
            buffer[0] = value;
            await WriteAsync(buffer, sizeof(byte));
        }
        finally
        {
            arrayPool.Return(buffer);
        }
    }

    public async ValueTask WriteFloatAsync(float population)
    {
        var buffer = arrayPool.Rent(sizeof(float));
        try
        {
            BitConverter.TryWriteBytes(buffer, population);
            await WriteAsync(buffer, sizeof(float));
        }
        finally
        {
            arrayPool.Return(buffer);
        }
    }

    public async ValueTask WriteZeroBytesAsync(int count)
    {
        var buffer = arrayPool.Rent(count);
        try
        {
            Array.Fill<byte>(buffer, 0);
            await WriteAsync(buffer, count);
        }
        finally
        {
            arrayPool.Return(buffer);
        }
    }

    private async ValueTask WriteAsync(byte[] buffer, int length)
    {
        await socket.SendAsync(buffer.AsMemory(0, length), cancellationToken);
    }
}
