using Duckie2Client.Views.Base;

namespace Duckie2Client.Views.Screens.ManagerConsole;

public partial class PriceTypesScreenView : TabUserControlView
{
    public PriceTypesScreenView()
    {
        InitializeComponent();
        DataContext = App.VM_PriceTypesScreen;
    }

    public override void OnTabClose(string message)
    {
        App.VM_PersonnelScreen.OnScreenClose();
    }
}