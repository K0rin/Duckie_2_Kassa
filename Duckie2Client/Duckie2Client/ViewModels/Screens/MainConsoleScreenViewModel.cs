using System.Collections.ObjectModel;
using System.Reactive;
using Duckie2Client.Enums;
using Duckie2Client.Libs.Tabalonia;
using Duckie2Client.Services.Controls;
using Duckie2Client.ViewModels.Base;
using Duckie2Client.ViewModels.Screens.ManagerConsole.DataLoadingStates;
using Duckie2Client.Views.Base;
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
    internal object SwitchContentPanel;
    #region Commands

    public ReactiveCommand<Unit, Unit> ExitMenuCommand { get; set; }
    public ReactiveCommand<object, Unit> TabCloseCommand { get; set; }
    public ReactiveCommand<ManagerConsoleTabs, Unit> OpenTabCommand { get; set; }

    #endregion

    public ObservableCollection<TabItemViewModel> TabItems { get; } = [];
    [Reactive] public bool IsDashboardVisible { get; set; }

    [Reactive] public string DataLoadingTabStateName { get; set; }
    

    public MainConsoleScreenViewModel()
    {
        InitializeCommands();
        IsDashboardVisible = true;
    }


    private void InitializeCommands()
    {
        OpenTabCommand = ReactiveCommand.Create<ManagerConsoleTabs>(OpenTabCommandExecute);
        ExitMenuCommand = ReactiveCommand.Create(ExitMenuCommandExecute);
        TabCloseCommand = ReactiveCommand.Create<object>(TabCloseCommandExecute);
    }

    private void BranchesCommandExecute(ManagerConsoleTabs tabEnumValue)
    {
        AddTabItem(
            "Branches List",
            new BranchesScreenView(),
            new BranchesScreenViewModelTabState(),
            "Для загрузки списка персонала нажмите кнопку 'Обновить'.",
            "Загружается список персонала..."
        );
    }

    private void OpenTabCommandExecute(ManagerConsoleTabs tabEnumValue)
    {
        // todo: check for type of tabEnumValue

        var tabRecord = tabEnumValue.GetTabRecord();
        DataLoadingTabStateName = tabRecord.State.GetType().Name;
            
        
        
        AddTab(tabEnumValue.GetTitle(), tabEnumValue.GetView(), tabRecord);
    }

    private void AddTab(string title, TabUserControlView view, TabStateRecord tabStateRecord)
    {
        AddTabItem(
            title,
            view,
            tabStateRecord.State,
            tabStateRecord.ReadyStateMessage,
            tabStateRecord.DataLoadingStateMessage);
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


    private static void ExitMenuCommandExecute()
    {
        App.ShutdownApplication();
    }

    private void HideDashboard()
    {
        if (IsDashboardVisible) IsDashboardVisible = false;
    }
}