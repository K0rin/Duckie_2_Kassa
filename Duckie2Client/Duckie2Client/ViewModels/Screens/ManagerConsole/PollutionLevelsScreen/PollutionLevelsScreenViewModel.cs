using System.Collections.Generic;
using System.Reactive;
using Duckie2Client.ViewModels.Base;
using ReactiveUI;

namespace Duckie2Client.ViewModels.Screens.ManagerConsole.PollutionLevelsScreen;

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
        throw new System.NotImplementedException();
    }

    // todo: refact: Часть интерфейса. Можно не реализовывать, если не надо.
    public void OnScreenClose()
    {
        // todo: Запрос на сохранение не сохраненных данных.
        // Console.WriteLine(@"batch service close");
    }
}