using System;
using Avalonia;
using Avalonia.Controls;

namespace Duckie2Client.Views.Dialogs;

public partial class SpinnerDialog : UserControl
{
    public delegate void AttachedToVisualTreeHandler(bool status);

    public delegate void DetachedFromVisualTreenHandler();

    public event AttachedToVisualTreeHandler? Notify;
    public event DetachedFromVisualTreenHandler? OnDialogClosing;

    public SpinnerDialog()
    {
        InitializeComponent();
        AttachedToVisualTree += UserControl_AttachedToVisualTree;
        DetachedFromVisualTree += UserControl_DetachedFromVisualTree;
    }

    private void UserControl_DetachedFromVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
    {
        OnDialogClosing?.Invoke();
    }

    private void UserControl_AttachedToVisualTree(object? sender, EventArgs e)
    {
        Notify?.Invoke(true);
    }
}