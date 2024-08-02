using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Duckie2Client.Services.AppLoading;

public sealed class AppLoading
{
    // An event notifies about an action completion.
    public event Action<string>? ActionCompleted;

    public async Task<bool> LoadAppActionAsync()
    {
        var loadingJobs = new List<LoadingJob>
        {
            new ConfigFileCheck(ActionCompleted),
            new ServiceRunningCheck(ActionCompleted),
            new DatabaseConnectionCheck(ActionCompleted)
        };
        var result = false;
        // Run all jobs from the list.
        foreach (var job in loadingJobs)
        {
            result = await job.CallJob();

            if (!result)
                // If a job fails, stop loading and return.
                break;
        }

        return result;
    }
}