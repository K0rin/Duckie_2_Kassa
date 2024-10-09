using System;
using System.Collections.Generic;

namespace Duckie2Client.Services.DbmsService.Records.Builders;

public class TradeUnitRecordBuilder : RecordBuilderBase<TradeUnitRecord>
{
    public void AddIsGood(bool value)
    {
        GetProduct().IsGood = value;
    }

    public void AddProcessTime(TimeOnly value)
    {
        GetProduct().ProcessTime = value;
    }

    public void AddisIgnoreDiscounts(bool value)
    {
        GetProduct().IsIgnoreDiscounts = value;
    }

    public void AddPrices(List<PriceRecordBuilder> value)
    {
        GetProduct().Prices = [];
        foreach (var price in value) GetProduct().Prices.Add(price.Build());
    }

    public void AddNames(List<TradeUnitLocalizationRecordBuilder> value)
    {
        GetProduct().Names = [];
        foreach (var localization in value) GetProduct().Names.Add(localization.Build());
    }
}