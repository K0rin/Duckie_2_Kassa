using System.Collections.Generic;
using ReactiveUI.Fody.Helpers;

namespace Duckie2Client.ViewModels.Base;

public abstract class PagerViewModelBase : ViewModelBase
{
    private List<ViewModelPageBase> Pages { get; set; }

    /// <summary>
    /// Gets the current page. The property is read-only
    /// </summary>
    [Reactive]
    public ViewModelPageBase CurrentPage { get; set; }

    public void SwitchPage(int pageNumber)
    {
        CurrentPage = Pages[pageNumber];
    }

    protected PagerViewModelBase(List<ViewModelPageBase> pages, int defaultPageNumber)
    {
        Pages = pages;
        // Set references to the pager object.
        foreach (var page in Pages) page.PagerViewModel = this;
        CurrentPage = Pages[defaultPageNumber];
    }
}