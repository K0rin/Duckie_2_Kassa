using System;
using Duckie2Client.Libs;
using Duckie2Client.Libs.Enums;
using Duckie2Client.Resources;

namespace Duckie2Client.Services.Commands;

/// <summary>Class to wrap the database connection check functionality.</summary>
public class CheckDatabaseConnectionCommand : BaseDuckieCommand
{
    public override void Execute()
    {
        var notifyMessage = Localization.GetString(
            () => UserInterface.DatabaseConnectionCheck, ResourceTypes.UserInterface);
        Notify(notifyMessage);

        var dbConnection = new DbmsService().GetDatabaseConnection();
        dbConnection.Close();
    }

    public override void Execute(out bool result)
    {
        throw new NotImplementedException();
    }

    public override void ExecuteWithResult(out object? result)
    {
        throw new NotImplementedException();
    }
}