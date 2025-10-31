
// public class WcHandlersAuth
// {

//     public void On_CMSG_CHAR_ENUM(PacketClass packet, ClientClass client)
//     {
//         _clusterServiceLocator.WorldCluster.Log.WriteLine(LogType.DEBUG, "[{0}:{1}] CMSG_CHAR_ENUM", client.IP, client.Port);

//         // DONE: Query _WorldCluster.CHARACTERs DB
//         PacketClass response = new(Opcodes.SMSG_CHAR_ENUM);
//         DataTable mySqlQuery = new();
//         int accountId;
//         try
//         {
//             _clusterServiceLocator.WorldCluster.GetAccountDatabase().Query(string.Format("SELECT id FROM account WHERE username = '{0}';", client.Account), ref mySqlQuery);
//             accountId = mySqlQuery.Rows[0].As<int>("id");
//             mySqlQuery.Clear();
//             _clusterServiceLocator.WorldCluster.GetCharacterDatabase().Query(string.Format("SELECT * FROM characters WHERE account_id = '{0}' ORDER BY char_guid;", accountId), ref mySqlQuery);

//             // DONE: Make The Packet
//             response.AddInt8((byte)mySqlQuery.Rows.Count);
//             for (int i = 0, loopTo = mySqlQuery.Rows.Count - 1; i <= loopTo; i++)
//             {
//                 var dead = false;
//                 DataTable deadMySqlQuery = new();
//                 _clusterServiceLocator.WorldCluster.GetCharacterDatabase().Query(string.Format("SELECT COUNT(*) FROM corpse WHERE player = {0};", mySqlQuery.Rows[i]["char_guid"]), ref deadMySqlQuery);
//                 if (deadMySqlQuery.Rows[0].As<int>(0) > 0)
//                 {
//                     dead = true;
//                 }

//                 DataTable petQuery = new();
//                 _clusterServiceLocator.WorldCluster.GetCharacterDatabase().Query(string.Format("SELECT modelid, level, entry FROM character_pet WHERE owner = '{0}';", mySqlQuery.Rows[i]["char_guid"]), ref petQuery);
//                 response.AddInt64(mySqlQuery.Rows[i].As<long>("char_guid"));
//                 response.AddString(mySqlQuery.Rows[i].As<string>("char_name"));
//                 response.AddInt8(mySqlQuery.Rows[i].As<byte>("char_race"));
//                 response.AddInt8(mySqlQuery.Rows[i].As<byte>("char_class"));
//                 response.AddInt8(mySqlQuery.Rows[i].As<byte>("char_gender"));
//                 response.AddInt8(mySqlQuery.Rows[i].As<byte>("char_skin"));
//                 response.AddInt8(mySqlQuery.Rows[i].As<byte>("char_face"));
//                 response.AddInt8(mySqlQuery.Rows[i].As<byte>("char_hairStyle"));
//                 response.AddInt8(mySqlQuery.Rows[i].As<byte>("char_hairColor"));
//                 response.AddInt8(mySqlQuery.Rows[i].As<byte>("char_facialHair"));
//                 response.AddInt8(mySqlQuery.Rows[i].As<byte>("char_level"));
//                 response.AddInt32(mySqlQuery.Rows[i].As<int>("char_zone_id"));
//                 response.AddInt32(mySqlQuery.Rows[i].As<int>("char_map_id"));
//                 response.AddSingle(mySqlQuery.Rows[i].As<float>("char_positionX"));
//                 response.AddSingle(mySqlQuery.Rows[i].As<float>("char_positionY"));
//                 response.AddSingle(mySqlQuery.Rows[i].As<float>("char_positionZ"));
//                 response.AddInt32(mySqlQuery.Rows[i].As<int>("char_guildId"));
//                 var playerState = (uint)CharacterFlagState.CHARACTER_FLAG_NONE;
//                 var forceRestrictions = mySqlQuery.Rows[i].As<uint>("force_restrictions");
//                 if ((forceRestrictions & (uint)ForceRestrictionFlags.RESTRICT_TRANSFER) != 0)
//                 {
//                     playerState += (uint)CharacterFlagState.CHARACTER_FLAG_LOCKED_FOR_TRANSFER;
//                 }

//                 if ((forceRestrictions & (uint)ForceRestrictionFlags.RESTRICT_BILLING) != 0)
//                 {
//                     playerState += (uint)CharacterFlagState.CHARACTER_FLAG_LOCKED_BY_BILLING;
//                 }

//                 if ((forceRestrictions & (uint)ForceRestrictionFlags.RESTRICT_RENAME) != 0)
//                 {
//                     playerState += (uint)CharacterFlagState.CHARACTER_FLAG_RENAME;
//                 }

//                 if (dead)
//                 {
//                     playerState += (uint)CharacterFlagState.CHARACTER_FLAG_GHOST;
//                 }

//                 response.AddUInt32(playerState);
//                 response.AddInt8(mySqlQuery.Rows[i].As<byte>("char_restState"));
//                 var petModel = 0;
//                 var petLevel = 0;
//                 var petFamily = 0;
//                 if (petQuery.Rows.Count > 0)
//                 {
//                     petModel = petQuery.Rows[0].As<int>("modelid");
//                     petLevel = petQuery.Rows[0].As<int>("level");
//                     DataTable petFamilyQuery = new();
//                     _clusterServiceLocator.WorldCluster.GetWorldDatabase().Query(string.Format("SELECT family FROM creature_template WHERE entry = '{0}'", petQuery.Rows[0]["entry"]), ref petFamilyQuery);
//                     petFamily = petFamilyQuery.Rows[0].As<int>("family");
//                 }

//                 response.AddInt32(petModel);
//                 response.AddInt32(petLevel);
//                 response.AddInt32(petFamily);

//                 // DONE: Get items
//                 var guid = mySqlQuery.Rows[i].As<long>("char_guid");
//                 DataTable itemsMySqlQuery = new();
//                 var characterDb = _clusterServiceLocator.WorldCluster.GetCharacterDatabase().DBName();
//                 var worldDb = _clusterServiceLocator.WorldCluster.GetWorldDatabase().DBName();
//                 _clusterServiceLocator.WorldCluster.GetCharacterDatabase().Query(string.Format("SELECT item_slot, displayid, inventorytype FROM " + characterDb + ".characters_inventory, " + worldDb + ".item_template WHERE item_bag = {0} AND item_slot <> 255 AND entry = item_id  ORDER BY item_slot;", guid), ref itemsMySqlQuery);
//                 var e = itemsMySqlQuery.Rows.GetEnumerator();
//                 e.Reset();
//                 e.MoveNext();
//                 DataRow row = (DataRow)e.Current;

//                 // DONE: Add model info
//                 for (byte slot = 0, loopTo1 = (byte)EquipmentSlots.EQUIPMENT_SLOT_END; slot <= loopTo1; slot++) // - 1
//                 {
//                     if (row is null || row.As<int>("item_slot") != slot)
//                     {
//                         // No equiped item in this slot
//                         response.AddInt32(0); // Item Model
//                         response.AddInt8(0);  // Item Slot
//                     }
//                     else
//                     {
//                         // DONE: Do not show helmet or cloak
//                         if (((forceRestrictions & (uint)ForceRestrictionFlags.RESTRICT_HIDECLOAK) != 0) && (EquipmentSlots)row.As<byte>("item_slot") == EquipmentSlots.EQUIPMENT_SLOT_BACK || ((forceRestrictions & (uint)ForceRestrictionFlags.RESTRICT_HIDEHELM) != 0) && (EquipmentSlots)row.As<byte>("item_slot") == EquipmentSlots.EQUIPMENT_SLOT_HEAD)
//                         {
//                             response.AddInt32(0); // Item Model
//                             response.AddInt8(0);  // Item Slot
//                         }
//                         else
//                         {
//                             response.AddInt32(row.As<int>("displayid"));          // Item Model
//                             response.AddInt8(row.As<byte>("inventorytype"));
//                         }       // Item Slot

//                         e.MoveNext();
//                         row = (DataRow)e.Current;
//                     }
//                 }
//             }
//         }
//         catch (Exception e)
//         {
//             _clusterServiceLocator.WorldCluster.Log.WriteLine(LogType.FAILED, "[{0}:{1}] Unable to enum characters. [{2}]", client.IP, client.Port, e.Message);
//             // TODO: Find what opcode officials use
//             response = new PacketClass(Opcodes.SMSG_CHAR_CREATE);
//             response.AddInt8((byte)CharResponse.CHAR_LIST_FAILED);
//         }

//         client.Send(response);
//         _clusterServiceLocator.WorldCluster.Log.WriteLine(LogType.DEBUG, "[{0}:{1}] SMSG_CHAR_ENUM", client.IP, client.Port);
//     }

//     public void On_CMSG_CHAR_DELETE(PacketClass packet, ClientClass client)
//     {
//         _clusterServiceLocator.WorldCluster.Log.WriteLine(LogType.DEBUG, "[{0}:{1}] CMSG_CHAR_DELETE", client.IP, client.Port);
//         PacketClass response = new(Opcodes.SMSG_CHAR_DELETE);
//         packet.GetInt16();
//         var guid = packet.GetUInt64();
//         try
//         {
//             DataTable q = new();

//             // Done: Fixed packet manipulation protection
//             _clusterServiceLocator.WorldCluster.GetAccountDatabase().Query(string.Format("SELECT id FROM account WHERE username = \"{0}\";", client.Account), ref q);
//             if (q.Rows.Count == 0)
//             {
//                 return;
//             }

//             _clusterServiceLocator.WorldCluster.GetCharacterDatabase().Query(string.Format("SELECT char_guid FROM characters WHERE account_id = \"{0}\" AND char_guid = \"{1}\";", q.Rows[0]["id"], guid), ref q);
//             if (q.Rows.Count == 0)
//             {
//                 response.AddInt8((byte)AuthResult.WOW_FAIL_BANNED);
//                 client.Send(response);
//                 _clusterServiceLocator.Functions.Ban_Account(client.Account, "Packet Manipulation/Character Deletion");
//                 client.Delete();
//                 return;
//             }

//             q.Clear();
//             _clusterServiceLocator.WorldCluster.GetCharacterDatabase().Query(string.Format("SELECT item_guid FROM characters_inventory WHERE item_bag = {0};", guid), ref q);
//             foreach (DataRow row in q.Rows)
//             {
//                 // DONE: Delete items
//                 _clusterServiceLocator.WorldCluster.GetCharacterDatabase().Update(string.Format("DELETE FROM characters_inventory WHERE item_guid = \"{0}\";", row.As<string>("item_guid")));
//                 // DONE: Delete items in bags
//                 _clusterServiceLocator.WorldCluster.GetCharacterDatabase().Update(string.Format("DELETE FROM characters_inventory WHERE item_bag = \"{0}\";", row.As<ulong>("item_guid") + _clusterServiceLocator.GlobalConstants.GUID_ITEM));
//             }

//             _clusterServiceLocator.WorldCluster.GetCharacterDatabase().Query(string.Format("SELECT item_guid FROM characters_inventory WHERE item_owner = {0};", guid), ref q);
//             q.Clear();
//             _clusterServiceLocator.WorldCluster.GetCharacterDatabase().Query(string.Format("SELECT mail_id FROM characters_mail WHERE mail_receiver = \"{0}\";", guid), ref q);
//             foreach (DataRow row in q.Rows)
//             {
//                 // TODO: Return mails?
//                 // DONE: Delete mails
//                 _clusterServiceLocator.WorldCluster.GetCharacterDatabase().Update(string.Format("DELETE FROM characters_mail WHERE mail_id = \"{0}\";", row.As<string>("mail_id")));
//                 // DONE: Delete mail items
//                 _clusterServiceLocator.WorldCluster.GetCharacterDatabase().Update(string.Format("DELETE FROM mail_items WHERE mail_id = \"{0}\";", row.As<string>("mail_id")));
//             }

//             _clusterServiceLocator.WorldCluster.GetCharacterDatabase().Update(string.Format("DELETE FROM characters WHERE char_guid = \"{0}\";", guid));
//             _clusterServiceLocator.WorldCluster.GetCharacterDatabase().Update(string.Format("DELETE FROM characters_honor WHERE char_guid = \"{0}\";", guid));
//             _clusterServiceLocator.WorldCluster.GetCharacterDatabase().Update(string.Format("DELETE FROM characters_quests WHERE char_guid = \"{0}\";", guid));
//             _clusterServiceLocator.WorldCluster.GetCharacterDatabase().Update(string.Format("DELETE FROM character_social WHERE guid = '{0}' OR friend = '{0}';", guid));
//             _clusterServiceLocator.WorldCluster.GetCharacterDatabase().Update(string.Format("DELETE FROM characters_spells WHERE guid = \"{0}\";", guid));
//             _clusterServiceLocator.WorldCluster.GetCharacterDatabase().Update(string.Format("DELETE FROM petitions WHERE petition_owner = \"{0}\";", guid));
//             _clusterServiceLocator.WorldCluster.GetCharacterDatabase().Update(string.Format("DELETE FROM auctionhouse WHERE auction_owner = \"{0}\";", guid));
//             _clusterServiceLocator.WorldCluster.GetCharacterDatabase().Update(string.Format("DELETE FROM characters_tickets WHERE char_guid = \"{0}\";", guid));
//             _clusterServiceLocator.WorldCluster.GetCharacterDatabase().Update(string.Format("DELETE FROM corpse WHERE guid = \"{0}\";", guid));
//             q.Clear();
//             _clusterServiceLocator.WorldCluster.GetCharacterDatabase().Query(string.Format("SELECT guild_id FROM guilds WHERE guild_leader = \"{0}\";", guid), ref q);
//             if (q.Rows.Count > 0)
//             {
//                 _clusterServiceLocator.WorldCluster.GetCharacterDatabase().Update(string.Format("UPDATE characters SET char_guildid = 0, char_guildrank = 0, char_guildpnote = '', charguildoffnote = '' WHERE char_guildid = \"{0}\";", q.Rows[0]["guild_id"]));
//                 _clusterServiceLocator.WorldCluster.GetCharacterDatabase().Update(string.Format("DELETE FROM guild WHERE guild_id = \"{0}\";", q.Rows[0]["guild_id"]));
//             }

//             response.AddInt8((byte)CharResponse.CHAR_DELETE_SUCCESS); // Changed in 1.12.x client branch?
//         }
//         catch (Exception)
//         {
//             response.AddInt8((byte)CharResponse.CHAR_DELETE_FAILED);
//         }

//         client.Send(response);
//         _clusterServiceLocator.WorldCluster.Log.WriteLine(LogType.DEBUG, "[{0}:{1}] SMSG_CHAR_DELETE [{2:X}]", client.IP, client.Port, guid);
//     }

//     public void On_CMSG_CHAR_RENAME(PacketClass packet, ClientClass client)
//     {
//         packet.GetInt16();
//         var guid = packet.GetInt64();
//         var name = packet.GetString();
//         _clusterServiceLocator.WorldCluster.Log.WriteLine(LogType.DEBUG, "[{0}:{1}] CMSG_CHAR_RENAME [{2}:{3}]", client.IP, client.Port, guid, name);
//         var errCode = (byte)ATLoginFlags.AT_LOGIN_RENAME;

//         // DONE: Check for existing name
//         DataTable q = new();
//         _clusterServiceLocator.WorldCluster.GetCharacterDatabase().Query(string.Format("SELECT char_name FROM characters WHERE char_name LIKE \"{0}\";", name), ref q);
//         if (q.Rows.Count > 0)
//         {
//             errCode = (byte)CharResponse.CHAR_CREATE_NAME_IN_USE;
//         }

//         // DONE: Do the rename
//         if (errCode == (byte)ATLoginFlags.AT_LOGIN_RENAME)
//         {
//             _clusterServiceLocator.WorldCluster.GetCharacterDatabase().Update(string.Format("UPDATE characters SET char_name = \"{1}\", force_restrictions = 0 WHERE char_guid = {0};", guid, name));
//         }

//         // DONE: Send response
//         PacketClass response = new(Opcodes.SMSG_CHAR_RENAME);
//         response.AddInt8(errCode);
//         client.Send(response);
//         response.Dispose();
//         PacketClass argpacket = null;
//         On_CMSG_CHAR_ENUM(argpacket, client);
//     }

//     public void On_CMSG_CHAR_CREATE(PacketClass packet, ClientClass client)
//     {
//         packet.GetInt16();
//         var name = packet.GetString();
//         _clusterServiceLocator.WorldCluster.Log.WriteLine(LogType.DEBUG, "[{0}:{1}] CMSG_CHAR_CREATE [{2}]", client.IP, client.Port, name);
//         var race = packet.GetInt8();
//         var classe = packet.GetInt8();
//         var gender = packet.GetInt8();
//         var skin = packet.GetInt8();
//         var face = packet.GetInt8();
//         var hairStyle = packet.GetInt8();
//         var hairColor = packet.GetInt8();
//         var facialHair = packet.GetInt8();
//         var outfitId = packet.GetInt8();
//         var result = (int)CharResponse.CHAR_CREATE_DISABLED;

//         // Try to pass the packet to one of World Servers
//         try
//         {
//             if (_clusterServiceLocator.WcNetwork.WorldServer.Worlds.ContainsKey(0U))
//             {
//                 result = _clusterServiceLocator.WcNetwork.WorldServer.Worlds[0U].ClientCreateCharacter(client.Account, name, race, classe, gender, skin, face, hairStyle, hairColor, facialHair, outfitId);
//             }
//             else if (_clusterServiceLocator.WcNetwork.WorldServer.Worlds.ContainsKey(1U))
//             {
//                 result = _clusterServiceLocator.WcNetwork.WorldServer.Worlds[1U].ClientCreateCharacter(client.Account, name, race, classe, gender, skin, face, hairStyle, hairColor, facialHair, outfitId);
//             }
//         }
//         catch (Exception ex)
//         {
//             result = (int)CharResponse.CHAR_CREATE_ERROR;
//             _clusterServiceLocator.WorldCluster.Log.WriteLine(LogType.FAILED, "[{0}:{1}] Character creation failed!{2}{3}", client.IP, client.Port, Constants.vbCrLf, ex.ToString());
//         }

//         PacketClass response = new(Opcodes.SMSG_CHAR_CREATE);
//         response.AddInt8((byte)result);
//         client.Send(response);
//     }

//     public void On_CMSG_PLAYER_LOGIN(PacketClass packet, ClientClass client)
//     {
//         packet.GetInt16();               // int16 unknown
//         var guid = packet.GetUInt64();
//         _clusterServiceLocator.WorldCluster.Log.WriteLine(LogType.DEBUG, "[{0}:{1}] CMSG_PLAYER_LOGIN [0x{2:X}]", client.IP, client.Port, guid);
//         if (client.Character is null)
//         {
//             client.Character = new WcHandlerCharacter.CharacterObject(guid, client, _clusterServiceLocator);
//         }
//         else if (client.Character.Guid != guid)
//         {
//             client.Character.Dispose();
//             client.Character = new WcHandlerCharacter.CharacterObject(guid, client, _clusterServiceLocator);
//         }
//         else
//         {
//             client.Character.ReLoad();
//         }

//         if (_clusterServiceLocator.WcNetwork.WorldServer.InstanceCheck(client, client.Character.Map))
//         {
//             client.Character.GetWorld.ClientConnect(client.Index, client.GetClientInfo());
//             client.Character.IsInWorld = true;
//             client.Character.GetWorld.ClientLogin(client.Index, client.Character.Guid);
//             client.Character.OnLogin();
//         }
//         else
//         {
//             _clusterServiceLocator.WorldCluster.Log.WriteLine(LogType.FAILED, "[{0:000000}] Unable to login: WORLD SERVER DOWN", client.Index);
//             client.Character.Dispose();
//             client.Character = null;
//             PacketClass r = new(Opcodes.SMSG_CHARACTER_LOGIN_FAILED);
//             try
//             {
//                 r.AddInt8((byte)CharResponse.CHAR_LOGIN_NO_WORLD);
//                 client.Send(r);
//             }
//             catch (Exception ex)
//             {
//                 _clusterServiceLocator.WorldCluster.Log.WriteLine(LogType.FAILED, "[{0:000000}] Unable to login: {1}", client.Index, ex.ToString());
//                 client.Character.Dispose();
//                 client.Character = null;
//                 PacketClass a = new(Opcodes.SMSG_CHARACTER_LOGIN_FAILED);
//                 try
//                 {
//                     a.AddInt8((byte)CharResponse.CHAR_LOGIN_FAILED);
//                     client.Send(a);
//                 }
//                 finally
//                 {
//                     r.Dispose();
//                 }
//             }
//         }
//     }

//     // Leak is with in this code. Needs a rewrite to correct the leak. This only effects the CPU Usage.
//     // Happens when the client disconnects from the server.
//     public void On_CMSG_PLAYER_LOGOUT(PacketClass packet, ClientClass client)
//     {
//         _clusterServiceLocator.WorldCluster.Log.WriteLine(LogType.DEBUG, "[{0}:{1}] CMSG_PLAYER_LOGOUT", client.IP, client.Port);
//         client.Character.OnLogout();
//         client.Character.GetWorld.ClientDisconnect(client.Index); // Likely the cause of it
//         client.Character.Dispose();
//         client.Character = null;
//     }

//     public void On_MSG_MOVE_WORLDPORT_ACK(PacketClass packet, ClientClass client)
//     {
//         _clusterServiceLocator.WorldCluster.Log.WriteLine(LogType.DEBUG, "[{0}:{1}] MSG_MOVE_WORLDPORT_ACK", client.IP, client.Port);
//         try
//         {
//             if (!_clusterServiceLocator.WcNetwork.WorldServer.InstanceCheck(client, client.Character.Map))
//             {
//                 return;
//             }

//             if (client.Character.IsInWorld)
//             {
//                 // Inside server transfer
//                 client.Character.GetWorld.ClientLogin(client.Index, client.Character.Guid);
//             }
//             else
//             {
//                 // Inter-server transfer
//                 client.Character.ReLoad();
//                 client.Character.GetWorld.ClientConnect(client.Index, client.GetClientInfo());
//                 client.Character.IsInWorld = true;
//                 client.Character.GetWorld.ClientLogin(client.Index, client.Character.Guid);
//             }
//         }
//         catch (Exception ex)
//         {
//             _clusterServiceLocator.WorldCluster.Log.WriteLine(LogType.CRITICAL, "{0}", ex.ToString());
//         }
//     }
// }
