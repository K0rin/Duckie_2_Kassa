using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Duckie2Client.Libs.DatabaseManager;

public static class StoredProcedure
{
    /// <summary>Returns an object of a stored procedure that has parameters, which should return a value.</summary>
    /// <param name="name">The name of the stored procedure in the database.</param>
    /// <param name="parameters">The stored procedure parameters list.</param>
    /// <param name="connection">A database connection object.</param>
    // todo: test
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