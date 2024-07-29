using System;
using System.Threading.Tasks;

namespace Duckie2Client.Services;

public sealed class AppLoading
{
    // An event notifies about an action completion.
    public event Action<string>? ActionCompleted;
    public async Task ExecuteActionAsync()
    {
        // Check if SQL Server service is running.
        
        var t1 = new AppLoadingMethodServiceRunning(ActionCompleted);
        var result = await t1.TemplateMethod();

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