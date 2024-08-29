using System;
using Duckie2Client.Libs;
using Duckie2Client.Libs.DatabaseManager;
using Duckie2Client.Libs.Enums;
using Duckie2Client.Resources;

namespace Duckie2Client.Services.AppLoading;

/// <summary>Checks that SQL Server service is running.</summary>
public class ServiceRunningCheck : LoadingJob
{
    public ServiceRunningCheck(Action<string>? action) : base(action)
    {
        LoadingMessages = new LoadingMessages(
            Localization.GetString(() => UserInterface.DBMSServiceRunCheck, ResourceTypes.UserInterface));
    }

    protected override void DoTask()
    {
        new DbmsService.DbmsService().IsDbServiceRun();
    }
}