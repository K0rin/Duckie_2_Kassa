using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;

namespace Duckie2Client.Views;

public partial class SplashWindow : Window
{
    private readonly Action? _mainAction;
    public SplashWindow(Action mainAction)
    {
        InitializeComponent();
        
        
        CenterWindowOnScreen(this);
        
        
        
        
        
        
        
        
        
        _mainAction = mainAction;
    }
    private void CenterWindowOnScreen(Window window)
    {
        // Retrieve the scale factor from the environmental variable
        var scaleFactorStr = Environment.GetEnvironmentVariable("AVALONIA_GLOBAL_SCALE_FACTOR");
        var scaleFactor = 1.0;
        
        if (!string.IsNullOrEmpty(scaleFactorStr) && double.TryParse(scaleFactorStr, out var parsedScaleFactor))
        {
            scaleFactor = parsedScaleFactor;
        }

        // Get the primary screen's working area
        var screen = Screens.Primary;
        var workingArea = screen.WorkingArea;

        // Adjust working area size based on the scale factor
        var scaledScreenWidth = workingArea.Width / scaleFactor;
        var scaledScreenHeight = workingArea.Height / scaleFactor;

        // Calculate the centered position
        var centerX = (scaledScreenWidth - window.Width) / 2;
        var centerY = (scaledScreenHeight - window.Height) / 2;

        // Apply the scaled position
        window.Position = new PixelPoint((int)centerX, (int)centerY);
        
        // note: the splash centers by itself somehow.
        // window.Position = new PixelPoint(0, 0);
    }
    protected override void OnLoaded(RoutedEventArgs routedEventArgs)
    {
        DummyLoad();
    }

    private async void DummyLoad()
    {
        // Do some background stuff here.
        await Task.Delay(3000);

        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            _mainAction?.Invoke();
            Close();
        });
    }
}