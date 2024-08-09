using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Duckie2Client.Libs;
using Duckie2Client.Libs.Enums;
using Duckie2Client.Resources;
using Duckie2Client.Services.AppLoading;

namespace Duckie2Client.Services.Checks;

public class AuthorizationCheck : LoadingJob
{
    private readonly string _userId;
    private readonly string _userPassword;

    private AuthorizationCheck()
    {
        var message = Localization.GetString(
            () => UserInterface.DatabaseConnectionCheck,
            ResourceTypes.UserInterface);
        LoadingMessages = new LoadingMessages(message);
    }

    public AuthorizationCheck(params object[] jobParameters) : this()
    {
        _userId = (string)jobParameters[(int)UserCredentials.Login];
        _userPassword = (string)jobParameters[(int)UserCredentials.Password];
    }

    protected override void DoTask()
    {
        // todo: settings: which type of credential is using for access to SQLServer (Windows, SQLServer).
        var currentCredentials = CredentialTypes.Windows;

        // todo: Data goes from the config file.
        var serverName = "DESKTOP-H1O55SG\\SQLEXPRESS";
        var initialCatalog = "CarWash";

        SqlConnection? dbConnection = null;

        try
        {
            var dbService =
                currentCredentials.GetDatabaseService([
                    serverName,
                    initialCatalog
                ]);

            dbConnection = dbService?.GetSqlConnection();
            // OpenAsync(CancellationToken)
            dbConnection?.Open();

            var sqlCmnd = new SqlCommand(
            StoredProcedures.AuthorizeUser.GetName(),
            dbConnection);


            // var sqlCmnd = DatabaseManager.StoredProcedure.SqlReturnCommand(
            //     StoredProcedures.AuthorizeUser.GetName(),
            //     [
            //         new SqlParameter
            //         {
            //             ParameterName = "@pName",
            //             Direction = ParameterDirection.Input,
            //             SqlDbType = SqlDbType.NVarChar
            //         },
            //         new SqlParameter
            //         {
            //             ParameterName = "@pPassword",
            //             Direction = ParameterDirection.Input,
            //             SqlDbType = SqlDbType.NVarChar
            //         }, 
            //         new SqlParameter
            //         {
            //             ParameterName = "@ReturnValue",
            //             Direction = ParameterDirection.ReturnValue
            //         },  
            //         
            //     ],
            //     dbConnection
            // );

            // sqlCmnd.Parameters["@pName"].Value = _userId;
            // sqlCmnd.Parameters["@pPassword"].Value = _userPassword;
            
            // var commandParameters = new Dictionary<string, object>
            // {
            //     ["@pName"] = _userId,
            //     ["@pPassword"] = _userPassword
            // };
            // var sqlCmnd =
            //     StoredProcedures.AuthorizeUser.GetCommand(
            //         ref dbConnection,
            //         ref commandParameters);


            
            sqlCmnd.CommandType = CommandType.StoredProcedure;
            
            sqlCmnd.Parameters.AddWithValue("@pName", SqlDbType.NVarChar)
                .Value = _userId;
            sqlCmnd.Parameters.AddWithValue("@pPassword", SqlDbType.NVarChar)
                .Value = _userPassword;
            
            var returnValue = new SqlParameter
            {
                ParameterName = "@ReturnValue",
                Direction = ParameterDirection.ReturnValue
            };
            sqlCmnd.Parameters.Add(returnValue);

            sqlCmnd?.ExecuteNonQuery();

            var storedProcedureReturnValue =
                (int)sqlCmnd.Parameters["@ReturnValue"].Value;

            if (storedProcedureReturnValue.Equals(0))
                throw new DuckieException(ErrorCodes.UserHasNoAccessRights);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e);
            throw;
        }
        catch (SqlException e)
        {
            // todo: throw DuckieException with params: sql error message
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            dbConnection?.Close();
        }
    }
}