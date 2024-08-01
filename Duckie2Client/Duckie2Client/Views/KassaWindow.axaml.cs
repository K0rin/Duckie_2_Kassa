using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Duckie2Client.Views;

public partial class KassaWindow : Window
{
    public KassaWindow()
    {
        InitializeComponent();
    }
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}