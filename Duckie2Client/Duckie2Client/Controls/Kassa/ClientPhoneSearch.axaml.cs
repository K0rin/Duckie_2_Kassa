using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Duckie2Client.ViewModels.Screens;

namespace Duckie2Client.Controls.Kassa;

public partial class ClientPhoneSearch : UserControl
{
    public ClientPhoneSearch()
    {
        InitializeComponent();
    }

    private void ClientPhoneSerachButtonCursorEntered(object? sender, PointerEventArgs e)
    {
        var ClientPhoneSearchButton = this.FindControl<Button>("ClientPhoneSearchButton");
        if (ClientPhoneSearchButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.ClientPhoneSearchButtonCursor(true);
            }
        }
        else
        {
            return;
        }
    }

    private void ClientPhoneSerachButtonCursorExited(object? sender, PointerEventArgs e)
    {
        var ClientPhoneSearchButton = this.FindControl<Button>("ClientPhoneSearchButton");
        if (ClientPhoneSearchButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.ClientPhoneSearchButtonCursor(false);
            }
        }
        else
        {
            return;
        }
    }

    private void ClientPhoneSerachBackButtonCursorEntered(object? sender, PointerEventArgs e)
    {
        var ClientPhoneBackSearchButton = this.FindControl<Button>("ClientPhoneSearchBackButton");
        if (ClientPhoneBackSearchButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.ClientPhoneSearchBackButtonCursor(true);
            }
        }
        else
        {
            return;
        }
    }

    private void ClientPhoneSerachBackButtonCursorExited(object? sender, PointerEventArgs e)
    {
        var ClientPhoneBackSearchButton = this.FindControl<Button>("ClientPhoneSearchBackButton");
        if (ClientPhoneBackSearchButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.ClientPhoneSearchBackButtonCursor(false);
            }
        }
        else
        {
            return;
        }
    }
}