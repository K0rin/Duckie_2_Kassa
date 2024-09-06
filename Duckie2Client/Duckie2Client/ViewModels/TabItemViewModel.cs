using System.Reactive;
using Avalonia.Controls;
using Duckie2Client.Services.Controls;
using Duckie2Client.ViewModels.Base;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Duckie2Client.ViewModels;

/// <summary>Represents the data model for an individual tab on the main screen of the Application in the “Manager
/// Console” mode of operation.</summary>
public class TabItemViewModel : ViewModelBase
{
    /// <summary>The tab header text.</summary>
    public string Header { get; set; }

    /// <summary>Content of the tab.</summary>
    public Control Content { get; set; }

    [Reactive] public bool IsDataStateVisible { get; set; }
    [Reactive] public TabState? CurrentState { get; set; }
    public TabContext? Context { get; }
    public ReactiveCommand<Unit, Unit> UpdateDateCommand { get; }
    public ReactiveCommand<Unit, Unit> DataLoadingCancelCommand { get; }

    /// <summary>
    /// Message showing in the "Ready" state of a tab.
    /// </summary>
    [Reactive]
    public string ReadyStateMessageText { get; set; }

    /// <summary>
    /// Message displayed in the “DataLoading” state of the tab.
    /// </summary>
    [Reactive]
    public string DataLoadingMessageText { get; set; }

    // ReSharper disable once ConvertToPrimaryConstructor
    public TabItemViewModel(string header, Control content, TabContext? context)
    {
        UpdateDateCommand = ReactiveCommand.Create(UpdateDataExecute);
        DataLoadingCancelCommand = ReactiveCommand.Create(DataLoadingCancelExecute);
        Header = header;
        Content = content;
        Context = context;
        CurrentState = Context?.CurrentState;
    }

    private void DataLoadingCancelExecute()
    {
        Context?.CurrentState?.CancelDataLoading();
    }

    private async void UpdateDataExecute()
    {
        // Switch state.
        Context?.SetState(new DataLoadingState());
        CurrentState = Context?.CurrentState;

        // Run update data method.
        var data = await Context?.CurrentState?.UpdateData()!;

        if (data.IsNull)
        {
            // todo: Switch to the Ready state.
            Context?.SetState(new ReadyLoadDataState());
            CurrentState = Context?.CurrentState;
        }
        else
        {
            ((ITabViewModel)Content.DataContext!).DataPayload = data.Result.ToString();
            // Switch to data display mode.
            Context?.SetState(new DataState());
            CurrentState = Context?.CurrentState;
            IsDataStateVisible = true;
        }
    }

    public override string ToString()
    {
        return Header;
    }
}