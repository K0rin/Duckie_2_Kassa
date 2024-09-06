using System.Threading.Tasks;
using Duckie2Client.Libs;

namespace Duckie2Client.Services.Controls;

public class ReadyLoadDataState : TabState
{
    public override Task<NullOrResult> UpdateData()
    {
        // Nothing to do due to Ready State does not perform any operations.
        return null!;
    }

    public override void CancelDataLoading()
    {
        // Nothing to do due to Ready State does not perform any operations.
    }
}