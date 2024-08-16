namespace Duckie2Client.Services.Commands;

public class DuckieCommandInvoker
{
    private IDuckieCommand? _duckieCommand;

    public void SetCommand(IDuckieCommand command)
    {
        _duckieCommand = command;
    }

    public void ExecuteCommand()
    {
        _duckieCommand?.Execute();
    }
}