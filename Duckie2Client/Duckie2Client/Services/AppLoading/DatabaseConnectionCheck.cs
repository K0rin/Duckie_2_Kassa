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
        var message = Localization.GetString(
            () => UserInterface.DatabaseConnectionCheck,
            ResourceTypes.UserInterface);
        LoadingMessages = new LoadingMessages(message);
    }

    protected override void DoTask()
    {
        // TODO: Check database connection.
        // throw new DuckieException(ErrorCodes.DbConnectionInvalid);
    }
}