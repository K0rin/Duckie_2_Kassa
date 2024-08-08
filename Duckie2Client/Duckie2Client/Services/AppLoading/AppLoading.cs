using System;
using System.Collections.Generic;
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
            new ServiceRunningCheck(ActionCompleted),
            // todo: Соединение должно проверяться на Экране Авторизации после ввода признаков пользователя.
            // new DatabaseConnectionCheck(ActionCompleted)
        };

        // Run all jobs from the list.
        foreach (var job in loadingJobs) await job.CallJob();
    }
}