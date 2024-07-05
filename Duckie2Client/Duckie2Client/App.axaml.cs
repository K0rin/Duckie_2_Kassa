using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Duckie2Client.ViewModels;
using Duckie2Client.Views;
using Avalonia.Controls;

namespace Duckie2Client;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
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
            // CenterWindowOnScreen(desktop.MainWindow);
        }

        base.OnFrameworkInitializationCompleted();
    }

}