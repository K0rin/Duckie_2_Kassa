using System;
using System.Threading.Tasks;

namespace Duckie2Client.Services.AppLoading;

public sealed class AppLoading
{
    // An event notifies about an action completion.
    public event Action<string>? ActionCompleted;
    public async Task LoadAppActionAsync()
    {
        // Check if SQL Server service is running.
        
        var serviceRunningCheck = new ServiceRunningCheck(ActionCompleted);
        var result = await serviceRunningCheck.TemplateMethod();

        if (result)
        {
            Console.WriteLine("ok");
        }
        else
        {
            Console.WriteLine("bad");
        }



    }

}