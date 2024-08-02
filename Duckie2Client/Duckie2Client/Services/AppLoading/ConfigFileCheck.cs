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
        // TODO: Check config file.
        // Todo: Call methods of the Config class.

        /*
        - Is file exists?
          - Create file with default structure and values if it does not exist.

        - Valid JSON format of the file.
          - If the file has invalid structure, show error message to user on
            the splash screen.
        */

        var config = new DuckieConfig();


        return true;
    }
}