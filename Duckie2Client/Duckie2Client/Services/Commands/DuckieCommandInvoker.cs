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

    public void ExecuteCommand(out bool result)
    {
        result = false;
        _duckieCommand?.Execute(out result);
    }

}