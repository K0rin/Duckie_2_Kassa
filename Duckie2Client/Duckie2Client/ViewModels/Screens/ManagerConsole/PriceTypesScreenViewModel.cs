using System;
using System.Collections.Generic;
using System.Reactive;
using Duckie2Client.Enums.Flags;
using Duckie2Client.Services.DbmsService;
using Duckie2Client.Services.DbmsService.Records.Builders;
using Duckie2Client.ViewModels.Base;
using ReactiveUI;

namespace Duckie2Client.ViewModels.Screens.ManagerConsole;

public class PriceTypesScreenViewModel : ViewModelBase, ITabViewModel<List<string>>
{
    public static object DialogIdentifier => "PriceTypesScreenDialogs";

    public List<string>? DataPayload { get; set; }
    public ReactiveCommand<Unit, Unit> AddPriceTypeCommand { get; }

    public PriceTypesScreenViewModel()
    {
        AddPriceTypeCommand = ReactiveCommand.Create(AddPriceTypeCommandExecute);
    }

    private void AddPriceTypeCommandExecute()
    {
        var newPriceTypeBuilder = new PriceTypeRecordBuilder();

        for (var i = 3 - 1; i >= 0; i--)
        {
            newPriceTypeBuilder.AddName($"P{i + 1}");
            var result = new PriceTypes().Create(newPriceTypeBuilder);
            if (!result.Equals(DataModelOperationResult.RecordNotFound)) continue;
            ShowDataConsistencyError();
            return;
        }
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