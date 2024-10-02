using Duckie2Client.ViewModels;
using Duckie2Client.ViewModels.Dialogs;
using Duckie2Client.ViewModels.Screens;
using Duckie2Client.ViewModels.Screens.ManagerConsole.BranchesScreen;
using Duckie2Client.ViewModels.Screens.ManagerConsole.ClientCardBatchAddScreen;
using Duckie2Client.ViewModels.Screens.ManagerConsole.ClientsScreen;
using Duckie2Client.ViewModels.Screens.ManagerConsole.PersonnelScreen;
using Duckie2Client.ViewModels.Screens.ManagerConsole.PollutionLevelsScreen;
using Duckie2Client.ViewModels.Screens.ManagerConsole.PriceTypesScreen;
using Splat;

namespace Duckie2Client;

public partial class App
{
    #region View Models

    // todo: move to another partial file.

    // ReSharper disable once InconsistentNaming
    public static MainConsoleScreenViewModel VM_MainConsoleScreen =>
        Locator.Current.GetService<MainConsoleScreenViewModel>()!;

    // ReSharper disable once InconsistentNaming
    public static AuthorizationScreenViewModel VM_AuthorizationScreen =>
        Locator.Current.GetService<AuthorizationScreenViewModel>()!;

    // ReSharper disable once InconsistentNaming
    public static ConsoleWindowViewModel VM_ConsoleWindow => Locator.Current.GetService<ConsoleWindowViewModel>()!;

    // ReSharper disable once InconsistentNaming
    public static KassaWindowViewModel VM_KassaWindow => Locator.Current.GetService<KassaWindowViewModel>()!;

    // ReSharper disable once InconsistentNaming
    public static SpinnerDialogViewModel VM_SpinnerDialog => Locator.Current.GetService<SpinnerDialogViewModel>()!;

    // ReSharper disable once InconsistentNaming
    public static InitialSetupWizardViewModel VM_InitialSetupWizard =>
        Locator.Current.GetService<InitialSetupWizardViewModel>()!;

    // ReSharper disable once InconsistentNaming
    public static ClientCardBatchAddScreenViewModel VM_ClientCardBatchAdd =>
        Locator.Current.GetService<ClientCardBatchAddScreenViewModel>()!;

    // ReSharper disable once InconsistentNaming
    public static PersonnelScreenViewModel VM_PersonnelScreen =>
        Locator.Current.GetService<PersonnelScreenViewModel>()!;

    // ReSharper disable once InconsistentNaming
    public static BranchesScreenViewModel VM_BranchesScreen =>
        Locator.Current.GetService<BranchesScreenViewModel>()!;

    // ReSharper disable once InconsistentNaming
    public static PollutionLevelsScreenViewModel VM_PollutionLevelsScreen =>
        Locator.Current.GetService<PollutionLevelsScreenViewModel>()!;

    // ReSharper disable once InconsistentNaming
    public static PriceTypesScreenViewModel VM_PriceTypesScreen =>
        Locator.Current.GetService<PriceTypesScreenViewModel>()!;

    // ReSharper disable once InconsistentNaming
    public static ClientsScreenViewModel VM_ClientsScreen =>
        Locator.Current.GetService<ClientsScreenViewModel>()!;

    #endregion
}