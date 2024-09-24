using System;

namespace Duckie2Client.Services.DbmsService.Records;

public class RateRecord : RecordBase
{
    public int Value { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}