using Duckie2Client.Views.Base;

namespace Duckie2Client.Views.Screens.ManagerConsole;

public partial class BranchesScreenView : TabUserControlView
{
    public BranchesScreenView()
    {
        InitializeComponent();
        DataContext = App.VM_BranchesScreen;
    }

    public override void OnTabClose(string message)
    {
        App.VM_PersonnelScreen.OnScreenClose();
    }
}