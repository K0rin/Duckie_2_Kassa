using System;

namespace Duckie2Client.Services.DbmsService.Records.Builders;

public class SalaryRateBuilder : RecordBuilderBase<RateRecord>
{
    public void AddRateValue(int value)
    {
        GetProduct().Value = value;
    }

    public void AddStartDate(DateOnly value)
    {
        GetProduct().StartDate = value;
    }

    public void AddEndDate(DateOnly value)
    {
        GetProduct().EndDate = value;
    }
}