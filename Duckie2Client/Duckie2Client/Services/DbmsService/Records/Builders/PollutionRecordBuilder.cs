using System.Collections.Generic;

namespace Duckie2Client.Services.DbmsService.Records.Builders;

public class PollutionRecordBuilder : RecordBuilderBase<PollutionLevelRecord>
{
    public void AddName(string value)
    {
        GetProduct().Name = value;
    }

    public void AddRate(List<RateBuilder> value)
    {
        GetProduct().Rates = [];
        foreach (var rate in value) GetProduct().Rates.Add(rate.Build());
    }
}