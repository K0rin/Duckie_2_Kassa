using System;

namespace Duckie2Client.Services.Commands;

public abstract class BaseDuckieCommand : IDuckieCommand
{
    public event Action<string>? NotifyStatus;
    public abstract void Execute();
    public abstract void Execute(out bool result);

    public abstract void ExecuteWithResult(out object? result);

    public void Notify(string message)
    {
        NotifyStatus?.Invoke(message);
    }
}