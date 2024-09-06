using System;
using System.Threading;
using System.Threading.Tasks;
using Duckie2Client.Libs;

namespace Duckie2Client.Services.Controls;

public class DataLoadingState : TabState
{
    private static CancellationTokenSource _cancelTokenSource = null!;

    private static NullOrResult LoadCompanyList()
    {
        Console.WriteLine(@"Task start...");

        var i = 0;
        const int TOTAL = 10;

        while (i < TOTAL)
        {
            if (_cancelTokenSource.IsCancellationRequested)
            {
                Console.WriteLine(@"Task was cancelled.");
                return new NullOrResult();
            }

            i++;
            Console.WriteLine($@"Loading company {i}/{TOTAL}");
            Thread.Sleep(500);
        }

        Console.WriteLine(@"...Task end");

        var result = new NullOrResult
        {
            Result = "company list data payload"
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