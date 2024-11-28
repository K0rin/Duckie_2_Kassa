using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Duckie2Client.ViewModels.Screens;
using System;
using System.ComponentModel;

namespace Duckie2Client.Views.Kassa;

public partial class OrderCheckoutPanel1 : UserControl
{

    public OrderCheckoutPanel1()
    {
        InitializeComponent();
    }

    private void ClientBonusButtonCursorEntered(object? sender, PointerEventArgs e)
    {
        var bonusButton = this.FindControl<Button>("BonusButton");
        if (bonusButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.ClientBonusButtonCursorEnteredExecute();
            }
        }
        else 
        { 
            return;
        }
    }

    private void ClientBonusButtonCursorExited(object? sender, PointerEventArgs e)
    {
        var bonusButton = this.FindControl<Button>("BonusButton");
        if (bonusButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.ClientBonusButtonCursorExitedExecute();
            }
        }
        else
        {
            return;
        }
    }

    private void OnPointerLeave(object? sender, PointerEventArgs e)
    {
        Console.WriteLine("Курсор покинул кнопку");
    }
}