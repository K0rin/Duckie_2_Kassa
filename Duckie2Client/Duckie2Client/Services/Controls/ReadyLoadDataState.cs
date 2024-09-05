namespace Duckie2Client.Services.Controls;

public class ReadyLoadDataState : TabState
{
    public override string? UpdateData()
    {
        // Nothing to do due to Ready State does not perform any operations.
        return null;
    }

    public override void CancelDataLoading()
    {
        // Nothing to do due to Ready State does not perform any operations.
    }
}