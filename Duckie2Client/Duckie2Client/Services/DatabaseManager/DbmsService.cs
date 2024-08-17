using System;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.ServiceProcess;
using Duckie2Client.Libs;
using Duckie2Client.Libs.Enums;

namespace Duckie2Client.Services.DatabaseManager;

public static class DbmsService
{
    public static void IsDbServiceRun()
    {
        PlatformSpecific.RunMethod(WindowsType, LinuxType);
    }

    [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
    private static void WindowsType()
    {
        // ReSharper disable once StringLiteralTypo
        const string serviceName = "MSSQL$SQLEXPRESS";
        var sc = new ServiceController(serviceName);

        if (sc.Status == ServiceControllerStatus.Running) return;

        try
        {
            var throwError = sc.Status switch
            {
                ServiceControllerStatus.Stopped
                    or ServiceControllerStatus.StopPending =>
                    ErrorCodes.DbServiceStopped,
                ServiceControllerStatus.Paused
                    or ServiceControllerStatus.PausePending =>
                    ErrorCodes.DbServicePaused,
                /*
                 * todo: Timeout?
                 * Maybe need to wait some time and service will be available.
                 */
                ServiceControllerStatus.ContinuePending =>
                    ErrorCodes.DbServiceUnavailable,
                _ => throw new ArgumentOutOfRangeException(
                    $"Unknown service status: {sc.Status.ToString()}")
            };
            throw new DuckieException(throwError);
        }
        catch (InvalidOperationException)
        {
            throw new DuckieException(ErrorCodes.DbServiceUnavailable);
        }
    }

    private static void LinuxType()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Checks if a database with the specified name exists on the database server.
    /// </summary>
    /// <param name="sqlConnection">A database connection object.</param>
    /// <param name="databaseName">A database name.</param>
    /// <returns>
    /// <list type="bullet">
    /// <item>True - the database exists.</item>
    /// <item>False - the database does not exist.</item>
    /// </list>
    /// </returns>
    public static bool CheckDatabaseExists(ref SqlConnection? sqlConnection, string databaseName)
    {
        // todo: error: no schema "databases'.
        // todo: error: databaseName is empty or null
        const string schemaDirectoryName = "Databases";
        var databases = sqlConnection?.GetSchema(schemaDirectoryName);
        var sqlQuery = $"database_name = '{databaseName}'";
        var recordNumber = databases.Select(sqlQuery).Length;

        return recordNumber != 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="serverName"></param>
    /// <param name="databaseName"></param>
    /// <returns>Opened database connection.</returns>
    /// <exception cref="Exception"></exception>
    public static SqlConnection GetDatabaseConnection(string serverName, string databaseName)
    {
        // TODO: SETTINGS: which type of credential is using for access to SQLServer (Windows, SQLServer).
        const CredentialTypes currentCredentials = CredentialTypes.Windows;

        SqlConnection? dbConnection = null;
        var isDatabaseExists = false;

        try
        {
            var dbService = currentCredentials.GetDatabaseService([serverName, databaseName]);
            dbConnection = dbService?.GetSqlConnection();
            // TODO: open errors
            dbConnection?.Open();

            isDatabaseExists = CheckDatabaseExists(ref dbConnection, databaseName);

            if (!isDatabaseExists)
                // todo: create DuckieException - DatabaseNotExist
                throw new Exception("The database does not exist.");

            return dbConnection;
        }
        finally
        {
            if (!isDatabaseExists) dbConnection?.Close();
        }
    }

    public static Common.ServerDatabaseNames GetServerDatabaseNames()
    {
        var output = new Common.ServerDatabaseNames
        {
            // todo: strings go from settings.
            ServerName = "DESKTOP-H1O55SG\\SQLEXPRESS",
            DatabaseName = "CarWash"
        };
        return output;
    }
}