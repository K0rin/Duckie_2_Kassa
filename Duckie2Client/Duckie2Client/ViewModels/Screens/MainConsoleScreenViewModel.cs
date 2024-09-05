using System.Collections.ObjectModel;
using System.Reactive;
using Duckie2Client.Libs.Tabalonia;
using Duckie2Client.Services.Controls;
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
    [Reactive] public bool IsDashboardVisible { get; set; }

    public MainConsoleScreenViewModel()
    {
        ExitMenuCommand = ReactiveCommand.Create(ExitMenuCommandExecute);
        BatchServiceAddingCommand = ReactiveCommand.Create(BatchServiceAdditionCommandExecute);
        TabCloseCommand = ReactiveCommand.Create<object>(TabCloseCommandExecute);
        IsDashboardVisible = true;
    }

    private void TabCloseCommandExecute(object value)
    {
        var x = ((DragTabItem)value).DataContext;
        x = ((TabItemViewModel)x!).Content;
        ((ITabaloniaTabItemContent)x).OnTabClose("close");
    }


    private void BatchServiceAdditionCommandExecute()
    {
        HideDashboard();
        var t = new TabItemViewModel(
            "Client Card Batch Add (stated)",
            new ClientCardBatchAddScreenView(),
            new TabContext(new ReadyLoadDataState()));
        TabItems.Add(t);
    }

    private void ExitMenuCommandExecute()
    {
        App.ShutdownApplication();
    }

    private void HideDashboard()
    {
        if (IsDashboardVisible) IsDashboardVisible = false;
    }
}