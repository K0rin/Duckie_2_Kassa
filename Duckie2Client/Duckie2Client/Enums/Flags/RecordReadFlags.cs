using System;

namespace Duckie2Client.Enums.Flags;

/// <summary>
/// Controls the data to be included in the query.
/// </summary>
[Flags]
public enum RecordReadFlags
{
    None = 0,

    /// <summary>
    /// Fetch only records marked as "active".
    /// </summary>
    ActiveRecords = 1,

    /// <summary>
    /// Fetch only records marked as "deleted".
    /// </summary>
    InactiveRecords = 2
}