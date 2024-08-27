using Duckie2Client.Libs.HanumanInstitute;
using Duckie2Client.ViewModels;
using Duckie2Client.ViewModels.Dialogs;
using Duckie2Client.ViewModels.Screens;
using Duckie2Client.ViewModels.Screens.ManagerConsole;
using Duckie2Client.Views;
using Duckie2Client.Views.Dialogs;
using Duckie2Client.Views.Screens;
using Duckie2Client.Views.Screens.ManagerConsole;

namespace Duckie2Client;

public class ViewLocator : StrongViewLocator
{
    public ViewLocator()
    {
        Register<MainConsoleScreenViewModel, MainConsoleScreenView>();
        Register<AuthorizationScreenViewModel, AuthorizationScreenView>();
        Register<ConsoleWindowViewModel, ConsoleWindow>();
        Register<KassaWindowViewModel, KassaWindow>();
        Register<InitialSetupWizardViewModel, InitialSetupWizardWindow>();
        // Dialogs
        Register<SpinnerDialogViewModel, SpinnerDialog>();
        // Screens
        Register<BatchServiceAddingScreenViewModel, BatchServiceAddingScreenView>();
    }
}