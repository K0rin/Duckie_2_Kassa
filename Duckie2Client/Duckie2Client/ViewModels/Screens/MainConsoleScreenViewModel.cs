using System.Collections.ObjectModel;
using System.Reactive;
using Duckie2Client.Libs.Tabalonia;
using Duckie2Client.Services.Controls;
using Duckie2Client.ViewModels.Base;
using Duckie2Client.Views.Base;
using Duckie2Client.Views.Screens.ManagerConsole;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Tabalonia.Controls;
using PersonnelScreenDataLoadingState =
    Duckie2Client.ViewModels.Screens.ManagerConsole.PersonnelScreen.DataLoadingState;
using ClientCardBatchAddScreenDataLoadingState =
    Duckie2Client.ViewModels.Screens.ManagerConsole.ClientCardBatchAddScreen.DataLoadingState;

namespace Duckie2Client.ViewModels.Screens;

/// <summary>
/// Controller to process events on the main screen of the Application in the “Manager Console” mode of operation.
/// </summary>
public class MainConsoleScreenViewModel : ViewModelPageBase
{
    #region Commands

    public ReactiveCommand<Unit, Unit> BatchServiceAddingCommand { get; }
    public ReactiveCommand<Unit, Unit> ExitMenuCommand { get; }
    public ReactiveCommand<Unit, Unit> PersonnelCommand { get; }
    public ReactiveCommand<object, Unit> TabCloseCommand { get; }

    #endregion

    public ObservableCollection<TabItemViewModel> TabItems { get; } = [];
    [Reactive] public bool IsDashboardVisible { get; set; }

    public MainConsoleScreenViewModel()
    {
        BatchServiceAddingCommand = ReactiveCommand.Create(BatchServiceAdditionCommandExecute);
        ExitMenuCommand = ReactiveCommand.Create(ExitMenuCommandExecute);
        PersonnelCommand = ReactiveCommand.Create(PersonnelCommandExecute);
        TabCloseCommand = ReactiveCommand.Create<object>(TabCloseCommandExecute);

        IsDashboardVisible = true;
    }

    private void TabCloseCommandExecute(object value)
    {
        var x = ((DragTabItem)value).DataContext;
        x = ((TabItemViewModel)x!).Content;
        ((ITabaloniaTabItemContent)x).OnTabClose("close");
    }

    /// <summary>
    /// Adds a new tab to the main window of the Manager Console operating mode.
    /// </summary>
    /// <param name="tabName">The name of a new tab.</param>
    /// <param name="tabContent">The content of a new tab.</param>
    /// <param name="loadingState">The loading data state of a new tab.</param>
    /// <param name="readyStateMessage">The message shows after a new tab is showed on the screen.</param>
    /// <param name="dataLoadingMessage">The message shows during the data loading process.</param>
    private void AddTabItem(
        string tabName,
        TabUserControlView tabContent,
        TabState loadingState,
        string readyStateMessage,
        string dataLoadingMessage)
    {
        HideDashboard();

        var tabItem = new TabItemViewModel(
            tabName,
            tabContent,
            new TabContext(new ReadyLoadDataState()),
            loadingState)
        {
            ReadyStateMessageText = readyStateMessage,
            DataLoadingMessageText = dataLoadingMessage
        };
        TabItems.Add(tabItem);
    }

    private void BatchServiceAdditionCommandExecute()
    {
        AddTabItem(
            "Client Card Batch Add (stated)",
            new ClientCardBatchAddScreenView(),
            new ClientCardBatchAddScreenDataLoadingState(),
            "Для загрузки данных нажмите кнопку 'Обновить'.",
            "Загружается список фирм..."
        );
    }

    private void PersonnelCommandExecute()
    {
        AddTabItem(
            "Personnel List",
            new PersonnelScreenView(),
            new PersonnelScreenDataLoadingState(),
            "Для загрузки списка персонала нажмите кнопку 'Обновить'.",
            "Загружается список персонала..."
        );
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