using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Duckie2Client.Libs;
using Duckie2Client.Libs.ErrorCollection;
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
            var argsParser = new ArgsParser(desktop.Args, "mode");
            var parseResult = argsParser.CheckArgs();
            if (parseResult != null) desktop.Shutdown((int)parseResult.Value);

            // Check number of the app instances.
            if (!_multiInstance.IsSingleInstance(desktop.Args[1]))
            {
                _multiInstance.SetInstanceForeground();
                desktop.Shutdown(
                    (int)ErrorCodes.ApplicationInstanceAlreadyExists);
            }

            desktop.MainWindow = new SplashWindow(() =>
            {
                Window? mainWindow = null;

                try
                {
                    // Select appropriate to an app mode view.
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

    private static Window GetMainWindow(AppModes appMode)
    {
        Window? result = appMode switch
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