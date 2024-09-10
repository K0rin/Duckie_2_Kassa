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
        [
            ClientPhoneTextBox,
            ClientNewFirmNameTextBox,
            ClientFirmName,
            VehicleLicenceTextBox,
            VehicleCategoryComboBox,
            VehicleDiscount
        ];
    }

    public override void OnTabClose(string message)
    {
        App.VM_ClientCardBatchAdd.OnScreenClose();
    }
}