using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading;
using Avalonia.Controls;
using Duckie2Client.Models;
using Duckie2Client.Services.Controls;
using Duckie2Client.ViewModels.Base;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Duckie2Client.ViewModels.Screens.ManagerConsole;

// todo: abstract class for other tabs.

// ReSharper disable once ClassNeverInstantiated.Global
public class BatchServiceAddingScreenViewModel : ViewModelBase
{
    public ObservableCollection<RemovableListItem>? VehicleLicensesItems { get; set; }
    [Reactive] public string VehicleLicenceTextBoxValue { get; set; }
    [Reactive] public bool IsUpdateDataContainerVisible { get; set; }
    [Reactive] public bool IsDataLoadingContainerVisible { get; set; }
    [Reactive] public bool IsDataContainerVisible { get; set; }
    public ReactiveCommand<TextBox, Unit> AddVehicleLicenceCommand { get; }
    public ReactiveCommand<Unit, Unit> UpdateDateCommand { get; }
    private readonly TabContext _tabContext = new(new ReadyLoadDataState());

    public BatchServiceAddingScreenViewModel()
    {
        AddVehicleLicenceCommand = ReactiveCommand.Create<TextBox>(AddVehicleLicenceExecute);
        UpdateDateCommand = ReactiveCommand.Create(UpdateDateExecute);
        VehicleLicensesItems = [];
        IsUpdateDataContainerVisible = true;
        IsDataLoadingContainerVisible = false;
        IsDataContainerVisible = false;
    }

    private void UpdateDateExecute()
    {
        _tabContext.SetState(new DataLoadingState());
        // _tabContext.LoadData();

        // IsUpdateDataContainerVisible = false;
        // IsDataLoadingContainerVisible = true;

        // Thread.Sleep(5000);
        //
        // IsDataLoadingContainerVisible = false;
        // IsDataContainerVisible = true;
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