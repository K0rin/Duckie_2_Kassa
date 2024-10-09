// Each ViewModel must be registered after creating its own class and the View class where that ViewModel is used.
// Registration takes place at the following locations:
// 
// - Duckie2Client/App.axaml.cs
// 
//     Add string to the Initialize method before calling the SplatRegistrations.SetupIOC() method:
// 
//         SplatRegistrations.Register<{viewmodel_type}>();
// 
//     Add class attribute:
// 
//         public static {viewmodel_type} VM_{viewmodel_type_name_without_viewmodel_suffix} => 
//             Locator.Current.GetService<{viewmodel_type}>()!;
// 
// - Duckie2Client/ViewLocator.cs
// 
//       Add the following line to the class constructor:
// 
//           Register<{viewmodel_type}, {view_type}>();


using Duckie2Client.Libs.HanumanInstitute;
using Duckie2Client.ViewModels;
using Duckie2Client.ViewModels.Dialogs;
using Duckie2Client.ViewModels.Screens;
using Duckie2Client.ViewModels.Screens.ManagerConsole.BranchesScreen;
using Duckie2Client.ViewModels.Screens.ManagerConsole.ClientCardBatchAddScreen;
using Duckie2Client.ViewModels.Screens.ManagerConsole.ClientsScreen;
using Duckie2Client.ViewModels.Screens.ManagerConsole.CompaniesScreen;
using Duckie2Client.ViewModels.Screens.ManagerConsole.PersonnelScreen;
using Duckie2Client.ViewModels.Screens.ManagerConsole.PollutionLevelsScreen;
using Duckie2Client.ViewModels.Screens.ManagerConsole.PriceTypesScreen;
using Duckie2Client.ViewModels.Screens.ManagerConsole.TradeUnitsScreen;
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
        Register<ClientCardBatchAddScreenViewModel, ClientCardBatchAddScreenView>();
        Register<PersonnelScreenViewModel, PersonnelScreenView>();
        Register<BranchesScreenViewModel, BranchesScreenView>();
        Register<PollutionLevelsScreenViewModel, PollutionLevelsScreenView>();
        Register<PriceTypesScreenViewModel, PriceTypesScreenView>();
        Register<ClientsScreenViewModel, ClientsScreenView>();
        Register<CompaniesScreenViewModel, CompaniesScreenView>();
        Register<TradeUnitsScreenViewModel, TradeUnitsScreenView>();
    }
}