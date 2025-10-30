using System.Collections;
using System.Text;

namespace AzerothReborn.RealmServer.Network;

public class PacketClass : IDisposable
{
    public byte[] Data;
    public int Offset = 4;

    public int Length => Data[1] + (Data[0] * 256);

    public Opcodes OpCode
    {
        get
        {
            if (Data.Length > 3)
            {
                return (Opcodes)(Data[2] + (Data[3] * 256));
            }

            // If it's a dodgy packet, change it to a null packet
            return 0;
        }
    }

    public PacketClass(Opcodes opcode)
    {
        Array.Resize(ref Data, 4);
        Data[0] = 0;
        Data[1] = 2;
        Data[2] = (byte)((short)opcode % 256);
        Data[3] = (byte)((short)opcode / 256);
    }

    public PacketClass(byte[] rawdata)
    {
        Data = rawdata;
    }

    public void AddInt8(byte buffer)
    {
        Array.Resize(ref Data, Data.Length + 1);
        Data[0] = (byte)((Data.Length - 2) / 256);
        Data[1] = (byte)((Data.Length - 2) % 256);
        Data[^1] = buffer;
    }

    public void AddInt16(short buffer)
    {
        Array.Resize(ref Data, Data.Length + 1 + 1);
        Data[0] = (byte)((Data.Length - 2) / 256);
        Data[1] = (byte)((Data.Length - 2) % 256);
        Data[^2] = (byte)(buffer & 255);
        Data[^1] = (byte)((buffer >> 8) & 255);
    }

    public void AddInt32(int buffer, int position = 0)
    {
        if (position <= 0 || position > Data.Length - 3)
        {
            position = Data.Length;
            Array.Resize(ref Data, Data.Length + 3 + 1);
            Data[0] = (byte)((Data.Length - 2) / 256);
            Data[1] = (byte)((Data.Length - 2) % 256);
        }

        Data[position] = (byte)(buffer & 255);
        Data[position + 1] = (byte)((buffer >> 8) & 255);
        Data[position + 2] = (byte)((buffer >> 16) & 255);
        Data[position + 3] = (byte)((buffer >> 24) & 255);
    }

    public void AddInt64(long buffer)
    {
        Array.Resize(ref Data, Data.Length + 7 + 1);
        Data[0] = (byte)((Data.Length - 2) / 256);
        Data[1] = (byte)((Data.Length - 2) % 256);
        Data[^8] = (byte)(buffer & 255L);
        Data[^7] = (byte)((buffer >> 8) & 255L);
        Data[^6] = (byte)((buffer >> 16) & 255L);
        Data[^5] = (byte)((buffer >> 24) & 255L);
        Data[^4] = (byte)((buffer >> 32) & 255L);
        Data[^3] = (byte)((buffer >> 40) & 255L);
        Data[^2] = (byte)((buffer >> 48) & 255L);
        Data[^1] = (byte)((buffer >> 56) & 255L);
    }

    public void AddString(string buffer)
    {
        if (string.IsNullOrWhiteSpace(buffer))
        {
            AddInt8(0);
        }
        else
        {
            var bytes = Encoding.UTF8.GetBytes(buffer.ToCharArray());
            Array.Resize(ref Data, Data.Length + bytes.Length + 1);
            Data[0] = (byte)((Data.Length - 2) / 256);
            Data[1] = (byte)((Data.Length - 2) % 256);
            for (int i = 0, loopTo = bytes.Length - 1; i <= loopTo; i++)
            {
                Data[Data.Length - 1 - bytes.Length + i] = bytes[i];
            }

            Data[^1] = 0;
        }
    }

    public void AddDouble(double buffer2)
    {
        var buffer1 = BitConverter.GetBytes(buffer2);
        Array.Resize(ref Data, Data.Length + buffer1.Length);
        Buffer.BlockCopy(buffer1, 0, Data, Data.Length - buffer1.Length, buffer1.Length);
        Data[0] = (byte)((Data.Length - 2) / 256);
        Data[1] = (byte)((Data.Length - 2) % 256);
    }

    public void AddSingle(float buffer2)
    {
        var buffer1 = BitConverter.GetBytes(buffer2);
        Array.Resize(ref Data, Data.Length + buffer1.Length);
        Buffer.BlockCopy(buffer1, 0, Data, Data.Length - buffer1.Length, buffer1.Length);
        Data[0] = (byte)((Data.Length - 2) / 256);
        Data[1] = (byte)((Data.Length - 2) % 256);
    }

    public void AddByteArray(byte[] buffer)
    {
        var tmp = Data.Length;
        Array.Resize(ref Data, Data.Length + buffer.Length);
        Array.Copy(buffer, 0, Data, tmp, buffer.Length);
        Data[0] = (byte)((Data.Length - 2) / 256);
        Data[1] = (byte)((Data.Length - 2) % 256);
    }

    public void AddPackGuid(ulong buffer)
    {
        var guid = BitConverter.GetBytes(buffer);
        BitArray flags = new(8);
        var offsetStart = Data.Length;
        var offsetNewSize = offsetStart;
        for (byte i = 0; i <= 7; i++)
        {
            flags[i] = guid[i] != 0;
            if (flags[i])
            {
                offsetNewSize += 1;
            }
        }

        Array.Resize(ref Data, offsetNewSize + 1);
        flags.CopyTo(Data, offsetStart);
        offsetStart += 1;
        for (byte i = 0; i <= 7; i++)
        {
            if (flags[i])
            {
                Data[offsetStart] = guid[i];
                offsetStart += 1;
            }
        }
    }

    public ushort GetUInt8()
    {
        var num1 = (ushort)(Data.Length + 1);
        Offset += 1;
        return num1;
    }

    public void AddUInt16(ushort buffer)
    {
        Array.Resize(ref Data, Data.Length + 1 + 1);
        Data[0] = (byte)((Data.Length - 2) / 256);
        Data[1] = (byte)((Data.Length - 2) % 256);
        Data[^2] = (byte)(buffer & 255);
        Data[^1] = (byte)((buffer >> 8) & 255);
    }

    public void AddUInt32(uint buffer)
    {
        Array.Resize(ref Data, Data.Length + 3 + 1);
        Data[0] = (byte)((Data.Length - 2) / 256);
        Data[1] = (byte)((Data.Length - 2) % 256);
        Data[^4] = (byte)(buffer & 255L);
        Data[^3] = (byte)((buffer >> 8) & 255L);
        Data[^2] = (byte)((buffer >> 16) & 255L);
        Data[^1] = (byte)((buffer >> 24) & 255L);
    }

    public void AddUInt64(ulong buffer)
    {
        Array.Resize(ref Data, Data.Length + 7 + 1);
        Data[0] = (byte)((Data.Length - 2) / 256);
        Data[1] = (byte)((Data.Length - 2) % 256);
        Data[^8] = (byte)((long)buffer & 255L);
        Data[^7] = (byte)((long)(buffer >> 8) & 255L);
        Data[^6] = (byte)((long)(buffer >> 16) & 255L);
        Data[^5] = (byte)((long)(buffer >> 24) & 255L);
        Data[^4] = (byte)((long)(buffer >> 32) & 255L);
        Data[^3] = (byte)((long)(buffer >> 40) & 255L);
        Data[^2] = (byte)((long)(buffer >> 48) & 255L);
        Data[^1] = (byte)((long)(buffer >> 56) & 255L);
    }

    public byte GetInt8()
    {
        Offset += 1;
        return Data[Offset - 1];
    }

    public short GetInt16()
    {
        var num1 = BitConverter.ToInt16(Data, Offset);
        Offset += 2;
        return num1;
    }

    public int GetInt32()
    {
        var num1 = BitConverter.ToInt32(Data, Offset);
        Offset += 4;
        return num1;
    }

    public long GetInt64()
    {
        var num1 = BitConverter.ToInt64(Data, Offset);
        Offset += 8;
        return num1;
    }

    public float GetFloat()
    {
        var single1 = BitConverter.ToSingle(Data, Offset);
        Offset += 4;
        return single1;
    }

    public double GetDouble()
    {
        var num1 = BitConverter.ToDouble(Data, Offset);
        Offset += 8;
        return num1;
    }

    public string GetString()
    {
        var start = Offset;
        var i = 0;
        while (Data[start + i] != 0)
        {
            i += 1;
            Offset += 1;
        }

        Offset += 1;
        return Encoding.UTF8.GetString(Data, start, i);
    }

    public ushort GetUInt16()
    {
        var num1 = BitConverter.ToUInt16(Data, Offset);
        Offset += 2;
        return num1;
    }

    public uint GetUInt32()
    {
        var num1 = BitConverter.ToUInt32(Data, Offset);
        Offset += 4;
        return num1;
    }

    public ulong GetUInt64()
    {
        var num1 = BitConverter.ToUInt64(Data, Offset);
        Offset += 8;
        return num1;
    }


    private bool _disposedValue;

    // IDisposable
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            // TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            // TODO: set large fields to null.
        }

        _disposedValue = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
