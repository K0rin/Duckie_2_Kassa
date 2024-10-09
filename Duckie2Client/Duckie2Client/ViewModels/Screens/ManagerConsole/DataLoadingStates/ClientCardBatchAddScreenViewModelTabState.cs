using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Duckie2Client.Libs;
using Duckie2Client.Services.Controls;
using Duckie2Client.Services.DbmsService;

namespace Duckie2Client.ViewModels.Screens.ManagerConsole.DataLoadingStates;

/// <summary>
/// Класс реализует функционал, задействованный в работе состояния вкладки "DataLoading" экрана "Пакетное добавление
/// скидки Карты Клиента".
/// </summary>
public class ClientCardBatchAddScreenViewModelTabState : TabState
{
    private static CancellationTokenSource _cancelTokenSource = null!;

    private static NullOrResult LoadCompanyList()
    {
        // Get the company list.
        List<string> companies;
        using (var db = new DbmsService())
        {
            // companies = db.LegacyCompanies.Select(e => e.Name).ToList();
        }

        // Clean collection. Remove empty names if they exist.
        // companies = companies.Where(n => n.Trim().Length > 0).ToList();

        var result = new NullOrResult
        {
            // Result = companies
        };
        return result;
    }


    public override async Task<NullOrResult> UpdateData()
    {
        _cancelTokenSource = new CancellationTokenSource();
        var token = _cancelTokenSource.Token;

        var x = await Task.Run(LoadCompanyList, token);
        return x;
    }

    public override void CancelDataLoading()
    {
        Console.WriteLine(@"Load data cancellation...");
        _cancelTokenSource.Cancel();
        _cancelTokenSource.Dispose();
    }
}