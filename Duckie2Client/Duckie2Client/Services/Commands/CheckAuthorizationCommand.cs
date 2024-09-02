using System;
using System.Collections.Generic;
using Duckie2Client.Libs.DatabaseManager;
using Duckie2Client.Libs.Enums;

namespace Duckie2Client.Services.Commands;

public class CheckAuthorizationCommand : BaseDuckieCommand
{
    private string UserId { get; }
    private string UserPassword { get; }

    // ReSharper disable once ConvertToPrimaryConstructor
    public CheckAuthorizationCommand(string userId, string userPassword)
    {
        UserPassword = userPassword;
        UserId = userId;
    }

    public override void Execute()
    {
        throw new NotImplementedException();
    }

    public override void Execute(out bool result)
    {
        // DbmsService.Clients.Add();


        // var notifyMessage = Localization.GetString(
        // () => UserInterface.DatabaseConnectionCheck, ResourceTypes.UserInterface);
        const string NOTIFY_MESSAGE = "User authorizing...";
        Notify(NOTIFY_MESSAGE);

        var dbConnection = new DbmsService.DbmsService().GetDatabaseConnection();

        var parameters = new Dictionary<string, object>
        {
            { StoredProcedureParameters.Name.Name(), UserId },
            { StoredProcedureParameters.Password.Name(), UserPassword }
        };
        var sqlAuthorizeCommand = StoredProcedure.ReturnValueProcedure(
            StoredProcedures.AuthorizeUser.GetName(),
            StoredProcedures.AuthorizeUser.Parameters(parameters), dbConnection);

        using (dbConnection)
        {
            sqlAuthorizeCommand.ExecuteNonQuery();
        }

        result = sqlAuthorizeCommand.ReturnValue!.Equals(1);
    }


    public override void ExecuteWithResult(out object? result)
    {
        throw new NotImplementedException();
    }
}