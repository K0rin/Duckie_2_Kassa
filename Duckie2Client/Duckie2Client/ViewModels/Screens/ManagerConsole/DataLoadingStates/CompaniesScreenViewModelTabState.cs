using System;
using System.Threading;
using System.Threading.Tasks;
using Duckie2Client.Enums;
using Duckie2Client.Enums.Flags;
using Duckie2Client.Libs;
using Duckie2Client.Models.Database;
using Duckie2Client.Services.Controls;
using Duckie2Client.Services.DbmsService;

namespace Duckie2Client.ViewModels.Screens.ManagerConsole.DataLoadingStates;

public class CompaniesScreenViewModelTabState : TabState
{
    private static CancellationTokenSource _cancelTokenSource = null!;

    public CompaniesScreenViewModelTabState()
    {
        State = TabStates.DataLoading;
    }

    private static NullOrResult LoadData()
    {
        var output = new Companies().Read<Company>(RecordReadFlags.ActiveRecords);

        var result = new NullOrResult
        {
            Result = output
        };
        return result;
    }


    public override async Task<NullOrResult> UpdateData()
    {
        _cancelTokenSource = new CancellationTokenSource();
        var token = _cancelTokenSource.Token;

        var x = await Task.Run(LoadData, token);
        return x;
    }

    public override void CancelDataLoading()
    {
        Console.WriteLine(@"Load data cancellation...");
        _cancelTokenSource.Cancel();
        _cancelTokenSource.Dispose();
    }
}