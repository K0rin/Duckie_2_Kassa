using Duckie2Client.ViewModels;
using Duckie2Client.ViewModels.Dialogs;
using Duckie2Client.ViewModels.Screens;
using Duckie2Client.ViewModels.Screens.ManagerConsole;
using Splat;

// ReSharper disable InconsistentNaming

namespace Duckie2Client;

public partial class App
{
    #region View Models

    // todo: move to another partial file.

    public static MainConsoleScreenViewModel VM_MainConsoleScreen =>
        Locator.Current.GetService<MainConsoleScreenViewModel>()!;

    public static AuthorizationScreenViewModel VM_AuthorizationScreen =>
        Locator.Current.GetService<AuthorizationScreenViewModel>()!;

    public static ConsoleWindowViewModel VM_ConsoleWindow => Locator.Current.GetService<ConsoleWindowViewModel>()!;

    public static KassaWindowViewModel VM_KassaWindow => Locator.Current.GetService<KassaWindowViewModel>()!;

    public static SpinnerDialogViewModel VM_SpinnerDialog => Locator.Current.GetService<SpinnerDialogViewModel>()!;

    public static InitialSetupWizardViewModel VM_InitialSetupWizard =>
        Locator.Current.GetService<InitialSetupWizardViewModel>()!;

    public static ClientCardBatchAddScreenViewModel VM_ClientCardBatchAdd =>
        Locator.Current.GetService<ClientCardBatchAddScreenViewModel>()!;

    public static PersonnelScreenViewModel VM_PersonnelScreen =>
        Locator.Current.GetService<PersonnelScreenViewModel>()!;

    public static BranchesScreenViewModel VM_BranchesScreen =>
        Locator.Current.GetService<BranchesScreenViewModel>()!;

    public static PollutionLevelsScreenViewModel VM_PollutionLevelsScreen =>
        Locator.Current.GetService<PollutionLevelsScreenViewModel>()!;

    public static PriceTypesScreenViewModel VM_PriceTypesScreen =>
        Locator.Current.GetService<PriceTypesScreenViewModel>()!;

    public static ClientsScreenViewModel VM_ClientsScreen =>
        Locator.Current.GetService<ClientsScreenViewModel>()!;

    public static CompaniesScreenViewModel VM_CompaniesScreen =>
        Locator.Current.GetService<CompaniesScreenViewModel>()!;

    public static TradeUnitsScreenViewModel VM_TradeUnitsScreen =>
        Locator.Current.GetService<TradeUnitsScreenViewModel>()!;

    #endregion
}