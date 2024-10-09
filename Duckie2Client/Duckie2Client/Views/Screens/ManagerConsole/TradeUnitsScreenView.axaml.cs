using Duckie2Client.Views.Base;

namespace Duckie2Client.Views.Screens.ManagerConsole;

public partial class TradeUnitsScreenView : TabUserControlView
{
    public TradeUnitsScreenView()
    {
        InitializeComponent();
        DataContext = App.VM_TradeUnitsScreen;
    }

    public override void OnTabClose(string message)
    {
        App.VM_PersonnelScreen.OnScreenClose();
    }
}