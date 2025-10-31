using System.Buffers.Binary;
using System.Text;

namespace AzerothReborn.RealmServer.Network;

internal sealed class PacketReader
{
    private Memory<byte> _data;

    public PacketReader(Memory<byte> data)
    {
        _data = data;
    }

    public short GetInt16()
    {
        var value = BinaryPrimitives.ReadInt16LittleEndian(_data.Span);
        _data = _data.Slice(sizeof(short));
        return value;
    }

    public int GetInt32()
    {
        var value = BinaryPrimitives.ReadInt32LittleEndian(_data.Span);
        _data = _data.Slice(sizeof(int));
        return value;
    }

    public uint GetUInt32()
    {
        var value = BinaryPrimitives.ReadUInt32LittleEndian(_data.Span);
        _data = _data.Slice(sizeof(uint));
        return value;
    }

    public ulong GetUInt64()
    {
        var value = BinaryPrimitives.ReadUInt64LittleEndian(_data.Span);
        _data = _data.Slice(sizeof(ulong));
        return value;
    }

    public string GetString()
    {
        var size = 1;

        while (_data.Span[size] != 0)
        {
            size++;
        }

        var value = Encoding.UTF8.GetString(_data.Span.ToArray(), 0, size);

        _data = _data.Slice(size);

        return value;
    }

    public byte[] GetBytes(int size)
    {
        var value = _data.Span.Slice(size);
        _data = _data.Slice(size);
        return value.ToArray();
    }
}
