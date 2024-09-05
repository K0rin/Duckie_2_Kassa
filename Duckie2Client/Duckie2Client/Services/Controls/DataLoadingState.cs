using System;

namespace Duckie2Client.Services.Controls;

// Описание состояния для всех вкладок. В этом состоянии может меняться текстовая метка.
public class DataLoadingState : TabState
{
    public override string UpdateData()
    {
        Console.WriteLine(@"Load data begin...");
        Console.WriteLine(@"Load data end.");
        return "loaded data";
    }

    public override void CancelDataLoading()
    {
        Console.WriteLine(@"Load data cancel...");
    }
}