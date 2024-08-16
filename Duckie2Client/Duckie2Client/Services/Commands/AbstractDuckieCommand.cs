using System;

namespace Duckie2Client.Services.Commands;

public abstract class AbstractDuckieCommand : IDuckieCommand
{
    public void Notify(string message)
    {
        NotifyStatus?.Invoke(message);
    }

    public event Action<string>? NotifyStatus;
    public abstract void Execute();
}