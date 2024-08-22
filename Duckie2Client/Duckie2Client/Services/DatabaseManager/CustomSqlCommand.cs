using System.Data;
using System.Data.SqlClient;

namespace Duckie2Client.Services.DatabaseManager;

public class CustomSqlCommand
{
    private readonly SqlCommand _sqlCommand;

    // ReSharper disable once ConvertToPrimaryConstructor
    public CustomSqlCommand(string name, SqlConnection? connection)
    {
        _sqlCommand = new SqlCommand(name, connection);
    }

    public CommandType CommandType
    {
        get => _sqlCommand.CommandType;
        set => _sqlCommand.CommandType = value;
    }

    public SqlParameterCollection Parameters => _sqlCommand.Parameters;

    public object? ReturnValue => _sqlCommand.Parameters[Common.DefaultReturnValueParameterName].Value;

    public int ExecuteNonQuery()
    {
        return _sqlCommand.ExecuteNonQuery();
    }
}