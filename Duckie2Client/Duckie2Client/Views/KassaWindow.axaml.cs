using Avalonia.Controls;
using Avalonia;
using Avalonia.Interactivity;
using System;
using Duckie2Client.ViewModels.Screens;
using System.Diagnostics;

using System.Drawing;
using ReactiveUI;

namespace Duckie2Client.Views;

public partial class KassaWindow : Window
{

    private bool isMaximized = false;
    public KassaWindow()
    {
        InitializeComponent();
        //this.PropertyChanged += OnPropertyChanged;
        var debug = true;
        MainKassaScreenViewModel.SetPanelHeigt(this.Height * 0.93);
        //MainKassaScreenViewModel.PanelHeigth += this.Height*0.93;
        //this.WindowState = WindowState.Maximized;
    }

    private void OnPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        
        if (e.Property == Window.WindowStateProperty)
        {
            var debug = true;
            //MainKassaScreenViewModel.PanelHeigth = 900;
            if (this.WindowState == WindowState.Maximized)
            {
                debug = true;
                //kassaScreenViewModel = new MainKassaScreenViewModel();
                
                //MainKassaScreenViewModel.PanelHeigth = 900;

            }
            else if (this.WindowState == WindowState.Normal)
            {
                //MainKassaScreenViewModel.PanelHeigth = 220;
            }
            else if (this.WindowState == WindowState.Minimized)
            {
                Console.WriteLine("Окно свернуто!");
                //MainKassaScreenViewModel.PanelHeigth = 880;
            }
        }
    }
}