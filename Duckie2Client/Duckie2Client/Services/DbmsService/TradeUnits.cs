using System.Collections.Generic;
using System.Linq;
using Duckie2Client.Enums.Flags;
using Duckie2Client.Models.Database;
using Duckie2Client.Services.DbmsService.Libs;
using Duckie2Client.Services.DbmsService.Records;
using Duckie2Client.Services.DbmsService.Records.Builders;

namespace Duckie2Client.Services.DbmsService;

public class TradeUnits : CrudOperationsBase
{
    public override DataModelOperationResult Create<T>(RecordBuilderBase<T> builder)
    {
        if (builder.Build() is not TradeUnitRecord builtTradeUnit) return DataModelOperationResult.RecordNotFound;

        var newTradeUnit = CreateTradeUnit(builtTradeUnit);

        using var db = new DbmsService();
        newTradeUnit.Prices = CreatePriceList(builtTradeUnit, newTradeUnit, db);
        newTradeUnit.Names = CreateLocalizations(builtTradeUnit);

        return AddAndSave(db.TradeUnits, db, newTradeUnit);
    }

    private static TradeUnit CreateTradeUnit(TradeUnitRecord builtTradeUnit)
    {
        var newTradeUnit = new TradeUnit
        {
            IsGood = false,
            Prices = null!,
            Names = null!
        };
        PropertySetter.SetProperties<TradeUnitRecord, TradeUnit>(ref builtTradeUnit!, ref newTradeUnit);
        return newTradeUnit;
    }

    private List<Price> CreatePriceList(TradeUnitRecord builtTradeUnit, TradeUnit newTradeUnit, DbmsService dbmsService)
    {
        var priceList = new List<Price>();
        // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
        foreach (var p in builtTradeUnit.Prices)
        {
            var rateRecord = p.Value;
            var price = new Price
            {
                Id = p.Id,
                PriceType = dbmsService.PriceTypes.First(pt => pt.Id.Equals(p.PriceType.Id)),
                Value = RatesHelper.CreateRate(rateRecord),
                TradeUnit = [newTradeUnit]
            };
            priceList.Add(price);
        }

        return priceList;
    }

    private static List<TradeUnitLocalization> CreateLocalizations(TradeUnitRecord builtTradeUnit)
    {
        var localizations = new List<TradeUnitLocalization>();
        // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
        foreach (var l in builtTradeUnit.Names)
        {
            var tradeUnitLocalization = new TradeUnitLocalization
            {
                Id = l.Id,
                Locale = l.Locale,
                Value = l.Value
            };
            localizations.Add(tradeUnitLocalization);
        }

        return localizations;
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