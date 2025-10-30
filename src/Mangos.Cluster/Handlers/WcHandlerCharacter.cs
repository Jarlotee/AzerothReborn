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
using Mangos.Cluster.Handlers.Guild;
using Mangos.Cluster.Network;
using Mangos.Common.Enums.Chat;
using Mangos.Common.Enums.Global;
using Mangos.Common.Enums.Group;
using Mangos.Common.Enums.Guild;
using Mangos.Common.Enums.Misc;
using Mangos.Common.Enums.Player;
using Mangos.Common.Enums.Social;
using Mangos.Common.Globals;
using Mangos.Common.Legacy;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Mangos.Cluster.Handlers;

public class WcHandlerCharacter
{
    private readonly ClusterServiceLocator _clusterServiceLocator;

    public WcHandlerCharacter(ClusterServiceLocator clusterServiceLocator)
    {
        _clusterServiceLocator = clusterServiceLocator;
    }

    public ulong GetCharacterGuidByName(string name)
    {
        var guid = 0UL;
        _clusterServiceLocator.WorldCluster.CharacteRsLock.AcquireReaderLock(_clusterServiceLocator.GlobalConstants.DEFAULT_LOCK_TIMEOUT);
        foreach (var objCharacter in _clusterServiceLocator.WorldCluster.CharacteRs)
        {
            if (_clusterServiceLocator.CommonFunctions.UppercaseFirstLetter(objCharacter.Value.Name) == _clusterServiceLocator.CommonFunctions.UppercaseFirstLetter(name))
            {
                guid = objCharacter.Value.Guid;
                break;
            }
        }

        _clusterServiceLocator.WorldCluster.CharacteRsLock.ReleaseReaderLock();
        if (guid == 0m)
        {
            DataTable q = new();
            _clusterServiceLocator.WorldCluster.GetCharacterDatabase().Query(string.Format("SELECT char_guid FROM characters WHERE char_name = \"{0}\";", _clusterServiceLocator.Functions.EscapeString(name)), ref q);
            return q.Rows.Count > 0 ? q.Rows[0].As<ulong>("char_guid") : 0UL;
        }

        return guid;
    }

    public string GetCharacterNameByGuid(string guid)
    {
        if (_clusterServiceLocator.WorldCluster.CharacteRs.ContainsKey(Conversions.ToULong(guid)))
        {
            return _clusterServiceLocator.WorldCluster.CharacteRs[Conversions.ToULong(guid)].Name;
        }

        DataTable q = new();
        _clusterServiceLocator.WorldCluster.GetCharacterDatabase().Query(string.Format("SELECT char_name FROM characters WHERE char_guid = \"{0}\";", guid), ref q);
        return q.Rows.Count > 0 ? q.Rows[0].As<string>("char_name") : "";
    }
}
