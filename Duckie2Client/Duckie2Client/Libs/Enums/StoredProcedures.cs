using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Duckie2Client.Libs.Enums;

public enum StoredProcedures
{
    AuthorizeUser
}

public static class StoredProceduresExtensions
{
    public static string GetName(this StoredProcedures sp)
    {
        return sp.ToString();
    }

    public static SqlCommand? GetCommand(this StoredProcedures sp,
        ref SqlConnection? connection,
        ref Dictionary<string, object> commandParameters)
    {
        SqlCommand? result = null;
        if (sp.Equals(StoredProcedures.AuthorizeUser))
        {
            result = Services.DatabaseManager.StoredProcedure.SqlReturnCommand(
                sp.ToString(),
                [
                    new SqlParameter
                    {
                        ParameterName = "@pName",
                        Direction = ParameterDirection.Input,
                        SqlDbType = SqlDbType.NVarChar,
                        Value = commandParameters["@pName"]
                    },
                    new SqlParameter
                    {
                        ParameterName = "@pPassword",
                        Direction = ParameterDirection.Input,
                        SqlDbType = SqlDbType.NVarChar,
                        Value = commandParameters["@pPassword"]
                    },
                    new SqlParameter
                    {
                        ParameterName = "@ReturnValue",
                        Direction = ParameterDirection.ReturnValue
                    }
                ],
                connection
            );
        }

        return result;
    }
    // public static void Execute(this StoredProcedures sp)
    // {
    //     Duckie2Client.Services.DatabaseManager.StoredProcedure.Execute()
    //     
    // }
}