using System;
using System.Reactive;
using System.Threading;
using Avalonia.Threading;
using DialogHostAvalonia;
using Duckie2Client.Services.Commands;
using Duckie2Client.ViewModels.Base;
using Duckie2Client.Views.Dialogs;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Duckie2Client.ViewModels;

public class AuthorizationScreenViewModel : ViewModelPageBase
{
    public ReactiveCommand<Unit, Unit> BeginAuthorizationCommand { get; set; }
    [Reactive] public string Message { get; set; }
    [Reactive] public string UserLogin { get; set; }
    [Reactive] public string UserPassword { get; set; }
    private const string DIALOG_IDENTIFIER = "AuthorizationDialog";
    private bool _isDialogLoaded;
    private readonly Mutex _mutexObj = new();

    public AuthorizationScreenViewModel()
    {
        BeginAuthorizationCommand = ReactiveCommand.Create(BeginAuthorizationCommandExecute);
#if DEBUG
        Message = "::NONE::";
        UserLogin = "jevgeni";
        UserPassword = "urugula";
#endif
    }

    private void DoAuthorization(Action<bool> callback)
    {
        _mutexObj.WaitOne();

        // Wait until the dialog is shown on the screen.
        while (!_isDialogLoaded)
        {
        }

        var authorizationResult = false;

        try
        {
            var checkDatabaseConnectionCommand = new CheckDatabaseConnectionCommand();
            var checkAuthorizationCommand = new CheckAuthorizationCommand(UserLogin, UserPassword);

            checkDatabaseConnectionCommand.NotifyStatus += status => Message = status;
            checkAuthorizationCommand.NotifyStatus += status => Message = status;

            var invoker = new DuckieCommandInvoker();

            invoker.SetCommand(checkDatabaseConnectionCommand);
            invoker.ExecuteCommand();

            // Thread.Sleep(10000);

            invoker.SetCommand(checkAuthorizationCommand);
            invoker.ExecuteCommand(out authorizationResult);

            // Thread.Sleep(10000);
        }
        catch (ThreadAbortException e)
        {
            // Console.WriteLine(@"thread aborted");
        }
        catch (ThreadInterruptedException)
        {
            // Console.WriteLine(@"thread interrupted");
        }
        finally
        {
            // After all tasks are finished, the dialog will be closed.
            Dispatcher.UIThread.InvokeAsync(() => DialogHost.Close(DIALOG_IDENTIFIER));
            _mutexObj.ReleaseMutex();
        }

        callback(authorizationResult);
    }


    private void DialogAttachedToVisualTreeHandler(bool status)
    {
        _isDialogLoaded = true;
    }


    private async void BeginAuthorizationCommandExecute()
    {
        var d = new SpinnerDialog();
        d.Notify += DialogAttachedToVisualTreeHandler;

        var authResult = false;
        var authorizationJobThread = new Thread(() => DoAuthorization(result => authResult = result));

        // Start the thread and then show the dialog. The thread will wait for showing it on the screen.
        authorizationJobThread.Start();
        var dialogResult = (await DialogHost.Show(d, DIALOG_IDENTIFIER))!;

        var result = (dialogResult, authResult);
        if (result.Equals((false, false))) AuthorizationCanceled(authorizationJobThread);
        if (result.Equals((null, false)!)) AuthorizationFailed();
        if (result.Equals((null, true)!)) AuthorizationGranted();
    }

    private static void AuthorizationCanceled(Thread thread)
    {
        // todo: create aborting sequence if it is necessary.
        thread.Interrupt();
        thread.Join();
    }

    private void AuthorizationFailed()
    {
        // todo: show error dialog.
    }

    private void AuthorizationGranted()
    {
        PagerViewModel.SwitchPage(1);
    }
}