using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using Duckie2Client.Libs;
using Duckie2Client.Libs.Enums;
using Duckie2Client.Resources;

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
        // TODO: create localization string.
        // var notifyMessage = Localization.GetString(
        // () => UserInterface.DatabaseConnectionCheck, ResourceTypes.UserInterface);
        var notifyMessage = "User authorizing...";
        Notify(notifyMessage);


        // ---- duplication code

        // todo: settings: which type of credential is using for access to SQLServer (Windows, SQLServer).
        var currentCredentials = CredentialTypes.Windows;

        // todo: Data goes from the config file.
        var serverName = "DESKTOP-H1O55SG\\SQLEXPRESS";
        var initialCatalog = "CarWash";

        SqlConnection? dbConnection = null;

        // ---- 

        try
        {
            // ---- duplication code
            var dbService = currentCredentials.GetDatabaseService([serverName, initialCatalog]);
            dbConnection = dbService?.GetSqlConnection();
            dbConnection?.Open();
            // ---- 

            var parameters = new Dictionary<string, object>
            {
                { StoredProcedureParameters.Name.Name(), UserId },
                { StoredProcedureParameters.Password.Name(), UserPassword }
            };
            var sqlAuthorizeCommand = DatabaseManager.StoredProcedure.ReturnValueProcedure(
                StoredProcedures.AuthorizeUser.GetName(),
                StoredProcedures.AuthorizeUser.Parameters(parameters), dbConnection);

            sqlAuthorizeCommand.ExecuteNonQuery();

            if (sqlAuthorizeCommand.ReturnValue!.Equals(0)) throw new DuckieException(ErrorCodes.UserHasNoAccessRights);
        }
        catch (SqlException e)
        {
            // todo: throw DuckieException with params: sql error message
            throw;
        }

        finally
        {
            dbConnection?.Close();
        }
    }
}