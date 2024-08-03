using System.Collections.Generic;
using System.Linq;
using Duckie2Client.Libs.Enums;

namespace Duckie2Client.Libs;

public enum AppModes
{
    Console,
    Kassa
}

public class ArgsParser(string[] args, string option)
{
    private const int ValidArgsNumber = 2;
    private readonly string[] _modeNames = ["console", "kassa"];

    public ErrorCodes? CheckArgs()
    {
        // Check valid number of args.
        if (args.Length != ValidArgsNumber)
            return ErrorCodes.ArgumentInvalidNumber;

        // Check if first argument is option.
        if (!args[0].StartsWith("--")) return ErrorCodes.ArgumentsHaveNoOption;

        // Check if option has valid name.
        if (!args[0].Equals($"--{option}"))
            return ErrorCodes.OptionInvalidName;

        // Check if a mode name is valid.
        var isNameValid = _modeNames.Contains(args[1]);
        if (!isNameValid) return ErrorCodes.InvalidAppMode;
        // _errMessages["ERR04"].Replace("{*}", _args[1]));
        // Set current application mode.
        SetAppMode();

        return null;
    }

    private void SetAppMode()
    {
        var modes = new Dictionary<string, AppModes>
        {
            { "console", AppModes.Console },
            { "kassa", AppModes.Kassa }
        };
        CurrentAppMode = modes[args[1]];
    }


    public AppModes CurrentAppMode { get; private set; }
}