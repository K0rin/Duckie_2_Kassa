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

    private readonly MultiInstance _multiInstance = new();

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime
            is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Exit += OnExit;

            var argsParser = new ArgsParser(desktop.Args, "mode");
            argsParser.CheckArgs();

            if (!_multiInstance.IsSingleInstance(desktop.Args[1]))
            {
                desktop.Shutdown();
            }

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

    private void OnExit(object? sender,
        ControlledApplicationLifetimeExitEventArgs e)
    {
        _multiInstance.UnlockFile();
    }
}