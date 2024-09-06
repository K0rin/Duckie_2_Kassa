using System.Threading.Tasks;
using Duckie2Client.Libs;

namespace Duckie2Client.Services.Controls;

public class DataState : TabState
{
    public override async Task<NullOrResult> UpdateData()
    {
        // nothing to do.
        return new NullOrResult();
    }

    public override void CancelDataLoading()
    {
        // nothing to do.
    }
}