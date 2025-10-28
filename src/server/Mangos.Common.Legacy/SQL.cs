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

using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Data;
using System.Threading;

namespace Mangos.Common.Legacy;

public class SQL : IDisposable
{
    internal MySqlConnection _mySqlConn;

    public enum EMessages
    {
        ID_Error = 0,
        ID_Message = 1
    }

    public enum ReturnState
    {
        Success = 0,
        MinorError = 1,
        FatalError = 2
    }

    public event SQLMessageEventHandler SQLMessage;

    public delegate void SQLMessageEventHandler(EMessages MessageID, string OutBuf);

    private readonly string _connectionString;
    internal readonly string _database;

    public SQL(string connectionString, string database)
    {
        _connectionString = connectionString;
        _database = database;
    }

    public string DBName()
    {
        return _database;
    }

    internal virtual void EnsureDatabase()
    {

    }

    [Description("Start up the SQL connection.")]
    public int Connect()
    {
        try
        {
            _mySqlConn = new MySqlConnection(_connectionString);
            _mySqlConn.Open();
            EnsureDatabase();
            _mySqlConn.ChangeDatabase(_database);
            SQLMessage?.Invoke(EMessages.ID_Message, $"MySQL Connection Opened Successfully [{_mySqlConn.DataSource}]");
        }
        catch (MySqlException e)
        {
            SQLMessage?.Invoke(EMessages.ID_Error, "MySQL Connection Error [" + e.Message + "]");
            return (int)ReturnState.FatalError;
        }

        return (int)ReturnState.Success;
    }

    [Description("Restart the SQL connection.")]
    public void Restart()
    {
        try
        {
            _mySqlConn.Close();
            _mySqlConn.Dispose();
            _mySqlConn = new MySqlConnection(_connectionString);
            _mySqlConn.Open();
            _mySqlConn.ChangeDatabase(_database);
            if (_mySqlConn.State == ConnectionState.Open)
            {
                SQLMessage?.Invoke(EMessages.ID_Message, "MySQL Connection restarted!");
            }
            else
            {
                SQLMessage?.Invoke(EMessages.ID_Error, "Unable to restart MySQL connection.");
            }
        }
        catch (MySqlException e)
        {
            SQLMessage?.Invoke(EMessages.ID_Error, "MySQL Connection Error [" + e.Message + "]");
        }
    }

    private bool _disposedValue; // To detect redundant calls

    // IDisposable
    [Description("Close file and dispose the wdb reader.")]
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            // TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            // TODO: set large fields to null.
            _mySqlConn.Close();
            _mySqlConn.Dispose();
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

    private string mQuery = "";
    private DataTable mResult;

    [Description("SQLQuery. EG.: (SELECT * FROM db_accounts WHERE account = 'name';')")]
    public bool QuerySQL(string SQLQuery)
    {
        mQuery = SQLQuery;
        Query(mQuery, ref mResult);
        if (mResult.Rows.Count > 0)
        {
            // Table gathered
            return true;
        }

        // Table dosent exist
        return false;
    }

    [Description("SQLGet. Used after the query to get a section value")]
    public string GetSQL(string TableSection)
    {
        return mResult.Rows[0][TableSection].ToString();
    }

    public DataTable GetDataTableSQL()
    {
        return mResult;
    }

    [Description("SQLInsert. EG.: (INSERT INTO db_textpage (pageid, text, nextpageid, wdbversion, checksum) VALUES ('pageid DWORD', 'pagetext STRING', 'nextpage DWORD', 'version DWORD', 'checksum DWORD')")]
    public void InsertSQL(string SQLInsertionQuery)
    {
        Insert(SQLInsertionQuery);
    }

    [Description("SQLUpdate. EG.: (UPDATE db_textpage SET pagetext='pagetextstring' WHERE pageid = 'pageiddword';")]
    public void UpdateSQL(string SQLUpdateQuery)
    {
        Update(SQLUpdateQuery);
    }

    public int Query(string sqlquery, ref DataTable Result)
    {
        if (_mySqlConn.State != ConnectionState.Open)
        {
            Restart();
            if (_mySqlConn.State != ConnectionState.Open)
            {
                SQLMessage?.Invoke(EMessages.ID_Error, "MySQL Database Request Failed!");
                return (int)ReturnState.MinorError;
            }
        }

        var ExitCode = (int)ReturnState.Success;
        try
        {
            Monitor.Enter(_mySqlConn);
            MySqlCommand MySQLCommand = new(sqlquery, _mySqlConn);
            MySqlDataAdapter MySQLAdapter = new(MySQLCommand);
            if (Result is null)
            {
                Result = new DataTable();
            }
            else
            {
                Result.Clear();
            }

            MySQLAdapter.Fill(Result);
        }
        catch (MySqlException e)
        {
            SQLMessage?.Invoke(EMessages.ID_Error, "Error Reading From MySQL Database " + e.Message);
            SQLMessage?.Invoke(EMessages.ID_Error, "Query string was: " + sqlquery);
            ExitCode = (int)ReturnState.FatalError;
        }
        finally
        {
            Monitor.Exit(_mySqlConn);
        }

        return ExitCode;
    }

    public void Insert(string sqlquery)
    {
        if (_mySqlConn.State != ConnectionState.Open)
        {
            Restart();
            if (_mySqlConn.State != ConnectionState.Open)
            {
                SQLMessage?.Invoke(EMessages.ID_Error, "MySQL Database Request Failed!");
                return;
            }
        }

        try
        {
            Monitor.Enter(_mySqlConn);
            var MySQLTransaction = _mySqlConn.BeginTransaction();
            MySqlCommand MySQLCommand = new(sqlquery, _mySqlConn, MySQLTransaction);
            MySQLCommand.ExecuteNonQuery();
            MySQLTransaction.Commit();
            Console.WriteLine("transaction completed");
        }
        catch (MySqlException e)
        {
            SQLMessage?.Invoke(EMessages.ID_Error, "Error Reading From MySQL Database " + e.Message);
            SQLMessage?.Invoke(EMessages.ID_Error, "Insert string was: " + sqlquery);
        }
        finally
        {
            Monitor.Exit(_mySqlConn);
        }
    }

    // TODO: Apply proper implementation as needed
    public int TableInsert(string tablename, string dbField1, string dbField1Value, string dbField2, int dbField2Value)
    {
        MySqlCommand cmd = new("", _mySqlConn);
        cmd.Connection.Open();
        cmd.CommandText = "insert into `" + tablename + "`(`" + dbField1 + "`,`" + dbField2 + "`) " + "VALUES (@field1value, @field2value)";
        cmd.Parameters.AddWithValue("@field1value", dbField1Value);
        cmd.Parameters.AddWithValue("@field2value", dbField2Value);
        try
        {
            cmd.ExecuteScalar();
            cmd.Connection.Close();
            return 0;
        }
        catch (Exception)
        {
            cmd.Connection.Close();
            return -1;
        }
    }

    // TODO: Apply proper implementation as needed
    public DataSet TableSelect(string tablename, string returnfields, string dbField1, string dbField1Value)
    {
        MySqlCommand cmd = new("", _mySqlConn);
        cmd.Connection.Open();
        cmd.CommandText = "select " + returnfields + " FROM `" + tablename + "` WHERE `" + dbField1 + "` = '@dbField1value';";
        cmd.Parameters.AddWithValue("@dbfield1value", dbField1Value);
        try
        {
            MySqlDataAdapter adapter = new();
            DataSet myDataset = new();
            adapter.SelectCommand = cmd;
            adapter.Fill(myDataset);
            cmd.ExecuteScalar();
            cmd.Connection.Close();
            return myDataset;
        }
        catch (Exception)
        {
            cmd.Connection.Close();
            return null;
        }
    }

    public void Update(string sqlquery)
    {
        if (_mySqlConn.State != ConnectionState.Open)
        {
            Restart();
            if (_mySqlConn.State != ConnectionState.Open)
            {
                SQLMessage?.Invoke(EMessages.ID_Error, "MySQL Database Request Failed!");
                return;
            }
        }

        try
        {
            Monitor.Enter(_mySqlConn);
            MySqlCommand MySQLCommand = new(sqlquery, _mySqlConn);
            MySqlDataAdapter MySQLAdapter = new(MySQLCommand);
            DataTable result = new();
            MySQLAdapter.Fill(result);
        }
        catch (MySqlException e)
        {
            SQLMessage?.Invoke(EMessages.ID_Error, "Error Reading From MySQL Database " + e.Message);
            SQLMessage?.Invoke(EMessages.ID_Error, "Update string was: " + sqlquery);
        }
        finally
        {
            Monitor.Exit(_mySqlConn);
        }
    }
}

public class CharacterSql : SQL
{
    public CharacterSql(string connectionString, string database) : base(connectionString, database) { }

    internal override void EnsureDatabase()
    {
        var show_command = _mySqlConn.CreateCommand();
        show_command.CommandText = "SHOW DATABASES LIKE @name;";
        show_command.Parameters.AddWithValue("name", _database);
        var results = show_command.ExecuteReader();

        var exists = results.Read();
        results.Close();

        if (!exists)
        {
            var create_command = _mySqlConn.CreateCommand();
            create_command.CommandText = $"CREATE DATABASE {_database};";
            create_command.ExecuteNonQuery();

            _mySqlConn.ChangeDatabase(_database);

            var init_command = _mySqlConn.CreateCommand();
            init_command.CommandText = SqlScripts.ReadEmbeddedResource("Database", "character.sql");
            init_command.ExecuteNonQuery();
        }
    }
}

public class WorldSql : SQL
{
    public WorldSql(string connectionString, string database) : base(connectionString, database) { }

    internal override void EnsureDatabase()
    {
        var show_command = _mySqlConn.CreateCommand();
        show_command.CommandText = "SHOW DATABASES LIKE @name;";
        show_command.Parameters.AddWithValue("name", _database);
        var results = show_command.ExecuteReader();

        var exists = results.Read();
        results.Close();

        if (!exists)
        {
            var create_command = _mySqlConn.CreateCommand();
            create_command.CommandText = $"CREATE DATABASE {_database};";
            create_command.ExecuteNonQuery();

            _mySqlConn.ChangeDatabase(_database);

            var init_command = _mySqlConn.CreateCommand();
            init_command.CommandText = SqlScripts.ReadEmbeddedResource("Database", "world.sql");
            init_command.ExecuteNonQuery();

            var data_command = _mySqlConn.CreateCommand();
            data_command.CommandText = SqlScripts.ReadEmbeddedResource("Database", "world.data.sql");
            data_command.ExecuteNonQuery();
        }
    }
}
