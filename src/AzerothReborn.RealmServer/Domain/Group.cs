using AzerothReborn.RealmServer.Reference;

namespace AzerothReborn.RealmServer.Domain;

public class Group : IDisposable
{
    public long Id;
    public GroupType Type = GroupType.PARTY;
    public GroupDungeonDifficulty DungeonDifficulty = GroupDungeonDifficulty.DIFFICULTY_NORMAL;
    // private byte _lootMaster;
    public GroupLootMethod LootMethod = GroupLootMethod.LOOT_GROUP;
    public GroupLootThreshold LootThreshold = GroupLootThreshold.Uncommon;
    public Character Leader;
    public List<Character> Members;
    public ulong[] TargetIcons = new ulong[8];

    public Group(Character character)
    {
        Members = new List<Character> { character };
        // Id = Interlocked.Increment(ref _clusterServiceLocator.WcHandlersGroup._groupCounter);
        // _clusterServiceLocator.WcHandlersGroup.GrouPs.Add(Id, this);
        Leader = character;
        // _lootMaster = 255;
        character.Group = this;
        character.GroupAssistant = false;
        // character.GetWorld.ClientSetGroup(character.Client.Index, Id);
    }

    private bool _disposedValue; // To detect redundant calls

    // IDisposable
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            // PacketClass packet;
            // if (Type == GroupType.RAID)
            // {
            //     packet = new PacketClass(Opcodes.SMSG_GROUP_LIST);
            //     packet.AddInt16(0);          // GroupType 0:Party 1:Raid
            //     packet.AddInt32(0);          // GroupCount
            // }
            // else
            // {
            //     packet = new PacketClass(Opcodes.SMSG_GROUP_DESTROYED);
            // }

            // for (byte i = 0, loopTo = (byte)(Members.Length - 1); i <= loopTo; i++)
            // {
            //     if (Members[i] is not null)
            //     {
            //         Members[i].Group = null;
            //         if (Members[i].Client is not null)
            //         {
            //             Members[i].Client.SendMultiplyPackets(packet);
            //             Members[i].GetWorld.ClientSetGroup(Members[i].Client.Index, -1);
            //         }

            //         Members[i] = null;
            //     }
            // }

            // packet.Dispose();
            // _clusterServiceLocator.WcNetwork.WorldServer.GroupSendUpdate(Id);
            // _clusterServiceLocator.WcHandlersGroup.GrouPs.Remove(Id);
        }

        _disposedValue = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    // public void Join(WcHandlerCharacter.CharacterObject objCharacter)
    // {
    //     for (byte i = 0, loopTo = (byte)(Members.Length - 1); i <= loopTo; i++)
    //     {
    //         if (Members[i] is null)
    //         {
    //             Members[i] = objCharacter;
    //             objCharacter.Group = this;
    //             objCharacter.GroupAssistant = false;
    //             break;
    //         }
    //     }

    //     _clusterServiceLocator.WcNetwork.WorldServer.GroupSendUpdate(Id);
    //     objCharacter.GetWorld.ClientSetGroup(objCharacter.Client.Index, Id);
    //     SendGroupList();
    // }

    // public void Leave(WcHandlerCharacter.CharacterObject objCharacter)
    // {
    //     if (GetMembersCount() == 2)
    //     {
    //         Dispose();
    //         return;
    //     }

    //     for (byte i = 0, loopTo = (byte)(Members.Length - 1); i <= loopTo; i++)
    //     {
    //         if (ReferenceEquals(Members[i], objCharacter))
    //         {
    //             objCharacter.Group = null;
    //             Members[i] = null;

    //             // DONE: If current is leader then choose new
    //             if (i == Leader)
    //             {
    //                 NewLeader();
    //             }

    //             // DONE: If current is lootMaster then choose new
    //             if (i == _lootMaster)
    //             {
    //                 _lootMaster = Leader;
    //             }

    //             if (objCharacter.Client is not null)
    //             {
    //                 PacketClass packet = new(Opcodes.SMSG_GROUP_UNINVITE);
    //                 objCharacter.Client.Send(packet);
    //                 packet.Dispose();
    //             }

    //             break;
    //         }
    //     }

    //     _clusterServiceLocator.WcNetwork.WorldServer.GroupSendUpdate(Id);
    //     objCharacter.GetWorld.ClientSetGroup(objCharacter.Client.Index, -1);
    //     CheckMembers();
    // }

    // public void CheckMembers()
    // {
    //     if (GetMembersCount() < 2)
    //     {
    //         Dispose();
    //     }
    //     else
    //     {
    //         SendGroupList();
    //     }
    // }

    // public void NewLeader(WcHandlerCharacter.CharacterObject leaver = null)
    // {
    //     byte chosenMember = 255;
    //     var newLootMaster = false;
    //     for (byte i = 0, loopTo = (byte)(Members.Length - 1); i <= loopTo; i++)
    //     {
    //         if (Members[i] is not null && Members[i].Client is not null)
    //         {
    //             if (leaver is not null && ReferenceEquals(leaver, Members[i]))
    //             {
    //                 if (i == _lootMaster)
    //                 {
    //                     newLootMaster = true;
    //                 }

    //                 if (chosenMember != 255)
    //                 {
    //                     break;
    //                 }
    //             }
    //             else if (Members[i].GroupAssistant && chosenMember == 255)
    //             {
    //                 chosenMember = i;
    //             }
    //             else if (chosenMember == 255)
    //             {
    //                 chosenMember = i;
    //             }
    //         }
    //     }

    //     if (chosenMember != 255)
    //     {
    //         Leader = chosenMember;
    //         if (newLootMaster)
    //         {
    //             _lootMaster = Leader;
    //         }

    //         PacketClass response = new(Opcodes.SMSG_GROUP_SET_LEADER);
    //         response.AddString(Members[Leader].Name);
    //         Broadcast(response);
    //         response.Dispose();
    //         _clusterServiceLocator.WcNetwork.WorldServer.GroupSendUpdate(Id);
    //     }
    // }

    public bool IsFull
    {
        get
        {
            for (byte i = 0, loopTo = (byte)(Members.Count - 1); i <= loopTo; i++)
            {
                if (Members[i] is null)
                {
                    return false;
                }
            }

            return true;
        }
    }

    // public void ConvertToRaid()
    // {
    //     Array.Resize(ref Members, _clusterServiceLocator.GlobalConstants.GROUP_RAIDSIZE + 1);
    //     for (int i = _clusterServiceLocator.GlobalConstants.GROUP_SIZE + 1, loopTo = _clusterServiceLocator.GlobalConstants.GROUP_RAIDSIZE; i <= loopTo; i++)
    //     {
    //         Members[i] = null;
    //     }

    //     Type = GroupType.RAID;
    // }

    // public void SetLeader(WcHandlerCharacter.CharacterObject objCharacter)
    // {
    //     for (byte i = 0, loopTo = (byte)Members.Length; i <= loopTo; i++)
    //     {
    //         if (ReferenceEquals(Members[i], objCharacter))
    //         {
    //             Leader = i;
    //             break;
    //         }
    //     }

    //     PacketClass packet = new(Opcodes.SMSG_GROUP_SET_LEADER);
    //     packet.AddString(objCharacter.Name);
    //     Broadcast(packet);
    //     packet.Dispose();
    //     _clusterServiceLocator.WcNetwork.WorldServer.GroupSendUpdate(Id);
    //     SendGroupList();
    // }

    // public void SetLootMaster(WcHandlerCharacter.CharacterObject objCharacter)
    // {
    //     _lootMaster = Leader;
    //     for (byte i = 0, loopTo = (byte)(Members.Length - 1); i <= loopTo; i++)
    //     {
    //         if (ReferenceEquals(Members[i], objCharacter))
    //         {
    //             _lootMaster = i;
    //             break;
    //         }
    //     }

    //     SendGroupList();
    // }

    // public void SetLootMaster(ulong guid)
    // {
    //     _lootMaster = 255;
    //     for (byte i = 0, loopTo = (byte)(Members.Length - 1); i <= loopTo; i++)
    //     {
    //         if (Members[i] is not null && Members[i].Guid == guid)
    //         {
    //             _lootMaster = i;
    //             break;
    //         }
    //     }

    //     SendGroupList();
    // }

    public Character GetLeader()
    {
        return Leader;
    }

    public Character GetLootMaster()
    {
        return Leader;
    }

    public byte GetMembersCount()
    {
        byte count = 0;
        for (byte i = 0, loopTo = (byte)(Members.Count - 1); i <= loopTo; i++)
        {
            if (Members[i] is not null)
            {
                count = (byte)(count + 1);
            }
        }

        return count;
    }

    public ulong[] GetMembers()
    {
        List<ulong> list = new();
        for (byte i = 0, loopTo = (byte)(Members.Count - 1); i <= loopTo; i++)
        {
            if (Members[i] is not null)
            {
                list.Add(Members[i].Guid);
            }
        }

        return list.ToArray();
    }

    // public void Broadcast(PacketClass packet)
    // {
    //     for (byte i = 0, loopTo = (byte)(Members.Length - 1); i <= loopTo; i++)
    //     {
    //         if (Members[i] is not null && Members[i].Client is not null)
    //         {
    //             Members[i].Client.SendMultiplyPackets(packet);
    //         }
    //     }
    // }

    // public void BroadcastToOther(PacketClass packet, WcHandlerCharacter.CharacterObject objCharacter)
    // {
    //     for (byte i = 0, loopTo = (byte)(Members.Length - 1); i <= loopTo; i++)
    //     {
    //         if (Members[i] is not null && !ReferenceEquals(Members[i], objCharacter) && Members[i].Client is not null)
    //         {
    //             Members[i].Client.SendMultiplyPackets(packet);
    //         }
    //     }
    // }

    // public void BroadcastToOutOfRange(PacketClass packet, WcHandlerCharacter.CharacterObject objCharacter)
    // {
    //     for (byte i = 0, loopTo = (byte)(Members.Length - 1); i <= loopTo; i++)
    //     {
    //         if (Members[i] is not null && !ReferenceEquals(Members[i], objCharacter) && Members[i].Client is not null)
    //         {
    //             if (objCharacter.Map != Members[i].Map || Math.Sqrt(Math.Pow(objCharacter.PositionX - Members[i].PositionX, 2d) + Math.Pow(objCharacter.PositionY - Members[i].PositionY, 2d)) > _clusterServiceLocator.GlobalConstants.DEFAULT_DISTANCE_VISIBLE)
    //             {
    //                 Members[i].Client.SendMultiplyPackets(packet);
    //             }
    //         }
    //     }
    // }

    // public void SendGroupList()
    // {
    //     var groupCount = GetMembersCount();
    //     for (byte i = 0, loopTo = (byte)(Members.Length - 1); i <= loopTo; i++)
    //     {
    //         if (Members[i] is not null)
    //         {
    //             PacketClass packet = new(Opcodes.SMSG_GROUP_LIST);
    //             packet.AddInt8((byte)Type);                                    // GroupType 0:Party 1:Raid
    //             var memberFlags = (byte)(i / _clusterServiceLocator.GlobalConstants.GROUP_SUBGROUPSIZE);
    //             // If Members(i).GroupAssistant Then MemberFlags = MemberFlags Or &H1
    //             packet.AddInt8(memberFlags);
    //             packet.AddInt32(groupCount - 1);
    //             for (byte j = 0, loopTo1 = (byte)(Members.Length - 1); j <= loopTo1; j++)
    //             {
    //                 if (Members[j] is not null && !ReferenceEquals(Members[j], Members[i]))
    //                 {
    //                     packet.AddString(Members[j].Name);
    //                     packet.AddUInt64(Members[j].Guid);
    //                     if (Members[j].IsInWorld)
    //                     {
    //                         packet.AddInt8(1);                           // CharOnline?
    //                     }
    //                     else
    //                     {
    //                         packet.AddInt8(0);
    //                     }                           // CharOnline?

    //                     memberFlags = (byte)(j / _clusterServiceLocator.GlobalConstants.GROUP_SUBGROUPSIZE);
    //                     // If Members(j).GroupAssistant Then MemberFlags = MemberFlags Or &H1
    //                     packet.AddInt8(memberFlags);
    //                 }
    //             }

    //             packet.AddUInt64(Members[Leader].Guid);
    //             packet.AddInt8((byte)LootMethod);
    //             if (_lootMaster != 255)
    //             {
    //                 packet.AddUInt64(Members[_lootMaster].Guid);
    //             }
    //             else
    //             {
    //                 packet.AddUInt64(0UL);
    //             }

    //             packet.AddInt8((byte)LootThreshold);
    //             packet.AddInt16(0);
    //             Members[i].Client?.Send(packet);

    //             packet.Dispose();
    //         }
    //     }
    // }

    // public void SendChatMessage(WcHandlerCharacter.CharacterObject sender, string message, LANGUAGES language, ChatMsg thisType)
    // {
    //     var packet = _clusterServiceLocator.Functions.BuildChatMessage(sender.Guid, message, thisType, language, (byte)sender.ChatFlag);
    //     Broadcast(packet);
    //     packet.Dispose();
    // }
}
