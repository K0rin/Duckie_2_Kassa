using System;
using System.Collections.ObjectModel;
using System.Reactive;
using Duckie2Client.Libs;
using Duckie2Client.ViewModels.Base;
using Duckie2Client.Views.Screens.Console;
using ReactiveUI;
using Tabalonia.Controls;

namespace Duckie2Client.ViewModels.Screens;

public class MainConsoleScreenViewModel : ViewModelPageBase
{
    #region Commands

    public ReactiveCommand<Unit, Unit> ExitMenuCommand { get; }
    public ReactiveCommand<Unit, Unit> BatchServiceAddingCommand { get; }
    public ReactiveCommand<object, Unit> TabCloseCommand { get; }

    #endregion

    public ObservableCollection<TabItemViewModel> TabItems { get; } = [];

    public MainConsoleScreenViewModel()
    {
        ExitMenuCommand = ReactiveCommand.Create(ExitMenuCommandExecute);
        BatchServiceAddingCommand = ReactiveCommand.Create(BatchServiceAdditionCommandExecute);
        TabCloseCommand = ReactiveCommand.Create<object>(TabCloseCommandExecute);

        TabItems.Add(new TabItemViewModel("Batch Service Adding", new BatchServiceAddingScreenView()));
    }

    private static void TabCloseCommandExecute(object value)
    {
        var x = ((DragTabItem)value).DataContext;
        x = ((TabItemViewModel)x!).Content;
        ((ITabaloniaTabItemContent)x).OnTabClose("close");
    }


    private static void BatchServiceAdditionCommandExecute()
    {
        throw new NotImplementedException();
    }

    private static void ExitMenuCommandExecute()
    {
        Services.Common.ExitApplication();
    }
}