using System;
using Duckie2Client.Libs.Enums;

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
    InvalidAppMode = 104,

    // "Application instance is already ran."
    ApplicationInstanceAlreadyExists = 105,

    // "Cannot create main window. Unknown application mode."
    UnknownAppMode = 106
}

public static class ErrorCodesExtensions
{
    public static string GetErrorMessage(this ErrorCodes e)
    {
        var errorCode = (int)e;
        var errorName = Enum.GetName(e);
        var errorResourceName = $"{errorCode}_{errorName}";
        var errorMessage = Localization.GetString(
            errorResourceName,
            ResourceTypes.ErrorMessages);

        return errorMessage;
    }
}