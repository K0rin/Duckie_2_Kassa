using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Duckie2Client.ViewModels.Screens;

namespace Duckie2Client.Controls.Kassa;

public partial class NewClient : UserControl
{
    public NewClient()
    {
        InitializeComponent();
    }

    private void NewClientBackButtonCursorEntered(object? sender, PointerEventArgs e)
    {
        var BackButton = this.FindControl<Button>("BackButton");
        if (BackButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.NewClientBackButtonCursor(true);
            }
        }
        else
        {
            return;
        }
    }

    private void NewClientBackButtonCursorExited(object? sender, PointerEventArgs e)
    {
        var BackButton = this.FindControl<Button>("BackButton");
        if (BackButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.NewClientBackButtonCursor(false);
            }
        }
        else
        {
            return;
        }
    }

    private void SaveNewClientButtonCursorEntered(object? sender, PointerEventArgs e)
    {
        var SaveButton = this.FindControl<Button>("SaveNewClient");
        if (SaveButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.SaveNewClientButtonCursor(true);
            }
        }
        else
        {
            return;
        }
    }

    private void SaveNewClientButtonCursorExited(object? sender, PointerEventArgs e)
    {
        var SaveButton = this.FindControl<Button>("SaveNewClient");
        if (SaveButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.SaveNewClientButtonCursor(false);
            }
        }
        else
        {
            return;
        }
    }
}