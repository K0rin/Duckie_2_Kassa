using System.Linq;
using Duckie2Client.Libs.ErrorCollection;

namespace Duckie2Client.Libs;

public class ArgsParser
{
    private readonly string[] _args;
    private readonly string _option;
    private const int ValidArgsNumber = 2;
    private readonly string[] _modeNames = ["console", "kassa"];

    public ArgsParser(string[] args, string option)
    {
        _args = args;
        _option = option;
    }

    public ErrorCodes? CheckArgs()
    {
        // Check valid number of args.
        if (_args.Length != ValidArgsNumber)
        {
            return ErrorCodes.ArgumentInvalidNumber;
        }

        // Check if first argument is option.
        if (!_args[0].StartsWith("--"))
        {
            return ErrorCodes.ArgumentsHaveNoOption;
        }

        // Check if option has valid name.
        if (!_args[0].Equals($"--{_option}"))
        {
            return ErrorCodes.OptionInvalidName;
        }

        // Check if a mode name is valid.

        var isNameValid = _modeNames.Contains(_args[1]);
        if (!isNameValid)
        {
            
            return ErrorCodes.InvalidAppMode;
                // _errMessages["ERR04"].Replace("{*}", _args[1]));
        }

        return null;
    }
}