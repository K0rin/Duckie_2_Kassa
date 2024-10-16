using System.Collections.ObjectModel;
using System.Windows;
using System.Reactive;
using Duckie2Client.Libs.Tabalonia;
using Duckie2Client.Services.Controls;
using Duckie2Client.ViewModels.Base;
using Duckie2Client.Views.Base;
using Duckie2Client.Views.Screens.ManagerConsole;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Tabalonia.Controls;
//using PersonnelScreenDataLoadingState =
//    Duckie2Client.ViewModels.Screens.ManagerConsole.PersonnelScreen.DataLoadingState;
//using ClientCardBatchAddScreenDataLoadingState =
//    Duckie2Client.ViewModels.Screens.ManagerConsole.ClientCardBatchAddScreen.DataLoadingState;
using System.Collections.Generic;
using Avalonia.Controls;
using Duckie2Client.Controls.Kassa;
using System;
using Duckie2Client.Models;
using System.Linq;
using Avalonia.Controls.Primitives;
using Microsoft.IdentityModel.Tokens;
using Avalonia;
//using MessageBox.Avalonia;

namespace Duckie2Client.ViewModels.Screens;

public class MainKassaScreenViewModel : ViewModelPageBase
{
    #region Commands

    //public ReactiveCommand<Unit, Unit> BatchServiceAddingCommand { get; }
    public ReactiveCommand<Unit, Unit> ExitMenuCommand { get; }
    //public ReactiveCommand<Unit, Unit> PersonnelCommand { get; }
    public ReactiveCommand<Unit, Unit> ShowOrdersScreen { get; }
    public ReactiveCommand<string, Unit> ShowClientsConnectedWithTS { get; }
    public ReactiveCommand<string, Unit> NewClientScreen { get; }
    public ReactiveCommand<Unit, Unit> ShowWashesScreen { get; }
    public ReactiveCommand<Unit, Unit> NewUserAuthorization { get; }
    public ReactiveCommand<Unit, Unit> CheckoutAndExit { get; }
    //public ReactiveCommand<Unit, Unit> AddOperatorButton { get; }
    //public ReactiveCommand<object, Unit> TabCloseCommand { get; }

    // Correct constructor with parameters

    #endregion

    private List<LoginUserRecord?> _loggedInUsers = [];
    public ObservableCollection<TabItemViewModel> TabItems { get; } = [];


    [Reactive] public bool IsDashboardVisible { get; set; }

    [Reactive] public bool TransportNomerScreen { get; set; }

    [Reactive] public string CarNumber { get; set; }

    [Reactive] public string TypeOfClient { get; set; }

    //public DockPanel usersPanel {  get; set; }

    //[Reactive] public Panel usersPanel { get; set; }

    //[Reactive] public ArrayList usersIDs { get; set; }

    private object _currentPage;

    public object contentUserButtons = new DockPanel();

    public object DockaPanelUserButtons
    {
        get => contentUserButtons;
        set => this.RaiseAndSetIfChanged(ref contentUserButtons, value);
    }

    public object CurrentPage
    {
        get => _currentPage;
        set => this.RaiseAndSetIfChanged(ref _currentPage, value);
    }

    public MainKassaScreenViewModel()
    {
        ExitMenuCommand = ReactiveCommand.Create(ExitMenuCommandExecute);
        ShowOrdersScreen = ReactiveCommand.Create(ShowOrdersScreenExecute);
        ShowClientsConnectedWithTS = ReactiveCommand.Create<string>(ShowClientsConnectedWithTSExecute);
        CheckoutAndExit = ReactiveCommand.Create(CheckoutAndExitExecute);
        NewUserAuthorization = ReactiveCommand.Create(NewUserAuthorizationExecute);
        ShowWashesScreen = ReactiveCommand.Create(ShowWashesScreenExecute);
        NewClientScreen = ReactiveCommand.Create<string>(NewClientScreenExecute);
    }

    public void AddLoggedInUser(LoginUserRecord? userRecord)
    {
        var foundUser = _loggedInUsers.FirstOrDefault(u => u?.Id == userRecord.Id);
        if (foundUser == null) 
        {
            _loggedInUsers.Add(userRecord);
            AddOperatorButtonExecute(_loggedInUsers);
        }
        
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

    //private void BatchServiceAdditionCommandExecute()
    //{
    //    AddTabItem(
    //        "Client Card Batch Add (stated)",
    //        new ClientCardBatchAddScreenView(),
    //        new ClientCardBatchAddScreenDataLoadingState(),
    //        "Для загрузки данных нажмите кнопку 'Обновить'.",
    //        "Загружается список фирм..."
    //    );
    //}

    //private void PersonnelCommandExecute()
    //{
    //    AddTabItem(
    //        "Personnel List",
    //        new PersonnelScreenView(),
    //        new PersonnelScreenDataLoadingState(),
    //        "Для загрузки списка персонала нажмите кнопку 'Обновить'.",
    //        "Загружается список персонала..."
    //    );
    //}

    private void ExitMenuCommandExecute()
    {
        App.ShutdownApplication();
    }

    private void CheckoutAndExitExecute()
    {
        App.ShutdownApplication();
    }

    private void ShowWashesScreenExecute()
    {
        App.ShutdownApplication();
    }

    private void NewUserAuthorizationExecute()
    {
        PagerViewModel?.SwitchPage(0);
    }

    private void ShowOrdersScreenExecute()
    {
        var panel1View = new TransportScreen();
        CurrentPage = panel1View;
    }

    private void NewClientScreenExecute(string parameter)
    {
        var panel1View = new NewClient();
        CurrentPage = panel1View;
    }

    private void ShowClientsConnectedWithTSExecute(string parameter)
    {
        if (parameter.Equals("private")) 
        {
            if (string.IsNullOrWhiteSpace(CarNumber)) 
            {

            }
            else 
            {
                var panel1View = new ClientConnectedTS();
                TypeOfClient = "private";
                CurrentPage = panel1View;
            }
            
        }
        if (parameter.Equals("firm"))
        {
            if (string.IsNullOrWhiteSpace(CarNumber))
            {

            }
            else
            {
                var panel1View = new ClientConnectedTS();
                TypeOfClient = "firm";
                CurrentPage = panel1View;
            }
        }
        else 
        { 
        
        }
        
    }

    public void AddOperatorButtonExecute(List<LoginUserRecord> users)
    {
        var dockPanel = new DockPanel();
        foreach (LoginUserRecord? user in users) 
        {
            var userButton = new Button();
            var stackPanel = new StackPanel();
            var popup = new Popup();
            var stackPanelList = new StackPanel();
            var textBlock1 = new TextBlock();
            var textBlock2 = new TextBlock();
            textBlock1.Text = "Add to order";
            textBlock2.Text = "Finish job";
            textBlock2.Name = user.Id.ToString();
            textBlock1.Margin = new Thickness(50, 0, 0, 0);
            textBlock2.Margin = new Thickness(50, 0, 0, 0);
            textBlock1.Background = Avalonia.Media.Brushes.Red;
            textBlock2.Background = Avalonia.Media.Brushes.Red;
            userButton.Content = user.Initials;
            popup.IsOpen = false;
            userButton.Click += (sender, args) => 
            {
                if (popup.IsOpen == false)
                {
                    popup.IsOpen = true;
                }
                else 
                {
                    popup.IsOpen = false;
                }
                
            };
            textBlock2.Tapped += (sender, args) => 
            {
                var userId = textBlock2.Name.ToString();
                finishOperator(userId);
                popup.IsOpen = false;
            };
            stackPanel.Children.Add( userButton );
            stackPanel.Children.Add( popup );
            popup.Child = stackPanelList;
            stackPanelList.Children.Add( textBlock1 );
            stackPanelList.Children.Add(textBlock2);
            dockPanel.Children.Add(stackPanel);
        }
        DockaPanelUserButtons = dockPanel;
    }

    private void finishOperator(string userId) 
    {
        var id = userId;
        var guid = Guid.Parse(userId);
        var foundUser = _loggedInUsers.FirstOrDefault(u => u?.Id == guid);
        _loggedInUsers.Remove(foundUser);
        var debug = true;
        if (_loggedInUsers.IsNullOrEmpty() == true) 
        {
            App.ShutdownApplication();
        }
        else 
        {
            AddOperatorButtonExecute(_loggedInUsers);
        }
    }

    private void HideDashboard()
    {
        if (IsDashboardVisible) IsDashboardVisible = false;
    }

    private void HideTransportNomerScreen()
    {
        if (TransportNomerScreen.Equals(true))
            TransportNomerScreen = false;
        else
            TransportNomerScreen = true;
    }
}