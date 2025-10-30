using Microsoft.Extensions.Logging;

namespace AzerothReborn.RealmServer.ClientData;

public class Loader
{
    private readonly ILogger _logger;
    public Dictionary<int, Domain.Map> Maps { get; private set; }
    public Dictionary<int, Domain.WorldSafeLocation> WorldSafeLocations { get; private set; }
    public Dictionary<int, Domain.ChatChannel> ChatChannels { get; private set; }
    public Dictionary<int, Domain.CharacterRace> CharacterRaces { get; private set; }
    public Dictionary<int, Domain.CharacterClass> CharacterClasses { get; private set; }

    public Loader(ILogger<Loader> logger)
    {
        _logger = logger;

        Maps = [];
        WorldSafeLocations = [];
        ChatChannels = [];
        CharacterRaces = [];
        CharacterClasses = [];
    }

    public async Task LoadAsync()
    {
        await Task.WhenAll(
            InitializeMapsAsync(),
            InitializeChatChannelsAsync(),
            InitializeWorldSafeLocationsAsync(),
            InitializeCharRacesAsync(),
            InitializeCharacterClassesAsync()
        );
    }

    private async Task InitializeMapsAsync()
    {
        try
        {
            var data = await GetDataStoreAsync("Map.dbc");
            for (int i = 0, loopTo = data.Rows - 1; i <= loopTo; i++)
            {
                Domain.Map m = new()
                {
                    Id = data.ReadInt(i, 0),
                    Type = (Reference.MapTypes)data.ReadInt(i, 2),
                    Name = data.ReadString(i, 4),
                    ParentMap = data.ReadInt(i, 3),
                    ResetTime = data.ReadInt(i, 38),
                };
                Maps.Add(m.Id, m);
            }

            _logger.LogInformation("DBC: {0} Maps Initialized.", data.Rows - 1);
        }
        catch (DirectoryNotFoundException ex)
        {
            _logger.LogError(ex, "DBC: Maps.dbc missing.");
        }
    }

    private async Task InitializeWorldSafeLocationsAsync()
    {
        try
        {
            var data = await GetDataStoreAsync("WorldSafeLocs.dbc");
            for (int i = 0, loopTo = data.Rows - 1; i <= loopTo; i++)
            {
                Domain.WorldSafeLocation w = new()
                {
                    Id = data.ReadInt(i, 0),
                    Map = (uint)data.ReadInt(i, 1),
                    X = data.ReadFloat(i, 2),
                    Y = data.ReadFloat(i, 3),
                    Z = data.ReadFloat(i, 4),
                };
                WorldSafeLocations.Add(w.Id, w);
            }

            _logger.LogInformation("DBC: {0} WorldSafeLocs Initialized.", data.Rows - 1);
        }
        catch (DirectoryNotFoundException ex)
        {
            _logger.LogError(ex, "DBC: WorldSafeLocs.dbc  missing.");
        }
    }

    private async Task InitializeChatChannelsAsync()
    {
        try
        {
            var data = await GetDataStoreAsync("ChatChannels.dbc");
            for (int i = 0, loopTo = data.Rows - 1; i <= loopTo; i++)
            {
                var chatChannel = new Domain.ChatChannel
                {
                    Index = data.ReadInt(i, 0),
                    Flags = data.ReadInt(i, 1),
                    Name = data.ReadString(i, 3),
                };
                ChatChannels.Add(chatChannel.Index, chatChannel);
            }

            _logger.LogInformation("DBC: {0} ChatChannels Initialized.", data.Rows - 1);
        }
        catch (DirectoryNotFoundException ex)
        {
            _logger.LogError(ex, "DBC: ChatChannels.dbc  missing.");
        }
    }

    private async Task InitializeCharRacesAsync()
    {
        try
        {
            var data = await GetDataStoreAsync("ChrRaces.dbc");
            for (int i = 0, loopTo = data.Rows - 1; i <= loopTo; i++)
            {
                var cr = new Domain.CharacterRace
                {
                    RaceId = data.ReadInt(i, 0),
                    FactionId = (short)data.ReadInt(i, 2),
                    ModelMale = data.ReadInt(i, 4),
                    ModelFemale = data.ReadInt(i, 5),
                    TeamId = (byte)data.ReadInt(i, 8), // 1 = Horde, 7 = Alliance
                    CinematicId = data.ReadInt(i, 16),
                };

                CharacterRaces.Add(cr.RaceId, cr);
            }

            _logger.LogInformation("DBC: {0} ChrRace Loaded.", data.Rows - 1);
        }
        catch (DirectoryNotFoundException ex)
        {
            _logger.LogError(ex, "DBC: ChrRaces.dbc missing.");
        }
    }

    public async Task InitializeCharacterClassesAsync()
    {
        try
        {
            var dataStore = await GetDataStoreAsync("ChrClasses.dbc");
            for (int i = 0, loopTo = dataStore.Rows - 1; i <= loopTo; i++)
            {
                var cc = new Domain.CharacterClass
                {
                    ClassId = dataStore.ReadInt(i, 0),
                    CinematicId = dataStore.ReadInt(i, 5),
                };

                CharacterClasses.Add(cc.ClassId, cc);
            }

            _logger.LogInformation("DBC: {0} ChrClasses Loaded.", dataStore.Rows - 1);
        }
        catch (DirectoryNotFoundException ex)
        {
            _logger.LogError(ex, "DBC: ChrClasses.dbc missing.");
        }
    }

    private async Task<File> GetDataStoreAsync(string dbcFileName)
    {
        File file = new(Path.Combine("dbc", dbcFileName));
        return await file.LoadAsync();
    }

}
