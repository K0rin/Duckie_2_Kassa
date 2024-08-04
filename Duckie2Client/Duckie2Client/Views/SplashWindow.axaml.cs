using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Duckie2Client.Libs;
using Duckie2Client.Libs.Enums;
using Duckie2Client.Resources;
using Duckie2Client.Services.AppLoading;

namespace Duckie2Client.Views;

/// <summary>
///     Loading Screen.<br />
///     This screen shows an application loading sequence.
///     In case of errors during the loading sequence, it shows error messages.
/// </summary>
public partial class SplashWindow : Window
{
    private readonly AppModes _appMode;
    private readonly Action? _mainAction;

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
    ///     Call the Application loading sequence.
    /// </summary>
    private async void DuckieLoad()
    {
        var appLoading = new AppLoading();

        // Subscribe on event.
        appLoading.ActionCompleted += actionMessage =>
            StatusMessage.Text = actionMessage;

        try
        {
            // Run tasks.
            await appLoading.LoadAppActionAsync();
        }
        catch (DuckieException e)
        {
            // TODO: Switch the Splash Screen to Error Message Mode.
            // SwitchErrorMode();

            Console.WriteLine(e.Message);

            // Avoid closing the Splash Screen and opening the Main Screen.
            return;
        }

        await LoadedSuccessfully();
    }

    private void SwitchErrorMode()
    {
        throw new NotImplementedException();
    }

    private async Task LoadedSuccessfully()
    {
        // Successful loading.
        StatusMessage.Text = Localization.GetString(
            () => UserInterface.LoadingProcessDone,
            ResourceTypes.UserInterface);

        // ::debug::
        await Task.Delay(1500);

        // Show main window and close the splash screen.
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            _mainAction?.Invoke();
            Close();
        });
    }
}