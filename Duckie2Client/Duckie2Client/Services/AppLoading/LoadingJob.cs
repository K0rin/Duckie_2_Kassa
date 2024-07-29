using System;
using System.Threading.Tasks;

namespace Duckie2Client.Services.AppLoading;
/// <summary>
/// Template method for loading tasks.
/// </summary>
/// <param name="action"></param>
public abstract class LoadingJob(Action<string>? action)
{
    private event Action<string>? Action = action;

    public async Task<bool> TemplateMethod()
    {
        ShowStatusMessage(LoadingMessages.Start);
        await Task.Delay(3000);
        var result = DoTask();
        ShowStatusMessage(result
            ? LoadingMessages.GoodStatus
            : LoadingMessages.BadStatus);
        await Task.Delay(6000);
        return result;
    }

    protected abstract bool DoTask();

    private void ShowStatusMessage(string message)
    {
        Action?.Invoke(message);
    }

    protected LoadingMessages LoadingMessages { get; init; }
}