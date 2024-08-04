using System;
using Duckie2Client.Libs.Enums;

namespace Duckie2Client.Libs;

public class DuckieException : Exception
{
    private readonly ErrorCodes _errorCode;

    public DuckieException(ErrorCodes errorCode)
    {
        _errorCode = errorCode;
        ErrorNumber = (int)errorCode;
        CreateMessage();
    }

    public int ErrorNumber { get; }
    public override string Message => CreateMessage();


    private string CreateMessage()
    {
        var errorName = Enum.GetName(_errorCode);
        var errorResourceName = $"{ErrorNumber}_{errorName}";
        var errorMessage = Localization.GetString(
            errorResourceName,
            ResourceTypes.ErrorMessages);

        return errorMessage;
    }
}