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
using BranchesScreenDataLoadingState =
    Duckie2Client.ViewModels.Screens.ManagerConsole.BranchesScreen.DataLoadingState;
using PollutionLevelsScreenDataLoadingState =
    Duckie2Client.ViewModels.Screens.ManagerConsole.PollutionLevelsScreen.DataLoadingState;
using PriceTypesScreenDataLoadingState =
    Duckie2Client.ViewModels.Screens.ManagerConsole.PriceTypesScreen.DataLoadingState;
using ClientCardBatchAddScreenDataLoadingState =
    Duckie2Client.ViewModels.Screens.ManagerConsole.ClientCardBatchAddScreen.DataLoadingState;
using ClientsScreenDataLoadingState =
    Duckie2Client.ViewModels.Screens.ManagerConsole.ClientsScreen.DataLoadingState;

namespace Duckie2Client.ViewModels.Screens;

/// <summary>
/// Controller to process events on the main screen of the Application in the “Manager Console” mode of operation.
/// </summary>
public class MainConsoleScreenViewModel : ViewModelPageBase
{
    #region Commands

    public ReactiveCommand<Unit, Unit> BatchServiceAddingCommand { get; }
    public ReactiveCommand<Unit, Unit> ExitMenuCommand { get; }
    public ReactiveCommand<Unit, Unit> PersonnelListCommand { get; }
    public ReactiveCommand<Unit, Unit> BranchesListCommand { get; }

    public ReactiveCommand<Unit, Unit> PriceTypesListCommand { get; }
    public ReactiveCommand<Unit, Unit> PollutionLevelsListCommand { get; }
    public ReactiveCommand<object, Unit> TabCloseCommand { get; }

    #endregion

    public ObservableCollection<TabItemViewModel> TabItems { get; } = [];
    [Reactive] public bool IsDashboardVisible { get; set; }
    public ReactiveCommand<Unit, Unit> ClientListCommand { get; }

    public MainConsoleScreenViewModel()
    {
        BatchServiceAddingCommand = ReactiveCommand.Create(BatchServiceAdditionCommandExecute);
        ExitMenuCommand = ReactiveCommand.Create(ExitMenuCommandExecute);
        PersonnelListCommand = ReactiveCommand.Create(PersonnelCommandExecute);
        BranchesListCommand = ReactiveCommand.Create(BranchesListCommandExecute);
        TabCloseCommand = ReactiveCommand.Create<object>(TabCloseCommandExecute);
        PriceTypesListCommand = ReactiveCommand.Create(PriceTypesListCommandExecute);
        PollutionLevelsListCommand = ReactiveCommand.Create(PollutionLevelsListCommandExecute);
        ClientListCommand = ReactiveCommand.Create(ClientListCommandExecute);

        IsDashboardVisible = true;
    }

    private void ClientListCommandExecute()
    {
        AddTabItem(
            "Pollution Levels List",
            new ClientsScreenView(),
            new ClientsScreenDataLoadingState(),
            "Для загрузки списка клиентов нажмите кнопку 'Обновить'.",
            "Загружается список клиентов..."
        );
    }

    private void PollutionLevelsListCommandExecute()
    {
        AddTabItem(
            "Pollution Levels List",
            new PollutionLevelsScreenView(),
            new PollutionLevelsScreenDataLoadingState(),
            "Для загрузки списка уровней загрязнения нажмите кнопку 'Обновить'.",
            "Загружается список уровней загрязнения..."
        );
    }

    private void PriceTypesListCommandExecute()
    {
        AddTabItem(
            "Price Types List",
            new PriceTypesScreenView(),
            new PriceTypesScreenDataLoadingState(),
            "Для загрузки списка типов цен нажмите кнопку 'Обновить'.",
            "Загружается список типов цен..."
        );
    }

    private void BranchesListCommandExecute()
    {
        AddTabItem(
            "Branch List",
            new BranchesScreenView(),
            new BranchesScreenDataLoadingState(),
            "Для загрузки списка филиалов нажмите кнопку 'Обновить'.",
            "Загружается список филиалов..."
        );
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

    private static void ExitMenuCommandExecute()
    {
        App.ShutdownApplication();
    }

    private void HideDashboard()
    {
        if (IsDashboardVisible) IsDashboardVisible = false;
    }
}