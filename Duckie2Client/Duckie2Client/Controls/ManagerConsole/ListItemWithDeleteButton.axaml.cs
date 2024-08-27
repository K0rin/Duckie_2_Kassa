using Avalonia;
using Avalonia.Controls;

namespace Duckie2Client.Controls.ManagerConsole;

/// <summary>
/// Represents a control with a text label and the “Delete” button (in the form of an icon).
/// </summary>
public partial class RemovableListItemControl : UserControl
{
    public static readonly StyledProperty<string> TextProperty =
        AvaloniaProperty.Register<RemovableListItemControl, string>(nameof(Text));

    public RemovableListItemControl()
    {
        InitializeComponent();
        DataContext = this; // Set the DataContext for binding custom attributes (e.g., Text).
    }

    /// <summary>
    /// The value for the element's text label.
    /// </summary>
    public string Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
}