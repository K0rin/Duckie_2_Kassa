using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Duckie2Client.ViewModels.Screens;
using System;

namespace Duckie2Client.Controls.Kassa;

public partial class OrdersPerYearScreen : UserControl
{
    public OrdersPerYearScreen()
    {
        InitializeComponent();
    }

    private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (DataContext is MainKassaScreenViewModel viewModel)
        {
            viewModel.SelectCLientExecute(sender,e);
        }
    }

    private void VehicleClientBackButtonCursorEntered(object? sender, PointerEventArgs e)
    {
        var VehicleClientBackButton = this.FindControl<Button>("VehicleClientBackButton");
        if (VehicleClientBackButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.VehicleClientBackButtonCursor(true);
            }
        }
        else
        {
            return;
        }
    }

    private void VehicleClientBackButtonCursorExited(object? sender, PointerEventArgs e)
    {
        var VehicleCLientBackButton = this.FindControl<Button>("VehicleClientBackButton");
        if (VehicleCLientBackButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.VehicleClientBackButtonCursor(false);
            }
        }
        else
        {
            return;
        }
    }

    private void AnotherClientButtonCursorEntered(object? sender, PointerEventArgs e)
    {
        var AnotherClientButton = this.FindControl<Button>("AnotherClientButton");
        if (AnotherClientButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.AnotherClientButtonCursor(true);
            }
        }
        else
        {
            return;
        }
    }

    private void AnotherClientButtonCursorExited(object? sender, PointerEventArgs e)
    {
        var AnotherClientButton = this.FindControl<Button>("AnotherClientButton");
        if (AnotherClientButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.AnotherClientButtonCursor(false);
            }
        }
        else
        {
            return;
        }
    }

    private void VehicleScreenNextButtonCursorEntered(object? sender, PointerEventArgs e)
    {
        var NextButton = this.FindControl<Button>("NextButton");
        if (NextButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.VehicleClientsScreenNextButtonCursor(true);
            }
        }
        else
        {
            return;
        }
    }

    private void VehicleScreenNextButtonCursorExited(object? sender, PointerEventArgs e)
    {
        var NextButton = this.FindControl<Button>("NextButton");
        if (NextButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.VehicleClientsScreenNextButtonCursor(false);
            }
        }
        else
        {
            return;
        }
    }


}