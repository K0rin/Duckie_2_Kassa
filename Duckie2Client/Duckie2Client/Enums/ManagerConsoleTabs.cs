using System.Collections.Generic;
using Duckie2Client.Libs;
using Duckie2Client.Libs.Enums;
using Duckie2Client.Resources;
using Duckie2Client.Services.Controls;
using Duckie2Client.ViewModels.Screens.ManagerConsole.DataLoadingStates;
using Duckie2Client.Views.Base;
using Duckie2Client.Views.Screens.ManagerConsole;

namespace Duckie2Client.Enums;

public enum ManagerConsoleTabs
{
    BranchesList,
    ClientsList,
    CompaniesList,
    PersonnelList,
    PriceTypesList,
    PollutionLevelsList,
    TradeUnitsList
}

public static class ManagerConsoleTabsExtensions
{
    private static readonly Dictionary<ManagerConsoleTabs, string> Titles = new()
    {
        {
            ManagerConsoleTabs.BranchesList,
            Localization.GetString(() => UserInterface.TabTitleBranchList, ResourceTypes.UserInterface)
        },
        {
            ManagerConsoleTabs.ClientsList,
            Localization.GetString(() => UserInterface.TabTitleClientList, ResourceTypes.UserInterface)
        },
        {
            ManagerConsoleTabs.CompaniesList,
            Localization.GetString(() => UserInterface.TabTitleCompanyList, ResourceTypes.UserInterface)
        },
        {
            ManagerConsoleTabs.PersonnelList,
            Localization.GetString(() => UserInterface.TabTitlePersonnelList, ResourceTypes.UserInterface)
        },
        {
            ManagerConsoleTabs.PriceTypesList,
            Localization.GetString(() => UserInterface.TabTitlePriceTypeList, ResourceTypes.UserInterface)
        },
        {
            ManagerConsoleTabs.PollutionLevelsList,
            Localization.GetString(() => UserInterface.TabTitlePollutionList, ResourceTypes.UserInterface)
        },
        {
            ManagerConsoleTabs.TradeUnitsList,
            Localization.GetString(() => UserInterface.TabTitleTradeUnitList, ResourceTypes.UserInterface)
        }
    };

    private static readonly Dictionary<ManagerConsoleTabs, TabStateRecord> TabRecords = new()
    {
        {
            ManagerConsoleTabs.BranchesList, new TabStateRecord(
                new BranchesScreenViewModelTabState(),
                Localization.GetString(() => UserInterface.TabReadyStateMessageBranchList,
                    ResourceTypes.UserInterface),
                Localization.GetString(() => UserInterface.TabDataLoadingStateBranchList,
                    ResourceTypes.UserInterface))
        },
        {
            ManagerConsoleTabs.ClientsList, new TabStateRecord(
                new ClientsScreenViewModelTabState(),
                Localization.GetString(() => UserInterface.TabReadyStateMessageClientList,
                    ResourceTypes.UserInterface),
                Localization.GetString(() => UserInterface.TabDataLoadingStateClientList,
                    ResourceTypes.UserInterface))
        },
        {
            ManagerConsoleTabs.CompaniesList, new TabStateRecord(
                new CompaniesScreenViewModelTabState(),
                Localization.GetString(() => UserInterface.TabReadyStateMessageCompanyList,
                    ResourceTypes.UserInterface),
                Localization.GetString(() => UserInterface.TabDataLoadingStateCompanyList,
                    ResourceTypes.UserInterface))
        },
        {
            ManagerConsoleTabs.PersonnelList, new TabStateRecord(
                new PersonnelScreenViewModelTabState(),
                Localization.GetString(() => UserInterface.TabReadyStateMessagePersonnelList,
                    ResourceTypes.UserInterface),
                Localization.GetString(() => UserInterface.TabDataLoadingStatePersonnelList,
                    ResourceTypes.UserInterface))
        },
        {
            ManagerConsoleTabs.PriceTypesList, new TabStateRecord(
                new PriceTypesScreenViewModelTabState(),
                Localization.GetString(() => UserInterface.TabReadyStateMessagePriceTypeList,
                    ResourceTypes.UserInterface),
                Localization.GetString(() => UserInterface.TabDataLoadingStatePriceTypeList,
                    ResourceTypes.UserInterface))
        },
        {
            ManagerConsoleTabs.PollutionLevelsList, new TabStateRecord(
                new PollutionLevelsScreenViewModelTabState(),
                Localization.GetString(() => UserInterface.TabReadyStateMessagePollutionList,
                    ResourceTypes.UserInterface),
                Localization.GetString(() => UserInterface.TabDataLoadingStatePollutionList,
                    ResourceTypes.UserInterface))
        },
        {
            ManagerConsoleTabs.TradeUnitsList, new TabStateRecord(
                new TradeUnitsScreenViewModelTabState(),
                Localization.GetString(() => UserInterface.TabReadyStateMessageTradeUnitList,
                    ResourceTypes.UserInterface),
                Localization.GetString(() => UserInterface.TabDataLoadingStateTradeUnitList,
                    ResourceTypes.UserInterface))
        }
    };

    private static readonly Dictionary<ManagerConsoleTabs, TabUserControlView> Views = new()
    {
        { ManagerConsoleTabs.BranchesList, new BranchesScreenView() },
        { ManagerConsoleTabs.ClientsList, new ClientsScreenView() },
        { ManagerConsoleTabs.CompaniesList, new CompaniesScreenView() },
        { ManagerConsoleTabs.PersonnelList, new PersonnelScreenView() },
        { ManagerConsoleTabs.PriceTypesList, new PriceTypesScreenView() },
        { ManagerConsoleTabs.PollutionLevelsList, new PollutionLevelsScreenView() },
        { ManagerConsoleTabs.TradeUnitsList, new TradeUnitsScreenView() }
    };

    public static string GetTitle(this ManagerConsoleTabs mct)
    {
        return Titles[mct];
    }

    public static TabStateRecord GetTabRecord(this ManagerConsoleTabs mct)
    {
        return TabRecords[mct];
    }

    public static TabUserControlView GetView(this ManagerConsoleTabs mct)
    {
        return Views[mct];
    }
}