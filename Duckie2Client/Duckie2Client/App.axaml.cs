using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Duckie2Client.Libs;
using Duckie2Client.Libs.Enums;
using Duckie2Client.Libs.ErrorCollection;
using Duckie2Client.Services;
using Duckie2Client.ViewModels;
using Duckie2Client.Views;

namespace Duckie2Client;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private readonly MultiInstance _multiInstance = new();

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime
            is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Exit += OnExit;

            // Check command line parameters.
            var argsParser = CheckCommandLineArguments(desktop);

            // Check number of the app instances.
            CheckAppInstancesNumber(desktop);

            desktop.MainWindow = new SplashWindow(() =>
            {
                Window? mainWindow = null;

                try
                {
                    mainWindow = GetMainWindow(argsParser.CurrentAppMode);
                }
                catch (InvalidOperationException)
                {
                    desktop.Shutdown((int)ErrorCodes.UnknownAppMode);
                }

                mainWindow?.Show();
                mainWindow?.Focus();

                desktop.MainWindow = mainWindow;
            }, argsParser.CurrentAppMode);
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void CheckAppInstancesNumber(
        IClassicDesktopStyleApplicationLifetime desktop)
    {
        if (_multiInstance.IsSingleInstance(desktop.Args[1])) return;
        _multiInstance.SetInstanceForeground();
        desktop.Shutdown((int)ErrorCodes.ApplicationInstanceAlreadyExists);
    }

    private ArgsParser CheckCommandLineArguments(
        IClassicDesktopStyleApplicationLifetime desktop)
    {
        var argsParser = new ArgsParser(desktop.Args, "mode");
        var parseResult = argsParser.CheckArgs();
        if (parseResult != null) desktop.Shutdown((int)parseResult.Value);
        return argsParser;
    }

    private Window? ShowInitialSetupWizard()
    {
        // Read option 'InitialSetup' from the settings file
        // and according to its value run or not Initial Setup Wizard.
        var conf = new DuckieConfig();
        var initialSetupOption = Convert.ToInt32(
            conf.Configuration[SettingsFileOptions.InitialSetup]);

        return initialSetupOption switch
        {
            (int)InitialSetup.Hide => null,
            (int)InitialSetup.Show => new InitialSetupWizardWindow
            {
                DataContext = new InitialSetupWizardViewModel()
            },
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private Window GetMainWindow(AppModes appMode)
    {
        // Return Initial Setup Wizard window if the settings file has option
        // 'InitialSetup' set to 'Show'.
        var result = ShowInitialSetupWizard();
        if (result != null) return result;

        // Select a view appropriate to a selected app mode.
        result = appMode switch
        {
            AppModes.Console => new MainWindow
            {
                DataContext = new MainWindowViewModel()
            },
            AppModes.Kassa => new KassaWindow
            {
                DataContext = new KassaWindowViewModel()
            },
            _ => null
        };

        if (result == null)
            throw new InvalidOperationException(
                "Cannot create main window. Unknown application mode.");

        return result;
    }

    private void OnExit(object? sender,
        ControlledApplicationLifetimeExitEventArgs e)
    {
        _multiInstance.UnlockFile();
    }
}