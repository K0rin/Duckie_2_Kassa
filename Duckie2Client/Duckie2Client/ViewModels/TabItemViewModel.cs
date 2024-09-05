using System.Reactive;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Duckie2Client.Services.Controls;
using ReactiveUI;

namespace Duckie2Client.ViewModels;

/// <summary>Represents the data model for an individual tab on the main screen of the Application in the “Manager
/// Console” mode of operation.</summary>
public class TabItemViewModel : ObservableObject
{
    /// <summary>The tab header text.</summary>
    public string Header { get; set; }

    /// <summary>Content of the tab.</summary>
    public Control Content { get; set; }

    public bool IsDataStateVisible { get; set; }
    public TabState? CurrentState => Context?.CurrentState;
    public TabContext? Context { get; }
    public ReactiveCommand<Unit, Unit> UpdateDateCommand { get; }

    // ReSharper disable once ConvertToPrimaryConstructor
    public TabItemViewModel(string header, Control content, TabContext? context)
    {
        UpdateDateCommand = ReactiveCommand.Create(UpdateDateExecute);
        Header = header;
        Content = content;
        Context = context;
    }

    private void UpdateDateExecute()
    {
        // todo: change state
        Context?.SetState(new DataLoadingState());
    }

    public override string ToString()
    {
        return Header;
    }
}