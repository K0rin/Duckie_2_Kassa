using System;

namespace Duckie2Client.Services.DbmsService.Records.Builders;

public class PriceRecordBuilder : RecordBuilderBase<PriceRecord>
{
    public void AddTradeUnit(TradeUnitRecordBuilder value)
    {
        GetProduct().TradeUnit = value.Build();
    }

    public void AddValue(RateBuilder value)
    {
        GetProduct().Value = value.Build();
    }

    public void AddPriceType(Guid existingPriceTypeId)
    {
        var priceType = new PriceTypeRecord
        {
            Id = existingPriceTypeId
        };
        GetProduct().PriceType = priceType;
    }
}