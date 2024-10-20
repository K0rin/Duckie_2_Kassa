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
using DynamicData.Kernel;
using Avalonia.Controls;
using Duckie2Client.Controls.Kassa;
using System;
using System.Collections;
using Duckie2Client.Services.DbmsService;
using System.IO;
using Duckie2Client.Models;
using Duckie2Client.Services.DbmsService.Records;
using System.Linq;
using Avalonia.Controls.Primitives;
using Microsoft.IdentityModel.Tokens;
using Avalonia;

namespace Duckie2Client.ViewModels.Screens;

public class MainKassaScreenViewModel : ViewModelPageBase
{
    #region Commands

    //public ReactiveCommand<Unit, Unit> BatchServiceAddingCommand { get; }
    public ReactiveCommand<Unit, Unit> ExitMenuCommand { get; }
    //public ReactiveCommand<Unit, Unit> PersonnelCommand { get; }
    public ReactiveCommand<Unit, Unit> ShowOrdersScreen { get; }
    public ReactiveCommand<Unit, Unit> ShowWashesScreen { get; }
    public ReactiveCommand<Unit, Unit> NewUserAuthorization { get; }
    public ReactiveCommand<Unit, Unit> CheckoutAndExit { get; }
    public ReactiveCommand<Unit, Unit> SearchClientPhone { get; }
    public ReactiveCommand<string, Unit> NewClientScreen { get; }
    public ReactiveCommand<string, Unit> ShowClientsConnectedWithVehicle { get; }
    //public ReactiveCommand<Unit, Unit> AddOperatorButton { get; }
    //public ReactiveCommand<object, Unit> TabCloseCommand { get; }

    // Correct constructor with parameters

    #endregion

    private List<LoginUserRecord?> _loggedInUsers = [];

    private List<VehicleRecord?> _vehicle = [];


    public ObservableCollection<TabItemViewModel> TabItems { get; } = [];


    [Reactive] public bool IsDashboardVisible { get; set; }

    [Reactive] public string CarNumber { get; set; }
    [Reactive] public string ClientPhone { get; set; }
    [Reactive] public string ClientType { get; set; }




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
        ShowClientsConnectedWithVehicle = ReactiveCommand.Create<string>(VehicleRoute);
        SearchClientPhone = ReactiveCommand.Create(SearchClientPhoneExecute);
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
        CurrentPage = new VehicleScreen();
    }


    private void NewClientScreenExecute(string parameter)
    {
        CurrentPage = new NewClient();
    }

    private void SearchClientPhoneExecute() 
    {
        if (string.IsNullOrWhiteSpace(ClientPhone)) return;
        CommunicationMeansRecords searchPhone = CommunicationMeans.FindCommunicationClient(ClientPhone);
        if (searchPhone == null) 
        {
            CurrentPage = new NewClient();
        }
    }

    private void VehicleRoute(string parameter)
    {
        if (string.IsNullOrWhiteSpace(CarNumber)) return;
        ClientType = parameter;
        CurrentPage = Vehicles.FindVehicle(CarNumber) == null
            ? new ClientPhoneSearch()
            : new ClientConnectedWithVehicle();
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
                popup.IsOpen = !popup.IsOpen;
            };
            textBlock2.Tapped += (sender, args) => 
            {
                var userId = textBlock2.Name.ToString();
                LogoutOperator(userId);
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

    private void LogoutOperator(string userId) 
    {
        var guid = Guid.Parse(userId);
        var foundUser = _loggedInUsers.FirstOrDefault(u => u?.Id == guid);
        _loggedInUsers.Remove(foundUser);
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


}