using ReactiveUI;

namespace Duckie2Client.ViewModels;

public class ConsoleWindowViewModel : ViewModelBase
{
    private readonly ViewModelBase[] _pages =
    [
        new AuthorizationScreenViewModel(),
        new MainConsoleScreenViewModel()
    ];

    private ViewModelBase _currentPage;

    /// <summary>
    /// Gets the current page. The property is read-only
    /// </summary>
    public ViewModelBase CurrentPage
    {
        get => _currentPage;
        private set => this.RaiseAndSetIfChanged(ref _currentPage, value);
    }

    public void SwitchPage(int pageNumber)
    {
        CurrentPage = _pages[pageNumber];
    }

    public ConsoleWindowViewModel()
    {
        // Set current page to first on start up
        _currentPage = _pages[0];


        // By default, the Authorization Screen is visible.
        // IsAuthorizationScreenVisible = true;
        // By default, the spinner is hidden.
        // IsSpinnerVisible = false;
        // IsAuthControlsVisible = true;

        // OpenMainScreenCommand = ReactiveCommand.Create<UserControl>(OpenMainScreen);
    }


    // private void OpenMainScreen(object screen)
    private void OpenMainScreen()
    {
        CurrentPage = _pages[1];

        // IsAuthControlsVisible = false;
        // IsSpinnerVisible = true;

        // MainWindowContent = screen;
        // IsAuthorizationScreenVisible = false;
    }

    // public ReactiveCommand<UserControl, Unit> OpenMainScreenCommand { get; }

    // private object _mainWindowContent;
    //
    // public object MainWindowContent
    // {
    //     get => _mainWindowContent;
    //     set => this.RaiseAndSetIfChanged(ref _mainWindowContent, value);
    // }

    // private bool _isAuthorizationScreenVisible;

    // public bool IsAuthorizationScreenVisible
    // {
    //     get => _isAuthorizationScreenVisible;
    //     set => this.RaiseAndSetIfChanged(
    //         ref _isAuthorizationScreenVisible,
    //         value);
    // }

    // private bool _isSpinnerVisible;
    //
    // public bool IsSpinnerVisible
    // {
    //     get => _isSpinnerVisible;
    //     set => this.RaiseAndSetIfChanged(ref _isSpinnerVisible, value);
    // }
    //
    // private bool _isAuthControlsVisible;
    //
    // public bool IsAuthControlsVisible
    // {
    //     get => _isAuthControlsVisible;
    //     set => this.RaiseAndSetIfChanged(ref _isAuthControlsVisible, value);
    // }
}