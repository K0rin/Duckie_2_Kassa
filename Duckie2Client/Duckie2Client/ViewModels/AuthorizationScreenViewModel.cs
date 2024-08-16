using System;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
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

    public AuthorizationScreenViewModel()
    {
        BeginAuthorizationCommand = ReactiveCommand.Create(BeginAuthorizationCommandExecute);
#if DEBUG
        UserLogin = "jevgeni";
        UserPassword = "urugula";
#endif
    }

    private bool _isDialogLoaded;
    private readonly Mutex _mutexObj = new();

    private void DoAuthorization()
    {
        // Wait until the dialog is displayed on the screen.
        // while (true)
        // {
        //     _mutexObj.WaitOne();
        //     if (!_isDialogLoaded) continue;
        //     _mutexObj.ReleaseMutex();
        //     break;
        // }

        var checkDatabaseConnectionCommand = new CheckDatabaseConnectionCommand();
        var checkAuthorizationCommand = new CheckAuthorizationCommand(UserLogin, UserPassword);
        checkDatabaseConnectionCommand.NotifyStatus += status => Message = status;
        checkAuthorizationCommand.NotifyStatus += status => Message = status;

        var invoker = new DuckieCommandInvoker();

        try
        {
            invoker.SetCommand(checkDatabaseConnectionCommand);
            invoker.ExecuteCommand();

            Thread.Sleep(2000);

            invoker.SetCommand(checkAuthorizationCommand);
            invoker.ExecuteCommand();
        }
        catch (Exception e)
        {
            throw;
        }
    }

    private void DialogAttachedToVisualTreeHandler(bool status)
    {
        _isDialogLoaded = true;
    }

    private void ExceptionHandler(Task task)
    {
        var exception = task.Exception;
        Console.WriteLine("custom user handler");
        Console.WriteLine(exception);
    }

    /// <summary>
    /// 
    /// </summary>
    private async void BeginAuthorizationCommandExecute()
    {
        var d = new SpinnerDialog();
        d.Notify += DialogAttachedToVisualTreeHandler;
        var t1 = new Task(DoAuthorization);
        t1.ContinueWith(ExceptionHandler, TaskContinuationOptions.OnlyOnFaulted);
        t1.Start();

        var dialogResult = (bool)(await DialogHost.Show(d, "AuthorizationDialog"))!;

        return;

        // var myThread2 = new Thread(DoAuthorization)
        // {
        //     Name = "AuthorizationSpinnerDialog"
        // };
        //
        // var d = new SpinnerDialog();
        // d.Notify += DialogAttachedToVisualTreeHandler;
        //
        // try
        // {
        //     myThread2.Start(d);
        //     var dialogResult = (bool)(await DialogHost.Show(d, "AuthorizationDialog"))!;
        //     // ReSharper disable once InvertIf
        //     if (!dialogResult)
        //     {
        //         myThread2.Interrupt();
        //         myThread2.Join();
        //     }
        // }
        // // TODO: catch checking commands errors.
        // catch (DuckieException e)
        // {
        //     Console.WriteLine(e.Message);
        // }
        // catch (ThreadInterruptedException e)
        // {
        //     // todo: create aborting sequence.
        //     // todo: проверить, будет ли закрываться соединение с базой, если прервать поток.
        //
        //     Console.WriteLine("authorization aborted");
        // }


        // // Switch to the Main Console Screen.
        // ((ConsoleWindowViewModel)value).SwitchPage(1);
    }
}