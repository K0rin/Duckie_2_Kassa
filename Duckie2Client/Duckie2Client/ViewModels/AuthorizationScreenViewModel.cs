using System.Reactive;
using ReactiveUI;

namespace Duckie2Client.ViewModels;

public class AuthorizationScreenViewModel : ViewModelBase
{
    private bool _isSpinnerVisible;

    public bool IsSpinnerVisible
    {
        get => _isSpinnerVisible;
        set => this.RaiseAndSetIfChanged(ref _isSpinnerVisible, value);
    }

    private bool _isAuthControlsVisible;

    public bool IsAuthControlsVisible
    {
        get => _isAuthControlsVisible;
        set => this.RaiseAndSetIfChanged(ref _isAuthControlsVisible, value);
    }

    public ReactiveCommand<object, Unit> OpenMainScreenCommand { get; set; }

    public AuthorizationScreenViewModel()
    {
        IsSpinnerVisible = false;
        IsAuthControlsVisible = true;

        OpenMainScreenCommand = ReactiveCommand.Create<object>(OpenMainScreen);
    }


    private void OpenMainScreen(object value)
    {
        ((ConsoleWindowViewModel)value).SwitchPage(1);
    }
}