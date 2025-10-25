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

using Dapper;
using Mangos.Configuration;
using Mangos.MySql.Connections;
using MySql.Data.MySqlClient;

namespace Mangos.MySql;

internal sealed class ConnectionFactory
{
    private readonly MangosConfiguration mangosConfiguration;

    public ConnectionFactory(MangosConfiguration mangosConfiguration)
    {
        this.mangosConfiguration = mangosConfiguration;
    }

    public AccountConnection ConnectToAccountDataBase()
    {
        var mySqlConnection = new MySqlConnection(mangosConfiguration.Realm.ConnectionString);
        mySqlConnection.Open();
        EnsureDatabase(mySqlConnection);
        return new AccountConnection(mySqlConnection);
    }

    private void EnsureDatabase(MySqlConnection mySqlConnection)
    {
        var databaseName = mangosConfiguration.Realm.DatabaseName;
        var results = mySqlConnection.ExecuteReader(
            "SHOW DATABASES LIKE @name;",
            new { name = databaseName }
        );

        var exists = results.Read();
        results.Close();

        if (!exists)
        {
            mySqlConnection.Execute($"CREATE DATABASE {databaseName};");

            mySqlConnection.ChangeDatabase(databaseName);

            var realm_script = SqlScripts.ReadEmbeddedResource("Database", "realm.sql");

            mySqlConnection.Execute(realm_script);

#if DEBUG
            var test_accounts_script = SqlScripts.ReadEmbeddedResource("Database", "test-accounts.sql");

            mySqlConnection.Execute(test_accounts_script);
#endif
        }
    }
}
