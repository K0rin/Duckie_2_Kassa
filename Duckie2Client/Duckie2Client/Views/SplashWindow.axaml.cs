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
/// <para>Splash Screen.</para>
/// <para>This screen shows an application loading sequence.</para>
/// <para>In case of errors during the loading sequence, it shows error
/// messages.</para>
/// </summary>
// ReSharper disable PartialTypeWithSinglePart
public partial class SplashWindow : Window
// ReSharper restore PartialTypeWithSinglePart
{
    private readonly AppModes _appMode;
    private readonly Action? _mainAction;

    // ReSharper disable once UnusedMember.Global
    public SplashWindow()
    {
    }

    public SplashWindow(Action mainAction, AppModes appMode)
    {
        InitializeComponent();
        Ui.CenterWindowOnScreen(this);

        _mainAction = mainAction;
        _appMode = appMode;

        // todo: load localization for exit button
        ExitButton.Content = "Exit";
    }


    protected override void OnLoaded(RoutedEventArgs routedEventArgs)
    {
        SetModeNameText();
        LoadDuckieApplication();
    }

    // todo: refact: Change to enum.
    private void SetModeNameText()
    {
        var modeNames = new Dictionary<AppModes, string>
        {
            { AppModes.Console, "Admin" },
            { AppModes.Kassa, "Kassa" }
        };
        ApplicationModeName.Text = modeNames[_appMode];
    }

    /// <summary>Call the Application loading sequence.</summary>
    private async void LoadDuckieApplication()
    {
        var appLoading = new AppLoading();

        // Subscribe on event.
        appLoading.ActionCompleted += actionMessage => StatusMessage.Text = actionMessage;

        try
        {
            // Run tasks.
            await appLoading.LoadAppActionAsync();
        }
        catch (Exception e)
        {
            // TODO: Switch the Splash Screen to Error Message Mode.
            SwitchErrorMode(e);

            // Avoid closing the Splash Screen and opening the Main Screen.
            return;
        }

        await LoadedSuccessfully();
    }

    private void SwitchErrorMode(Exception error)
    {
        var errorModePanel = GetElementByName<Grid>("ErrorModePanel");
        var normalModePanel = GetElementByName<DockPanel>("NormalModePanel");

        ErrorMessage.Text = error.Message;

        ErrorNumber.IsVisible = error is DuckieException;
        if (error is DuckieException exception) ErrorNumber.Text = exception.ErrorNumber.ToString();

        errorModePanel.IsVisible = true;
        normalModePanel.IsVisible = false;
    }

    private T GetElementByName<T>(string elementName) where T : Control
    {
        var nameScope =
            this.FindNameScope() ?? throw new InvalidOperationException(RuntimeErrors.NameScopeNotFound.GetMessage());
        var output = nameScope.Find<T>(elementName)
                     ?? throw new ArgumentNullException(
                         RuntimeErrors.CannotFindAnElementWithName.GetMessage([elementName]));
        return output;
    }

    /// <summary>
    /// This method call after the application is loaded successfully without any exceptions.
    /// </summary>
    private async Task LoadedSuccessfully()
    {
        // Successful loading.
        StatusMessage.Text = Localization.GetString(
            () => UserInterface.LoadingProcessDone, ResourceTypes.UserInterface);

        // ::debug::
        const int delay = 1000;
        await Task.Delay(delay);
        // ::debug::

        // Show the main window and close the splash screen.
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            _mainAction?.Invoke();
            Close();
        });
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        Services.Common.ExitApplication();
    }
}