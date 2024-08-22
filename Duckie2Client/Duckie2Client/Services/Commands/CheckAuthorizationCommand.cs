using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using Duckie2Client.Libs.Enums;

namespace Duckie2Client.Services.Commands;

public class CheckAuthorizationCommand : AbstractDuckieCommand
{
    private string UserId { get; set; }
    private string UserPassword { get; set; }

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
        // var notifyMessage = Localization.GetString(
        // () => UserInterface.DatabaseConnectionCheck, ResourceTypes.UserInterface);
        const string NOTIFY_MESSAGE = "User authorizing...";
        Notify(NOTIFY_MESSAGE);

        var names = DatabaseManager.DbmsService.GetServerDatabaseNames();
        var (serverName, initialCatalog) = names;
        var dbConnection = DatabaseManager.DbmsService.GetDatabaseConnection(serverName, initialCatalog);

        var parameters = new Dictionary<string, object>
        {
            { StoredProcedureParameters.Name.Name(), UserId },
            { StoredProcedureParameters.Password.Name(), UserPassword }
        };
        var sqlAuthorizeCommand = DatabaseManager.StoredProcedure.ReturnValueProcedure(
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