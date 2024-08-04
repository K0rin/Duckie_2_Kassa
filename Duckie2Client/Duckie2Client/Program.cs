using System;
using Avalonia;
using Avalonia.ReactiveUI;

namespace Duckie2Client;

internal static class Program
{
    // Avalonia configuration, don't remove; also used by visual designer.
    // ReSharper disable MemberCanBePrivate.Global
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

    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called:
    // things aren't initialized yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        // FIX: LINUX:
        // System.InvalidOperationException:
        // Cannot perform requested operation because the Dispatcher shut down

        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }
}