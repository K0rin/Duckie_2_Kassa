using System;

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

    /// <summary>
    /// Sends a text message to the NotifyStatus event subscribers.
    /// </summary>
    /// <param name="message">Message to send.</param>
    public void Notify(string message);
}