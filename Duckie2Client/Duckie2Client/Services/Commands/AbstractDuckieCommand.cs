using System;
using System.Data.SqlClient;
using System.Threading;

namespace Duckie2Client.Services.Commands;

public abstract class AbstractDuckieCommand : IDuckieCommand
{
    public event Action<string>? NotifyStatus;
    public abstract void Execute();
    public abstract void Execute(CancellationToken token);
    public abstract void Execute(out bool result);
    public abstract void Execute(CancellationToken token, out bool result);

    public abstract void ExecuteWithResult(out object? result);

    public void Notify(string message)
    {
        NotifyStatus?.Invoke(message);
    }
}