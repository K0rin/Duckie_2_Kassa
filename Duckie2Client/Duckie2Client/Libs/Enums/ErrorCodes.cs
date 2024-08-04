namespace Duckie2Client.Libs.Enums;

public enum ErrorCodes
{
    // Program executable
    ArgumentInvalidNumber = 101,
    ArgumentsHaveNoOption = 102,
    OptionInvalidName = 103,
    InvalidAppMode = 104,
    ApplicationInstanceAlreadyExists = 105,
    UnknownAppMode = 106,

    // Database
    DbConnectionInvalid = 201,
    DbServiceUnavailable = 202
}

// public static class ErrorCodesExtensions
// {
//     public static string GetErrorMessage(this ErrorCodes e)
//     {
//         var errorCode = (int)e;
//         var errorName = Enum.GetName(e);
//         var errorResourceName = $"{errorCode}_{errorName}";
//         var errorMessage = Localization.GetString(
//             errorResourceName,
//             ResourceTypes.ErrorMessages);
//
//         return errorMessage;
//     }
// }