using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Duckie2Client.Libs;
using Duckie2Client.Services.AppLoading;

namespace Duckie2Client.Views;

/// <summary>
/// Loading Screen.
///
/// This screen shows an application loading sequence.
/// In case of errors during the loading sequence, it shows error messages.
/// </summary>
public partial class SplashWindow : Window
{
    private readonly Action? _mainAction;
    private AppModes _appMode;

    public SplashWindow(Action mainAction, AppModes appMode)
    {
        InitializeComponent();
        Ui.CenterWindowOnScreen(this);
        _mainAction = mainAction;
        _appMode = appMode;
    }


    protected override void OnLoaded(RoutedEventArgs routedEventArgs)
    {
        SetModeNameText();
        // Begin the Application loading.
        DuckieLoad();
    }

    private void SetModeNameText()
    {
        var modeNames = new Dictionary<AppModes, string>
        {
            { AppModes.Console, "Admin" },
            { AppModes.Kassa, "Kassa" }
        };
        ApplicationModeName.Text = modeNames[_appMode];
    }

    /// <summary>
    /// Call an application loading sequence.
    /// </summary>
    private async void DuckieLoad()
    {
        var appLoading = new AppLoading();
        // Subscribe on event.
        appLoading.ActionCompleted += actionMessage =>
        {
            StatusMessage.Text = actionMessage;
        };
        // Run tasks.
        var result = await appLoading.LoadAppActionAsync();
        // Check loading status.
        if (result)
        {
            // Successful loading.
            StatusMessage.Text = "All done";
            await Task.Delay(1500);
            // Show main window and close the splash screen.
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                _mainAction?.Invoke();
                Close();
            });
        }
        else
        {
            // TODO: switch to error message mode
            Console.WriteLine("error mode");
        }
    }
}