using System;
using Duckie2Client.Libs;
using Duckie2Client.Libs.Enums;
using Duckie2Client.Resources;

namespace Duckie2Client.Services.AppLoading;

public class ConfigFileCheck : LoadingJob
{
    public ConfigFileCheck(Action<string>? action) : base(action)
    {
        var message = Localization.GetString(
            () => UserInterface.ConfigFileChecking,
            ResourceTypes.UserInterface);
        LoadingMessages = new LoadingMessages(message);
    }

    protected override void DoTask()
    {
        _ = new DuckieConfig();
    }
}