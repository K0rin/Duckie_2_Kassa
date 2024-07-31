using System;

namespace Duckie2Client.Libs.ErrorCollection;

public enum ErrorCodes
{
    // "Invalid number of command line arguments."
    ArgumentInvalidNumber = 101,

    // "There is no option in a command line arguments."
    ArgumentsHaveNoOption = 102,

    // "The option has invalid name."
    OptionInvalidName = 103,

    // "There is no such application working mode: {*}."
    InvalidAppMode = 104
}

public static class Extensions
{
    public static string GetErrorName(this ErrorCodes e)
    {
        // TODO: IMPLEMENT
        throw new NotImplementedException();
    }
}