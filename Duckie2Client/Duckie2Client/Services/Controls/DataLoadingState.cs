using System;

namespace Duckie2Client.Services.Controls;

public class DataLoadingState : TabState
{
    public override void Handle()
    {
        Console.WriteLine(@"Load data begin...");
    }
}