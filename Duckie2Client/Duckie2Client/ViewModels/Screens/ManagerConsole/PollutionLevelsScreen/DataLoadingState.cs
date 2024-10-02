using System;
using System.Threading;
using System.Threading.Tasks;
using Duckie2Client.Enums.Flags;
using Duckie2Client.Libs;
using Duckie2Client.Models.Database;
using Duckie2Client.Services.Controls;
using Duckie2Client.Services.DbmsService;

namespace Duckie2Client.ViewModels.Screens.ManagerConsole.PollutionLevelsScreen;

public class DataLoadingState : TabState
{
    private static CancellationTokenSource _cancelTokenSource = null!;

    private static NullOrResult LoadData()
    {
        var output = new PollutionLevels().Read<PollutionLevel>(RecordReadFlags.None);

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