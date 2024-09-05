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

    public bool IsDataStateVisible { get; set; }
    [Reactive] public TabState? CurrentState { get; set; }
    public TabContext? Context { get; }
    public ReactiveCommand<Unit, Unit> UpdateDateCommand { get; }
    public ReactiveCommand<Unit, Unit> DataLoadingCancelCommand { get; }

    // ReSharper disable once ConvertToPrimaryConstructor
    public TabItemViewModel(string header, Control content, TabContext? context)
    {
        UpdateDateCommand = ReactiveCommand.Create(UpdateDateExecute);
        DataLoadingCancelCommand = ReactiveCommand.Create(DataLoadingCancelExecute);
        Header = header;
        Content = content;
        Context = context;
        CurrentState = Context?.CurrentState;
    }

    private void DataLoadingCancelExecute()
    {
        // todo: implement
        throw new System.NotImplementedException();
    }

    private void UpdateDateExecute()
    {
        Context?.SetState(new DataLoadingState());
        CurrentState = Context?.CurrentState;
    }

    public override string ToString()
    {
        return Header;
    }
}