using Avalonia.Controls;
using Duckie2Client.Libs;

namespace Duckie2Client.Views.Base;

public abstract class TabUserControlView : UserControl, ITabaloniaTabItemContent
{
    public abstract void OnTabClose(string message);
}