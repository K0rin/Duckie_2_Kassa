using System;

namespace Duckie2Client.Enums.Flags;

[Flags]
public enum DataModelOperationResult
{
    Successful,
    RecordNotFound
}