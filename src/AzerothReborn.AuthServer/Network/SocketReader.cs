using System.Buffers;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;

namespace AzerothReborn.AuthServer.Network;

internal sealed class SocketReader
{
    private readonly Socket socket;
    private readonly CancellationToken cancellationToken;

    private readonly ArrayPool<byte> arrayPool = ArrayPool<byte>.Shared;

    public SocketReader(Socket socket, CancellationToken cancellationToken)
    {
        this.socket = socket;
        this.cancellationToken = cancellationToken;
    }

    public bool HasData()
    {
        return socket.Available > 0;
    }

    public async ValueTask ReadVoidAsync(int length)
    {
        var buffer = arrayPool.Rent(length);
        try
        {
            await ReadAsync(buffer, length);
        }
        finally
        {
            arrayPool.Return(buffer);
        }
    }

    public async ValueTask<byte> ReadByteAsync()
    {
        var buffer = arrayPool.Rent(1);
        try
        {
            await ReadAsync(buffer, 1);
            return buffer[0];
        }
        finally
        {
            arrayPool.Return(buffer);
        }
    }

    public async ValueTask<short> ReadInt16Async()
    {
        var buffer = arrayPool.Rent(2);
        try
        {
            await ReadAsync(buffer, 2);
            return BitConverter.ToInt16(buffer);
        }
        finally
        {
            arrayPool.Return(buffer);
        }
    }

    public async ValueTask<string> ReadStringAsync(int length)
    {
        var buffer = arrayPool.Rent(length);
        try
        {
            await ReadAsync(buffer, length);
            return Encoding.UTF8.GetString(buffer, 0, length);
        }
        finally
        {
            arrayPool.Return(buffer);
        }
    }

    public async ValueTask<byte[]> ReadByteArrayAsync(int length)
    {
        var buffer = new byte[length];
        await ReadAsync(buffer, length);
        return buffer;
    }

    private async ValueTask ReadAsync(byte[] buffer, int length)
    {
        if (length == 0)
        {
            return;
        }

        var number = await socket.ReceiveAsync(buffer.AsMemory(0, length), cancellationToken);
        if (number != length)
        {
            Debugger.Launch();
            throw new Exception("There was no data to read");
        }
    }
}
