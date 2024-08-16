using System;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using Duckie2Client.Libs;
using Duckie2Client.Libs.Enums;
using Duckie2Client.Resources;
using Duckie2Client.Services.AppLoading;

namespace Duckie2Client.Services.Checks;

/// <summary>
///     Checks that the database is available.
/// </summary>
public class DatabaseConnectionCheck
{
    private DatabaseConnectionCheck()
    {
        var message = Localization.GetString(
            () => UserInterface.DatabaseConnectionCheck,
            ResourceTypes.UserInterface);
        // LoadingMessages = new LoadingMessages(message);
    }

    private string _userId;
    private string _userPassword;

    public DatabaseConnectionCheck(params object[] jobParameters) : this()
    {
        _userId = (string)jobParameters[(int)UserCredentials.Login];
        _userPassword = (string)jobParameters[(int)UserCredentials.Password];
    }


    public void DoTask()
    {
        // TODO: Check database connection.
        // throw new DuckieException(ErrorCodes.DbConnectionInvalid);

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

            CheckDatabaseExists(ref dbConnection);
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

    // ReSharper disable once MemberCanBeMadeStatic.Local
    [SuppressMessage("Performance", "CA1822:Mark members as static")]
    private void CheckDatabaseExists(ref SqlConnection? sqlConnection)
    {
        var databases = sqlConnection?.GetSchema("Databases");

        var x = databases.Select("database_name = 'CarWash'").Length;
        if (x == 0) throw new DuckieException(ErrorCodes.TargetDbDoesNotExist);
    }
}