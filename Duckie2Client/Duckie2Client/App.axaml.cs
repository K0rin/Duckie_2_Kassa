using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Duckie2Client.Libs;
using Duckie2Client.ViewModels;
using Duckie2Client.Views;

namespace Duckie2Client;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime
            is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var argsParser = new ArgsParser(
                desktop.Args,
                "mode");
            argsParser.CheckArgs();


            desktop.MainWindow = new SplashWindow(() =>
            {
                var mainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel()
                };

                mainWindow.Show();
                mainWindow.Focus();

                desktop.MainWindow = mainWindow;
            });
        }

        base.OnFrameworkInitializationCompleted();
    }
}