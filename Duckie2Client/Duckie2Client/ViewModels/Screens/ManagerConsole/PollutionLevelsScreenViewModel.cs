using System;
using System.Collections.Generic;
using System.Reactive;
using Duckie2Client.Enums.Flags;
using Duckie2Client.Services.DbmsService;
using Duckie2Client.Services.DbmsService.Records.Builders;
using Duckie2Client.ViewModels.Base;
using ReactiveUI;

namespace Duckie2Client.ViewModels.Screens.ManagerConsole;

public class PollutionLevelsScreenViewModel : ViewModelBase, ITabViewModel<List<string>>
{
    public static object DialogIdentifier => "PollutionLevelsScreenDialogs";

    public List<string>? DataPayload { get; set; }
    public ReactiveCommand<Unit, Unit> AddPollutionLevelCommand { get; }

    public PollutionLevelsScreenViewModel()
    {
        AddPollutionLevelCommand = ReactiveCommand.Create(AddPollutionLevelCommandExecute);
    }

    private void AddPollutionLevelCommandExecute()
    {
        var newPollutionLevel = new PollutionRecordBuilder();
        newPollutionLevel.AddName("Слабое");

        var rateRecordBuilder = new RateBuilder();
        var todayDateOnly = DateOnly.FromDateTime(DateTime.Today);
        rateRecordBuilder.AddRateValue(1);
        rateRecordBuilder.AddStartDate(todayDateOnly);
        rateRecordBuilder.AddEndDate(todayDateOnly.AddDays(1));
        newPollutionLevel.AddRate([rateRecordBuilder]);

        var result = new PollutionLevels().Create(newPollutionLevel);
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

    // todo: refact: Часть интерфейса. Можно не реализовывать, если не надо.
    public void OnScreenClose()
    {
        // todo: Запрос на сохранение не сохраненных данных.
        // Console.WriteLine(@"batch service close");
    }
}