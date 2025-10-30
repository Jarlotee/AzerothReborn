namespace AzerothReborn.RealmServer.Domain;

public class Guild : IDisposable
{
    public uint Id;
    public string? Name;
    public ulong Leader;
    public string? Motd;
    public string? Info;
    public List<ulong> Members = new();
    public string[] Ranks = new string[10];
    public uint[] RankRights = new uint[10];
    public byte EmblemStyle;
    public byte EmblemColor;
    public byte BorderStyle;
    public byte BorderColor;
    public byte BackgroundColor;
    public short CYear;
    public byte CMonth;
    public byte CDay;

    // public Guild(uint guildId)
    // {
    //     Id = guildId;
    //     DataTable mySqlQuery = new();
    //     _clusterServiceLocator.WorldCluster.GetCharacterDatabase().Query("SELECT * FROM guilds WHERE guild_id = " + Id + ";", ref mySqlQuery);
    //     if (mySqlQuery.Rows.Count == 0)
    //     {
    //         throw new ApplicationException("GuildID " + Id + " not found in database.");
    //     }

    //     var guildInfo = mySqlQuery.Rows[0];
    //     Name = guildInfo.As<string>("guild_name");
    //     Leader = guildInfo.As<ulong>("guild_leader");
    //     Motd = guildInfo.As<string>("guild_MOTD");
    //     EmblemStyle = guildInfo.As<byte>("guild_tEmblemStyle");
    //     EmblemColor = guildInfo.As<byte>("guild_tEmblemColor");
    //     BorderStyle = guildInfo.As<byte>("guild_tBorderStyle");
    //     BorderColor = guildInfo.As<byte>("guild_tBorderColor");
    //     BackgroundColor = guildInfo.As<byte>("guild_tBackgroundColor");
    //     CYear = guildInfo.As<short>("guild_cYear");
    //     CMonth = guildInfo.As<byte>("guild_cMonth");
    //     CDay = guildInfo.As<byte>("guild_cDay");
    //     for (var i = 0; i <= 9; i++)
    //     {
    //         Ranks[i] = guildInfo.As<string>("guild_rank" + i);
    //         RankRights[i] = guildInfo.As<uint>("guild_rank" + i + "_Rights");
    //     }

    //     mySqlQuery.Clear();
    //     _clusterServiceLocator.WorldCluster.GetCharacterDatabase().Query("SELECT char_guid FROM characters WHERE char_guildId = " + Id + ";", ref mySqlQuery);
    //     foreach (DataRow memberInfo in mySqlQuery.Rows)
    //     {
    //         Members.Add(guildInfo.As<ulong>("char_guid"));
    //     }

    //     _clusterServiceLocator.WcGuild.GuilDs.Add(Id, this);
    // }

    private bool _disposedValue; // To detect redundant calls

    // IDisposable
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            // TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            // TODO: set large fields to null.
            //_clusterServiceLocator.WcGuild.GuilDs.Remove(Id);
        }

        _disposedValue = true;
    }

    // This code added by Visual Basic to correctly implement the disposable pattern.
    public void Dispose()
    {
        // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}