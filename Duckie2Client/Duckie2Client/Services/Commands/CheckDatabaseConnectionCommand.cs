using System;
using System.Data.SqlClient;
using System.Threading;
using Duckie2Client.Libs;
using Duckie2Client.Libs.Enums;
using Duckie2Client.Resources;

namespace Duckie2Client.Services.Commands;

/// <summary>
/// Class to wrap the database connection check functionality.
/// </summary>
public class CheckDatabaseConnectionCommand : AbstractDuckieCommand
{
    public override void Execute()
    {
        var notifyMessage = Localization.GetString(
            () => UserInterface.DatabaseConnectionCheck, ResourceTypes.UserInterface);
        Notify(notifyMessage);

        // TODO: Check database connection.
        // throw new DuckieException(ErrorCodes.DbConnectionInvalid);

        // TODO: SETTINGS: which type of credential is using for access to SQLServer (Windows, SQLServer).
        var currentCredentials = CredentialTypes.Windows;
        // TODO: Data goes from the config file.
        var serverName = "DESKTOP-H1O55SG\\SQLEXPRESS";
        var initialCatalog = "CarWash";

        SqlConnection? dbConnection = null;

        try
        {
            var dbService = currentCredentials.GetDatabaseService([serverName, initialCatalog]);

            dbConnection = dbService?.GetSqlConnection();
            // TODO: open errors
            dbConnection?.Open();
            CheckDatabaseExists(ref dbConnection);
        }
        catch (SqlException e)
        {
            // TODO: throw DuckieException with params: sql error message
            throw;
        }
        finally
        {
            dbConnection?.Close();


            // for (var i = 0; i < 100; i++)
            // {
            // NotifyByTextMessage($"status {i}");
            // Thread.Sleep(500);
            // }
        }
    }

    // TODO: REFACT: Move to DbmsService.cs
    private static void CheckDatabaseExists(ref SqlConnection? sqlConnection)
    {
        // todo: erorr: no schema "databases'.
        var databases = sqlConnection?.GetSchema("Databases"); // todo: refact: string const.
        var x = databases.Select("database_name = 'CarWash'").Length; // todo: refact: string const.
        if (x == 0) throw new DuckieException(ErrorCodes.TargetDbDoesNotExist);
    }
}