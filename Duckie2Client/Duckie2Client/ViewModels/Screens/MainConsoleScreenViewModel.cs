using System.Reactive;
using Duckie2Client.ViewModels.Base;
using ReactiveUI;

namespace Duckie2Client.ViewModels.Screens;

public class MainConsoleScreenViewModel : ViewModelPageBase
{
    public ReactiveCommand<Unit, Unit> ExitMenuCommand { get; }

    public MainConsoleScreenViewModel()
    {
        ExitMenuCommand = ReactiveCommand.Create(ExitMenuCommandExecute);
    }

    private void ExitMenuCommandExecute()
    {
        Services.Common.ExitApplication();
    }
}