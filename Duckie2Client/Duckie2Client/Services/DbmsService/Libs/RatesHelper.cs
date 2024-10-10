using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Duckie2Client.Models.Database;
using Duckie2Client.Services.DbmsService.Records;

namespace Duckie2Client.Services.DbmsService.Libs;

public static class RatesHelper
{
    private const string RATES_PROPERTY_NAME = "Rates";

    public static List<Rate> CreateRateList<TRecord>(TRecord record) where TRecord : RecordBase
    {
        // Get 'Rates' property value.
        var recordType = record.GetType();
        var rateProperty = recordType.GetRuntimeProperties().First(p => p.Name.Equals(RATES_PROPERTY_NAME));
        var rates = rateProperty.GetValue(record) as List<RateRecord>;

        return rates!.Select(CreateRate).ToList();
    }

    public static Rate CreateRate(RateRecord rateRecord)
    {
        return new Rate
        {
            Id = rateRecord.Id,
            EndDate = rateRecord.EndDate,
            StartDate = rateRecord.StartDate,
            Value = rateRecord.Value
        };
    }
}