using System.Buffers.Binary;
using System.Text;

namespace AzerothReborn.RealmServer.Network;

internal sealed class PacketWriter
{
    private readonly Memory<byte> buffer;
    private int offset = 4;

    public PacketWriter(Memory<byte> buffer, Opcodes opcode)
    {
        this.buffer = buffer;
        var span = buffer.Slice(2).Span;
        BinaryPrimitives.WriteUInt16LittleEndian(span, (ushort)opcode);
    }

    public Memory<byte> ToPacket()
    {
        var span = buffer.Span;
        BinaryPrimitives.WriteUInt16BigEndian(span, (ushort)(offset - 2));
        return buffer.Slice(0, offset);
    }

    public void AddInt8(byte value)
    {
        buffer.Slice(offset).Span[0] = value;
        offset += sizeof(sbyte);
    }

    public void AddInt32(int value)
    {
        BinaryPrimitives.WriteInt32LittleEndian(buffer.Slice(offset).Span, value);
        offset += sizeof(int);
    }

    public void AddUInt32(uint value)
    {
        BinaryPrimitives.WriteUInt32LittleEndian(buffer.Slice(offset).Span, value);
        offset += sizeof(uint);
    }

    public void AddUInt64(ulong value)
    {
        BinaryPrimitives.WriteUInt64LittleEndian(buffer.Slice(offset).Span, value);
        offset += sizeof(ulong);
    }

    public void AddString(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            AddInt8(0);
        }
        else
        {
            var bytes = Encoding.UTF8.GetBytes(value.ToCharArray());
            for (int i = 0; i <= bytes.Length - 1; i++)
            {
                AddInt8(bytes[i]);
            }
        }
    }
}
