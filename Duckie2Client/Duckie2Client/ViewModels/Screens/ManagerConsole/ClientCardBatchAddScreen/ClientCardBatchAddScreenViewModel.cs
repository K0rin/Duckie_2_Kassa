using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using Avalonia.Controls;
using Duckie2Client.Models;
using Duckie2Client.ViewModels.Base;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Duckie2Client.ViewModels.Screens.ManagerConsole.ClientCardBatchAddScreen;

public class ClientCardBatchAddScreenViewModel : ViewModelBase, ITabViewModel<List<string>>
{
    [Reactive] public List<string>? DataPayload { get; set; }
    public ObservableCollection<ClientCardBatchItem>? ClientClientCardBatchAddItems { get; set; }
    public ReactiveCommand<TextBox, Unit> AddVehicleLicenceCommand { get; }

    public ClientCardBatchAddScreenViewModel()
    {
        AddVehicleLicenceCommand = ReactiveCommand.Create<TextBox>(AddVehicleLicenceExecute);
        ClientClientCardBatchAddItems = [];
    }


    [Reactive] public string ClientFirstNameValue { get; set; }
    [Reactive] public string ClientLastNameValue { get; set; }
    [Reactive] public string ClientPhoneValue { get; set; }
    [Reactive] public string CompanyNameValue { get; set; }
    [Reactive] public string NewCompanyNameValue { get; set; }
    [Reactive] public string VehicleLicenceValue { get; set; }
    [Reactive] public object VehiclePriceCategoryValue { get; set; }
    [Reactive] public string VehicleDiscountValue { get; set; }

    public ObservableCollection<ClientCardBatchItem> ClientsToAdd { get; set; }
    private readonly List<ClientCardBatchItem> _newClients = [];

    private void AddVehicleLicenceExecute(TextBox textBox)
    {
        var isDiscountSuccessfully = int.TryParse(VehicleDiscountValue, out var vehicleDiscount);
        if (!isDiscountSuccessfully)
        {
            // todo: Show error message to user.
            Console.WriteLine(@"Cannot convert the vehicle discount value.");
            return;
        }

        // todo: Restriction to a discount value.

        // todo: If there is a new firm name entered, do not treat the company name combobox.

        // todo: change to DataGrid.
        // https://docs.avaloniaui.net/docs/reference/controls/datagrid

        var vehiclePriceCategory = ((ComboBoxItem)VehiclePriceCategoryValue).Content as string;

        var newItem = new ClientCardBatchItem(
            ClientFirstNameValue,
            ClientLastNameValue,
            ClientPhoneValue,
            CompanyNameValue,
            VehicleLicenceValue,
            vehiclePriceCategory,
            vehicleDiscount);

        _newClients.Add(newItem);
        // ClientClientCardBatchAddItems?.Add(newLicence);

        ClientsToAdd = new ObservableCollection<ClientCardBatchItem>(_newClients);

        // --- 
        textBox.Clear();
        textBox.Focus();
        // --- 
    }

    public void OnScreenClose()
    {
        Console.WriteLine(@"batch service close");
    }
}