using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Duckie2Client.Services.DatabaseManager;

public static class StoredProcedure
{
    public static bool Execute(SqlCommand command)
    {
        return true;
    }

    public static SqlCommand? SqlReturnCommand(
        string name,
        List<SqlParameter> parameters,
        SqlConnection? connection)
    {
        var result = new SqlCommand(name, connection);
        foreach (var parameter in parameters) result.Parameters.Add(parameter);

        return result;
    }
}