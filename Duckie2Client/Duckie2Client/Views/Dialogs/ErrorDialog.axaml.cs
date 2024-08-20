using System;
using Avalonia.Controls;

namespace Duckie2Client.Views.Dialogs;

public partial class ErrorDialog : UserControl
{
    public ErrorDialog(string message)
    {
        InitializeComponent();
        if (string.IsNullOrWhiteSpace(message)) throw new Exception("The Error Dialog message is empty.");
        MessageTextBlock.Text = message;
    }
}