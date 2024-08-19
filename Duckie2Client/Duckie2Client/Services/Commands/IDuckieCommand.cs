using System;
using System.Data.SqlClient;
using System.Threading;

namespace Duckie2Client.Services.Commands;

public interface IDuckieCommand
{
    /// <summary>
    /// Event to return a message about the status of procedure execution.
    /// </summary>
    public event Action<string>? NotifyStatus;

    /// <summary>
    /// A command payload execution.
    /// </summary>
    void Execute();

    void Execute(out bool result);
    void Execute(CancellationToken token);

    void ExecuteWithResult(out object? result);

    /// <summary>
    /// Sends a text message to the NotifyStatus event subscribers.
    /// </summary>
    /// <param name="message">Message to send.</param>
    public void Notify(string message);
}