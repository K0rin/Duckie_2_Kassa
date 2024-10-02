using Duckie2Client.Views.Base;

namespace Duckie2Client.Views.Screens.ManagerConsole;

public partial class PollutionLevelsScreenView : TabUserControlView
{
    public PollutionLevelsScreenView()
    {
        InitializeComponent();
        DataContext = App.VM_PollutionLevelsScreen;
    }

    public override void OnTabClose(string message)
    {
        App.VM_PersonnelScreen.OnScreenClose();
    }
}