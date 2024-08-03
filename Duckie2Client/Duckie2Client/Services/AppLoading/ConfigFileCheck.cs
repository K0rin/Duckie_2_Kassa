using System;
using Duckie2Client.Libs;
using Duckie2Client.Libs.Enums;
using Duckie2Client.Resources;

namespace Duckie2Client.Services.AppLoading;

public class ConfigFileCheck : LoadingJob
{
    public ConfigFileCheck(Action<string>? action) : base(action)
    {
        LoadingMessages = new LoadingMessages(
            Localization.GetString(
                () => UserInterface.ConfigFileChecking,
                ResourceTypes.UserInterface),
            Localization.GetString(
                () => UserInterface.ConfigFileValid,
                ResourceTypes.UserInterface),
            // no message cuz will be created a new file in case of fail
            "");
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