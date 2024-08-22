using System.Collections.Generic;
using Duckie2Client.ViewModels.Base;

namespace Duckie2Client.ViewModels;

public class ConsoleWindowViewModel : PagerViewModelBase
{
    // ReSharper disable once ConvertToPrimaryConstructor
    public ConsoleWindowViewModel(List<ViewModelPageBase> pages, int defaultPageNumber) : base(pages, defaultPageNumber)
    {
    }
}