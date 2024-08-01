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

    public async Task<bool> CallJob()
    {
        // ::debug::
        var overallTimer = 1000;

        // Show a start message before performing a custom functionality.
        ShowStatusMessage(LoadingMessages.Start);
        // ::debug::
        await Task.Delay(overallTimer);
        // Run custom functionality.
        var result = DoTask();
        // Show an end message after a custom job done.
        ShowStatusMessage(result
            ? LoadingMessages.GoodStatus
            : LoadingMessages.BadStatus);
        // ::debug::
        await Task.Delay(overallTimer);
        return result;
    }

    /// <summary>
    /// Contains the functionality of the task invoked when the application is
    /// loading. 
    /// 
    /// A method that must be overridden in an inherited class.
    /// </summary>
    /// <returns>
    /// True - if there is no any error during performing a loading job.
    /// False - an error occured during a loading job.
    /// </returns>
    protected abstract bool DoTask();

    /// <summary>
    /// Invokes event for showing a message on a splash screen.
    /// </summary>
    /// <param name="message">Message text.</param>
    private void ShowStatusMessage(string message)
    {
        Action?.Invoke(message);
    }

    protected LoadingMessages LoadingMessages { get; init; }
}