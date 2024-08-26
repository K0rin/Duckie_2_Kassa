using System.Collections.Generic;
using System.Linq;
using Duckie2Client.Libs.Enums;

namespace Duckie2Client.Libs;

/// <summary>An option passed on the command line to the Application executable.</summary>
public struct StartupOption(string name, List<string> values, bool isValueList)
{
    /// <summary>Option name.</summary>
    public readonly string Name = name;

    /// <summary>The option list of values.</summary>
    public readonly List<string> Values = values;

    /// <summary>Flag: whether the option value is a list (true) or a single value (false).</summary>
    public readonly bool IsList = isValueList;
}

public class ArgsParser
{
    private string[] CurrentArguments { get; }
    private const string OPTION_MARKER = "--";

    private List<StartupOption> ValidArguments { get; }

    // ReSharper disable once ConvertToPrimaryConstructor
    public ArgsParser(string[] currentArguments, ref List<StartupOption> validValidArguments)
    {
        ValidArguments = validValidArguments;
        CurrentArguments = currentArguments;
    }

    public void CheckArgumentValidity()
    {
        NumberOfArguments();
        OptionIsFirst();
        OptionValidName();
        ArgumentValueIsValid();
    }

    private void NumberOfArguments()
    {
        // If there are less than two arguments, then there are not enough arguments.
        if (CurrentArguments.Length < 2) throw new DuckieException(ErrorCodes.ArgumentInvalidNumber);

        if (CurrentArguments.Length > 2)
        {
            // Filter out options whose values are lists.
            var nonListValueOptions = ValidArguments.Where(e => !e.IsList).Select(e => e.Name).ToList();

            for (var i = 0; i < CurrentArguments.Length; i += 2)
                if (nonListValueOptions.Contains(CurrentArguments[i].Replace(OPTION_MARKER, "")) &&
                    !CurrentArguments[i + 2].StartsWith(OPTION_MARKER))
                    throw new DuckieException(ErrorCodes.ArgumentInvalidNumber);
        }
    }

    /// <summary>Determines whether the specified option name is valid.</summary>
    /// <exception cref="DuckieException">The specified option name is not in the collection of valid option
    /// names.</exception>
    private void OptionValidName()
    {
        if (ValidArguments.Any(argument => !CurrentArguments[0].Equals($"{OPTION_MARKER}{argument.Name}")))
            throw new DuckieException(ErrorCodes.OptionInvalidName);
    }

    /// <summary>Determines whether the first argument is an option.</summary>
    /// <exception cref="DuckieException">The first argument is not an option.</exception>
    private void OptionIsFirst()
    {
        if (!CurrentArguments[0].StartsWith(OPTION_MARKER)) throw new DuckieException(ErrorCodes.ArgumentsHaveNoOption);
    }

    /// <summary>Checks each option in the collection of passed arguments for the correctness of the specified
    /// value.</summary>
    /// <exception cref="DuckieException">The specified option value is not correct (not in the list of valid option
    /// values).</exception>
    private void ArgumentValueIsValid()
    {
        for (var i = 0; i < CurrentArguments.Length; i += 2)
        {
            var currentOption = CurrentArguments[i].Substring(2, CurrentArguments[i].Length - 2);
            var currentValue = CurrentArguments[i + 1];
            var validOption = ValidArguments.Find(e => e.Name.Equals(currentOption));
            var isValueValid = validOption.Values.Contains(currentValue);

            if (!isValueValid) throw new DuckieException(ErrorCodes.InvalidOptionValue);
        }
    }
}