using System;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.ServiceProcess;
using Duckie2Client.Libs.Enums;

namespace Duckie2Client.Libs.DatabaseManager;

// NOTE: For this moment, there is only SQL Server support.

public abstract class BaseDbmsService
{
    /// <summary>The DBMS service name. Must be overriden in an inheriting class.</summary>
    protected abstract string? ServiceName { get; }

    /// <summary>The DBMS server name. Must be overriden in an inheriting class.</summary>
    protected abstract string? ServerName { get; }

    /// <summary>The database name (initial catalog). Must be overriden in an inheriting class.</summary>
    protected abstract string? DatabaseName { get; }

    public void IsDbServiceRun()
    {
        PlatformSpecific.RunMethod(WindowsType, LinuxType);
    }

    /// <summary>Checks whether the database server is running on the Windows platform.</summary>
    /// <exception cref="ArgumentOutOfRangeException">The status of the database server is unknown.</exception>
    /// <exception cref="DuckieException">The database server is not available.</exception>
    [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
    private void WindowsType()
    {
        var sc = new ServiceController(ServiceName!);

        if (sc.Status == ServiceControllerStatus.Running) return;

        try
        {
            var throwError = sc.Status switch
            {
                ServiceControllerStatus.Stopped or ServiceControllerStatus.StopPending => ErrorCodes.DbServiceStopped,
                ServiceControllerStatus.Paused or ServiceControllerStatus.PausePending => ErrorCodes.DbServicePaused,
                /*
                 * todo: Timeout?
                 * Maybe need to wait some time and service will be available.
                 */
                ServiceControllerStatus.ContinuePending => ErrorCodes.DbServiceUnavailable,
                _ => throw new ArgumentOutOfRangeException($"Unknown service status: {sc.Status.ToString()}")
            };
            throw new DuckieException(throwError);
        }
        catch (InvalidOperationException)
        {
            throw new DuckieException(ErrorCodes.DbServiceUnavailable);
        }
    }

    /// <summary>Checks whether the database server is running on the Windows platform.</summary>
    /// <exception cref="NotImplementedException"></exception>
    private static void LinuxType()
    {
        throw new NotImplementedException();
    }

    /// <summary>Checks if a database with the specified name exists on the database server.</summary>
    /// <param name="sqlConnection">A database connection object.</param>
    /// <param name="databaseName">A database name.</param>
    /// <returns>
    /// <list type="bullet">
    /// <item>True - the database exists.</item>
    /// <item>False - the database does not exist.</item>
    /// </list>
    /// </returns>
    private static bool CheckDatabaseExists(ref SqlConnection? sqlConnection, string databaseName)
    {
        const string SCHEMA_DIRECTORY_NAME = "Databases";
        var databases = sqlConnection?.GetSchema(SCHEMA_DIRECTORY_NAME);
        var sqlQuery = $"database_name = '{databaseName}'";
        var recordNumber = databases!.Select(sqlQuery).Length;

        return recordNumber != 0;
    }

    /// <summary>Returns an open database connection object.</summary>
    /// <exception cref="Exception"></exception>
    public SqlConnection GetDatabaseConnection()
    {
        // todo: check empty values for ServerName, DatabaseName 

        const CredentialTypes CURRENT_CREDENTIALS = CredentialTypes.Windows;

        SqlConnection? dbConnection = null;
        var isDatabaseExists = false;

        try
        {
            var dbService = CURRENT_CREDENTIALS.GetDatabaseService([ServerName!, DatabaseName!]);
            dbConnection = dbService.GetSqlConnection();
            dbConnection.Open();

            isDatabaseExists = CheckDatabaseExists(ref dbConnection, DatabaseName!);

            if (!isDatabaseExists)
                throw new Exception("The database does not exist.");

            return dbConnection;
        }
        finally
        {
            if (!isDatabaseExists) dbConnection?.Close();
        }
    }
}