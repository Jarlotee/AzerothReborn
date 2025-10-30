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
using Mangos.Cluster.Network;
using Mangos.Common.Enums.Chat;
using Mangos.Common.Enums.Global;
using Mangos.Common.Enums.Group;
using Mangos.Common.Enums.Misc;
using Mangos.Common.Globals;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;

namespace Mangos.Cluster.Handlers;

public class WcHandlersGroup
{
    private readonly ClusterServiceLocator _clusterServiceLocator;

    public WcHandlersGroup(ClusterServiceLocator clusterServiceLocator)
    {
        _clusterServiceLocator = clusterServiceLocator;
    }

    // Used as counter for unique Group.ID
    private long _groupCounter = 1L;

    public Dictionary<long, Group> GrouPs = new();

    public void On_CMSG_REQUEST_RAID_INFO(PacketClass packet, ClientClass client)
    {
        _clusterServiceLocator.WorldCluster.Log.WriteLine(LogType.DEBUG, "[{0}:{1}] CMSG_REQUEST_RAID_INFO", client.IP, client.Port);
        DataTable q = new();
        if (client.Character is not null)
        {
            _clusterServiceLocator.WorldCluster.GetCharacterDatabase().Query(string.Format("SELECT * FROM characters_instances WHERE char_guid = {0};", client.Character.Guid), ref q);
        }

        PacketClass response = new(Opcodes.SMSG_RAID_INSTANCE_INFO);
        response.AddInt32(q.Rows.Count);                                 // Instances Counts
        var i = 0;
        foreach (DataRow r in q.Rows)
        {
            response.AddUInt32(Conversions.ToUInteger(r["map"]));                               // MapID
            response.AddUInt32((uint)(Conversions.ToInteger(r["expire"]) - _clusterServiceLocator.Functions.GetTimestamp(DateAndTime.Now)));  // TimeLeft
            response.AddUInt32(Conversions.ToUInteger(r["instance"]));                          // InstanceID
            response.AddUInt32((uint)i);                                           // Counter
            i += 1;
        }

        client.Send(response);
        response.Dispose();
    }

    public void SendPartyResult(ClientClass objCharacter, string name, PartyCommand operation, PartyCommandResult result)
    {
        PacketClass response = new(Opcodes.SMSG_PARTY_COMMAND_RESULT);
        response.AddInt32((byte)operation);
        response.AddString(name);
        response.AddInt32((byte)result);
        objCharacter.Send(response);
        response.Dispose();
    }

    public void On_CMSG_GROUP_INVITE(PacketClass packet, ClientClass client)
    {
        if (packet.Data.Length - 1 < 6)
        {
            return;
        }

        packet.GetInt16();
        var name = _clusterServiceLocator.Functions.CapitalizeName(packet.GetString());
        _clusterServiceLocator.WorldCluster.Log.WriteLine(LogType.DEBUG, "[{0}:{1}] CMSG_GROUP_INVITE [{2}]", client.IP, client.Port, name);
        var guid = 0UL;
        _clusterServiceLocator.WorldCluster.CharacteRsLock.AcquireReaderLock(_clusterServiceLocator.GlobalConstants.DEFAULT_LOCK_TIMEOUT);
        foreach (var character in _clusterServiceLocator.WorldCluster.CharacteRs)
        {
            if (_clusterServiceLocator.CommonFunctions.UppercaseFirstLetter(character.Value.Name) == _clusterServiceLocator.CommonFunctions.UppercaseFirstLetter(name))
            {
                guid = character.Value.Guid;
                break;
            }
        }

        _clusterServiceLocator.WorldCluster.CharacteRsLock.ReleaseReaderLock();
        var errCode = PartyCommandResult.INVITE_OK;
        // TODO: InBattlegrounds: INVITE_RESTRICTED
        if (guid == 0m)
        {
            errCode = PartyCommandResult.INVITE_NOT_FOUND;
        }
        else if (_clusterServiceLocator.WorldCluster.CharacteRs[guid].IsInWorld == false)
        {
            errCode = PartyCommandResult.INVITE_NOT_FOUND;
        }
        else if (_clusterServiceLocator.Functions.GetCharacterSide((byte)_clusterServiceLocator.WorldCluster.CharacteRs[guid].Race) != _clusterServiceLocator.Functions.GetCharacterSide((byte)client.Character.Race))
        {
            errCode = PartyCommandResult.INVITE_NOT_SAME_SIDE;
        }
        else if (_clusterServiceLocator.WorldCluster.CharacteRs[guid].IsInGroup)
        {
            errCode = PartyCommandResult.INVITE_ALREADY_IN_GROUP;
            PacketClass denied = new(Opcodes.SMSG_GROUP_INVITE);
            denied.AddInt8(0);
            denied.AddString(client.Character.Name);
            _clusterServiceLocator.WorldCluster.CharacteRs[guid].Client.Send(denied);
            denied.Dispose();
        }
        else if (_clusterServiceLocator.WorldCluster.CharacteRs[guid].IgnoreList.Contains(client.Character.Guid))
        {
            errCode = PartyCommandResult.INVITE_IGNORED;
        }
        else if (!client.Character.IsInGroup)
        {
            Group newGroup = new(client.Character, _clusterServiceLocator);
            // TODO: Need to do fully test this
            _clusterServiceLocator.WorldCluster.CharacteRs[guid].Group = newGroup;
            _clusterServiceLocator.WorldCluster.CharacteRs[guid].GroupInvitedFlag = true;
        }
        else if (client.Character.Group.IsFull)
        {
            errCode = PartyCommandResult.INVITE_PARTY_FULL;
        }
        else if (client.Character.IsGroupLeader == false && client.Character.GroupAssistant == false)
        {
            errCode = PartyCommandResult.INVITE_NOT_LEADER;
        }
        else
        {
            _clusterServiceLocator.WorldCluster.CharacteRs[guid].Group = client.Character.Group;
            _clusterServiceLocator.WorldCluster.CharacteRs[guid].GroupInvitedFlag = true;
        }

        SendPartyResult(client, name, PartyCommand.PARTY_OP_INVITE, errCode);
        if (errCode == PartyCommandResult.INVITE_OK)
        {
            PacketClass invited = new(Opcodes.SMSG_GROUP_INVITE);
            invited.AddInt8(1);
            invited.AddString(client.Character.Name);
            _clusterServiceLocator.WorldCluster.CharacteRs[guid].Client.Send(invited);
            invited.Dispose();
        }
    }

    public void On_CMSG_GROUP_CANCEL(PacketClass packet, ClientClass client)
    {
        _clusterServiceLocator.WorldCluster.Log.WriteLine(LogType.DEBUG, "[{0}:{1}] CMSG_GROUP_CANCEL", client.IP, client.Port);
    }

    public void On_CMSG_GROUP_ACCEPT(PacketClass packet, ClientClass client)
    {
        _clusterServiceLocator.WorldCluster.Log.WriteLine(LogType.DEBUG, "[{0}:{1}] CMSG_GROUP_ACCEPT", client.IP, client.Port);
        if (client.Character.GroupInvitedFlag && !client.Character.Group.IsFull)
        {
            client.Character.Group.Join(client.Character);
        }
        else
        {
            SendPartyResult(client, client.Character.Name, PartyCommand.PARTY_OP_INVITE, PartyCommandResult.INVITE_PARTY_FULL);
            client.Character.Group = null;
        }

        client.Character.GroupInvitedFlag = false;
    }

    public void On_CMSG_GROUP_DECLINE(PacketClass packet, ClientClass client)
    {
        _clusterServiceLocator.WorldCluster.Log.WriteLine(LogType.DEBUG, "[{0}:{1}] CMSG_GROUP_DECLINE", client.IP, client.Port);
        if (client.Character.GroupInvitedFlag)
        {
            PacketClass response = new(Opcodes.SMSG_GROUP_DECLINE);
            response.AddString(client.Character.Name);
            client.Character.Group.GetLeader().Client.Send(response);
            response.Dispose();
            client.Character.Group.CheckMembers();
            client.Character.Group = null;
            client.Character.GroupInvitedFlag = false;
        }
    }

    public void On_CMSG_GROUP_DISBAND(PacketClass packet, ClientClass client)
    {
        _clusterServiceLocator.WorldCluster.Log.WriteLine(LogType.DEBUG, "[{0}:{1}] CMSG_GROUP_DISBAND", client.IP, client.Port);
        if (client.Character.IsInGroup)
        {
            // TODO: InBattlegrounds: INVITE_RESTRICTED
            if (client.Character.Group.GetMembersCount() > 2)
            {
                client.Character.Group.Leave(client.Character);
            }
            else
            {
                client.Character.Group.Dispose();
            }
        }
    }

    public void On_CMSG_GROUP_UNINVITE(PacketClass packet, ClientClass client)
    {
        if (packet.Data.Length - 1 < 6)
        {
            return;
        }

        packet.GetInt16();
        var name = packet.GetString();
        _clusterServiceLocator.WorldCluster.Log.WriteLine(LogType.DEBUG, "[{0}:{1}] CMSG_GROUP_UNINVITE [{2}]", client.IP, client.Port, name);
        var guid = 0UL;
        _clusterServiceLocator.WorldCluster.CharacteRsLock.AcquireReaderLock(_clusterServiceLocator.GlobalConstants.DEFAULT_LOCK_TIMEOUT);
        foreach (var character in _clusterServiceLocator.WorldCluster.CharacteRs)
        {
            if (_clusterServiceLocator.CommonFunctions.UppercaseFirstLetter(character.Value.Name) == _clusterServiceLocator.CommonFunctions.UppercaseFirstLetter(name))
            {
                guid = character.Value.Guid;
                break;
            }
        }

        _clusterServiceLocator.WorldCluster.CharacteRsLock.ReleaseReaderLock();

        // TODO: InBattlegrounds: INVITE_RESTRICTED
        if (guid == 0m)
        {
            SendPartyResult(client, name, PartyCommand.PARTY_OP_LEAVE, PartyCommandResult.INVITE_NOT_FOUND);
        }
        else if (!client.Character.IsGroupLeader)
        {
            SendPartyResult(client, "", PartyCommand.PARTY_OP_LEAVE, PartyCommandResult.INVITE_NOT_LEADER);
        }
        else
        {
            var tmp = _clusterServiceLocator.WorldCluster.CharacteRs;
            var argobjCharacter = tmp[guid];
            client.Character.Group.Leave(argobjCharacter);
            tmp[guid] = argobjCharacter;
        }
    }

    public void On_CMSG_GROUP_UNINVITE_GUID(PacketClass packet, ClientClass client)
    {
        if (packet.Data.Length - 1 < 13)
        {
            return;
        }

        packet.GetInt16();
        var guid = packet.GetUInt64();
        _clusterServiceLocator.WorldCluster.Log.WriteLine(LogType.DEBUG, "[{0}:{1}] CMSG_GROUP_UNINVITE_GUID [0x{2:X}]", client.IP, client.Port, guid);

        // TODO: InBattlegrounds: INVITE_RESTRICTED
        if (guid == 0m)
        {
            SendPartyResult(client, "", PartyCommand.PARTY_OP_LEAVE, PartyCommandResult.INVITE_NOT_FOUND);
        }
        else if (_clusterServiceLocator.WorldCluster.CharacteRs.ContainsKey(guid) == false)
        {
            SendPartyResult(client, "", PartyCommand.PARTY_OP_LEAVE, PartyCommandResult.INVITE_NOT_FOUND);
        }
        else if (!client.Character.IsGroupLeader)
        {
            SendPartyResult(client, "", PartyCommand.PARTY_OP_LEAVE, PartyCommandResult.INVITE_NOT_LEADER);
        }
        else
        {
            var tmp = _clusterServiceLocator.WorldCluster.CharacteRs;
            var argobjCharacter = tmp[guid];
            client.Character.Group.Leave(argobjCharacter);
            tmp[guid] = argobjCharacter;
        }
    }

    public void On_CMSG_GROUP_SET_LEADER(PacketClass packet, ClientClass client)
    {
        if (packet.Data.Length - 1 < 6)
        {
            return;
        }

        packet.GetInt16();
        var name = packet.GetString();
        _clusterServiceLocator.WorldCluster.Log.WriteLine(LogType.DEBUG, "[{0}:{1}] CMSG_GROUP_SET_LEADER [Name={2}]", client.IP, client.Port, name);
        var guid = _clusterServiceLocator.WcHandlerCharacter.GetCharacterGuidByName(name);
        if (guid == 0m)
        {
            SendPartyResult(client, "", PartyCommand.PARTY_OP_INVITE, PartyCommandResult.INVITE_NOT_FOUND);
        }
        else if (_clusterServiceLocator.WorldCluster.CharacteRs.ContainsKey(guid) == false)
        {
            SendPartyResult(client, "", PartyCommand.PARTY_OP_INVITE, PartyCommandResult.INVITE_NOT_FOUND);
        }
        else if (!client.Character.IsGroupLeader)
        {
            SendPartyResult(client, client.Character.Name, PartyCommand.PARTY_OP_INVITE, PartyCommandResult.INVITE_NOT_LEADER);
        }
        else
        {
            var tmp = _clusterServiceLocator.WorldCluster.CharacteRs;
            var argobjCharacter = tmp[guid];
            client.Character.Group.SetLeader(argobjCharacter);
            tmp[guid] = argobjCharacter;
        }
    }

    public void On_CMSG_GROUP_RAID_CONVERT(PacketClass packet, ClientClass client)
    {
        _clusterServiceLocator.WorldCluster.Log.WriteLine(LogType.DEBUG, "[{0}:{1}] CMSG_GROUP_RAID_CONVERT", client.IP, client.Port);
        if (client.Character.IsInGroup)
        {
            SendPartyResult(client, "", PartyCommand.PARTY_OP_INVITE, PartyCommandResult.INVITE_OK);
            client.Character.Group.ConvertToRaid();
            client.Character.Group.SendGroupList();
            _clusterServiceLocator.WcNetwork.WorldServer.GroupSendUpdate(client.Character.Group.Id);
        }
    }

    public void On_CMSG_GROUP_CHANGE_SUB_GROUP(PacketClass packet, ClientClass client)
    {
        if (packet.Data.Length - 1 < 6)
        {
            return;
        }

        packet.GetInt16();
        var name = packet.GetString();
        if (packet.Data.Length - 1 < 6 + name.Length + 1)
        {
            return;
        }

        var subGroup = packet.GetInt8();
        _clusterServiceLocator.WorldCluster.Log.WriteLine(LogType.DEBUG, "[{0}:{1}] CMSG_GROUP_CHANGE_SUB_GROUP [{2}:{3}]", client.IP, client.Port, name, subGroup);
        if (client.Character.IsInGroup)
        {
            int j;
            var loopTo = ((subGroup + 1) * _clusterServiceLocator.GlobalConstants.GROUP_SUBGROUPSIZE) - 1;
            for (j = subGroup * _clusterServiceLocator.GlobalConstants.GROUP_SUBGROUPSIZE; j <= loopTo; j++)
            {
                if (client.Character.Group.Members[j] is null)
                {
                    break;
                }
            }

            for (int i = 0, loopTo1 = client.Character.Group.Members.Length - 1; i <= loopTo1; i++)
            {
                if (client.Character.Group.Members[i] is not null && (client.Character.Group.Members[i].Name ?? "") == (name ?? ""))
                {
                    client.Character.Group.Members[j] = client.Character.Group.Members[i];
                    client.Character.Group.Members[i] = null;
                    if (client.Character.Group.Leader == i)
                    {
                        client.Character.Group.Leader = (byte)j;
                    }

                    client.Character.Group.SendGroupList();
                    break;
                }
            }
        }
    }

    public void On_CMSG_GROUP_SWAP_SUB_GROUP(PacketClass packet, ClientClass client)
    {
        if (packet.Data.Length - 1 < 6)
        {
            return;
        }

        packet.GetInt16();
        var name1 = packet.GetString();
        if (packet.Data.Length - 1 < 6 + name1.Length + 1)
        {
            return;
        }

        var name2 = packet.GetString();
        _clusterServiceLocator.WorldCluster.Log.WriteLine(LogType.DEBUG, "[{0}:{1}] CMSG_GROUP_SWAP_SUB_GROUP [{2}:{3}]", client.IP, client.Port, name1, name2);
        if (client.Character.IsInGroup)
        {
            int j;
            var loopTo = client.Character.Group.Members.Length - 1;
            for (j = 0; j <= loopTo; j++)
            {
                if (client.Character.Group.Members[j] is not null && (client.Character.Group.Members[j].Name ?? "") == (name2 ?? ""))
                {
                    break;
                }
            }

            for (int i = 0, loopTo1 = client.Character.Group.Members.Length - 1; i <= loopTo1; i++)
            {
                if (client.Character.Group.Members[i] is not null && (client.Character.Group.Members[i].Name ?? "") == (name1 ?? ""))
                {
                    var tmpPlayer = client.Character.Group.Members[j];
                    client.Character.Group.Members[j] = client.Character.Group.Members[i];
                    client.Character.Group.Members[i] = tmpPlayer;
                    if (client.Character.Group.Leader == i)
                    {
                        client.Character.Group.Leader = (byte)j;
                    }
                    else if (client.Character.Group.Leader == j)
                    {
                        client.Character.Group.Leader = (byte)i;
                    }

                    client.Character.Group.SendGroupList();
                    break;
                }
            }
        }
    }

    public void On_CMSG_LOOT_METHOD(PacketClass packet, ClientClass client)
    {
        if (packet.Data.Length - 1 < 21)
        {
            return;
        }

        packet.GetInt16();
        var method = packet.GetInt32();
        var master = packet.GetUInt64();
        var threshold = packet.GetInt32();
        _clusterServiceLocator.WorldCluster.Log.WriteLine(LogType.DEBUG, "[{0}:{1}] CMSG_LOOT_METHOD [Method={2}, Master=0x{3:X}, Threshold={4}]", client.IP, client.Port, method, master, threshold);
        if (!client.Character.IsGroupLeader)
        {
            return;
        }

        client.Character.Group.SetLootMaster(master);
        client.Character.Group.LootMethod = (GroupLootMethod)method;
        client.Character.Group.LootThreshold = (GroupLootThreshold)threshold;
        client.Character.Group.SendGroupList();
        _clusterServiceLocator.WcNetwork.WorldServer.GroupSendUpdateLoot(client.Character.Group.Id);
    }

    public void On_MSG_MINIMAP_PING(PacketClass packet, ClientClass client)
    {
        packet.GetInt16();
        var x = packet.GetFloat();
        var y = packet.GetFloat();
        _clusterServiceLocator.WorldCluster.Log.WriteLine(LogType.DEBUG, "[{0}:{1}] MSG_MINIMAP_PING [{2}:{3}]", client.IP, client.Port, x, y);
        if (client.Character.IsInGroup)
        {
            PacketClass response = new(Opcodes.MSG_MINIMAP_PING);
            response.AddUInt64(client.Character.Guid);
            response.AddSingle(x);
            response.AddSingle(y);
            client.Character.Group.Broadcast(response);
            response.Dispose();
        }
    }

    public void On_MSG_RANDOM_ROLL(PacketClass packet, ClientClass client)
    {
        if (packet.Data.Length - 1 < 13)
        {
            return;
        }

        packet.GetInt16();
        var minRoll = packet.GetInt32();
        var maxRoll = packet.GetInt32();
        _clusterServiceLocator.WorldCluster.Log.WriteLine(LogType.DEBUG, "[{0}:{1}] MSG_RANDOM_ROLL [min={2} max={3}]", client.IP, client.Port, minRoll, maxRoll);
        PacketClass response = new(Opcodes.MSG_RANDOM_ROLL);
        response.AddInt32(minRoll);
        response.AddInt32(maxRoll);
        response.AddInt32(_clusterServiceLocator.WorldCluster.Rnd.Next(minRoll, maxRoll));
        response.AddUInt64(client.Character.Guid);
        if (client.Character.IsInGroup)
        {
            client.Character.Group.Broadcast(response);
        }
        else
        {
            client.SendMultiplyPackets(response);
        }

        response.Dispose();
    }

    public void On_MSG_RAID_READY_CHECK(PacketClass packet, ClientClass client)
    {
        _clusterServiceLocator.WorldCluster.Log.WriteLine(LogType.DEBUG, "[{0}:{1}] MSG_RAID_READY_CHECK", client.IP, client.Port);
        if (client.Character.IsGroupLeader)
        {
            client.Character.Group.BroadcastToOther(packet, client.Character);
        }
        else
        {
            if (packet.Data.Length - 1 < 6)
            {
                return;
            }

            packet.GetInt16();
            var result = packet.GetInt8();
            if (result == 0)
            {
                // DONE: Not ready
                client.Character.Group.GetLeader().Client.Send(packet);
            }
            else
            {
                // DONE: Ready
                PacketClass response = new(Opcodes.MSG_RAID_READY_CHECK);
                response.AddUInt64(client.Character.Guid);
                client.Character.Group.GetLeader().Client.Send(response);
                response.Dispose();
            }
        }
    }

    public void On_MSG_RAID_ICON_TARGET(PacketClass packet, ClientClass client)
    {
        if (packet.Data.Length < 7)
        {
            return; // Too short packet
        }

        if (client.Character.Group is null)
        {
            return;
        }

        packet.GetInt16();
        var icon = packet.GetInt8();
        if (icon == 255)
        {
            // DONE: Send icon target list
            PacketClass response = new(Opcodes.MSG_RAID_ICON_TARGET);
            response.AddInt8(1); // Target list
            for (byte i = 0; i <= 7; i++)
            {
                if (client.Character.Group.TargetIcons[i] == 0m)
                {
                    continue;
                }

                response.AddInt8(i);
                response.AddUInt64(client.Character.Group.TargetIcons[i]);
            }

            client.Send(response);
            response.Dispose();
        }
        else
        {
            if (icon > 7)
            {
                return; // Not a valid icon
            }

            if (packet.Data.Length < 15)
            {
                return; // Too short packet
            }

            var guid = packet.GetUInt64();

            // DONE: Set the raid icon target
            client.Character.Group.TargetIcons[icon] = guid;
            PacketClass response = new(Opcodes.MSG_RAID_ICON_TARGET);
            response.AddInt8(0); // Set target
            response.AddInt8(icon);
            response.AddUInt64(guid);
            client.Character.Group.Broadcast(response);
            response.Dispose();
        }
    }

    public void On_CMSG_REQUEST_PARTY_MEMBER_STATS(PacketClass packet, ClientClass client)
    {
        if (packet.Data.Length - 1 < 13)
        {
            return;
        }

        packet.GetInt16();
        var guid = packet.GetUInt64();
        _clusterServiceLocator.WorldCluster.Log.WriteLine(LogType.DEBUG, "[{0}:{1}] CMSG_REQUEST_PARTY_MEMBER_STATS [{2:X}]", client.IP, client.Port, guid);
        if (!_clusterServiceLocator.WorldCluster.CharacteRs.ContainsKey(guid))
        {
            // Character is offline
            var response = _clusterServiceLocator.Functions.BuildPartyMemberStatsOffline(guid);
            client.Send(response);
            response.Dispose();
        }
        else if (_clusterServiceLocator.WorldCluster.CharacteRs[guid].IsInWorld == false)
        {
            // Character is offline (not in world)
            var response = _clusterServiceLocator.Functions.BuildPartyMemberStatsOffline(guid);
            client.Send(response);
            response.Dispose();
        }
        else
        {
            // Request information from WorldServer
            PacketClass response = new(0) { Data = _clusterServiceLocator.WorldCluster.CharacteRs[guid].GetWorld.GroupMemberStats(guid, 0) };
            client.Send(response);
            response.Dispose();
        }
    }
}
