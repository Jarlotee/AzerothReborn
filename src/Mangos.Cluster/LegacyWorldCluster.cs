//
// Copyright (C) 2013-2025 getMaNGOS <https://www.getmangos.eu>
//
// This program is free software. You can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation. either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY. Without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//

using Mangos.Cluster.Globals;
using Mangos.Cluster.Handlers;
using Mangos.Cluster.Network;
using Mangos.Common.Enums.Global;
using Mangos.Common.Globals;
using Mangos.Common.Legacy;
using Mangos.Common.Legacy.Logging;
using Mangos.Configuration;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mangos.Cluster;

public class LegacyWorldCluster
{
    private readonly MangosConfiguration _mangosConfiguration;
    private readonly ClusterServiceLocator _clusterServiceLocator;
    private readonly SQL _accountDatabase;
    private readonly SQL _characterDatabase;
    private readonly SQL _worldDatabase;

    public LegacyWorldCluster(ClusterServiceLocator clusterServiceLocator, MangosConfiguration mangosConfiguration)
    {
        _clusterServiceLocator = clusterServiceLocator;
        _mangosConfiguration = mangosConfiguration;

        _accountDatabase = new SQL(
            _mangosConfiguration.Realm.ConnectionString,
            _mangosConfiguration.Realm.DatabaseName);

        _characterDatabase = new CharacterSql(
            _mangosConfiguration.Character.ConnectionString,
            _mangosConfiguration.Character.DatabaseName);

        _worldDatabase = new WorldSql(
            _mangosConfiguration.World.ConnectionString,
            _mangosConfiguration.World.DatabaseName);
    }

    public SQL GetAccountDatabase()
    {
        return _accountDatabase;
    }

    public SQL GetCharacterDatabase()
    {
        return _characterDatabase;
    }

    public SQL GetWorldDatabase()
    {
        return _worldDatabase;
    }



    // Players' containers
    public long ClietniDs;

    public Dictionary<uint, ClientClass> ClienTs = new();
    public ReaderWriterLock CharacteRsLock = new();
    public Dictionary<ulong, WcHandlerCharacter.CharacterObject> CharacteRs = new();
    // Public CHARACTER_NAMEs As New Hashtable

    // System Things...
    public BaseWriter Log = new ColoredConsoleWriter();

    public Random Rnd = new();

    public delegate void HandlePacket(PacketClass packet, ClientClass client);

    private readonly Dictionary<Opcodes, HandlePacket> _packetHandlers = new();

    public Dictionary<Opcodes, HandlePacket> GetPacketHandlers()
    {
        return _packetHandlers;
    }

    public void AccountSqlEventHandler(SQL.EMessages messageId, string outBuf)
    {
        switch (messageId)
        {
            case var @case when @case == SQL.EMessages.ID_Error:
                {
                    Log.WriteLine(LogType.FAILED, "[ACCOUNT] " + outBuf);
                    break;
                }

            case var case1 when case1 == SQL.EMessages.ID_Message:
                {
                    Log.WriteLine(LogType.SUCCESS, "[ACCOUNT] " + outBuf);
                    break;
                }
        }
    }

    public void CharacterSqlEventHandler(SQL.EMessages messageId, string outBuf)
    {
        switch (messageId)
        {
            case var @case when @case == SQL.EMessages.ID_Error:
                {
                    Log.WriteLine(LogType.FAILED, "[CHARACTER] " + outBuf);
                    break;
                }

            case var case1 when case1 == SQL.EMessages.ID_Message:
                {
                    Log.WriteLine(LogType.SUCCESS, "[CHARACTER] " + outBuf);
                    break;
                }
        }
    }

    public void WorldSqlEventHandler(SQL.EMessages messageId, string outBuf)
    {
        switch (messageId)
        {
            case var @case when @case == SQL.EMessages.ID_Error:
                {
                    Log.WriteLine(LogType.FAILED, "[WORLD] " + outBuf);
                    break;
                }

            case var case1 when case1 == SQL.EMessages.ID_Message:
                {
                    Log.WriteLine(LogType.SUCCESS, "[WORLD] " + outBuf);
                    break;
                }
        }
    }

    public async Task StartAsync()
    {
        _accountDatabase.SQLMessage += AccountSqlEventHandler;
        _characterDatabase.SQLMessage += CharacterSqlEventHandler;
        _worldDatabase.SQLMessage += WorldSqlEventHandler;
        int returnValues;
        returnValues = _accountDatabase.Connect();
        if (returnValues > (int)SQL.ReturnState.Success)   // Ok, An error occurred
        {
            Console.WriteLine("[{0}] An SQL Error has occurred", Strings.Format(DateAndTime.TimeOfDay, "hh:mm:ss"));
            Console.WriteLine("*************************");
            Console.WriteLine("* Press any key to exit *");
            Console.WriteLine("*************************");
            Console.ReadKey();
            Environment.Exit(0);
        }

        _accountDatabase.Update("SET NAMES 'utf8';");
        returnValues = _characterDatabase.Connect();
        if (returnValues > (int)SQL.ReturnState.Success)   // Ok, An error occurred
        {
            Console.WriteLine("[{0}] An SQL Error has occurred", Strings.Format(DateAndTime.TimeOfDay, "hh:mm:ss"));
            Console.WriteLine("*************************");
            Console.WriteLine("* Press any key to exit *");
            Console.WriteLine("*************************");
            Console.ReadKey();
            Environment.Exit(0);
        }

        _characterDatabase.Update("SET NAMES 'utf8';");
        returnValues = _worldDatabase.Connect();
        if (returnValues > (int)SQL.ReturnState.Success)   // Ok, An error occurred
        {
            Console.WriteLine("[{0}] An SQL Error has occurred", Strings.Format(DateAndTime.TimeOfDay, "hh:mm:ss"));
            Console.WriteLine("*************************");
            Console.WriteLine("* Press any key to exit *");
            Console.WriteLine("*************************");
            Environment.Exit(0);
        }

        _worldDatabase.Update("SET NAMES 'utf8';");
        await _clusterServiceLocator.WsDbcLoad.InitializeInternalDatabaseAsync();
        _clusterServiceLocator.WcHandlers.IntializePacketHandlers();
        _clusterServiceLocator.WorldServerClass.Start();

        Log.WriteLine(LogType.INFORMATION, "Load Time: {0}", Strings.Format(DateAndTime.DateDiff(DateInterval.Second, DateAndTime.Now, DateAndTime.Now), "0 seconds"));
        Log.WriteLine(LogType.INFORMATION, "Used memory: {0}", Strings.Format(GC.GetTotalMemory(false), "### ### ##0 bytes"));
    }

    public void WaitConsoleCommand()
    {
        var tmp = "";
        string[] commandList;
        string[] cmds;
        var cmd = Array.Empty<string>();
        int varList;
        while (!_clusterServiceLocator.WcNetwork.WorldServer.MFlagStopListen)
        {
            try
            {
                tmp = Log.ReadLine();
                commandList = tmp.Split(";");
                var loopTo = Information.UBound(commandList);
                for (varList = Information.LBound(commandList); varList <= loopTo; varList++)
                {
                    cmds = Strings.Split(commandList[varList], " ", 2);
                    if (commandList[varList].Length > 0)
                    {
                        // <<<<<<<<<<<COMMAND STRUCTURE>>>>>>>>>>
                        switch (cmds[0].ToLower() ?? "")
                        {
                            case "shutdown":
                                {
                                    Log.WriteLine(LogType.WARNING, "Server shutting down...");
                                    _clusterServiceLocator.WcNetwork.WorldServer.MFlagStopListen = true;
                                    break;
                                }

                            case "info":
                                {
                                    Log.WriteLine(LogType.INFORMATION, "Used memory: {0}", Strings.Format(GC.GetTotalMemory(false), "### ### ##0 bytes"));
                                    break;
                                }

                            case "help":
                                {
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.WriteLine("'WorldCluster' Command list:");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("---------------------------------");
                                    Console.WriteLine("");
                                    Console.WriteLine("'help' - Brings up the 'WorldCluster' Command list (this).");
                                    Console.WriteLine("");
                                    Console.WriteLine("'info' - Displays used memory.");
                                    Console.WriteLine("");
                                    Console.WriteLine("'shutdown' - Shuts down WorldCluster.");
                                    break;
                                }

                            default:
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Error! Cannot find specified command. Please type 'help' for information on console for commands.");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    break;
                                }
                        }
                        // <<<<<<<<<<</END COMMAND STRUCTURE>>>>>>>>>>>>
                    }
                }
            }
            catch (Exception e)
            {
                Log.WriteLine(LogType.FAILED, "Error executing command [{0}]. {2}{1}", Strings.Format(DateAndTime.TimeOfDay, "hh:mm:ss"), tmp, e.ToString(), Constants.vbCrLf);
            }
        }
    }
}
