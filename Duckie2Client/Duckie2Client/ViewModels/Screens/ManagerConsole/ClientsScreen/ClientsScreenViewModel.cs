using System.Collections.Generic;
using System.Reactive;
using Duckie2Client.ViewModels.Base;
using ReactiveUI;

namespace Duckie2Client.ViewModels.Screens.ManagerConsole.ClientsScreen;

public class ClientsScreenViewModel : ViewModelBase, ITabViewModel<List<string>>
{
    public static object DialogIdentifier => "ClientsScreenDialogs";
    public List<string>? DataPayload { get; set; }
    public ReactiveCommand<Unit, Unit> AddClientCommand { get; }

    public ClientsScreenViewModel()
    {
        AddClientCommand = ReactiveCommand.Create(AddClientCommandExecute);
    }

    private void AddClientCommandExecute()
    {
        throw new System.NotImplementedException();
    }
}