using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Duckie2Client.Libs;
using Duckie2Client.Libs.Enums;
using Duckie2Client.Resources;
using Duckie2Client.Services.AppLoading;
using StoredProcedures = Duckie2Client.Libs.Enums.StoredProcedures;

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

            var sqlAuthorizeCommand =
                DatabaseManager.StoredProcedure.ReturnValueProcedure(
                    StoredProcedures.AuthorizeUser.GetName(),
                    StoredProcedures.AuthorizeUser.Parameters(
                        new Dictionary<string, object>
                        {
                            {
                                StoredProcedureParameters.Name.Name(),
                                _userId
                            },
                            {
                                StoredProcedureParameters.Password.Name(),
                                _userPassword
                            }
                        }),
                    dbConnection);

            sqlAuthorizeCommand.ExecuteNonQuery();

            if (sqlAuthorizeCommand.ReturnValue!.Equals(0))
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