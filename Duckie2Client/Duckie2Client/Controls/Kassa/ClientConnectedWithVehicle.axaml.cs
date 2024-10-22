using Avalonia;
using Avalonia.Controls;
using Duckie2Client.ViewModels.Screens;
using System;

namespace Duckie2Client.Controls.Kassa;

public partial class ClientConnectedWithVehicle : UserControl
{
    public ClientConnectedWithVehicle()
    {
        InitializeComponent();
    }

    private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        // Получаем ViewModel из DataContext
        if (DataContext is MainKassaScreenViewModel viewModel)
        {
            var debug = true;
            // Выполняем команду SelectClient
            viewModel.SelectCLientExecute(sender,e);
        }
    }


}