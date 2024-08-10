using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Duckie2Client.Services.DatabaseManager;

public static class StoredProcedure
{
    public static CustomSqlCommand ReturnValueProcedure(
        string name,
        List<SqlParameter>? parameters,
        SqlConnection? connection)
    {
        var result = new CustomSqlCommand(name, connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        foreach (var param in parameters!) result.Parameters.Add(param);

        return result;
    }
}