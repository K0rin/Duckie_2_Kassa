using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Duckie2Client.ViewModels.Screens;

namespace Duckie2Client.Controls.Kassa;

public partial class FindCompanyName : UserControl
{
    public FindCompanyName()
    {
        InitializeComponent();
    }

    private void CompanySerachBackButtonCursorEntered(object? sender, PointerEventArgs e)
    {
        var CompanySearchBackButton = this.FindControl<Button>("CompanySearchBackButton");
        if (CompanySearchBackButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.CompanySearchBackButtonCursor(true);
            }
        }
        else
        {
            return;
        }
    }

    private void CompanySerachBackButtonCursorExited(object? sender, PointerEventArgs e)
    {
        var CompanySearchBackButton = this.FindControl<Button>("CompanySearchBackButton");
        if (CompanySearchBackButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.CompanySearchBackButtonCursor(false);
            }
        }
        else
        {
            return;
        }
    }

    private void CompanySearchButtonCursorEntered(object? sender, PointerEventArgs e)
    {
        var CompanySearchButton = this.FindControl<Button>("CompanySearchButton");
        if (CompanySearchButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.CompanySearchButtonCursor(true);
            }
        }
        else
        {
            return;
        }
    }

    private void CompanySearchButtonCursorExited(object? sender, PointerEventArgs e)
    {
        var CompanySearchButton = this.FindControl<Button>("CompanySearchButton");
        if (CompanySearchButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.CompanySearchButtonCursor(false);
            }
        }
        else
        {
            return;
        }
    }
}