using System.Threading.Tasks;
using Duckie2Client.Enums;
using Duckie2Client.Libs;

namespace Duckie2Client.Services.Controls;

public class DataState : TabState
{
    public DataState()
    {
        State = TabStates.Data;
    }
    public override Task<NullOrResult> UpdateData()
    {
        // nothing to do.
        return Task.FromResult(new NullOrResult());
    }

    public override void CancelDataLoading()
    {
        // nothing to do.
    }
}