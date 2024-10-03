using System;

namespace Duckie2Client.Services.DbmsService.Records;

public class PriceType : RecordBase
{
    public string Name { get; set; }
    public Guid Id { get; set; }
}