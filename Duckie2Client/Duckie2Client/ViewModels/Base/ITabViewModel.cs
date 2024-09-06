namespace Duckie2Client.ViewModels.Base;

/// <summary>
/// The interface provides attributes and functions for controls that inherit from the Tabalonia object.
/// </summary>
public interface ITabViewModel
{
    /// <summary>
    /// Attribute to pass the data object to the "Data" state of the tab.
    /// </summary>
    public string? DataPayload { get; set; }
}