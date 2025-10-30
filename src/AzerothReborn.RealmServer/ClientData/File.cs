using System.ComponentModel;
using System.Text;

namespace AzerothReborn.RealmServer.ClientData;

public class File
{
    private readonly string _path;

    private byte[]? data;

    [Description("Header information: File type.")]
    public string? Type { get; private set; }

    [Description("Header information: Rows contained in the file.")]
    public int Rows { get; private set; }

    [Description("Header information: Columns for each row.")]
    public int Columns { get; private set; }

    [Description("Header information: Bytes ocupied by each row.")]
    public int RowLength { get; private set; }

    [Description("Header information: Strings data block length.")]
    public int StringBlockLength { get; private set; }

    public File(string path)
    {
        _path = path;
    }

    public async Task<File> LoadAsync()
    {
        data = await System.IO.File.ReadAllBytesAsync(_path);
        Type = Encoding.ASCII.GetString(data, 0, 4);
        Rows = BitConverter.ToInt32(new ReadOnlySpan<byte>(data, 4, 4));
        Columns = BitConverter.ToInt32(new ReadOnlySpan<byte>(data, 8, 4));
        RowLength = BitConverter.ToInt32(new ReadOnlySpan<byte>(data, 12, 4));
        StringBlockLength = BitConverter.ToInt32(new ReadOnlySpan<byte>(data, 16, 4));

        return this;
    }

    public int ReadInt(int row, int column)
    {
        if (data is null) throw new ApplicationException("Data was null");
        return BitConverter.ToInt32(data, GetOffset(row, column));
    }

    public float ReadFloat(int row, int column)
    {
        if (data is null) throw new ApplicationException("Data was null");
        return BitConverter.ToSingle(data, GetOffset(row, column));
    }

    public string ReadString(int row, int column)
    {
        if (data is null) throw new ApplicationException("Data was null");
        var offset = GetRowOffset(row) + ReadInt(row, column);
        var length = Array.IndexOf<byte>(data, 0, offset) - offset;
        return BitConverter.ToString(data, offset, length);
    }

    private int GetOffset(int row, int column)
    {
        return column >= Columns
            ? throw new ApplicationException("DBC: Column index outside file definition.")
            : GetRowOffset(row) + (column * 4);
    }

    private int GetRowOffset(int row)
    {
        return row >= Rows ? throw new ApplicationException("DBC: Row index outside file definition.") : 20 + (row * RowLength);
    }
}
