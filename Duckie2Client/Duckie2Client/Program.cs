using System;
using Avalonia;
using Avalonia.ReactiveUI;
using Duckie2Client.Libs.ErrorCollection;

namespace Duckie2Client;

internal sealed class Program
{
    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder
            .Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();
    }

    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called:
    // things aren't initialized yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        var x = ErrorCodes.UnknownAppMode.GetErrorMessage();

        Console.WriteLine(x);

        return;

        // FIX: LINUX:
        // System.InvalidOperationException:
        // Cannot perform requested operation because the Dispatcher shut down

        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }
}