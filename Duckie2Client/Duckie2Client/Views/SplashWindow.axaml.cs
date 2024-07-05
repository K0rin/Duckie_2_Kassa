using System;
using System.Threading.Tasks;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Styling;
using Avalonia.Threading;
using Duckie2Client.Libs;
using Duckie2Client.Services;

namespace Duckie2Client.Views;

public partial class SplashWindow : Window
{
    private readonly Action? _mainAction;
    public SplashWindow(Action mainAction)
    {
        InitializeComponent();
        Ui.CenterWindowOnScreen(this);
        _mainAction = mainAction;
    }
    protected override void OnLoaded(RoutedEventArgs routedEventArgs)
    {
        // StartAnimation();
        
        // Begin the Application loading.
        DuckieLoad();
        
    }

    private async void DuckieLoad()
    {
        var appLoading = new AppLoading();
        // Subscribe on event.
        appLoading.ActionCompleted += actionNumber =>
        {
            StatusMessage.Text = $"Action {actionNumber}";
        };
        // Run tasks.
        await appLoading.ExecuteActionAsync();

        StatusMessage.Text = "All done";
        await Task.Delay(1500);
        
        return; 
        
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            _mainAction?.Invoke();
            Close();
        });
    }
    
    private void StartAnimation()
    {
        var movingImage = this.FindControl<Image>("movingImage");

        var animation = new Animation
        {
            Duration = TimeSpan.FromSeconds(5),
            Easing = new Avalonia.Animation.Easings.CubicEaseInOut(),
            Children =
            {
                new KeyFrame
                {
                    Cue = new Cue(0d),
                    Setters =
                    {
                        new Setter(Canvas.LeftProperty, 0d),
                        new Setter(Canvas.TopProperty, 0d)
                    }
                },
                new KeyFrame
                {
                    Cue = new Cue(1d),
                    Setters =
                    {
                        new Setter(Canvas.LeftProperty, 400d),
                        new Setter(Canvas.TopProperty, 300d)
                    }
                }
            }
        };

        animation.RunAsync(movingImage);
    }
}