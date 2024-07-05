using System;
using System.Threading.Tasks;

namespace Duckie2Client.Services;

public sealed class AppLoading
{
    // An event notifies about an action completion.
    public event Action<int>? ActionCompleted;
    public async Task ExecuteActionAsync()
    {
        for (int i = 0; i < 5; i++)
        {
            // todo: do loading.
            Console.WriteLine($"Task {i} in progress...");
            await Task.Delay(1000);
            OnActionCompleted(i);
        }
    }
    private void OnActionCompleted(int actionNumber)
    {
        ActionCompleted?.Invoke(actionNumber);
    }
}