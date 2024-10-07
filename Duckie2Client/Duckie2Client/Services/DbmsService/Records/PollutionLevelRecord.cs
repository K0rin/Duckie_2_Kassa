using System.Collections.Generic;

namespace Duckie2Client.Services.DbmsService.Records;

public class PollutionLevelRecord : RecordBase
{
    public string Name { get; set; }
    public List<RateRecord> Rates { get; set; }
}