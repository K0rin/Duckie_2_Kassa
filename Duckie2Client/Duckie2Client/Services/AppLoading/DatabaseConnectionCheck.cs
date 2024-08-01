using System;

namespace Duckie2Client.Services.AppLoading;

/// <summary>
/// Checks that the database is available.
/// </summary>
public class DatabaseConnectionCheck : LoadingJob
{
    public DatabaseConnectionCheck(Action<string>? action) : base(action)
    {
        LoadingMessages = new LoadingMessages(
            "Database connection...",
            "Database connection successful.",
            "Database connection error.");
    }

    protected override bool DoTask()
    {
        // TODO: Check database connection.
        return true;
    }
}