using System;

namespace Duckie2Client.Services.DbmsService.Records;

public abstract class RecordBase
{
    /// <summary>
    /// The identifier of the record.
    /// </summary>
    public Guid Id { get; set; }
}