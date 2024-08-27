using System.Collections.ObjectModel;
using System.Reactive;
using Duckie2Client.Libs.Tabalonia;
using Duckie2Client.ViewModels.Base;
using Duckie2Client.Views.Screens.ManagerConsole;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Tabalonia.Controls;

namespace Duckie2Client.ViewModels.Screens;

/// <summary>
/// Controller to process events on the main screen of the Application in the “Manager Console” mode of operation.
/// </summary>
public class MainConsoleScreenViewModel : ViewModelPageBase
{
    #region Commands

    public ReactiveCommand<Unit, Unit> ExitMenuCommand { get; }
    public ReactiveCommand<Unit, Unit> BatchServiceAddingCommand { get; }
    public ReactiveCommand<object, Unit> TabCloseCommand { get; }

    #endregion

    public ObservableCollection<TabItemViewModel> TabItems { get; } = [];
    [Reactive] public bool IsStartupVisible { get; set; }

    public MainConsoleScreenViewModel()
    {
        ExitMenuCommand = ReactiveCommand.Create(ExitMenuCommandExecute);
        BatchServiceAddingCommand = ReactiveCommand.Create(BatchServiceAdditionCommandExecute);
        TabCloseCommand = ReactiveCommand.Create<object>(TabCloseCommandExecute);
        IsStartupVisible = true;
    }

    private void TabCloseCommandExecute(object value)
    {
        var x = ((DragTabItem)value).DataContext;
        x = ((TabItemViewModel)x!).Content;
        ((ITabaloniaTabItemContent)x).OnTabClose("close");
    }


    private void BatchServiceAdditionCommandExecute()
    {
        HideStartupControl();

        TabItems.Add(new TabItemViewModel("Batch Service Adding", new BatchServiceAddingScreenView()));
    }

    private void ExitMenuCommandExecute()
    {
        App.ShutdownApplication();
    }

    private void HideStartupControl()
    {
        if (IsStartupVisible) IsStartupVisible = false;
    }
}