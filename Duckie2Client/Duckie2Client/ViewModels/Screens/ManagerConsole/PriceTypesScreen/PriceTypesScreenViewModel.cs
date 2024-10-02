using System.Collections.Generic;
using System.Reactive;
using Duckie2Client.ViewModels.Base;
using ReactiveUI;

namespace Duckie2Client.ViewModels.Screens.ManagerConsole.PriceTypesScreen;

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
        throw new System.NotImplementedException();
    }

    // todo: refact: Часть интерфейса. Можно не реализовывать, если не надо.
    public void OnScreenClose()
    {
        // todo: Запрос на сохранение не сохраненных данных.
        // Console.WriteLine(@"batch service close");
    }
}