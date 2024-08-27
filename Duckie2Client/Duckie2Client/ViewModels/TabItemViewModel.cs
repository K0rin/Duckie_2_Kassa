using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Duckie2Client.ViewModels;

/// <summary>Represents the data model for an individual tab on the main screen of the Application in the “Manager
/// Console” mode of operation.</summary>
public class TabItemViewModel : ObservableObject
{
    // ReSharper disable once ConvertToPrimaryConstructor
    public TabItemViewModel(string header, Control content)
    {
        Header = header;
        Content = content;
    }

    /// <summary>The tab header text.</summary>
    public string Header { get; set; }

    /// <summary>Content of the tab.</summary>
    public Control Content { get; set; }

    public override string ToString()
    {
        return Header;
    }
}