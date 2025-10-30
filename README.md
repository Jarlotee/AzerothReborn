Based of Mangos


Consider:
* Move AuthServer Network objects to a central lib, just for colocation and consolidation for things like generics, opcodes, etc.


<!-- 

Find a home

public Dictionary<byte, Domain.Battleground> Battlegrounds { get; private set; }


public void InitializeBattlegrounds()
    {
        byte entry;
        DataTable mySqlQuery = new();
        _clusterServiceLocator.WorldCluster.GetWorldDatabase().Query("SELECT * FROM battleground_template", ref mySqlQuery);
        foreach (DataRow row in mySqlQuery.Rows)
        {
            entry = row.As<byte>("id");
            Battlegrounds.Add(entry, new Battleground());

            // TODO: the MAPId needs to be located from somewhere other than the template file
            // BUG: THIS IS AN UGLY HACK UNTIL THE ABOVE IS FIXED
            // Battlegrounds(Entry).Map = row.Item("Map")
            Battlegrounds[entry].MinPlayersPerTeam = row.As<byte>("MinPlayersPerTeam");
            Battlegrounds[entry].MaxPlayersPerTeam = row.As<byte>("MaxPlayersPerTeam");
            Battlegrounds[entry].MinLevel = row.As<byte>("MinLvl");
            Battlegrounds[entry].MaxLevel = row.As<byte>("MaxLvl");
            Battlegrounds[entry].AllianceStartLoc = row.As<int>("AllianceStartLoc");
            Battlegrounds[entry].AllianceStartO = row.As<float>("AllianceStartO");
            Battlegrounds[entry].HordeStartLoc = row.As<int>("HordeStartLoc");
            Battlegrounds[entry].HordeStartO = row.As<float>("HordeStartO");
        }

        _clusterServiceLocator.WorldCluster.Log.WriteLine(LogType.INFORMATION, "World: {0} Battlegrounds Initialized.", mySqlQuery.Rows.Count);
    } -->