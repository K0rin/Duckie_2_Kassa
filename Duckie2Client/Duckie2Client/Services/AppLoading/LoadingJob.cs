using System;
using System.Threading.Tasks;
using Duckie2Client.Libs;

namespace Duckie2Client.Services.AppLoading;

public abstract class LoadingJob
{
    protected LoadingJob()
    {
    }

    protected LoadingJob(Action<string>? action)
    {
        _action = action;
    }

    protected LoadingMessages LoadingMessages { get; init; }
    private static event Action<string>? _action;
    public event Action<string>? Action = _action;

    public async Task CallJobTask()
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
            // todo: Надо ли показывать сообщение, если все равно будет отображен Диалог Ошибки?
            ShowStatusMessage(e.Message);
            throw;
        }

        // ::debug::
        await Task.Delay(overallTimer);
    }

    public void CallJob()
    {
        // Show a start message before performing a custom functionality.
        ShowStatusMessage(LoadingMessages.Start);

        // Run custom functionality.
        try
        {
            DoTask();
        }
        catch (DuckieException e)
        {
            // todo: Надо ли показывать сообщение, если все равно будет отображен Диалог Ошибки?
            ShowStatusMessage(e.Message);
            throw;
        }
    }

    /// <summary>
    /// <para>Contains the functionality of the task invoked when the
    /// application is loading. A method that must be overridden in an
    /// inherited class.</para>
    /// </summary>
    /// <returns>
    /// <para>True - if there is no any error during performing a loading job.
    /// <br/>False - an error occured during a loading job.</para>
    /// </returns>
    protected abstract void DoTask();

    /// <summary>
    /// <para>Invokes event for showing a message on a splash screen.</para>
    /// </summary>
    /// <param name="message">Message text.</param>
    private void ShowStatusMessage(string message)
    {
        Action?.Invoke(message);
    }
}