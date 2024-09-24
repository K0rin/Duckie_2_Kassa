using System.Collections.Generic;
using System.Reactive;
using Duckie2Client.ViewModels.Base;
using Duckie2Client.Views.Dialogs;
using ReactiveUI;

namespace Duckie2Client.ViewModels.Screens.ManagerConsole.PersonnelScreen;

public class PersonnelScreenViewModel : ViewModelBase, ITabViewModel<List<string>>
{
    // todo: refact: перенести константу в интерфейс ITabViewModel.
    private const string DIALOG_IDENTIFIER = "PersonnelScreenDialogs";
    public List<string>? DataPayload { get; set; }

    public ReactiveCommand<Unit, Unit> AddUserCommand { get; }

    // todo: DRY
    private ErrorDialog? _errorDialog;

    public PersonnelScreenViewModel()
    {
        AddUserCommand = ReactiveCommand.Create(AddUserExecute);
    }

    private void AddUserExecute()
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