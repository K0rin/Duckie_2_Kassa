using Avalonia.ReactiveUI;
using Duckie2Client.ViewModels;

namespace Duckie2Client.Views;

public partial class ConsoleWindow : ReactiveWindow<ConsoleWindowViewModel>
{
    public ConsoleWindow()
    {
        InitializeComponent();
    }
}