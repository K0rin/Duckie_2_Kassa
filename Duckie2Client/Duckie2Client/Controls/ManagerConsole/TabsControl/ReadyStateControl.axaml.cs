using Avalonia;
using Avalonia.Controls;
using Duckie2Client.Enums;

namespace Duckie2Client.Controls.ManagerConsole.TabsControl;

public partial class ReadyStateControl : UserControl
{
    private string _readyStateMessage;

    public static readonly DirectProperty<ReadyStateControl, string> ReadyStateMessageProperty =
        AvaloniaProperty.RegisterDirect<ReadyStateControl, string>("ReadyStateMessage", o => o.ReadyStateMessage,
            (o, v) => o.ReadyStateMessage = v);

    public static TabStates State = TabStates.DataReady;
    public ReadyStateControl()
    {
        InitializeComponent();
    }

    public string ReadyStateMessage
    {
        get => _readyStateMessage;
        set => SetAndRaise(ReadyStateMessageProperty, ref _readyStateMessage, value);
    }
}