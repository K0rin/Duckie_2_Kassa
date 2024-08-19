using System;
using System.Collections.Generic;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Duckie2Client.Libs;
using Duckie2Client.Libs.Enums;
using Duckie2Client.Services;
using Duckie2Client.ViewModels;
using Duckie2Client.Views;
using DynamicData;

namespace Duckie2Client;

// ReSharper disable once PartialTypeWithSinglePart
public partial class App : Application
{
    private readonly MultiInstance _multiInstance = new();

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime
            is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Exit += OnExit;


            // Check command line parameters.
            /* todo: refactor:
            Убрать из метода создание объекта ArgsParser.
            Метод не должен ничего возвращать, только выбрасывать исключение,
            чтобы метод desktop.Shutdown вызывать только в одно месте.
            */
            var argsParser = CheckCommandLineArguments(desktop);

            // Check number of the app instances.
            /* todo: refactor:
            Пусть метод _multiInstanceIsSingleInstance выбрасывает исключение,
            чтобы здесь его ловить и вызывать Shutdown в одном месте (здесь).
            */
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
        const string modeOptionName = "mode";
        var argsParser = new ArgsParser(desktop.Args, modeOptionName);

        try
        {
            argsParser.CheckArguments();
        }
        catch (DuckieException e)
        {
            desktop.Shutdown(e.ErrorNumber);
        }

        return argsParser;
    }

    /// <summary>
    /// <para>Checks the <c>InitialSetup</c> option in the program settings
    /// file <c>appsettings.json</c>.</para>
    /// </summary>
    /// <returns>
    /// <para>If the option is set to <c>1</c> - returns the Window object of
    /// the Initial Setup Wizard.</para>
    /// <para>Otherwise, if the option is set to <c>0</c> - null.</para>
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
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

    /// <summary>
    /// Returns a specific Window object depending on the working mode of
    /// the application.
    /// </summary>
    /// <param name="appMode">
    /// Application working modes:
    /// <list type="bullet">
    ///   <item>
    ///     <description>console - </description>
    ///   </item>
    ///   <item>
    ///     <description>kassa - </description>
    ///   </item>
    /// </list>
    /// </param>
    /// <exception cref="InvalidOperationException"></exception>
    private Window GetMainWindow(AppModes appMode)
    {
        // Return Initial Setup Wizard window if the settings file has option
        // 'InitialSetup' set to 'Show'.
        var result = ShowInitialSetupWizard();
        if (result != null) return result;


        // Select a view appropriate to a selected app mode.
        result = appMode switch
        {
            AppModes.Console => CreateConsoleWindow(),
            AppModes.Kassa => CreateKassaWindow(),
            _ => null
        };

        if (result == null)
            // todo: add error message to resource file.
            throw new InvalidOperationException("Cannot create main window. Unknown application mode.");

        return result;
    }

    private static ConsoleWindow CreateConsoleWindow()
    {
        // Pages
        List<ViewModelPageBase> pages =
        [
            new AuthorizationScreenViewModel(),
            new MainConsoleScreenViewModel()
        ];

        var output = new ConsoleWindow
        {
            DataContext = new ConsoleWindowViewModel(pages, 0)
        };
        return output;
    }

    private static KassaWindow CreateKassaWindow()
    {
        var output = new KassaWindow
        {
            DataContext = new KassaWindowViewModel()
        };
        return output;
    }

    /// <summary>
    /// This method is called before exiting the application.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnExit(object? sender,
        ControlledApplicationLifetimeExitEventArgs e)
    {
        var currentThread = Thread.CurrentThread;


        _multiInstance.UnlockFile();
    }
}