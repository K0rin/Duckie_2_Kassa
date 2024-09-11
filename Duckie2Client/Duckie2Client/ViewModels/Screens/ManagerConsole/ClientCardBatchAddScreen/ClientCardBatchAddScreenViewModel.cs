using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using DialogHostAvalonia;
using Duckie2Client.Libs;
using Duckie2Client.Libs.Enums;
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

    [Reactive] public List<string>? DataPayload { get; set; } = [];

    public ObservableCollection<ClientCardBatchItem>? ClientClientCardBatchAddItems { get; set; }
    public ReactiveCommand<Unit, Unit> AddNewClientCommand { get; }
    public ReactiveCommand<Unit, Unit> RemoveItemCommand { get; }
    public ReactiveCommand<Unit, Unit> ClearListCommand { get; }

    public Dictionary<string, Control> RequiredControls
    {
        set
        {
        // Matching controls that are checked for data to error codes.
        // Necessary to set focus on a specific control.
        _requiredControlsDictionary.Add((int)ErrorCodes.ClientPhoneNotSpecified,
            value["ClientPhoneTextBox"]);
        _requiredControlsDictionary.Add((int)ErrorCodes.VehicleLicenceNotSpecified,
            value["VehicleLicenceTextBox"]);
        _requiredControlsDictionary.Add((int)ErrorCodes.VehiclePriceCategoryNotSpecified,
            value["VehicleCategoryComboBox"]);
        _requiredControlsDictionary.Add((int)ErrorCodes.VehicleDiscountNotSpecified,
            value["VehicleDiscount"]);
        }
    }

    private ErrorDialog _errorDialog;
    private readonly Dictionary<int, Control> _requiredControlsDictionary = new();

    public ClientCardBatchAddScreenViewModel()
    {
        AddNewClientCommand = ReactiveCommand.Create(AddNewClientExecute);
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


    private void AddNewClientExecute()
    {
        try
        {
            var newItem = new ClientCardBatchItem(
                GetClientName(ClientFirstNameValue),
                GetClientName(ClientLastNameValue),
                GetClientPhone(),
                GetCompanyName(),
                GetVehicleLicence(),
                GetVehiclePriceCategory(),
                GetVehicleDiscount());
            _newClients.Add(newItem);
        }
        catch (DuckieException e)
        {
            ShowErrorAndFocus(e);
            return;
        }

        ClientsToAdd = new ObservableCollection<ClientCardBatchItem>(_newClients);

        // --- 
        // textBox.Clear();
        // textBox.Focus();
        // --- 
    }

    private static NullOrResult GetClientName(string value)
    {
        var result = value.IsNullOrEmpty() ? new NullOrResult() : new NullOrResult { Result = value };
        return result;
    }

    private async void ShowErrorAndFocus(DuckieException exception)
    {
        _errorDialog = new ErrorDialog(exception.Message);
        var dialogResult = (DialogButtons)(await DialogHost.Show(_errorDialog, DIALOG_IDENTIFIER))!;

        // Put the focus to a control with an error.
        if (dialogResult.Equals(DialogButtons.OK)) _requiredControlsDictionary[exception.ErrorNumber].Focus();
    }

// todo: refact: GetClientPhone, GetVehicleLicence, GetVehiclePriceCategory, GetVehicleDiscount - по сути, одинаковый код.

    private int GetVehicleDiscount()
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (VehicleDiscountValue == null)
            throw new DuckieException(ErrorCodes.VehicleDiscountNotSpecified);

        var result = int.Parse(VehicleDiscountValue);

        return result;
    }

    private string GetVehiclePriceCategory()
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        var result = VehiclePriceCategoryValue != null
            ? ((ComboBoxItem)VehiclePriceCategoryValue).Content as string
            : "";
        if (result.IsNullOrEmpty())
            throw new DuckieException(ErrorCodes.VehiclePriceCategoryNotSpecified);

        return result!;
    }

    private string GetClientPhone()
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        var result = ClientPhoneValue != null ? ClientPhoneValue.Trim() : "";

        if (result.IsNullOrEmpty())
            throw new DuckieException(ErrorCodes.ClientPhoneNotSpecified);

        return result;
    }

    private string GetVehicleLicence()
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        var result = VehicleLicenceValue != null ? VehicleLicenceValue.Trim() : "";
        if (result.IsNullOrEmpty())
            throw new DuckieException(ErrorCodes.VehicleLicenceNotSpecified);

        return result;
    }

    private NullOrResult GetCompanyName()
    {
        var result = new NullOrResult();
        var isNoCompany = NewCompanyNameValue.IsNullOrEmpty() && CompanyNameValue.IsNullOrEmpty();

        // If there is no company name listed, then a private client is being registered.
        if (isNoCompany) return result;

        // If a company name is entered in an appropriate text box,
        // this value takes precedence over the value from the drop-down list of company names.

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        var companyName = NewCompanyNameValue != null
            ? NewCompanyNameValue.Trim().IsNullOrEmpty() ? CompanyNameValue : NewCompanyNameValue.Trim()
            : CompanyNameValue;

        result = new NullOrResult { Result = companyName };
        return result;
    }

    public void OnScreenClose()
    {
        // todo: Запрос на сохранение не сохраненных данных.
        Console.WriteLine(@"batch service close");
    }
}