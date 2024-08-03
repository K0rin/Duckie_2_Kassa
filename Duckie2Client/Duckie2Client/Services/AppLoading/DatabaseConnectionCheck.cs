using System;
using Duckie2Client.Libs;
using Duckie2Client.Libs.Enums;
using Duckie2Client.Resources;

namespace Duckie2Client.Services.AppLoading;

/// <summary>
///     Checks that the database is available.
/// </summary>
public class DatabaseConnectionCheck : LoadingJob
{
    public DatabaseConnectionCheck(Action<string>? action) : base(action)
    {
        LoadingMessages = new LoadingMessages(
            Localization.GetString(
                () => UserInterface.DatabaseConnectionCheck,
                ResourceTypes.UserInterface),
            Localization.GetString(
                () => UserInterface.DatabaseConnectionValid,
                ResourceTypes.UserInterface),
            // todo: error message
            "Database connection error.");
    }

    protected override bool DoTask()
    {
        // TODO: Check database connection.
        return true;
    }
}