using Avalonia;
using Avalonia.ReactiveUI;
using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;

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
        // rm.GetString("greeting"); 

        // var currentCulture = CultureInfo.CurrentCulture;

        // var rm = ResourceManager.CreateFileBasedResourceManager("ErrorMessages", "Resources", null); 
        // var errorDescr = rm.GetString("Error101", CultureInfo.CurrentCulture);


        //
        // Display the name of the current culture.
        // Console.WriteLine(@"CurrentCulture is {0}.",
        // CultureInfo.CurrentCulture.Name);
        // Console.WriteLine(Resources.ErrorMessages.Error101);
        // FooBar.ResourceManager.GetString("Hello", CultureInfo.GetCultureInfo("sv-SE")) 

        // CultureInfo.CurrentCulture = new CultureInfo("ru");
        // Thread.CurrentThread.CurrentUICulture =
        // CultureInfo.GetCultureInfo("ru-RU");

        // Thread.CurrentThread.CurrentCulture =
        // CultureInfo.CreateSpecificCulture("ru-RU"); 
        // Thread.CurrentThread.CurrentUICulture = 
        // CultureInfo.CreateSpecificCulture("ru-RU");

        // Console.WriteLine(@"CurrentCulture is {0}.",
        // CultureInfo.CurrentCulture.Name);

        SetDefaultCulture();

        var x = Resources.Resources1.ResourceManager.GetString(
            "ArgumentInvalidNumber");

        Console.WriteLine(x);

        // Change the current culture to th-TH.
        // CultureInfo.CurrentCulture = new CultureInfo("th-TH", false);
        // Console.WriteLine("CurrentCulture is now {0}.", CultureInfo.CurrentCulture.Name);

        // Display the name of the current UI culture.
        // Console.WriteLine("CurrentUICulture is {0}.", CultureInfo.CurrentUICulture.Name);

        // Change the current UI culture to ja-JP.
        // CultureInfo.CurrentUICulture = new CultureInfo( "ja-JP", false );
        // Console.WriteLine("CurrentUICulture is now {0}.", CultureInfo.CurrentUICulture.Name);


        return;

        // FIX: LINUX:
        // System.InvalidOperationException:
        // Cannot perform requested operation because the Dispatcher shut down

        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    public static void SetDefaultCulture()
    {
        var cultureInfo = CultureInfo.CreateSpecificCulture("ru");
        Thread.CurrentThread.CurrentCulture = cultureInfo;
        Thread.CurrentThread.CurrentUICulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

        var type = typeof(CultureInfo);
        type.InvokeMember("s_userDefaultCulture",
            BindingFlags.SetField | BindingFlags.NonPublic |
            BindingFlags.Static,
            null,
            cultureInfo,
            new object[] { cultureInfo });

        type.InvokeMember("s_userDefaultUICulture",
            BindingFlags.SetField | BindingFlags.NonPublic |
            BindingFlags.Static,
            null,
            cultureInfo,
            new object[] { cultureInfo });
    }
}