using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using DialogHostAvalonia;
using Duckie2Client.Models;
using Duckie2Client.ViewModels.Base;
using Duckie2Client.Views.Dialogs;
using Microsoft.IdentityModel.Tokens;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Duckie2Client.ViewModels.Screens.ManagerConsole.ClientCardBatchAddScreen;

public class ClientCardBatchAddScreenViewModel : ViewModelBase, ITabViewModel<List<string>>
{
    private const string DIALOG_IDENTIFIER = "ClientCardBatchAddScreenDialogs";

    [Reactive] public List<string>? DataPayload { get; set; }
    public ObservableCollection<ClientCardBatchItem>? ClientClientCardBatchAddItems { get; set; }
    public ReactiveCommand<TextBox, Unit> AddVehicleLicenceCommand { get; }
    public ReactiveCommand<Unit, Unit> RemoveItemCommand { get; }
    public ReactiveCommand<Unit, Unit> ClearListCommand { get; }

    public ClientCardBatchAddScreenViewModel()
    {
        AddVehicleLicenceCommand = ReactiveCommand.Create<TextBox>(AddVehicleLicenceExecute);
        RemoveItemCommand = ReactiveCommand.Create(RemoveItemExecute);
        ClearListCommand = ReactiveCommand.Create(ClearListExecute);
        ClientClientCardBatchAddItems = [];
    }

    /// <summary>
    /// Removes selected rows in a table containing a list of customers to be added to the database.
    /// </summary>
    private void ClearListExecute()
    {
        foreach (var client in _newClients.Where(client => client.IsSelected).ToList()) _newClients.Remove(client);
        ClientsToAdd = new ObservableCollection<ClientCardBatchItem>(_newClients);
    }

    private void RemoveItemExecute()
    {
        // todo: implement
        Console.WriteLine(@"Item will be removed.");
    }


    [Reactive] public string ClientFirstNameValue { get; set; }
    [Reactive] public string ClientLastNameValue { get; set; }
    [Reactive] public string ClientPhoneValue { get; set; }
    [Reactive] public string CompanyNameValue { get; set; }
    [Reactive] public string NewCompanyNameValue { get; set; }
    [Reactive] public string VehicleLicenceValue { get; set; }
    [Reactive] public object VehiclePriceCategoryValue { get; set; }
    [Reactive] public string VehicleDiscountValue { get; set; }

    [Reactive] public ObservableCollection<ClientCardBatchItem> ClientsToAdd { get; set; }

    private readonly List<ClientCardBatchItem> _newClients = [];

    private void AddVehicleLicenceExecute(TextBox textBox)
    {
        try
        {
            var newItem = new ClientCardBatchItem(
                ClientFirstNameValue,
                ClientLastNameValue,
                GetClientPhone(),
                GetCompanyName(),
                GetVehicleLicence(),
                GetVehiclePriceCategory(),
                GetVehicleDiscount());
            _newClients.Add(newItem);
        }
        // todo: refact: Make Duckie Exception.
        catch (Exception e)
        {
            ShowErrorMessageDialog(e.Message);
            // todo: Put the focus to a control with an error.
            return;
        }

        ClientsToAdd = new ObservableCollection<ClientCardBatchItem>(_newClients);

        // --- 
        textBox.Clear();
        textBox.Focus();
        // --- 
    }

    private async void ShowErrorMessageDialog(string message)
    {
        // todo: message localization
        // var msg = Localization.GetString(() => ErrorMessages._301_UserHasNoAccessRights, ResourceTypes.ErrorMessages);
        var errorDialog = new ErrorDialog(message);
        await DialogHost.Show(errorDialog, DIALOG_IDENTIFIER);
    }

    private int GetVehicleDiscount()
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (VehicleDiscountValue == null)
            throw new Exception(@"no vehicle discount.");

        var isParsed = int.TryParse(VehicleDiscountValue, out var result);
        if (!isParsed)
            throw new Exception(@"Cannot convert the vehicle discount value.");

        return result;
    }

// todo: refact: GetClientPhone, GetVehicleLicence, GetVehiclePriceCategory - по сути, одинаковый код.

    private string GetVehiclePriceCategory()
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        var result = VehiclePriceCategoryValue != null
            ? ((ComboBoxItem)VehiclePriceCategoryValue).Content as string
            : "";
        if (result.IsNullOrEmpty())
            throw new Exception(@"no vehicle discount");

        return result!;
    }


    private string GetClientPhone()
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        var result = ClientPhoneValue != null ? ClientPhoneValue.Trim() : "";

        if (result.IsNullOrEmpty())
            throw new Exception(@"no client phone");

        return result;
    }

    private string GetVehicleLicence()
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        var result = VehicleLicenceValue != null ? VehicleLicenceValue.Trim() : "";
        if (result.IsNullOrEmpty())
            throw new Exception(@"no vehicle licence");

        return result;
    }

    private string GetCompanyName()
    {
        // If a company name is entered in an appropriate text box,
        // this value takes precedence over the value from the drop-down list of company names.

        string result;

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (NewCompanyNameValue != null)
            result = NewCompanyNameValue.Trim().IsNullOrEmpty() ? CompanyNameValue : NewCompanyNameValue.Trim();
        else
            result = CompanyNameValue;

        if (result.Trim().IsNullOrEmpty())
            throw new Exception(@"no company name");

        return result;
    }

    public void OnScreenClose()
    {
        Console.WriteLine(@"batch service close");
    }
}