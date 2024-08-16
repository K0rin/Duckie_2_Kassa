using System;
using Avalonia.Controls;
using ReactiveUI.Fody.Helpers;

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

    private void UserControl_AttachedToVisualTree(object? sender, EventArgs e)
    {
        Notify?.Invoke(true);
    }
}