using Duckie2Client.Views.Base;

namespace Duckie2Client.Views.Screens.ManagerConsole;

public partial class CompaniesScreenView : TabUserControlView
{
    public CompaniesScreenView()
    {
        InitializeComponent();
        DataContext = App.VM_CompaniesScreen;
    }

    public override void OnTabClose(string message)
    {
        App.VM_CompaniesScreen.OnScreenClose();
    }
}