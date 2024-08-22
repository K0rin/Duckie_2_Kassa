using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Duckie2Client.Libs;
using Duckie2Client.Libs.Enums;
using Duckie2Client.Services;
using Duckie2Client.ViewModels;
using Duckie2Client.ViewModels.Base;
using Duckie2Client.ViewModels.Dialogs;
using Duckie2Client.ViewModels.Screens;
using Duckie2Client.Views;
using Splat;

namespace Duckie2Client;

// ReSharper disable once PartialTypeWithSinglePart
public partial class App : Application
{
    private readonly MultiInstance _multiInstance = new();

    // ReSharper disable once InconsistentNaming
    public static MainConsoleScreenViewModel VM_MainConsoleScreen =>
        Locator.Current.GetService<MainConsoleScreenViewModel>()!;

    // ReSharper disable once InconsistentNaming
    public static AuthorizationScreenViewModel VM_AuthorizationScreen =>
        Locator.Current.GetService<AuthorizationScreenViewModel>()!;

    // ReSharper disable once InconsistentNaming
    public static ConsoleWindowViewModel VM_ConsoleWindow => Locator.Current.GetService<ConsoleWindowViewModel>()!;

    // ReSharper disable once InconsistentNaming
    public static KassaWindowViewModel VM_KassaWindow => Locator.Current.GetService<KassaWindowViewModel>()!;

    // ReSharper disable once InconsistentNaming
    public static SpinnerDialogViewModel VM_SpinnerDialog => Locator.Current.GetService<SpinnerDialogViewModel>()!;

    // ReSharper disable once InconsistentNaming
    public static InitialSetupWizardViewModel VM_InitialSetupWizard =>
        Locator.Current.GetService<InitialSetupWizardViewModel>()!;


    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);

        // Register DI for view models.
        SplatRegistrations.Register<MainConsoleScreenViewModel>();
        SplatRegistrations.Register<AuthorizationScreenViewModel>();
        SplatRegistrations.Register<ConsoleWindowViewModel>();
        SplatRegistrations.Register<KassaWindowViewModel>();
        SplatRegistrations.Register<SpinnerDialogViewModel>();
        SplatRegistrations.Register<InitialSetupWizardViewModel>();
        SplatRegistrations.SetupIOC();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime
            is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Exit += OnExit;

            // Check command line parameters.
            var argsParser = CheckCommandLineArguments(desktop);

            // Check the number of the app instances.
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

    private void CheckAppInstancesNumber(IClassicDesktopStyleApplicationLifetime desktop)
    {
        if (_multiInstance.IsSingleInstance(desktop.Args?[1]!)) return;
        _multiInstance.SetInstanceForeground();
        desktop.Shutdown((int)ErrorCodes.ApplicationInstanceAlreadyExists);
    }

    private static ArgsParser CheckCommandLineArguments(IClassicDesktopStyleApplicationLifetime desktop)
    {
        const string MODE_OPTION_NAME = "mode";
        var argsParser = new ArgsParser(desktop.Args!, MODE_OPTION_NAME);

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
    private static Window? ShowInitialSetupWizard()
    {
        // Read option 'InitialSetup' from the settings file
        // and according to its value run or not Initial Setup Wizard.
        var conf = new DuckieConfig();
        var initialSetupOption = Convert.ToInt32(conf.Configuration[SettingsFileOptions.InitialSetup]);

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
    private static Window GetMainWindow(AppModes appMode)
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
    private void OnExit(object? sender, ControlledApplicationLifetimeExitEventArgs e)
    {
        _multiInstance.UnlockFile();
    }
}