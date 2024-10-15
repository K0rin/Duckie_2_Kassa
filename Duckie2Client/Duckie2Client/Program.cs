using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.ReactiveUI;
using Duckie2Client.Libs;
using Duckie2Client.Libs.Enums;

namespace Duckie2Client;

internal static class Program
{
    // Avalonia configuration, don't remove; also used by visual designer.
    // ReSharper disables MemberCanBePrivate.Global
    public static AppBuilder BuildAvaloniaApp()
        // ReSharper restore MemberCanBePrivate.Global
    {
        return AppBuilder
            .Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();
    }

    private static readonly MultiInstance MultiInstance = new();

    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called:
    // things aren't initialized yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        //new Duckie2Client.ViewModels.Screens.ManagerConsole.PersonnelScreen.PersonnelScreenViewModel().AddUserExecute();
        // FIX: LINUX:
        // System.InvalidOperationException:
        // Cannot perform requested operation because the Dispatcher shut down

        try
        {
            CheckCommandLineArguments(ref args);
        }
        catch (DuckieException e)
        {
            Environment.Exit(e.ErrorNumber);
        }

        CheckAppInstancesNumber(ref args);
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }


    /// <summary>
    /// Checks the correctness of the arguments passed to the Application executable.
    /// </summary>
    private static void CheckCommandLineArguments(ref string[] args)
    {
        var argumentCollection = new List<StartupOption> { new("mode", ["console", "kassa"], false) };
        var argumentManager = new ArgsParser(args, ref argumentCollection);
        argumentManager.CheckArgumentValidity();
    }

    private static void CheckAppInstancesNumber(ref string[] args)
    {
        if (MultiInstance.IsSingleInstance(args[1]))
        {
            MultiInstance.UnlockFile();
            return;
        }

        MultiInstance.SetInstanceForeground();
        Environment.Exit((int)ErrorCodes.ApplicationInstanceAlreadyExists);
    }
}