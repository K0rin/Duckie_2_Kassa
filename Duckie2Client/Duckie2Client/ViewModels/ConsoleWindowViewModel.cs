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
        // By default, the Authorization Screen is visible.
        _currentPage = _pages[0];
    }
}