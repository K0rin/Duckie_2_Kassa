using System;

namespace Duckie2Client.Services.DbmsService.Records.Builders;

// todo: refact: rename to 'RateRecordBuilder'.
public class RateBuilder : RecordBuilderBase<RateRecord>
{
    public void AddRateValue(decimal value)
    {
        // todo: Take the value from the settings.
        // todo: error: the settings file not found.
        // todo: check: Value in the valid range.

        GetProduct().Value = value;
    }

    public void AddStartDate(DateOnly value)
    {
        // todo: check: valid date

        GetProduct().StartDate = value;
    }

    public void AddEndDate(DateOnly value)
    {
        // todo: check: valid date

        GetProduct().EndDate = value;
    }
}