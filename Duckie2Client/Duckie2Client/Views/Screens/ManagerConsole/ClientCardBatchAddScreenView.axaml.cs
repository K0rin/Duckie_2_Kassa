using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Duckie2Client.Views.Base;

namespace Duckie2Client.Views.Screens.ManagerConsole;

public partial class ClientCardBatchAddScreenView : TabUserControlView
{
    public ClientCardBatchAddScreenView()
    {
        InitializeComponent();
        DataContext = App.VM_ClientCardBatchAdd;
    }

    public override void OnTabClose(string message)
    {
        App.VM_ClientCardBatchAdd.OnScreenClose();
    }
}