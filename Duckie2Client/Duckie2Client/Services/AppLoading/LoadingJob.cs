using System;
using System.Threading.Tasks;
using Duckie2Client.Libs;

namespace Duckie2Client.Services.AppLoading;

/// <summary>
///     Template method for loading tasks.
/// </summary>
/// <param name="action"></param>
public abstract class LoadingJob(Action<string>? action)
{
    protected LoadingJob() : this(null)
    {
    }
    protected LoadingMessages LoadingMessages { get; init; }
    public event Action<string>? Action = action;

    public async Task CallJob()
    {
        // ::debug::
        const int overallTimer = 0;

        // Show a start message before performing a custom functionality.
        ShowStatusMessage(LoadingMessages.Start);

        // ::debug::
        await Task.Delay(overallTimer);

        // Run custom functionality.
        try
        {
            DoTask();
        }
        catch (DuckieException e)
        {
            ShowStatusMessage(e.Message);
            throw;
        }

        // ::debug::
        await Task.Delay(overallTimer);
    }

    /// <summary>
    ///     Contains the functionality of the task invoked when the application is
    ///     loading.
    ///     A method that must be overridden in an inherited class.
    /// </summary>
    /// <returns>
    ///     True - if there is no any error during performing a loading job.
    ///     False - an error occured during a loading job.
    /// </returns>
    protected abstract void DoTask();

    /// <summary>
    ///     Invokes event for showing a message on a splash screen.
    /// </summary>
    /// <param name="message">Message text.</param>
    private void ShowStatusMessage(string message)
    {
        Action?.Invoke(message);
    }

  
}