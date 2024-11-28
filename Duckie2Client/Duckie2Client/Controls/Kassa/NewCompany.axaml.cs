using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Duckie2Client.ViewModels.Screens;

namespace Duckie2Client.Controls.Kassa;

public partial class NewCompany : UserControl
{
    public NewCompany()
    {      
        InitializeComponent();
    }

    private void NewCompanyBackButtonCursorEntered(object? sender, PointerEventArgs e)
    {
        var BackButton = this.FindControl<Button>("BackButton");
        if (BackButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.NewCompanyBackButtonCursor(true);
            }
        }
        else
        {
            return;
        }
    }

    private void NewCompanyBackButtonCursorExited(object? sender, PointerEventArgs e)
    {
        var BackButton = this.FindControl<Button>("BackButton");
        if (BackButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.NewCompanyBackButtonCursor(false);
            }
        }
        else
        {
            return;
        }
    }

    private void SaveNewCompanyButtonCursorEntered(object? sender, PointerEventArgs e)
    {
        var SaveButton = this.FindControl<Button>("SaveNewCompany");
        if (SaveButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.SaveNewCompanyButtonCursor(true);
            }
        }
        else
        {
            return;
        }
    }

    private void SaveNewCompanyButtonCursorExited(object? sender, PointerEventArgs e)
    {
        var SaveButton = this.FindControl<Button>("SaveNewCompany");
        if (SaveButton.IsEnabled == true)
        {
            if (DataContext is MainKassaScreenViewModel viewModel)
            {
                viewModel.SaveNewCompanyButtonCursor(false);
            }
        }
        else
        {
            return;
        }
    }
}