using Duckie2Client.Views.Base;

namespace Duckie2Client.Views.Screens.ManagerConsole;

public partial class PersonnelScreenView : TabUserControlView
{
    public PersonnelScreenView()
    {
        InitializeComponent();
        DataContext = App.VM_PersonnelScreen;
    }

    public override void OnTabClose(string message)
    {
        App.VM_PersonnelScreen.OnScreenClose();
    }
}