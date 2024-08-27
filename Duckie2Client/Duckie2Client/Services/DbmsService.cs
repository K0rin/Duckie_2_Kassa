using Duckie2Client.Libs.DatabaseManager;
using Common = Duckie2Client.Libs.DatabaseManager.Common;

namespace Duckie2Client.Services;

public class DbmsService : BaseDbmsService
{
    // todo: the data goes from the settings.
    protected override string ServiceName => "MSSQL$SQLEXPRESS";

    // todo: the data goes from the settings.
    protected override string ServerName => "DESKTOP-H1O55SG\\SQLEXPRESS";

    // todo: the data goes from the settings.
    protected override string DatabaseName => "CarWash";
}