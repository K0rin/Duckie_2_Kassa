using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Duckie2Client.Libs;
using Duckie2Client.Services;

namespace Duckie2Client.Views;

public partial class SplashWindow : Window
{
    private readonly Action? _mainAction;
    public SplashWindow(Action mainAction)
    {
        InitializeComponent();
        Ui.CenterWindowOnScreen(this);
        _mainAction = mainAction;
    }
    protected override void OnLoaded(RoutedEventArgs routedEventArgs)
    {
        // Begin the Application loading.
        DuckieLoad();
    }
    private async void DuckieLoad()
    {
        var appLoading = new AppLoading();
        // Subscribe on event.
        appLoading.ActionCompleted += actionNumber =>
        {
            StatusMessage.Text = $"Action {actionNumber}";
        };
        // Run tasks.
        await appLoading.ExecuteActionAsync();

        StatusMessage.Text = "All done";
        await Task.Delay(1500);
        
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            _mainAction?.Invoke();
            Close();
        });
    }
}