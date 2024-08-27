using ReactiveUI;

namespace Duckie2Client.Models;

public class RemovableListItem : ReactiveObject
{
    public string? TitleText { get; }

    // ReSharper disable once ConvertToPrimaryConstructor
    public RemovableListItem(string? title)
    {
        TitleText = title;
    }
}