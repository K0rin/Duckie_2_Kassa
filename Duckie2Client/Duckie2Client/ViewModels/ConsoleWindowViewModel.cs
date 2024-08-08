using System.Reactive;
using Avalonia.Controls;
using ReactiveUI;

namespace Duckie2Client.ViewModels;

public class ConsoleWindowViewModel : ViewModelBase
{
    public ConsoleWindowViewModel()
    {
        // By default, the Authorization Screen is visible.
        IsAuthorizationScreenVisible = true;
        OpenMainScreenCommand =
            ReactiveCommand.Create<UserControl>(OpenMainScreen);
    }

    private void OpenMainScreen(object screen)
    {
        MainWindowContent = screen;
        IsAuthorizationScreenVisible = false;
    }
#pragma warning disable CA1822 // Mark members as static
    public ReactiveCommand<UserControl, Unit> OpenMainScreenCommand { get; }

    private object _mainWindowContent;

    public object MainWindowContent
    {
        get => _mainWindowContent;
        set => this.RaiseAndSetIfChanged(ref _mainWindowContent, value);
    }

    private bool _isAuthorizationScreenVisible;

    public bool IsAuthorizationScreenVisible
    {
        get => _isAuthorizationScreenVisible;
        set => this.RaiseAndSetIfChanged(
            ref _isAuthorizationScreenVisible,
            value);
    }

#pragma warning restore CA1822 // Mark members as static
}