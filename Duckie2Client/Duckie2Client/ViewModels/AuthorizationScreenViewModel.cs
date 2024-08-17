using System;
using System.Net.Http.Headers;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Threading;
using DialogHostAvalonia;
using Duckie2Client.Libs;
using Duckie2Client.Services.Commands;
using Duckie2Client.Views.Dialogs;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Duckie2Client.ViewModels;

public class AuthorizationScreenViewModel : ViewModelBase
{
    public ReactiveCommand<Unit, Unit> BeginAuthorizationCommand { get; set; }
    [Reactive] public string Message { get; set; }
    [Reactive] public string UserLogin { get; set; }
    [Reactive] public string UserPassword { get; set; }
    private const string DIALOG_IDENTIFIER = "AuthorizationDialog";

    public AuthorizationScreenViewModel()
    {
        BeginAuthorizationCommand = ReactiveCommand.Create(BeginAuthorizationCommandExecute);
#if DEBUG
        UserLogin = "jevgeni";
        UserPassword = "urugula";
#endif
    }

    private bool _isDialogLoaded;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="token"></param>
    /// <returns><list type="bullet">
    /// <item>True - The user is authorized.</item>
    /// <item>False - The user is not authorized.</item>
    /// </list></returns>
    private bool DoAuthorization(CancellationToken token)
    {
        // Wait until the dialog is shown on the screen.
        while (!_isDialogLoaded)
        {
        }

        var checkDatabaseConnectionCommand = new CheckDatabaseConnectionCommand();
        var checkAuthorizationCommand = new CheckAuthorizationCommand(UserLogin, UserPassword);

        checkDatabaseConnectionCommand.NotifyStatus += status => Message = status;
        checkAuthorizationCommand.NotifyStatus += status => Message = status;

        var invoker = new DuckieCommandInvoker();

        // todo: put tasks/commands into list and pass it to SetCommand.

        var task1 = Task.Factory.StartNew(() =>
            {
                invoker.SetCommand(checkDatabaseConnectionCommand);
                invoker.ExecuteCommand();
                // debug
                // Thread.Sleep(1000);
            },
            token);
        task1.Wait(token);

        var authorizationResult = false;
        var task2 = task1.ContinueWith(_ =>
            {
                invoker.SetCommand(checkAuthorizationCommand);
                invoker.ExecuteCommand(out authorizationResult);
                // debug
                // Thread.Sleep(1000);
            },
            token);
        task2.Wait(token);

        // if (!authorizationResult) throw new Exception("not authorized");

        // After all tasks are finished, the dialog will be closed.
        Dispatcher.UIThread.InvokeAsync(() => DialogHost.Close(DIALOG_IDENTIFIER));

        return authorizationResult;
    }


    private void DialogAttachedToVisualTreeHandler(bool status)
    {
        _isDialogLoaded = true;
    }

    private void ExceptionHandler(Task task)
    {
        // Catches exceptions occurred in parent tasks of the authorization tasks.
        var exception = task.Exception;

        Console.WriteLine(@"custom user handler");
        Console.WriteLine(exception);


        // TODO: catch checking commands errors.

        // todo: handle errors.

        // if (exception is DuckieException) Console.WriteLine(exception.Message);
    }

    private async void BeginAuthorizationCommandExecute()
    {
        var cancelTokenSource = new CancellationTokenSource();
        var token = cancelTokenSource.Token;

        bool? dialogResult = null;
        var d = new SpinnerDialog();
        d.Notify += DialogAttachedToVisualTreeHandler;

        Task<bool> mainTask = null;
        d.OnDialogClosing += () => PostAuthorization(ref dialogResult, mainTask.Result, ref cancelTokenSource);
        mainTask = Task.Run(() => DoAuthorization(token), token);
        _ = mainTask.ContinueWith(ExceptionHandler, TaskContinuationOptions.OnlyOnFaulted);

        try
        {
            dialogResult = (bool)(await DialogHost.Show(d, DIALOG_IDENTIFIER))!;
        }
        catch (NullReferenceException)
        {
            // Console.WriteLine($"auth result: {mainTask.Result}");
            // This exception occurs if the dialog screen is closed. Ignore this exception.
            {
            }
        }
    }

    // todo: test with cancellation
    private static void PostAuthorization(
        ref bool? dialogResult,
        bool authorizationResult,
        ref CancellationTokenSource cancelTokenSource)
    {
        Console.WriteLine(@"OnDialogClosing");
        Console.WriteLine(@$"dialogResult={dialogResult}");
        Console.WriteLine(@$"authorizationResult={authorizationResult}");

        // todo: проверить, будет ли закрываться соединение с базой, если прервать поток.

        switch (dialogResult)
        {
            case false: // The authorization operation canceled.
                // todo: create aborting sequence.
                cancelTokenSource.CancelAsync();
                break;
            case null when !authorizationResult: // Authorization failed.
                break;
            case null when authorizationResult: // Authorization granted.
                // todo: Switch to the Main Console Screen.
                // ((ConsoleWindowViewModel)value).SwitchPage(1);
                break;
        }
    }
}