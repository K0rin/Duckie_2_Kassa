using System.Collections.Generic;
using Duckie2Client.ViewModels.Base;

namespace Duckie2Client.ViewModels;

public class KassaWindowViewModel : PagerViewModelBase
{
    public KassaWindowViewModel(List<ViewModelPageBase> pages, int defaultPageNumber) : base(pages, defaultPageNumber)
    {

    }
}
