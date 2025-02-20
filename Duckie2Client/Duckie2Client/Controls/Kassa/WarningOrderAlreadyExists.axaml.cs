using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Duckie2Client.ViewModels.Screens;

namespace Duckie2Client.Controls.Kassa;

public partial class WarningOrderAlreadyExists : UserControl
{
    public WarningOrderAlreadyExists()
    {
        InitializeComponent();
    }

    private void PrivateButtonCursorEntered(object? sender, PointerEventArgs e)
    {
        var PrivateButton = this.FindControl<Button>("PrivateButton");
        if (PrivateButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.PrivateButtonCursor(true);
            }
        }
        else
        {
            return;
        }
    }

    private void PrivateButtonCursorExited(object? sender, PointerEventArgs e)
    {
        var PrivateButton = this.FindControl<Button>("PrivateButton");
        if (PrivateButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.PrivateButtonCursor(false);
            }
        }
        else
        {
            return;
        }
    }

    private void EmptyButtonCursorEntered(object? sender, PointerEventArgs e)
    {
        var EmptyButton = this.FindControl<Button>("EmptyButton");
        if (EmptyButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.EmptyButtonCursor(true);
            }
        }
        else
        {
            return;
        }
    }

    private void EmptyButtonCursorExited(object? sender, PointerEventArgs e)
    {
        var EmptyButton = this.FindControl<Button>("EmptyButton");
        if (EmptyButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.EmptyButtonCursor(false);
            }
        }
        else
        {
            return;
        }
    }

    private void CompanyButtonCursorEntered(object? sender, PointerEventArgs e)
    {
        var CompanyButton = this.FindControl<Button>("CompanyButton");
        if (CompanyButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.CompanyButtonCursor(true);
            }
        }
        else
        {
            return;
        }
    }

    private void CompanyButtonCursorExited(object? sender, PointerEventArgs e)
    {
        var EmptyButton = this.FindControl<Button>("CompanyButton");
        if (EmptyButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.CompanyButtonCursor(false);
            }
        }
        else
        {
            return;
        }
    }
}