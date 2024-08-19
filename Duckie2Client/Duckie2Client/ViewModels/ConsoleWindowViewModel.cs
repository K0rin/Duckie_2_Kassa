using System.Collections.Generic;
using Duckie2Client.ViewModels.Base;

namespace Duckie2Client.ViewModels;

public class ConsoleWindowViewModel : PagerViewModelBase
{
    // public ConsoleWindowViewModel()
    // {
    //     _pages =
    //     [
    //         new AuthorizationScreenViewModel(this),
    //         new MainConsoleScreenViewModel(this)
    //     ];
    //
    //     // By default, the Authorization Screen is visible.
    //     _currentPage = _pages[0];
    // }
    public ConsoleWindowViewModel(List<ViewModelPageBase> pages, int defaultPageNumber) : base(pages, defaultPageNumber)
    {
    }
}