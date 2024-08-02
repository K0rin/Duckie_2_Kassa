using Avalonia;
using Avalonia.ReactiveUI;
using System;
using System.Globalization;
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
        // var currentCulture = CultureInfo.CurrentCulture;

        // var rm = ResourceManager.CreateFileBasedResourceManager("ErrorMessages", "Resources", null); 
        // var errorDescr = rm.GetString("Error101", CultureInfo.CurrentCulture);

        // var x = Resources.Resources1.MyString;


        //
        // Display the name of the current culture.
        // Console.WriteLine("CurrentCulture is {0}.", CultureInfo.CurrentCulture.Name);

        // Change the current culture to th-TH.
        // CultureInfo.CurrentCulture = new CultureInfo("th-TH", false);
        // Console.WriteLine("CurrentCulture is now {0}.", CultureInfo.CurrentCulture.Name);

        // Display the name of the current UI culture.
        // Console.WriteLine("CurrentUICulture is {0}.", CultureInfo.CurrentUICulture.Name);

        // Change the current UI culture to ja-JP.
        // CultureInfo.CurrentUICulture = new CultureInfo( "ja-JP", false );
        // Console.WriteLine("CurrentUICulture is now {0}.", CultureInfo.CurrentUICulture.Name);


        // FIX: LINUX:
        // System.InvalidOperationException:
        // Cannot perform requested operation because the Dispatcher shut down

        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }
}