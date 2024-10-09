using System;
using System.Collections.Generic;
using System.Reactive;
using DataFaker;
using Duckie2Client.Enums.Flags;
using Duckie2Client.Services.DbmsService;
using Duckie2Client.Services.DbmsService.Records.Builders;
using Duckie2Client.ViewModels.Base;
using ReactiveUI;

namespace Duckie2Client.ViewModels.Screens.ManagerConsole;

public class TradeUnitsScreenViewModel : ViewModelBase, ITabViewModel<List<string>>
{
    public List<string>? DataPayload { get; set; }
    public static object DialogIdentifier => "TradeUnitsScreenDialogs";
    public ReactiveCommand<Unit, Unit> AddTradeUnitCommand { get; }

    public TradeUnitsScreenViewModel()
    {
        AddTradeUnitCommand = ReactiveCommand.Create(AddTradeUnitCommandExecute);
    }

    private void AddTradeUnitCommandExecute()
    {
        // Trade Unit

        var newTradeUnitRecordBuilder = new TradeUnitRecordBuilder();
        newTradeUnitRecordBuilder.AddIsGood(false);
        newTradeUnitRecordBuilder.AddProcessTime(new TimeOnly(0, 45));
        newTradeUnitRecordBuilder.AddisIgnoreDiscounts(false);

        // Price

        List<PriceRecordBuilder> newTradeUnitPrices = [];
        var todayDateOnly = DateOnly.FromDateTime(DateTime.Today);

        var priceTypeIds = new List<Guid>
        {
            Guid.Parse("678AEE9B-718F-4406-D8DF-08DCE74F0DC9"),
            Guid.Parse("5F2DE0D9-687F-4185-D8DE-08DCE74F0DC9"),
            Guid.Parse("2157123E-AC6B-40B3-B00A-707A7305FFBD")
        };
        var priceRateValues = new Main().GetSortedSummaList(3);

        for (var i = 3 - 1; i >= 0; i--)
        {
            var newPriceRecordBuilder = new PriceRecordBuilder();
            newPriceRecordBuilder.AddTradeUnit(newTradeUnitRecordBuilder);

            var tradeUnitPriceRate = new RateBuilder();
            tradeUnitPriceRate.AddRateValue(priceRateValues[i]);
            tradeUnitPriceRate.AddStartDate(todayDateOnly);
            tradeUnitPriceRate.AddEndDate(todayDateOnly.AddDays(1));

            newPriceRecordBuilder.AddValue(tradeUnitPriceRate);
            newPriceRecordBuilder.AddPriceType(priceTypeIds[i]);

            newTradeUnitPrices.Add(newPriceRecordBuilder);
        }

        newTradeUnitRecordBuilder.AddPrices(newTradeUnitPrices);

        // Localization

        var newTradeUnitLocalizations = new List<TradeUnitLocalizationRecordBuilder>();

        // todo: settings: Choose a locale from the list.

        var servicesDict = new Dictionary<string, string>
        {
            { "ru", "service1-ru" },
            { "en", "service1-en" }
        };

        foreach (var keyValuePair in servicesDict)
        {
            var newTradeUnitLocalizationRecordBuilder = new TradeUnitLocalizationRecordBuilder();
            newTradeUnitLocalizationRecordBuilder.AddLocale(keyValuePair.Key);
            newTradeUnitLocalizationRecordBuilder.AddValue(keyValuePair.Value);
            newTradeUnitLocalizations.Add(newTradeUnitLocalizationRecordBuilder);
        }

        newTradeUnitRecordBuilder.AddNames(newTradeUnitLocalizations);

        var result = new TradeUnits().Create(newTradeUnitRecordBuilder);
        if (result.Equals(DataModelOperationResult.RecordNotFound)) ShowDataConsistencyError();
    }

    private void ShowDataConsistencyError()
    {
        // todo: show this message in a dialog.

        const string MSG =
            "DUCKIE_EXCEPTION: Нарушение целостности записей в базе данных.\n" +
            "Запрашиваемая запись не найдена в базе, но предполагается, что она должна существовать .\n" +
            "Необходима проверка базы данных.\n" +
            "Дальнейшая работа с программой может увеличит несогласованность данных.";
        throw new Exception(MSG);
    }
}