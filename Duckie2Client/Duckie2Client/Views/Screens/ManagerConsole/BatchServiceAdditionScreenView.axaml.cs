using Duckie2Client.Views.Base;

namespace Duckie2Client.Views.Screens.ManagerConsole;

/// <summary>
/// Represents a screen for batch adding one or more services to a client in a certain list.
/// </summary>
public partial class BatchServiceAddingScreenView : TabUserControlView
{
    public BatchServiceAddingScreenView()
    {
        InitializeComponent();
        DataContext = App.VM_BatchServiceAdding;
    }

    public override void OnTabClose(string message)
    {
        App.VM_BatchServiceAdding.OnScreenClose();
    }
}