using Avalonia;
using Avalonia.Controls;

namespace Duckie2Client.Controls.ManagerConsole.TabsControl;

public partial class DataLoadingState : UserControl
{
    private string _dataLoadingStateMessage;

    public static readonly DirectProperty<DataLoadingState, string> DataLoadingMessageProperty =
        AvaloniaProperty.RegisterDirect<DataLoadingState, string>("DataLoadingMessage", o => o.DataLoadingMessage,
            (o, v) => o.DataLoadingMessage = v);

    public DataLoadingState()
    {
        InitializeComponent();
    }

    public string DataLoadingMessage
    {
        get => _dataLoadingStateMessage;
        set => SetAndRaise(DataLoadingMessageProperty, ref _dataLoadingStateMessage, value);
    }
}