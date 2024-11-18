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
using Bogus;
using Duckie2Client.Models.Database;
using System.Reactive.Linq;
using Duckie2Client.Services.DbmsService.Records.Builders;
using Microsoft.EntityFrameworkCore;
using Duckie2Client.Views.Screens;
using System.Reflection;
using ExCSS;

namespace Duckie2Client.ViewModels.Screens;

public class MainKassaScreenViewModel : ViewModelPageBase
{
    #region Commands

    //public ReactiveCommand<Unit, Unit> BatchServiceAddingCommand { get; }
    public ReactiveCommand<Unit, Unit> ExitMenuCommand { get; }
    //public ReactiveCommand<Unit, Unit> PersonnelCommand { get; }
    public ReactiveCommand<Unit, Unit> ShowOrdersScreen { get; }
    public ReactiveCommand<Unit, Unit> ShowWashesScreen { get; }
    public ReactiveCommand<Unit, Unit> PageClientPhoneSearch { get; }
    public ReactiveCommand<Unit, Unit> NewUserAuthorization { get; }
    public ReactiveCommand<Unit, Unit> CheckoutAndExit { get; }
    public ReactiveCommand<Unit, Unit> OrderRoute { get; }
    public ReactiveCommand<Unit, Unit> SearchClientPhone { get; }
    public ReactiveCommand<Unit, Unit> ServicesListScreen { get; }
    public ReactiveCommand<Unit, Unit> GoodsListScreen { get; }
    public ReactiveCommand<Unit, Unit> AddItemToShoppingCart { get; }
    public ReactiveCommand<Unit, Unit> FindCompanyScreen { get; }
    public ReactiveCommand<Unit, Unit> FindCompany { get; }
    public ReactiveCommand<Unit, Unit> SaveNewClient { get; }
    public ReactiveCommand<Unit, Unit> ShoppingCartScreen { get; }
    public ReactiveCommand<Unit, Unit> SaveNewCompany { get; }
    public ReactiveCommand<string, Unit> NewClientScreen { get; }
    public ReactiveCommand<Unit, Unit> NewCompanyScreen { get; }
    public ReactiveCommand<string, Unit> ShowClientsConnectedWithVehicle { get; }
    //public ReactiveCommand<Unit, Unit> AddOperatorButton { get; }
    //public ReactiveCommand<object, Unit> TabCloseCommand { get; }

    // Correct constructor with parameters

    #endregion

    private List<LoginUserRecord?> _loggedInUsers = [];

    private VehiclesRecord? FoundVehicle { get; set; }

    public ObservableCollection<TabItemViewModel> TabItems { get; } = [];


    [Reactive] public bool IsDashboardVisible { get; set; }

    [Reactive] public string CarNumber { get; set; }
    
    [Reactive] public string NewClientFirstName { get; set; }
    [Reactive] public string NewCopmanyName { get; set; }
    [Reactive] public string NewCompanyRegister { get; set; }
    [Reactive] public string NewCompanyAdress { get; set; }
    [Reactive] public string NewClientLastName { get; set; }
    [Reactive] public string NewClientEmail { get; set; }
    [Reactive] public string CompanyName { get; set; }
    [Reactive] public string NewClientNotes { get; set; }
    [Reactive] public bool ClientPanelVisibility { get; set; }
    [Reactive] public ObservableCollection<CommunicationClientRecords> VehicleClient { get; set; }
    [Reactive] public CommunicationClientRecords SelectedClient { get; set; }
    [Reactive] public PricesRecord SelectedService { get; set; }
    [Reactive] public PricesRecord SelectedGood { get; set; }
    [Reactive] public ObservableCollection<CompaniesRecord> CompanyVehicle { get; set; }
    [Reactive] public ObservableCollection<PricesRecord> ServicesList { get; set; }
    [Reactive] public ObservableCollection<PricesRecord> GoodsList { get; set; }
    [Reactive] public ObservableCollection<PricesRecord> ItemsInShoppingCartList { get; set; }
    [Reactive] public string ClientPhone { get; set; }
    [Reactive] public string ClientType { get; set; }

    public static double PanelHeigth { get; set; }




    private object _currentPage;

    public object contentUserButtons = new DockPanel();

    public object ClientInfoStackPanel = new StackPanel();

    public object LeftPanelKassaButtons = new StackPanel();

    public object LeftPanelKassaButtonsExecute
    {
        get => LeftPanelKassaButtons;
        set => this.RaiseAndSetIfChanged(ref LeftPanelKassaButtons, value);
    }
    
    public object UserPanel = new DockPanel();

    public object UserPanelView
    {
        get => UserPanel;
        set => this.RaiseAndSetIfChanged(ref UserPanel, value);
    }

    

    public object OrderPanel = new DockPanel();

    public object OrderPanelView
    {
        get => OrderPanel;
        set => this.RaiseAndSetIfChanged(ref OrderPanel, value);
    }

    public object OrderCheckoutPanel = new DockPanel();

    public object OrderCheckoutPanelView
    {
        get => OrderCheckoutPanel;
        set => this.RaiseAndSetIfChanged(ref OrderCheckoutPanel, value);
    }

    public object DockaPanelUserButtons
    {
        get => contentUserButtons;
        set => this.RaiseAndSetIfChanged(ref contentUserButtons, value);
    }

    public object ClientInfo
    {
        get => ClientInfoStackPanel;
        set => this.RaiseAndSetIfChanged(ref ClientInfoStackPanel, value);
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
        PageClientPhoneSearch = ReactiveCommand.Create(PageClientPhoneSearchExecute);
        CheckoutAndExit = ReactiveCommand.Create(CheckoutAndExitExecute);
        NewUserAuthorization = ReactiveCommand.Create(NewUserAuthorizationExecute);
        ShowWashesScreen = ReactiveCommand.Create(ShowWashesScreenExecute);
        OrderRoute = ReactiveCommand.Create(OrderRouteExecute);
        NewCompanyScreen = ReactiveCommand.Create(NewCompanyScreenExecute);
        FindCompanyScreen = ReactiveCommand.Create(FindCompanyScreenExecute);
        FindCompany = ReactiveCommand.Create(FindCompanyExecute);
        SaveNewClient = ReactiveCommand.Create(SaveNewClientExecute);
        SaveNewCompany = ReactiveCommand.Create(SaveNewCompanyExecute);
        ServicesListScreen = ReactiveCommand.Create(ServicesListScreenExecute);
        GoodsListScreen = ReactiveCommand.Create(GoodsListScreenExecute);
        ShoppingCartScreen = ReactiveCommand.Create(ShoppingCartScreenExecute);
        AddItemToShoppingCart = ReactiveCommand.Create(AddItemToShoppingCartExecute);
        NewClientScreen = ReactiveCommand.Create<string>(NewClientScreenExecute);
        ClientPanelVisibility = false;
        LeftPanelKassaButtonsExecute = new KassaLeftPanel();
        UserPanelView = new UserPanel();
        OrderPanelView = new OrderPanel();
        OrderCheckoutPanelView = new OrderCheckoutPanel();
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
    public static void SetPanelHeigt(double value) 
    {
        PanelHeigth = value;
    }

    private void GetPanelHeight() 
    { 
    
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
        ClientPanelVisibility = false;
        CurrentPage = new VehicleScreen();
    }


    private void NewClientScreenExecute(string parameter)
    {
        CurrentPage = new NewClient();
    }

    private void SaveNewClientExecute() 
    {
        if (FoundVehicle != null)
        {
            var debug = true;
            Vehicles.AddClientToVehicle(CarNumber,NewClientFirstName,NewClientLastName,ClientPhone,NewClientEmail,NewClientNotes);
            VehicleRoute(ClientType);
        }
        else 
        {
            var newClientBuilder = new ClientRecordBuilder();
            newClientBuilder.AddFirstName(NewClientFirstName);
            newClientBuilder.AddLastName(NewClientLastName);
            newClientBuilder.AddNotes(NewClientNotes);
            newClientBuilder.AddFirstRegistrationDateTime(DateTime.Now);
            newClientBuilder.AddId(Guid.NewGuid());

            var communicationMeanBuilder = new CommunicationMeanBuilder();
            communicationMeanBuilder.AddEmail(NewClientEmail);
            communicationMeanBuilder.AddPhone(ClientPhone);
            communicationMeanBuilder.AddId(Guid.NewGuid());
            newClientBuilder.AddCommunicationMean([communicationMeanBuilder]);

            var vehiclePriceTypeBuilder = new PriceTypeRecordBuilder();
            var priceType = PriceTypes.FindFirstPriceType();
            vehiclePriceTypeBuilder.AddId(priceType.Id);

            var vehicleRecordBuilder = new VehicleRecordBuilder();
            vehicleRecordBuilder.AddLicence(CarNumber);
            vehicleRecordBuilder.AddId(Guid.NewGuid());
            vehicleRecordBuilder.AddPriceType(vehiclePriceTypeBuilder);
            //var vehicleClient = new 
            newClientBuilder.AddVehicle([vehicleRecordBuilder]);

            var clientBonus = new ClientBonusRecordBuilder();
            clientBonus.AddEndDateTime(DateTime.Now);
            clientBonus.AddSumma(0);
            clientBonus.AddId(Guid.NewGuid());

            newClientBuilder.AddBonus(clientBonus);

            //var debug = true;
            var result = new Clients().Create(newClientBuilder);
            VehicleRoute(ClientType);

        }
        
    }

    private void SaveNewCompanyExecute() 
    {
        Companies.AddCompanyToVehicle(CarNumber, NewCopmanyName, NewCompanyAdress, NewCompanyRegister);
        OrderRouteExecute();
    }

    private void FindCompanyScreenExecute() 
    {
        CurrentPage = new FindCompanyName();
    }

    private void FindCompanyExecute() 
    {
        CompaniesRecord company = Companies.FindCompanyConnectedWithVehicle(SelectedClient.VehicleId);
        bool exist = Companies.FindCompany(CompanyName);
        var debug = true;
        if (company == null )
        {
            CurrentPage = new NewCompany();
        }
        if (exist == true)
        {
            Companies.AddExistedCompanyToVehicle(CarNumber, CompanyName);
            OrderRouteExecute();
        }
        if (exist == false) 
        {
            CurrentPage = new NewCompany();
        }
    }

    private void NewCompanyScreenExecute()
    {
        CurrentPage = new NewCompany();
    }

    private void SearchClientPhoneExecute() 
    {
        if (string.IsNullOrWhiteSpace(ClientPhone)) return;
        CommunicationMeansRecords searchPhone = CommunicationMeans.FindCommunicationClient(ClientPhone);
        if (searchPhone == null)
        {
            CurrentPage = new NewClient();
        }
        else if (searchPhone != null && FoundVehicle != null)
        {
            var debug = true;
            Clients.AddExistedVehicletoExistedClient(ClientPhone, CarNumber);
            VehicleRoute(ClientType);
        }
        else 
        {
            var debug = true;
            Clients.AddVehicletoClient(ClientPhone, CarNumber);
            VehicleRoute(ClientType);
        }
    }

    private void PageClientPhoneSearchExecute()
    {
        
        CurrentPage = new ClientPhoneSearch();
    }

    public void SelectCLientExecute(object? sender , SelectionChangedEventArgs e)
    {
        var dataGrid = sender as DataGrid;
        if (dataGrid != null && dataGrid.SelectedItem != null)
        {
            var selectedItem = dataGrid.SelectedItem;

            var selectedClient = selectedItem as CommunicationClientRecords;

            var clientRecord = new CommunicationClientRecords(
                selectedClient.Id,
                selectedClient.Phone,
                selectedClient.FirstName,
                selectedClient.LastName,
                selectedClient.ClientId,
                selectedClient.VehicleId,
                selectedClient.VehicleName
            );
            SelectedClient = clientRecord;
        }
    }

    public void AddItemToShoppingCartExecute() 
    {
        if (ServicesList == null) 
        {
            if (SelectedGood == null)
            {
                return;
            }
            else 
            {
                ItemsInShoppingCartList.Add(SelectedGood);
                var debug = false;
            }
        }else if (GoodsList == null)
        {
            if (SelectedService == null)
            {
                return;
            }
            else 
            {
                ItemsInShoppingCartList.Add(SelectedService);
            }
        }
    }

    public void ShoppingCartScreenExecute() 
    {
        CurrentPage = new ShoppingCart();
    }

    public void SelectServiceExecute(object? sender, SelectionChangedEventArgs e)
    {
        var dataGrid = sender as DataGrid;
        if (dataGrid != null && dataGrid.SelectedItem != null)
        {
            var selectedItem = dataGrid.SelectedItem;

            var selectedService = selectedItem as PricesRecord;

            var serviceRecord = new PricesRecord(
                selectedService.PriceId,
                selectedService.TradeUnitId,
                selectedService.TradeUnitName,
                selectedService.RateId,
                selectedService.RateValue,
                selectedService.ProcessTime,
                selectedService.CurrentTime,
                selectedService.TimeToStop
            );
            SelectedService = serviceRecord;
            var debug = true;
        }
    }

    public void SelectGoodExecute(object? sender, SelectionChangedEventArgs e)
    {
        var dataGrid = sender as DataGrid;
        if (dataGrid != null && dataGrid.SelectedItem != null)
        {
            var selectedItem = dataGrid.SelectedItem;

            var selectedGood = selectedItem as PricesRecord;

            var goodRecord = new PricesRecord(
                selectedGood.PriceId,
                selectedGood.TradeUnitId,
                selectedGood.TradeUnitName,
                selectedGood.RateId,
                selectedGood.RateValue,
                selectedGood.ProcessTime,
                selectedGood.CurrentTime,
                selectedGood.TimeToStop
            );
            SelectedGood = goodRecord;
            var debug = true;
        }
    }

    private void OrderRouteExecute() 
    {
        if (SelectedClient == null) return;
        bool debug = true;
        if (ClientType.Equals("firm"))
        {
            CompaniesRecord company = Companies.FindCompanyConnectedWithVehicle(SelectedClient.VehicleId);
            CompanyVehicle = new ObservableCollection<CompaniesRecord>();
            CompanyVehicle.Add(company);
            CurrentPage = new CompanyConnectedWithVehicle();
        }
        else if (ClientType.Equals("private")) 
        {
            ServicesListScreenExecute();
        }
    }

    private void ServicesListScreenExecute() 
    {
        List<PricesRecord> services = Prices.GetServicesListInEn();
        ServicesList = new ObservableCollection<PricesRecord>(services);
        ClientInfoStackPanelExecute();
        GoodsList = null;
        CurrentPage = new ServicesListScreen();
        ItemsInShoppingCartList = new ObservableCollection<PricesRecord>();
    }

    private void GoodsListScreenExecute()
    {
        List<PricesRecord> goods = Prices.GetGoodsListInEn();
        GoodsList = new ObservableCollection<PricesRecord>(goods);
        ClientInfoStackPanelExecute();
        ServicesList = null;
        CurrentPage = new GoodsListScreen();
    }

    private void VehicleRoute(string parameter)
    {
        if (string.IsNullOrWhiteSpace(CarNumber)) return;
        FoundVehicle = Vehicles.FindVehicle(CarNumber);
        ClientType = parameter;
        if (FoundVehicle == null)
        {
            CurrentPage = new ClientPhoneSearch();
        }
        else
        {
            if (FoundVehicle.VehicleClients.IsNullOrEmpty()) return;
            List<CommunicationClientRecords> communicationClients = new List<CommunicationClientRecords>();
            foreach (Client client in FoundVehicle.VehicleClients)
            {
                foreach (CommunicationMean comm in client.CommunicationMeans)
                {
                    CommunicationClientRecords commClient = new CommunicationClientRecords(comm.Id, comm.Phone, client.FirstName, client.LastName, client.Id, FoundVehicle.Id, FoundVehicle.Licence);
                    communicationClients.Add(commClient);
                }
            }
            VehicleClient = new ObservableCollection<CommunicationClientRecords>(communicationClients);
            
            CurrentPage = new ClientConnectedWithVehicle();
        }
    }

    private void AddOperatorButtonExecute(List<LoginUserRecord> users)
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
            stackPanelList.Children.Add(textBlock1);
            stackPanelList.Children.Add(textBlock2);
            dockPanel.Children.Add(stackPanel);
        }
        DockaPanelUserButtons = dockPanel;
    }

    private void ClientInfoStackPanelExecute() 
    { 
        StackPanel userPanel = new StackPanel();
        TextBlock userName = new TextBlock();
        TextBlock userPhone = new TextBlock();
        TextBlock vehicleNumber = new TextBlock();
        TextBlock companyName = new TextBlock();
        userName.Text = SelectedClient.FirstName + " " + SelectedClient.LastName;
        userName.TextAlignment = Avalonia.Media.TextAlignment.Center;
        userPhone.Text = SelectedClient.Phone;
        userPhone.TextAlignment = Avalonia.Media.TextAlignment.Center;
        vehicleNumber.Text = SelectedClient.VehicleName;
        vehicleNumber.TextAlignment = Avalonia.Media.TextAlignment.Center;
        if (ClientType == "firm") 
        {
            foreach (var company in CompanyVehicle) 
            {
                companyName.Text = company.CompanyName;
                companyName.TextAlignment = Avalonia.Media.TextAlignment.Center;
                break;
            }
        }
        userPanel.Children.Add(vehicleNumber);
        userPanel.Children.Add(userPhone);
        userPanel.Children.Add(userName);
        if (ClientType.Equals("firm")) 
        {
            userPanel.Children.Add(companyName);
        }
        ClientPanelVisibility = true;
        ClientInfo = userPanel;
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