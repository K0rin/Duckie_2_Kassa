using System;

namespace Duckie2Client.Services.Controls;

public class DataLoadingState : TabState
{
    public override void LoadData()
    {
        Console.WriteLine(@"Load data begin...");
        // Context?.SetState(new DataLoadingState());
    }

    public override void CancelDataLoading()
    {
        Console.WriteLine(@"data loading cancelling...");
        // Context.SetState(new DataNotLoadedState());
    }
}