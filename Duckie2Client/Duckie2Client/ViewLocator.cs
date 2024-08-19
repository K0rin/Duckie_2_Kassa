using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Duckie2Client.Libs.HanumanInstitute;
using Duckie2Client.ViewModels;
using Duckie2Client.ViewModels.Base;
using Duckie2Client.ViewModels.Dialogs;
using Duckie2Client.Views;
using Duckie2Client.Views.Dialogs;
using Duckie2Client.Views.Screens;

namespace Duckie2Client;

public class ViewLocator : StrongViewLocator
{
    public ViewLocator()
    {
        Register<MainConsoleScreenViewModel, MainConsoleScreenView>();
        Register<AuthorizationScreenViewModel, AuthorizationScreenView>();
        Register<ConsoleWindowViewModel, ConsoleWindow>();
        Register<KassaWindowViewModel, KassaWindow>();

        // Dialogs
        Register<SpinnerDialogViewModel, SpinnerDialog>();
    }
}
// public class ViewLocator : IDataTemplate
// {
//     public Control? Build(object? data)
//     {
//         if (data is null) return null;
//
//         var name = data.GetType().FullName!;
//
//         if (name.EndsWith("ScreenViewModel"))
//             name = data.GetType().FullName!
//                 .Replace(
//                     "ScreenViewModel",
//                     "ScreenView",
//                     StringComparison.Ordinal)
//                 .Replace(
//                     "ViewModel",
//                     "Views.Screen",
//                     StringComparison.Ordinal);
//         else
//             name = data.GetType().FullName!
//                 .Replace("ViewModel", "View", StringComparison.Ordinal);
//
//         var type = Type.GetType(name);
//         if (type == null) return new TextBlock { Text = "Not Found: " + name };
//         var control = (Control)Activator.CreateInstance(type)!;
//         control.DataContext = data;
//         return control;
//     }
//
//     public bool Match(object? data)
//     {
//         return data is ViewModelBase;
//     }
// }