using System.Collections.Generic;
using System.Linq;
using Duckie2Client.Enums.Flags;
using Duckie2Client.Models.Database;
using Duckie2Client.Services.DbmsService.Records;
using Duckie2Client.Services.DbmsService.Records.Builders;

namespace Duckie2Client.Services.DbmsService;

public class TradeUnits : CrudOperationsBase
{
    public override DataModelOperationResult Create<T>(RecordBuilderBase<T> builder)
    {
        using var db = new DbmsService();
        var builtTradeUnit = builder.Build() as TradeUnitRecord;

        // Trade Unit

        var newTradeUnit = new TradeUnit
        {
            IsGood = false,
            Prices = null!,
            Names = null!
        };
        PropertySetter.SetProperties<TradeUnitRecord, TradeUnit>(ref builtTradeUnit!, ref newTradeUnit);

        // Price

        var priceList = builtTradeUnit.Prices.Select(
            p => new Price
            {
                Id = p.Id,
                PriceType = db.PriceTypes.First(pt => pt.Id.Equals(p.PriceType.Id)),
                Value = new Rate
                {
                    Id = p.Value.Id,
                    EndDate = p.Value.EndDate,
                    StartDate = p.Value.StartDate,
                    Value = p.Value.Value
                },
                TradeUnit = [newTradeUnit]
            }
        ).ToList();
        newTradeUnit.Prices = priceList;

        // Localization

        var localizations = builtTradeUnit.Names.Select(
            l => new TradeUnitLocalization
            {
                Id = l.Id,
                Locale = l.Locale,
                Value = l.Value
            }
        ).ToList();
        newTradeUnit.Names = localizations;

        db.TradeUnits.Add(newTradeUnit);
        db.SaveChanges();

        return DataModelOperationResult.Successful;
    }

    public override object Read<TDataModel>(RecordReadFlags readFlags)
    {
        // todo: implement

        // todo: error: empty flags. None - invalid flag

        var isOperatorActive = readFlags.HasFlag(RecordReadFlags.ActiveRecords);
        var isReadAllRecords = readFlags.HasFlag(RecordReadFlags.ActiveRecords) &
                               readFlags.HasFlag(RecordReadFlags.InactiveRecords);

        object result;

        result = new List<TradeUnit>();

        return (List<TDataModel>)result;
    }
}