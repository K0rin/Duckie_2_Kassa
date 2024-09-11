using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Input;
using Duckie2Client.ViewModels.Screens.ManagerConsole.ClientCardBatchAddScreen;
using Duckie2Client.Views.Base;

namespace Duckie2Client.Views.Screens.ManagerConsole;

public partial class ClientCardBatchAddScreenView : TabUserControlView
{
    public ClientCardBatchAddScreenView()
    {
        InitializeComponent();
        DataContext = App.VM_ClientCardBatchAdd;

        // Pass references to controls in ModelView whose data is checked for presence.
        ((ClientCardBatchAddScreenViewModel)DataContext).RequiredControls =
            new Dictionary<string, Control>
            {
                { ClientPhoneTextBox.Name!, ClientPhoneTextBox },
                { VehicleLicenceTextBox.Name!, VehicleLicenceTextBox },
                { VehicleCategoryComboBox.Name!, VehicleCategoryComboBox },
                { VehicleDiscount.Name!, VehicleDiscount }
            };
    }

    public override void OnTabClose(string message)
    {
        App.VM_ClientCardBatchAdd.OnScreenClose();
    }


    private void VehicleDiscount_OnKeyDown(object? sender, KeyEventArgs e)
    {
        // The text field accepts only digits.
        if ((int)e.Key < 34 || (int)e.Key > 43) e.Handled = true;
    }
}