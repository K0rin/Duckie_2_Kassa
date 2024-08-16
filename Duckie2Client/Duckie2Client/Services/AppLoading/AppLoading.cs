using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Duckie2Client.Services.AppLoading;

public sealed class AppLoading
{
    // An event notifies about an action completion.
    public event Action<string>? ActionCompleted;

    public async Task LoadAppActionAsync()
    {
        var loadingJobs = new List<LoadingJob>
        {
            /*
             * NOTE: Order of items is important.
             */
            new ConfigFileCheck(ActionCompleted),
            new ServiceRunningCheck(ActionCompleted)
        };

        // Run all jobs from the list.
        foreach (var job in loadingJobs) await job.CallJobTask();
    }

    public async Task LoadAppActionAsync(List<LoadingJob> loadingJobs)
    {
        foreach (var job in loadingJobs) job.Action += ActionCompleted;

        // Run all jobs from the list.
        foreach (var job in loadingJobs) await job.CallJobTask();
    }

    public void LoadAppAction(List<LoadingJob> loadingJobs)
    {
        foreach (var job in loadingJobs) job.Action += ActionCompleted;

        // Run all jobs from the list.
        foreach (var job in loadingJobs) job.CallJob();
    }
}
//     //todo: refact: Использовать данный конструктор и для загрузги приложения. 
//     jobRunner.LoadAppAction(jobList);