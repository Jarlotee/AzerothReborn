using ICSharpCode.SharpZipLib.Zip.Compression.Streams;


namespace AzerothReborn.RealmServer.Zip;

public static class Service
{
    public static byte[] Compress(byte[] data, int offset, int length)
    {
        using MemoryStream outputStream = new();
        using DeflaterOutputStream compressordStream = new(outputStream);
        compressordStream.Write(data, offset, length);
        compressordStream.Flush();
        return outputStream.ToArray();
    }

    public static byte[] DeCompress(byte[] data)
    {
        using MemoryStream outputStream = new();
        using MemoryStream compressedStream = new(data);
        using InflaterInputStream inputStream = new(compressedStream);
        inputStream.CopyTo(outputStream);
        outputStream.Position = 0;
        return outputStream.ToArray();
    }
}
