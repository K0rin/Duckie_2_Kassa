using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Duckie2Client.Libs.Enums;

public enum StoredProcedures
{
    AuthorizeUser
}

public enum StoredProcedureParameters
{
    Name,
    Password
}

public static class StoredProceduresExtensions

{
    public static string Name(this StoredProcedureParameters sppn)
    {
        var result = sppn switch
        {
            StoredProcedureParameters.Name => "@pName",
            StoredProcedureParameters.Password => "@pPassword",
            _ => throw new ArgumentOutOfRangeException(nameof(sppn), sppn, null)
        };

        return result;
    }

    public static string GetName(this StoredProcedures sp)
    {
        return sp.ToString();
    }

    public static List<SqlParameter>? Parameters(this StoredProcedures sp, Dictionary<string, object> parameterValues)
    {
        List<SqlParameter>? result = null;

        // todo: refact: Хранить спецификации процедур централизованно.

        if (sp.Equals(StoredProcedures.AuthorizeUser))
        {
            result = new List<SqlParameter>
            {
                new(
                    StoredProcedureParameters.Name.Name(),
                    SqlDbType.NVarChar,
                    50),
                new(
                    StoredProcedureParameters.Password.Name(),
                    SqlDbType.NVarChar,
                    50)
            };

            result[0].Direction = ParameterDirection.Input;
            result[1].Direction = ParameterDirection.Input;

            foreach (var param in result) param.Value = parameterValues[param.ParameterName];

            // Add return value parameter.
            result.Add(new SqlParameter(
                Services.DatabaseManager.Common.DefaultReturnValueParameterName,
                SqlDbType.Int, 1));
            result.Last().Direction = ParameterDirection.ReturnValue;
        }

        return result;
    }
}