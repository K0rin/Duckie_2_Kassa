using System;

namespace Duckie2Client.Services.AppLoading;

public class ConfigFileCheck : LoadingJob
{
    public ConfigFileCheck(Action<string>? action) : base(action)
    {
        LoadingMessages = new LoadingMessages(
            "Config file checking...",
            "Config file is valid.",
            "An error occured during checking the config file.");
    }

    protected override bool DoTask()
    {
        try
        {
            _ = new DuckieConfig();
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }
}