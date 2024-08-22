using System.Collections.Generic;
using System.Linq;
using Duckie2Client.Libs.Enums;

namespace Duckie2Client.Libs;

public enum AppModes
{
    Console,
    Kassa
}

// ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
public class ArgsParser(string[] args, string option)
{
    private const int VALID_ARGS_NUMBER = 2;
    private readonly string[] _modeNames = ["console", "kassa"];

    public AppModes CurrentAppMode { get; private set; }

    public void CheckArguments()
    {
        // Check the valid number of args.
        if (args.Length != VALID_ARGS_NUMBER)
            throw new DuckieException(ErrorCodes.ArgumentInvalidNumber);

        // Check if first argument is option.
        if (!args[0].StartsWith("--")) throw new DuckieException(ErrorCodes.ArgumentsHaveNoOption);

        // Check if option has valid name.
        if (!args[0].Equals($"--{option}")) throw new DuckieException(ErrorCodes.OptionInvalidName);

        // Check if a mode name is valid.
        var isNameValid = _modeNames.Contains(args[1]);
        if (!isNameValid) throw new DuckieException(ErrorCodes.InvalidAppMode);

        // Set current application mode.
        SetAppMode();
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
}