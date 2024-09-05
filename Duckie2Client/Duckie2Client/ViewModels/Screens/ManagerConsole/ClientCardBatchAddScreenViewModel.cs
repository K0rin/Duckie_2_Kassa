using System;
using System.Collections.ObjectModel;
using System.Reactive;
using Avalonia.Controls;
using Duckie2Client.Models;
using Duckie2Client.ViewModels.Base;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Duckie2Client.ViewModels.Screens.ManagerConsole;

public class ClientCardBatchAddScreenViewModel : ViewModelBase
{
    public ObservableCollection<RemovableListItem>? VehicleLicensesItems { get; set; }
    [Reactive] public string VehicleLicenceTextBoxValue { get; set; }
    public ReactiveCommand<TextBox, Unit> AddVehicleLicenceCommand { get; }

    public ClientCardBatchAddScreenViewModel()
    {
        AddVehicleLicenceCommand = ReactiveCommand.Create<TextBox>(AddVehicleLicenceExecute);
        VehicleLicensesItems = [];
    }


    private void AddVehicleLicenceExecute(TextBox textBox)
    {
        var newLicence = new RemovableListItem(VehicleLicenceTextBoxValue);
        VehicleLicensesItems?.Add(newLicence);

        // --- 
        textBox.Clear();
        textBox.Focus();
        // --- 
    }

    public void OnScreenClose()
    {
        Console.WriteLine("batch service close");
    }
}