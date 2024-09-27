using System;

namespace Duckie2Client.Enums;

[Flags]
public enum RecordReadFlags
{
    None = 0,
    ActiveRecords = 1,
    InactiveRecords = 2
}