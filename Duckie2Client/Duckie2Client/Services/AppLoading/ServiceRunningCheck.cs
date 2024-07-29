using System;
using Duckie2Client.Libs;

namespace Duckie2Client.Services.AppLoading;

public class ServiceRunningCheck : LoadingJob
{
    public ServiceRunningCheck(Action<string>? action) : base(action)
    {
        LoadingMessages = new LoadingMessages(
            "Check Server running...",
            "Server is running.", 
            "Server is not running.");
    }

    protected override bool DoTask()
    {
        var db = new Database();
        var status = db.IsDBServcieRun();
        return status;
    }
}