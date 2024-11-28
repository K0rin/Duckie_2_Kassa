using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Duckie2Client.ViewModels.Screens;

namespace Duckie2Client.Controls.Kassa;

public partial class CompanyConnectedWithVehicle : UserControl
{
    public CompanyConnectedWithVehicle()
    {
        InitializeComponent();
    }

    private void CompanyScreenNextButtonCursorEntered(object? sender, PointerEventArgs e)
    {
        var NextButton = this.FindControl<Button>("NextButton");
        if (NextButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.CompanyScreenNextButtonCursor(true);
            }
        }
        else
        {
            return;
        }
    }

    private void CompanyScreenNextButtonCursorExited(object? sender, PointerEventArgs e)
    {
        var NextButton = this.FindControl<Button>("NextButton");
        if (NextButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.CompanyScreenNextButtonCursor(false);
            }
        }
        else
        {
            return;
        }
    }

    private void CompanyScreenBackButtonCursorEntered(object? sender, PointerEventArgs e)
    {
        var BackButton = this.FindControl<Button>("BackButton");
        if (BackButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.CompanyScreenBackButtonCursor(true);
            }
        }
        else
        {
            return;
        }
    }

    private void CompanyScreenBackButtonCursorExited(object? sender, PointerEventArgs e)
    {
        var BackButton = this.FindControl<Button>("BackButton");
        if (BackButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.CompanyScreenBackButtonCursor(false);
            }
        }
        else
        {
            return;
        }
    }

    private void AnotherCompanyButtonCursorEntered(object? sender, PointerEventArgs e)
    {
        var anotherCompanyButton = this.FindControl<Button>("AnotherCompany");
        if (anotherCompanyButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.AnotherCompanyButtonCursor(true);
            }
        }
        else
        {
            return;
        }
    }

    private void AnotherCompanyBackButtonCursorExited(object? sender, PointerEventArgs e)
    {
        var anotherCompanyButton = this.FindControl<Button>("AnotherCompany");
        if (anotherCompanyButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.AnotherCompanyButtonCursor(false);
            }
        }
        else
        {
            return;
        }
    }
}