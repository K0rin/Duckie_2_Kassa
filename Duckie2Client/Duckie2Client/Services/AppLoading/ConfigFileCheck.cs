using System;

namespace Duckie2Client.Services.AppLoading;

public class ConfigFileCheck : LoadingJob
{
    public ConfigFileCheck(Action<string>? action) : base(action)
    {
        LoadingMessages = new LoadingMessages(
            "Config file checking...",
            "Config file is valid.",
            "Config file is invalid.");
    }

    protected override bool DoTask()
    {
        var config = new DuckieConfig();


        return true;
    }
}