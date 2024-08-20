using System;
using Avalonia.Controls;
using Avalonia.Data;

namespace Duckie2Client.Views.Dialogs;

public partial class SpinnerDialog : UserControl
{
    public delegate void AttachedToVisualTreeHandler(bool status);

    public event AttachedToVisualTreeHandler? Notify;

    public SpinnerDialog()
    {
        InitializeComponent();
        AttachedToVisualTree += UserControl_AttachedToVisualTree;
    }

    private SpinnerDialog SetDataContext(object value)
    {
        DataContext = value ?? throw new Exception("Data context cannot be null.");
        return this;
    }

    private void SetMessageBinding(string path, BindingMode mode)
    {
        CheckDataContext();

        var binding = new Binding
        {
            Path = path,
            Mode = mode
        };
        MessageTextBlock.Bind(TextBox.TextProperty, binding);
    }

    private void CheckDataContext()
    {
        if (DataContext is null) throw new Exception("No data context provided.");
    }

    private void UserControl_AttachedToVisualTree(object? sender, EventArgs e)
    {
        Notify?.Invoke(true);
    }

    public static SpinnerDialog GetNewDialog(object dataContext)
    {
        var output = new SpinnerDialog();
        output
            .SetDataContext(dataContext)
            .SetMessageBinding("Message", BindingMode.OneWay);
        return output;
    }
}