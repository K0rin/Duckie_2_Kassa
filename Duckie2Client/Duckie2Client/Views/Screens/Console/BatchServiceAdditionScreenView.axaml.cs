using Duckie2Client.Views.Base;

namespace Duckie2Client.Views.Screens.Console;

public partial class BatchServiceAddingScreenView : TabUserControlView
{
    public BatchServiceAddingScreenView()
    {
        InitializeComponent();
    }

    public override void OnTabClose(string message)
    {
        App.VM_BatchServiceAdding.OnScreenClose();
    }
}