using Duckie2Client.Views.Base;

namespace Duckie2Client.Views.Screens.ManagerConsole;

public partial class ClientsScreenView : TabUserControlView
{
    public ClientsScreenView()
    {
        InitializeComponent();
        DataContext = App.VM_ClientsScreen;
    }

    public override void OnTabClose(string message)
    {
        App.VM_PersonnelScreen.OnScreenClose();
    }
}