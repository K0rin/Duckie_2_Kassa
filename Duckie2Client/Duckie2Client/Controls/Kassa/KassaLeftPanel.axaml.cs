using Avalonia;
using Avalonia.Controls;
using Duckie2Client.ViewModels.Screens;
using System;

namespace Duckie2Client.Controls.Kassa;

public partial class KassaLeftPanel : UserControl
{
    public KassaLeftPanel()
    {
        InitializeComponent();
    }

    private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (DataContext is MainKassaScreenViewModel viewModel)
        {
            viewModel.SelectCLientExecute(sender,e);
        }
    }


}