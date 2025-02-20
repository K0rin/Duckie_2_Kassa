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
using System.ComponentModel;
using Avalonia.Input;
using System.Xml;
using Avalonia.LogicalTree;
using Avalonia.Svg;
using Avalonia.Svg.Skia;
using Avalonia.Media;
using Avalonia.Layout;
using DataFaker;

namespace Duckie2Client.ViewModels.Screens;

public class MainKassaScreenViewModel : ViewModelPageBase
{
    #region Commands

    //public ReactiveCommand<Unit, Unit> BatchServiceAddingCommand { get; }
    public ReactiveCommand<Unit, Unit> ExitMenuCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveOrder { get; }
    //public ReactiveCommand<Unit, Unit> PersonnelCommand { get; }
    public ReactiveCommand<Unit, Unit> ShowOrdersScreen { get; }
    public ReactiveCommand<Guid, Unit> OrderCompleteStatusSave { get; }
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
    
    private List<OrderButtonRecord?> _ordersList = [];

    private VehiclesRecord? FoundVehicle { get; set; }

    public ObservableCollection<TabItemViewModel> TabItems { get; } = [];


    [Reactive] public bool IsDashboardVisible { get; set; }
    [Reactive] public bool OrderCompletedButtonEnabled { get; set; }
    [Reactive] public string NewClientFirstName { get; set; }
    [Reactive] public string NewClientLastName { get; set; }
    [Reactive] public string NewClientEmail { get; set; }
    [Reactive] public string NewClientNotes { get; set; }
    [Reactive] public bool ClientPanelVisibility { get; set; }
    [Reactive] public ObservableCollection<CommunicationClientRecords> VehicleClient { get; set; }
    [Reactive] public PricesRecord SelectedService { get; set; }
    [Reactive] public PricesRecord SelectedGood { get; set; }
    [Reactive] public ObservableCollection<PricesRecord> ServicesList { get; set; }
    [Reactive] public ObservableCollection<PricesRecord> GoodsList { get; set; }
    [Reactive] public ObservableCollection<PricesRecord> ItemsInShoppingCartList { get; set; }
    [Reactive] public string ClientType { get; set; }
    [Reactive] public string BonusButtonIconPath { get; set; }
    [Reactive] public bool BonusButtonEnabled { get; set; }
    [Reactive] public bool SaveOrderButtonEnabled { get; set; }
    [Reactive] public bool OrderCompletedButtonIsVisible { get; set; }
    [Reactive] public decimal BonusValue { get; set; }
    [Reactive] public string PrivateButtonIconPath { get; set; }
    [Reactive] public string PrivateButtonTextColor { get; set; }
    [Reactive] public bool PrivateButtonEnabled { get; set; }
    [Reactive] public string EmptyButtonIconPath { get; set; }
    [Reactive] public string EmptyButtonTextColor { get; set; }
    [Reactive] public bool EmptyButtonEnabled { get; set; }
    [Reactive] public string CompanyButtonIconPath { get; set; }
    [Reactive] public string CompanyButtonTextColor { get; set; }
    [Reactive] public bool CompanyButtonEnabled { get; set; }
    [Reactive] public string ClientPhoneSearchButtonIconPath { get; set; }
    [Reactive] public string ClientPhoneSearchButtonTextColor { get; set; }
    [Reactive] public bool ClientPhoneSearchButtonEnabled { get; set; }
    [Reactive] public string ClientPhoneSearchBackButtonIconPath { get; set; }
    [Reactive] public string ClientPhoneSearchBackButtonTextColor { get; set; }
    [Reactive] public string CompanySearchButtonIconPath { get; set; }
    [Reactive] public string CompanySearchButtonTextColor { get; set; }
    [Reactive] public bool CompanySearchButtonEnabled { get; set; }
    [Reactive] public string CompanySearchBackButtonIconPath { get; set; }
    [Reactive] public string CompanySearchBackButtonTextColor { get; set; }
    [Reactive] public string VehicleScreenBackButtonIconPath { get; set; }
    [Reactive] public string VehicleScreenBackButtonTextColor { get; set; }
    [Reactive] public string AnotherClientButtonIconPath { get; set; }
    [Reactive] public string AnotherClientButtonTextColor { get; set; }
    [Reactive] public bool AnotherClientButtonEnabled { get; set; }
    [Reactive] public string VehicleClientsScreenNextButtonIconPath { get; set; }
    [Reactive] public string VehicleClientsScreenNextButtonTextColor { get; set; }
    [Reactive] public bool VehicleClientsScreenNextButtonEnabled { get; set; }
    [Reactive] public string CompanyScreenBackButtonIconPath { get; set; }
    [Reactive] public string CompanyScreenBackButtonTextColor { get; set; }
    [Reactive] public string AnotherCompanyButtonIconPath { get; set; }
    [Reactive] public string AnotherCompanyButtonTextColor { get; set; }
    [Reactive] public bool AnotherCompanyButtonEnabled { get; set; }
    [Reactive] public string CompanyScreenNextButtonIconPath { get; set; }
    [Reactive] public string CompanyScreenNextButtonTextColor { get; set; }
    [Reactive] public bool CompanyScreenNextButtonEnabled { get; set; }
    [Reactive] public string SaveNewCompanyButtonIconPath { get; set; }
    [Reactive] public string SaveNewCompanyButtonTextColor { get; set; }
    [Reactive] public bool SaveNewCompanyButtonEnabled { get; set; }
    [Reactive] public string SaveNewCompanyBackButtonIconPath { get; set; }
    [Reactive] public string SaveNewCompanyBackButtonTextColor { get; set; }
    [Reactive] public string SaveNewClientButtonIconPath { get; set; }
    [Reactive] public bool SaveNewClientButtonEnabled { get; set; }
    [Reactive] public string SaveNewClientBackButtonIconPath { get; set; }
    [Reactive] public string SaveNewClientBackButtonTextColor { get; set; }
    [Reactive] public string SaveNewClientButtonTextColor { get; set; }
    [Reactive] public bool OrderGoodsTableVisible { get; set; }
    [Reactive] public bool OrderServicesTableVisible { get; set; }



    private object _currentPage;

    public object CurrentPage
    {
        get => _currentPage;
        set => this.RaiseAndSetIfChanged(ref _currentPage, value);
    }

    private string _clientPhone;
    [Reactive] 
    public string ClientPhone 
    { 
        get => _clientPhone; 
        set => this.RaiseAndSetIfChanged(ref _clientPhone, value); 
    }
    
    private string _carNumber;

    [Reactive]
    public string CarNumber
    {
        get => _carNumber;
        set => this.RaiseAndSetIfChanged(ref _carNumber, value);
    }

    private CommunicationClientRecords _selectedClient;
    
    [Reactive] 
    public CommunicationClientRecords SelectedClient 
    { 
        get => _selectedClient; 
        set => this.RaiseAndSetIfChanged(ref _selectedClient, value); 
    }

    private OrderRecord _orderData;

    [Reactive]
    public OrderRecord OrderData
    {
        get => _orderData;
        set => this.RaiseAndSetIfChanged(ref _orderData, value);
    }

    private List<OrderRecord> _ordersPerPeriodData;

    [Reactive]
    public List<OrderRecord> OrdersPerPeriod
    {
        get => _ordersPerPeriodData;
        set => this.RaiseAndSetIfChanged(ref _ordersPerPeriodData, value);
    }

    private string _companyName;
    [Reactive] 
    public string CompanyName 
    { 
        get => _companyName; 
        set => this.RaiseAndSetIfChanged(ref _companyName, value); 
    }

    private string _newCompanyName;
    
    [Reactive] 
    public string NewCopmanyName 
    { 
        get => _newCompanyName; 
        set => this.RaiseAndSetIfChanged(ref _newCompanyName, value); 
    }

    private string _newCompanyRegister;
    [Reactive]
    public string NewCompanyRegister 
    { 
        get => _newCompanyRegister; 
        set => this.RaiseAndSetIfChanged(ref _newCompanyRegister, value); 
    }
    private string _newCompanyAddress;
    [Reactive] 
    public string NewCompanyAdress 
    { 
        get => _newCompanyAddress; 
        set => this.RaiseAndSetIfChanged(ref _newCompanyAddress, value); 
    }

    private ObservableCollection<CompaniesRecord> _companyVehicles;
    [Reactive] 
    public ObservableCollection<CompaniesRecord> CompanyVehicle 
    { 
        get => _companyVehicles; 
        set => this.RaiseAndSetIfChanged(ref _companyVehicles, value); 
    }

    public object contentUserButtons = new DockPanel();

    public object DockaPanelUserButtons
    {
        get => contentUserButtons;
        set => this.RaiseAndSetIfChanged(ref contentUserButtons, value);
    }

    public object contentOrderButtons = new DockPanel();

    public object DockaPanelOrderButtons
    {
        get => contentOrderButtons;
        set => this.RaiseAndSetIfChanged(ref contentOrderButtons, value);
    }

    public event PropertyChangedEventHandler PropertyChanged;
    private int _selectedPriceTypeIndex;
    public int SelectedPriceTypeIndex
    {
        get => _selectedPriceTypeIndex;
        set
        {
            if (_selectedPriceTypeIndex != value)
            {
                _selectedPriceTypeIndex = value;
                OnPropertyChanged(nameof(SelectedPriceTypeIndex));
            }
        }
    }

    private int _selectedPollutionTypeIndex;
    public int SelectedPollutionTypeIndex
    {
        get => _selectedPollutionTypeIndex;
        set
        {
            if (_selectedPollutionTypeIndex != value)
            {
                _selectedPollutionTypeIndex = value;
                OnPropertyChanged(nameof(SelectedPollutionTypeIndex));
            }
        }
    }

    private int _selectedPaymentTypeIndex;
    public int SelectedPaymentTypeIndex
    {
        get => _selectedPaymentTypeIndex;
        set
        {
            if (_selectedPaymentTypeIndex != value)
            {
                _selectedPaymentTypeIndex = value;
                OnPropertyChanged(nameof(SelectedPaymentTypeIndex));
            }
        }
    }

    public object ClientInfoStackPanel = new StackPanel();
    public object ClientInfo
    {
        get => ClientInfoStackPanel;
        set => this.RaiseAndSetIfChanged(ref ClientInfoStackPanel, value);
    }

    public object ClientBonusesStackPanel = new StackPanel();
    public object ClientBonusesPanel
    {
        get => ClientBonusesStackPanel;
        set => this.RaiseAndSetIfChanged(ref ClientBonusesStackPanel, value);
    }

    public MainKassaScreenViewModel()
    {
        ExitMenuCommand = ReactiveCommand.Create(ExitMenuCommandExecute);
        ShowOrdersScreen = ReactiveCommand.Create(ShowOrdersScreenExecute);
        OrderCompleteStatusSave = ReactiveCommand.Create<Guid>(OrderCompleteStatusSaveExecute);
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
        SaveOrder = ReactiveCommand.Create(SaveOrderExecute);
        SaveOrderButtonEnabled = false;
        ClientPanelVisibility = false;
        OrderCompletedButtonEnabled = false;
        this.WhenAnyValue(x => x.CarNumber)
            .Where(value => !string.IsNullOrEmpty(value)) // if CarNumber is not Empty
            .Subscribe(value =>
            {
                VehicleScreenButtons(true);
            });
        this.WhenAnyValue(x => x.CarNumber)
            .Where(value => !string.IsNullOrEmpty(value) == false) // if CarNumber is Empty
            .Subscribe(value =>
            {
                VehicleScreenButtons(false);
            });
        this.WhenAnyValue(x => x.ClientPhone)
            .Where(value => !string.IsNullOrEmpty(value)) // if ClientPhone is not Empty
            .Subscribe(value =>
            {
                ClientPhoneSearchButtons(true);
            });
        this.WhenAnyValue(x => x.ClientPhone)
            .Where(value => !string.IsNullOrEmpty(value) == false) // if ClientPhone is Empty
            .Subscribe(value =>
            {
                ClientPhoneSearchButtons(false);
            });
        this.WhenAnyValue(x => x.CompanyName)
            .Where(value => !string.IsNullOrEmpty(value)) // if CompanyName is not Empty
            .Subscribe(value =>
            {
                CompanySearchButtons(true);
            });
        this.WhenAnyValue(x => x.CompanyName)
            .Where(value => !string.IsNullOrEmpty(value) == false) // if CompanyName is Empty
            .Subscribe(value =>
            {
                CompanySearchButtons(false);
            });
        this.WhenAnyValue(x => x.SelectedClient)
            .Where(client => client != null) // if SelectedClient is not Empty
            .Subscribe(value =>
            {
                VehicleClientsScreenButtons(true);
            });
        this.WhenAnyValue(x => x.SelectedClient)
            .Where(client => client == null) // if SelectedClient is Empty
            .Subscribe(value =>
            {
                VehicleClientsScreenButtons(false);
            });
        this.WhenAnyValue(x => x.CompanyVehicle)
            .Where(company => company != null) // if CompanyVehicle is not Empty
            .Subscribe(value =>
            {
                CompanyScreenButtons(true);
            });
        this.WhenAnyValue(x => x.CompanyVehicle)
            .Where(company => company == null) // if CompanyVehicle is Empty
            .Subscribe(value =>
            {
                CompanyScreenButtons(false);
            });
        this.WhenAnyValue(x => x.NewCopmanyName)
            .Where(value => !string.IsNullOrEmpty(value)) // if NewCopmanyName is not Empty
            .Subscribe(value =>
            {
                if (NewCompanyRegister.IsNullOrEmpty())
                {
                    NewCompanyButtons(false);
                }
                else if (NewCompanyAdress.IsNullOrEmpty())
                {
                    NewCompanyButtons(false);
                }
                else 
                {
                    NewCompanyButtons(true);
                }
            });
        this.WhenAnyValue(x => x.NewCopmanyName)
            .Where(value => !string.IsNullOrEmpty(value) == false) // if NewCopmanyName is Empty
            .Subscribe(value =>
            {
                NewCompanyButtons(false);
            });
        this.WhenAnyValue(x => x.NewCompanyAdress)
            .Where(value => !string.IsNullOrEmpty(value)) // if NewCompanyAdress is not Empty
            .Subscribe(value =>
            {
                if (NewCompanyRegister.IsNullOrEmpty())
                {
                    NewCompanyButtons(false);
                }
                else if (NewCopmanyName.IsNullOrEmpty())
                {
                    NewCompanyButtons(false);
                }
                else
                {
                    NewCompanyButtons(true);
                }
            });
        this.WhenAnyValue(x => x.NewCompanyAdress)
            .Where(value => !string.IsNullOrEmpty(value) == false) // if NewCompanyAdress is Empty
            .Subscribe(value =>
            {
                NewCompanyButtons(false);
            });
        this.WhenAnyValue(x => x.NewCompanyRegister)
            .Where(value => !string.IsNullOrEmpty(value)) // if NewCompanyRegister is not Empty
            .Subscribe(value =>
            {
                if (NewCompanyAdress.IsNullOrEmpty())
                {
                    NewCompanyButtons(false);
                }
                else if (NewCopmanyName.IsNullOrEmpty())
                {
                    NewCompanyButtons(false);
                }
                else
                {
                    NewCompanyButtons(true);
                }
            });
        this.WhenAnyValue(x => x.NewCompanyRegister)
            .Where(value => !string.IsNullOrEmpty(value) == false) // if NewCompanyRegister is Empty
            .Subscribe(value =>
            {
                NewCompanyButtons(false);
            });
        this.WhenAnyValue(x => x.NewClientFirstName)
            .Where(value => !string.IsNullOrEmpty(value)) // if NewClientFirstName is not Empty
            .Subscribe(value =>
            {
                if (NewClientLastName.IsNullOrEmpty())
                {
                    NewClientButtons(false);
                }
                else if (NewClientEmail.IsNullOrEmpty())
                {
                    NewClientButtons(false);
                }
                else if (NewClientNotes.IsNullOrEmpty())
                {
                    NewClientButtons(false);
                }
                else
                {
                    NewClientButtons(true);
                }
            });
        this.WhenAnyValue(x => x.NewClientFirstName)
            .Where(value => !string.IsNullOrEmpty(value) == false) // if NewClientFirstName is Empty
            .Subscribe(value =>
            {
                NewClientButtons(false);
            });
        this.WhenAnyValue(x => x.NewClientLastName)
            .Where(value => !string.IsNullOrEmpty(value)) // if NewClientLastName is not Empty
            .Subscribe(value =>
            {
                if (NewClientFirstName.IsNullOrEmpty())
                {
                    NewClientButtons(false);
                }
                else if (NewClientEmail.IsNullOrEmpty())
                {
                    NewClientButtons(false);
                }
                else if (NewClientNotes.IsNullOrEmpty())
                {
                    NewClientButtons(false);
                }
                else
                {
                    NewClientButtons(true);
                }
            });
        this.WhenAnyValue(x => x.NewClientLastName)
            .Where(value => !string.IsNullOrEmpty(value) == false) // if NewClientLastName is Empty
            .Subscribe(value =>
            {
                NewClientButtons(false);
            });
        this.WhenAnyValue(x => x.NewClientEmail)
            .Where(value => !string.IsNullOrEmpty(value)) // if NewClientEmail is not Empty
            .Subscribe(value =>
            {
                if (NewClientFirstName.IsNullOrEmpty())
                {
                    NewClientButtons(false);
                }
                else if (NewClientLastName.IsNullOrEmpty())
                {
                    NewClientButtons(false);
                }
                else if (NewClientNotes.IsNullOrEmpty())
                {
                    NewClientButtons(false);
                }
                else
                {
                    NewClientButtons(true);
                }
            });
        this.WhenAnyValue(x => x.NewClientEmail)
            .Where(value => !string.IsNullOrEmpty(value) == false) // if NewClientEmail is Empty
            .Subscribe(value =>
            {
                NewClientButtons(false);
            });
        this.WhenAnyValue(x => x.NewClientNotes)
            .Where(value => !string.IsNullOrEmpty(value)) // if NewClientNotes is not Empty
            .Subscribe(value =>
            {
                if (NewClientFirstName.IsNullOrEmpty())
                {
                    NewClientButtons(false);
                }
                else if (NewClientLastName.IsNullOrEmpty())
                {
                    NewClientButtons(false);
                }
                else if (NewClientEmail.IsNullOrEmpty())
                {
                    NewClientButtons(false);
                }
                else
                {
                    NewClientButtons(true);
                }
            });
        this.WhenAnyValue(x => x.NewClientNotes)
            .Where(value => !string.IsNullOrEmpty(value) == false) // if NewClientNotes is Empty
            .Subscribe(value =>
            {
                NewClientButtons(false);
            });

    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        var debug = true;
    }

    public void AddLoggedInUser(LoginUserRecord? userRecord)
    {
        var foundUser = _loggedInUsers.FirstOrDefault(u => u?.Id == userRecord.Id);
        if (foundUser == null) 
        {
            _loggedInUsers.Add(userRecord);
            AddOperatorButtonExecute(_loggedInUsers);
        }
        CurrentPage = new VehicleScreen();
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
        bool isAnyoneBusy = false;
        foreach (var user in _loggedInUsers) 
        {
            if (user.IsBusy == true) 
            { 
                isAnyoneBusy = true;
            }
        }
        if (isAnyoneBusy == false && _ordersList.IsNullOrEmpty() == true)
        {
            App.ShutdownApplication();
        }        
    }

    private void ShowWashesScreenExecute()
    {
        OrdersPerPeriod = Washes.FindAllWashesPerPeriod();
        CurrentPage = new OrdersPerYearScreen();
    }

    private void NewUserAuthorizationExecute()
    {
        PagerViewModel?.SwitchPage(0);
    }

    private void ShowOrdersScreenExecute()
    {
        SelectedClient = null;
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
            NewCopmanyName = CompanyName;
            NewCompanyRegister = "nonexistent";
            CurrentPage = new NewCompany();
        }
        if (exist == true)
        {
            Companies.AddExistedCompanyToVehicle(CarNumber, CompanyName);
            OrderRouteExecute();
        }
        if (exist == false) 
        {
            NewCopmanyName = CompanyName;
            NewCompanyRegister = "nonexistent";
            CurrentPage = new NewCompany();
        }
    }

    private void NewCompanyScreenExecute()
    {
        CurrentPage = new NewCompany();
    }

    private void ClientBonus() 
    {
        
        decimal bonus = ClientBonuses.FindBoonus(SelectedClient.ClientId);
        if (bonus > 0)
        {
            BonusValue = bonus;
            BonusButtonIconPath = "/Assets/icon_use_bonuses.svg";
            BonusButtonEnabled = true;
        }
        else 
        {
            BonusValue = bonus;
            BonusButtonIconPath = "/Assets/icon_use_bonuses_disabled.svg";
            BonusButtonEnabled = false;
        }
    }

    private void VehicleScreenButtons(bool status) 
    {
        if (status == false)
        {
            PrivateButtonEnabled = false;
            PrivateButtonIconPath = "/Assets/icon_private_individual_disabled.svg";
            PrivateButtonTextColor = "#B5B8B1";
            EmptyButtonEnabled = false;
            EmptyButtonIconPath = "/Assets/icon_blank_order_disabled.svg";
            EmptyButtonTextColor = "#B5B8B1";
            CompanyButtonEnabled = false;
            CompanyButtonIconPath = "/Assets/icon_legal_entity_disabled.svg";
            CompanyButtonTextColor = "#B5B8B1";
        }
        else 
        {
            PrivateButtonEnabled = true;
            PrivateButtonIconPath = "/Assets/icon_private_individual.svg";
            PrivateButtonTextColor = "Black";
            EmptyButtonEnabled = true;
            EmptyButtonIconPath = "/Assets/icon_blank_order.svg";
            EmptyButtonTextColor = "Black";
            CompanyButtonEnabled = true;
            CompanyButtonIconPath = "/Assets/icon_legal_entity.svg";
            CompanyButtonTextColor = "Black";
        }
    }

    private void VehicleClientsScreenButtons(bool status)
    {
        if (status == false)
        {
            VehicleScreenBackButtonIconPath = "/Assets/icon_back.svg";
            VehicleScreenBackButtonTextColor = "Black";
            AnotherClientButtonEnabled = true;
            AnotherClientButtonIconPath = "/Assets/icon_new_client.svg";
            AnotherClientButtonTextColor = "Black";
            VehicleClientsScreenNextButtonEnabled = false;
            VehicleClientsScreenNextButtonIconPath = "/Assets/icon_next_disabled.svg";
            VehicleClientsScreenNextButtonTextColor = "#B5B8B1";
        }
        else
        {
            VehicleScreenBackButtonIconPath = "/Assets/icon_back.svg";
            VehicleScreenBackButtonTextColor = "Black";
            AnotherClientButtonEnabled = true;
            AnotherClientButtonIconPath = "/Assets/icon_new_client.svg";
            AnotherClientButtonTextColor = "Black";
            VehicleClientsScreenNextButtonEnabled = true;
            VehicleClientsScreenNextButtonIconPath = "/Assets/icon_next.svg";
            VehicleClientsScreenNextButtonTextColor = "Black";
        }
    }
    private void CompanyScreenButtons(bool status)
    {
        if (status == false)
        {
            CompanyScreenBackButtonIconPath = "/Assets/icon_back.svg";
            CompanyScreenBackButtonTextColor = "#B5B8B1";
            AnotherCompanyButtonEnabled = false;
            AnotherCompanyButtonIconPath = "/Assets/icon_new_firm_disabled.svg";
            AnotherCompanyButtonTextColor = "#B5B8B1";
            CompanyScreenNextButtonEnabled = false;
            CompanyScreenNextButtonIconPath = "/Assets/icon_next_disabled.svg";
            CompanyScreenNextButtonTextColor = "#B5B8B1";
        }
        else
        {
            CompanyScreenBackButtonIconPath = "/Assets/icon_back.svg";
            CompanyScreenBackButtonTextColor = "Black";
            AnotherCompanyButtonEnabled = true;
            AnotherCompanyButtonIconPath = "/Assets/icon_new_firm.svg";
            AnotherCompanyButtonTextColor = "Black";
            CompanyScreenNextButtonEnabled = true;
            CompanyScreenNextButtonIconPath = "/Assets/icon_next.svg";
            CompanyScreenNextButtonTextColor = "Black";
        }
    }

    private void ClientPhoneSearchButtons(bool status) 
    {
        if (status == false)
        {
            ClientPhoneSearchButtonEnabled = false;
            ClientPhoneSearchButtonIconPath = "/Assets/icon_search_disabled.svg";
            ClientPhoneSearchButtonTextColor = "#B5B8B1";
            ClientPhoneSearchBackButtonIconPath = "/Assets/icon_back.svg";
            ClientPhoneSearchBackButtonTextColor = "Black";
        }
        else 
        { 
            ClientPhoneSearchButtonEnabled = true;
            ClientPhoneSearchButtonIconPath = "/Assets/icon_search.svg";
            ClientPhoneSearchButtonTextColor = "Black";
            ClientPhoneSearchBackButtonIconPath = "/Assets/icon_back.svg";
            ClientPhoneSearchBackButtonTextColor = "Black";
        }
    }

    private void CompanySearchButtons(bool status)
    {
        if (status == false)
        {
            CompanySearchButtonEnabled = false;
            CompanySearchButtonIconPath = "/Assets/icon_search_disabled.svg";
            CompanySearchButtonTextColor = "#B5B8B1";
            CompanySearchBackButtonIconPath = "/Assets/icon_back.svg";
            CompanySearchBackButtonTextColor = "Black";
        }
        else
        {
            CompanySearchButtonEnabled = true;
            CompanySearchButtonIconPath = "/Assets/icon_search.svg";
            CompanySearchButtonTextColor = "Black";
            CompanySearchBackButtonIconPath = "/Assets/icon_back.svg";
            CompanySearchBackButtonTextColor = "Black";
        }
    }

    private void NewCompanyButtons(bool status)
    {
        if (status == false)
        {
            SaveNewCompanyButtonEnabled = false;
            SaveNewCompanyButtonIconPath = "/Assets/icon_save_disabled.svg";
            SaveNewCompanyButtonTextColor = "#B5B8B1";
            SaveNewCompanyBackButtonIconPath = "/Assets/icon_back.svg";
            SaveNewCompanyBackButtonTextColor = "Black";
        }
        else
        {
            SaveNewCompanyButtonEnabled = true;
            SaveNewCompanyButtonIconPath = "/Assets/icon_save.svg";
            SaveNewCompanyButtonTextColor = "Black";
            SaveNewCompanyBackButtonIconPath = "/Assets/icon_back.svg";
            SaveNewCompanyBackButtonTextColor = "Black";
        }
    }
    private void NewClientButtons(bool status)
    {
        if (status == false)
        {
            SaveNewClientButtonEnabled = false;
            SaveNewClientButtonIconPath = "/Assets/icon_save_disabled.svg";
            SaveNewClientButtonTextColor = "#B5B8B1";
            SaveNewClientBackButtonIconPath = "/Assets/icon_back.svg";
            SaveNewClientBackButtonTextColor = "Black";
        }
        else
        {
            SaveNewClientButtonEnabled = true;
            SaveNewClientButtonIconPath = "/Assets/icon_save.svg";
            SaveNewClientButtonTextColor = "Black";
            SaveNewClientBackButtonIconPath = "/Assets/icon_back.svg";
            SaveNewClientBackButtonTextColor = "Black";
        }
    }

    public void PrivateButtonCursor(bool status) 
    {
        if (status == true)
        {
            PrivateButtonTextColor = "#eef5f7";
        }
        else
        {
            PrivateButtonTextColor = "Black";
        }
    }

    public void EmptyButtonCursor(bool status)
    {
        if (status == true)
        {
            EmptyButtonTextColor = "#eef5f7";
        }
        else
        {
            EmptyButtonTextColor = "Black";
        }
    }

    public void CompanyButtonCursor(bool status)
    {
        if (status == true)
        {
            CompanyButtonTextColor = "#eef5f7";
        }
        else
        {
            CompanyButtonTextColor = "Black";
        }
    }

    public void ClientBonusButtonCursorEnteredExecute() 
    {
        BonusButtonIconPath = "/Assets/icon_use_bonuses_active.svg";
    }

    public void ClientBonusButtonCursorExitedExecute()
    {
        BonusButtonIconPath = "/Assets/icon_use_bonuses.svg";
    }

    public void ClientPhoneSearchButtonCursor(bool status) 
    {
        if (status == true)
        {
            ClientPhoneSearchButtonIconPath = "/Assets/icon_search_active.svg";
            ClientPhoneSearchButtonTextColor = "#eef5f7";
        }
        else 
        {
            ClientPhoneSearchButtonIconPath = "/Assets/icon_search.svg";
            ClientPhoneSearchButtonTextColor = "Black";
        }
    }

    public void ClientPhoneSearchBackButtonCursor(bool status)
    {
        if (status == true)
        {
            ClientPhoneSearchBackButtonIconPath = "/Assets/icon_back_active.svg";
            ClientPhoneSearchBackButtonTextColor = "#eef5f7";
        }
        else
        {
            ClientPhoneSearchBackButtonIconPath = "/Assets/icon_back.svg";
            ClientPhoneSearchBackButtonTextColor = "Black";
        }
    }

    public void CompanySearchButtonCursor(bool status)
    {
        if (status == true)
        {
            CompanySearchButtonIconPath = "/Assets/icon_search_active.svg";
            CompanySearchButtonTextColor = "#eef5f7";
        }
        else
        {
            CompanySearchButtonIconPath = "/Assets/icon_search.svg";
            CompanySearchButtonTextColor = "Black";
        }
    }

    public void CompanySearchBackButtonCursor(bool status)
    {
        if (status == true)
        {
            CompanySearchBackButtonIconPath = "/Assets/icon_back_active.svg";
            CompanySearchBackButtonTextColor = "#eef5f7";
        }
        else
        {
            CompanySearchBackButtonIconPath = "/Assets/icon_back.svg";
            CompanySearchBackButtonTextColor = "Black";
        }
    }

    public void VehicleClientBackButtonCursor(bool status)
    {
        if (status == true)
        {
            VehicleScreenBackButtonIconPath = "/Assets/icon_back_active.svg";
            VehicleScreenBackButtonTextColor = "#eef5f7";
        }
        else
        {
            VehicleScreenBackButtonIconPath = "/Assets/icon_back.svg";
            VehicleScreenBackButtonTextColor = "Black";
        }
    }

    public void AnotherClientButtonCursor(bool status)
    {
        if (status == true)
        {
            AnotherClientButtonIconPath = "/Assets/icon_new_client_active.svg";
            AnotherClientButtonTextColor = "#eef5f7";
        }
        else
        {
            AnotherClientButtonIconPath = "/Assets/icon_new_client.svg";
            AnotherClientButtonTextColor = "Black";
        }
    }

    public void VehicleClientsScreenNextButtonCursor(bool status)
    {
        if (status == true)
        {
            VehicleClientsScreenNextButtonIconPath = "/Assets/icon_next_active.svg";
            VehicleClientsScreenNextButtonTextColor = "#eef5f7";
        }
        else
        {
            VehicleClientsScreenNextButtonIconPath = "/Assets/icon_next.svg";
            VehicleClientsScreenNextButtonTextColor = "Black";
        }
    }

    public void CompanyScreenNextButtonCursor(bool status)
    {
        if (status == true)
        {
            CompanyScreenNextButtonIconPath = "/Assets/icon_next_active.svg";
            CompanyScreenNextButtonTextColor = "#eef5f7";
        }
        else
        {
            CompanyScreenNextButtonIconPath = "/Assets/icon_next.svg";
            CompanyScreenNextButtonTextColor = "Black";
        }
    }

    public void CompanyScreenBackButtonCursor(bool status)
    {
        if (status == true)
        {
            CompanyScreenBackButtonIconPath = "/Assets/icon_back_active.svg";
            CompanyScreenBackButtonTextColor = "#eef5f7";
        }
        else
        {
            CompanyScreenBackButtonIconPath = "/Assets/icon_back.svg";
            CompanyScreenBackButtonTextColor = "Black";
        }
    }

    public void AnotherCompanyButtonCursor(bool status)
    {
        if (status == true)
        {
            AnotherCompanyButtonIconPath = "/Assets/icon_new_firm_active.svg";
            AnotherCompanyButtonTextColor = "#eef5f7";
        }
        else
        {
            AnotherCompanyButtonIconPath = "/Assets/icon_new_firm.svg";
            AnotherCompanyButtonTextColor = "Black";
        }
    }

    public void NewCompanyBackButtonCursor(bool status)
    {
        if (status == true)
        {
            SaveNewCompanyBackButtonIconPath = "/Assets/icon_back_active.svg";
            SaveNewCompanyBackButtonTextColor = "#eef5f7";
        }
        else
        {
            SaveNewCompanyBackButtonIconPath = "/Assets/icon_back.svg";
            SaveNewCompanyBackButtonTextColor = "Black";
        }
    }

    public void SaveNewCompanyButtonCursor(bool status)
    {
        if (status == true)
        {
            SaveNewCompanyButtonIconPath = "/Assets/icon_save_active.svg";
            SaveNewCompanyButtonTextColor = "#eef5f7";
        }
        else
        {
            SaveNewCompanyButtonIconPath = "/Assets/icon_save.svg";
            SaveNewCompanyButtonTextColor = "Black";
        }
    }

    public void NewClientBackButtonCursor(bool status)
    {
        if (status == true)
        {
            SaveNewClientBackButtonIconPath = "/Assets/icon_back_active.svg";
            SaveNewClientBackButtonTextColor = "#eef5f7";
        }
        else
        {
            SaveNewClientBackButtonIconPath = "/Assets/icon_back.svg";
            SaveNewClientBackButtonTextColor = "Black";
        }
    }

    public void SaveNewClientButtonCursor(bool status)
    {
        if (status == true)
        {
            SaveNewClientButtonIconPath = "/Assets/icon_save_active.svg";
            SaveNewClientButtonTextColor = "#eef5f7";
        }
        else
        {
            SaveNewClientButtonIconPath = "/Assets/icon_save.svg";
            SaveNewClientButtonTextColor = "Black";
        }
    }


    public void SaveOrderExecute() 
    {
        Guid client = Clients.FindClientId(SelectedClient.Phone);
        Guid vehicle = Vehicles.FindVehicleId(SelectedClient.VehicleName);
        Guid company = Companies.FindCompanyId(SelectedClient.VehicleName);
        var user = _loggedInUsers[0];
        Guid branch = Branches.FindBranchId(user.Id);
        var items = ItemsInShoppingCartList;
        Washes.AddWashes(items, branch, client, company, vehicle, SelectedPaymentTypeIndex);
        CurrentPage = new VehicleScreen();
        Wash savedWash = Washes.FindLastWash();
        OrderButtonRecord order = new OrderButtonRecord(savedWash.Id, CarNumber);
        _ordersList.Add(order);
        ClientPanelVisibility = false;
        OrderButtonExecute(_ordersList);
        CurrentPage = new VehicleScreen();
        SaveOrderButtonEnabled = false;
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
                //var debug = false;
                SaveOrderButtonEnabled = true;
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
                SaveOrderButtonEnabled = true;
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
                selectedService.TimeToStop,
                selectedService.isGood
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
                selectedGood.TimeToStop,
                selectedGood.isGood
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
            if (company != null)
            {
                CurrentPage = new CompanyConnectedWithVehicle();
            }
            else 
            {
                FindCompanyScreenExecute();
            }
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
        ClientBonus();
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
        foreach (var orderInList in _ordersList)
        {
            if (CarNumber.Equals(orderInList.Carnumber) == true)
            {
                CurrentPage = new WarningOrderAlreadyExists();
                return;
            }
        }
        if (string.IsNullOrWhiteSpace(CarNumber)) return;
        FoundVehicle = Vehicles.FindVehicleRecord(CarNumber);
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
            userButton.Margin = new Thickness(0, 10, 3, 0);
            var addOrderDockPanel = new DockPanel();
            var excludeFromOrderDockPanel = new DockPanel();
            var finishJobDockPanel = new DockPanel();
            
            var svgAddOrder = new Image();
            var svgFinishJob = new Image();
            var svgExcludeOrder = new Image();

            string projectRoot = Path.Combine(Environment.CurrentDirectory, @"..\..\..");
            string assetsPathAddOrder = Path.Combine(projectRoot, "Assets", "icon_include_operator.png");
            string assetsPathExludeOrder = Path.Combine(projectRoot, "Assets", "icon_exclude_operator.png");
            string assetsPathFinishJob = Path.Combine(projectRoot, "Assets", "icon_logout_operator.png");
            
            svgAddOrder.Source = new Avalonia.Media.Imaging.Bitmap(assetsPathAddOrder);
            svgExcludeOrder.Source = new Avalonia.Media.Imaging.Bitmap(assetsPathExludeOrder);
            svgFinishJob.Source = new Avalonia.Media.Imaging.Bitmap(assetsPathFinishJob);
            
            svgAddOrder.Height = 40;
            svgAddOrder.Width = 40;
            svgExcludeOrder.Height = 40;
            svgExcludeOrder.Width = 40;
            svgFinishJob.Height = 40;
            svgFinishJob.Width = 40;

            svgAddOrder.Margin = new Thickness(15, 15, 15, 15);
            svgFinishJob.Margin = new Thickness(15, 15, 15, 15);
            svgExcludeOrder.Margin = new Thickness(15, 15, 15, 15);

            addOrderDockPanel.Margin = new Thickness(120,0,0,0);
            finishJobDockPanel.Margin = new Thickness(120, 0, 0, 0);
            excludeFromOrderDockPanel.Margin = new Thickness(120, 0, 0, 0);

            addOrderDockPanel.Background = new SolidColorBrush(Avalonia.Media.Color.Parse("#7a92a1"));
            addOrderDockPanel.PointerEntered += (sender, args) => 
            {
                addOrderDockPanel.Background = new SolidColorBrush(Avalonia.Media.Color.Parse("#f0c30f"));
            };
            addOrderDockPanel.PointerExited += (sender, args) =>
            {
                addOrderDockPanel.Background = new SolidColorBrush(Avalonia.Media.Color.Parse("#7a92a1"));
            };

            excludeFromOrderDockPanel.Background = new SolidColorBrush(Avalonia.Media.Color.Parse("#7a92a1"));
            excludeFromOrderDockPanel.PointerEntered += (sender, args) =>
            {
                excludeFromOrderDockPanel.Background = new SolidColorBrush(Avalonia.Media.Color.Parse("#f0c30f"));
            };
            excludeFromOrderDockPanel.PointerExited += (sender, args) =>
            {
                excludeFromOrderDockPanel.Background = new SolidColorBrush(Avalonia.Media.Color.Parse("#7a92a1"));
            };

            finishJobDockPanel.Background = new SolidColorBrush(Avalonia.Media.Color.Parse("#7a92a1"));
            finishJobDockPanel.PointerEntered += (sender, args) =>
            {
                finishJobDockPanel.Background = new SolidColorBrush(Avalonia.Media.Color.Parse("#f0c30f"));
            };
            finishJobDockPanel.PointerExited += (sender, args) =>
            {
                finishJobDockPanel.Background = new SolidColorBrush(Avalonia.Media.Color.Parse("#7a92a1"));
            };

            var stackPanel = new StackPanel();
            var popup = new Popup();
            var stackPanelOrdersList = new StackPanel();
            var stackPanelList = new StackPanel();
            
            var textBlock1 = new TextBlock();
            var textBlock2 = new TextBlock();
            var textBlock3 = new TextBlock();
            
            textBlock1.Text = "Add to order";
            textBlock2.Text = "Finish job";
            textBlock3.Text = "Exclude from order";

            textBlock1.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center;
            textBlock2.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center;
            textBlock3.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center;

            textBlock2.Name = user.Id.ToString();
            userButton.Name = user.Id.ToString();
            //textBlock1.Margin = new Thickness(0, 0, 0, 0);
            //textBlock2.Margin = new Thickness(0, 0, 0, 0);
            userButton.Content = user.Initials;
            popup.IsOpen = false;
            userButton.Click += (sender, args) => 
            {
                stackPanelOrdersList.IsVisible = false;
                popup.IsOpen = !popup.IsOpen;
            };

            addOrderDockPanel.Children.Add(svgAddOrder);
            addOrderDockPanel.Children.Add(textBlock1);
            finishJobDockPanel.Children.Add(svgFinishJob);
            finishJobDockPanel.Children.Add(textBlock2);
            excludeFromOrderDockPanel.Children.Add(svgExcludeOrder);
            excludeFromOrderDockPanel.Children.Add(textBlock3);

            stackPanel.Children.Add(userButton);
            stackPanel.Children.Add(popup);
            popup.Child = stackPanelList;
            stackPanelList.Children.Add(addOrderDockPanel);
            stackPanelList.Children.Add(finishJobDockPanel);
            dockPanel.Children.Add(stackPanel);

            textBlock1.Tapped += (sender, args) =>
            {
                addOrderDockPanel.Children.Remove(stackPanelOrdersList);
                if (user.IsBusy == false && _ordersList.IsNullOrEmpty() == false)
                {
                    stackPanelOrdersList = new StackPanel();
                    stackPanelOrdersList.IsVisible = true;
                    foreach (var order in _ordersList) 
                    {
                        var orderTextBlock = new TextBlock();
                        orderTextBlock.Text = order.Id.ToString().Substring(0,6) + "    " + order.Carnumber;
                        var buttonOrderToAdd = new Button();
                        buttonOrderToAdd.Content = orderTextBlock;
                        stackPanelOrdersList.Children.Add(buttonOrderToAdd);
                        buttonOrderToAdd.Click += (sender, args) => 
                        {
                            user.IsBusy = true;
                            user.OrderID = order.Id.ToString();
                            stackPanelOrdersList.IsVisible = false;
                            stackPanelList.Children.Remove(addOrderDockPanel);
                            stackPanelList.Children.Remove(finishJobDockPanel);
                            stackPanelList.Children.Add(excludeFromOrderDockPanel);
                            stackPanelList.Children.Add(finishJobDockPanel);
                        };
                    }
                    //addOrderDockPanel = new DockPanel();
                    addOrderDockPanel.Children.Add(stackPanelOrdersList);
                }
                else 
                {
                    return;
                }
            };

            textBlock2.Tapped += (sender, args) => 
            {
                var userId = textBlock2.Name.ToString();
                LogoutOperator(userId);
                popup.IsOpen = false;
            };

            textBlock3.Tapped += (sender, args) => 
            {
                user.IsBusy = false;
                user.OrderID = "";
                stackPanelOrdersList.IsVisible = true;
                stackPanelList.Children.Remove(excludeFromOrderDockPanel);
                stackPanelList.Children.Remove(finishJobDockPanel);
                stackPanelList.Children.Add(addOrderDockPanel);
                stackPanelList.Children.Add(finishJobDockPanel);
            };
        }
        DockaPanelUserButtons = dockPanel;
    }

    private void OrderButtonExecute(List<OrderButtonRecord> orders)
    {
        var dockPanel = new DockPanel();
        foreach (OrderButtonRecord order in orders)
        {
            // Создаем кнопку
            var orderButton = new Button
            {
                Margin = new Thickness(0, 0, 3, 0),
                Name = order.Id.ToString()
            };

            // Загрузка изображения
            string projectRoot = Path.Combine(Environment.CurrentDirectory, @"..\..\..");
            string assetsPathOrder = Path.Combine(projectRoot, "Assets", "icon_shopping_cart_disabled.png");
            var svgOrder = new Image
            {
                Source = new Avalonia.Media.Imaging.Bitmap(assetsPathOrder),
                //Stretch = Stretch.Fill, // Stretch image
                Height = 40,
                Width = 40
            };

            // Create Grid holding image and text for button
            var grid = new Grid();
            grid.Height = 45;
            grid.Width = 45;

            // Add image as background
            grid.Children.Add(svgOrder);

            // Add text as foreground
            var textBlock = new TextBlock
            {
                Text = order.Carnumber.Substring(0,3),
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Right,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Bottom,
                Foreground = Brushes.Red, // text color
                FontSize = 7, // fontsize
                FontWeight = Avalonia.Media.FontWeight.Bold
            };
            grid.Children.Add(textBlock);

            // make grid as button content
            orderButton.Content = grid;

            // click holder
            orderButton.Click += (sender, args) =>
            {
                Guid id = Guid.Parse(orderButton.Name);
                OrderData = Washes.FindWashById(id);

                if (OrderData.GoodsList.IsNullOrEmpty() == true)
                {
                    OrderGoodsTableVisible = false;
                }
                else 
                {
                    OrderGoodsTableVisible = true;
                }
                if (OrderData.ServiceList.IsNullOrEmpty() == true)
                {
                    OrderServicesTableVisible = false;
                }
                else 
                {
                    OrderServicesTableVisible = true;
                }
                foreach (var user in _loggedInUsers) 
                {
                    var debug = true;
                    if (user.OrderID.Equals("") == false) 
                    {
                        if (Guid.Parse(user.OrderID) == id)
                        {
                            OrderCompletedButtonEnabled = true;
                        }
                        else 
                        {
                            OrderCompletedButtonEnabled = false;
                        }                       
                    }
                }
                CurrentPage = new OrderScreen();
            };

            orderButton.Height = 45;
            orderButton.Width = 45;

            // Добавляем кнопку в DockPanel
            dockPanel.Children.Add(orderButton);
        }

        // assign DockaPanelOrderButtons
        DockaPanelOrderButtons = dockPanel;
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
        if (foundUser.IsBusy == false) 
        {
            _loggedInUsers.Remove(foundUser);
        }
        if (_loggedInUsers.IsNullOrEmpty() == true) 
        {
            App.ShutdownApplication();
        }
        else 
        {
            AddOperatorButtonExecute(_loggedInUsers);
        }
    }

    private void OrderCompleteStatusSaveExecute(Guid id) 
    {
        Washes.WashCompleted(id);
        foreach (var user in _loggedInUsers) 
        {
            if (Guid.Parse(user.OrderID) == id) 
            {
                user.IsBusy = false;
                user.OrderID = "";
            }
        }
        RemoveOrderFromOrderList(id);
        OrderButtonExecute(_ordersList);
        AddOperatorButtonExecute(_loggedInUsers);
        OrderCompletedButtonEnabled = false;
        CurrentPage = new VehicleScreen();
    }

    private void RemoveOrderFromOrderList(Guid id) 
    {
        var founOrder = _ordersList.FirstOrDefault(u => u?.Id == id);
        _ordersList.Remove(founOrder);
    }

    private void HideDashboard()
    {
        if (IsDashboardVisible) IsDashboardVisible = false;
    }


}