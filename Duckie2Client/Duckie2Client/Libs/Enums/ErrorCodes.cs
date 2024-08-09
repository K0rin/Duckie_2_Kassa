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
    DbServiceUnavailable = 202,
    DbServiceStopped = 203,
    DbServicePaused = 204,
    TargetDbDoesNotExist = 205,

    // The Application
    UserHasNoAccessRights = 301
}