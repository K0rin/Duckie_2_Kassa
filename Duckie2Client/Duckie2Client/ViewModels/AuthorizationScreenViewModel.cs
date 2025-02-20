using System;
using System.Reactive;
using System.Threading;
using Avalonia.Threading;
using DialogHostAvalonia;
using Duckie2Client.Libs;
using Duckie2Client.Libs.Enums;
using Duckie2Client.Models;
using Duckie2Client.Resources;
using Duckie2Client.Services.Commands;
using Duckie2Client.ViewModels.Base;
using Duckie2Client.Views.Dialogs;
using Duckie2Client.Services.DbmsService;
using Duckie2Client.ViewModels.Screens;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive.Linq;
using DynamicData.Binding;

namespace Duckie2Client.ViewModels;

public class AuthorizationScreenViewModel : ViewModelPageBase
{
    public ReactiveCommand<Unit, Unit> BeginAuthorizationCommand { get; set; }
    public ReactiveCommand<Unit, Unit> ExitApplicationCommand { get; set; }
    [Reactive] public string UserLogin { get; set; }
    //[Reactive] public string UserPassword { get; set; }
    [Reactive] public string Message { get; set; }
    private bool _isDialogLoaded;
    private readonly Mutex _mutexObj = new();
    private SpinnerDialog _processDialog;
    private ErrorDialog _errorDialog;
    [Reactive] public string LoginIconPath { get; set; }
    [Reactive] public string BackButtonIconPath { get; set; }
    [Reactive] public string BackButtonTextColor { get; set; }
    [Reactive] public string LoginButtonColor { get; set; }
    [Reactive] public string LoginTextColor { get; set; }
    [Reactive] public bool LoginButtonEnabled { get; set; }
    
    private string _userPassword;

    [Reactive]
    public string UserPassword
    {
        get => _userPassword;
        set => this.RaiseAndSetIfChanged(ref _userPassword, value);
    }

    public string initialUser { get; set; }
    private const string AUTHORIZATION_DIALOGS = "AuthorizationDialogs";

    public AuthorizationScreenViewModel()
    {
        BeginAuthorizationCommand = ReactiveCommand.Create(BeginAuthorizationCommandExecute);
        ExitApplicationCommand = ReactiveCommand.Create(ExitApplicationCommandExecute);
#if DEBUG
        UserLogin = "Evgenij";
        UserPassword = "Urugula";
#endif
        _processDialog = CreateDialog();
        // todo: Localization for "Enter" and "Exit" button.
        this.WhenAnyValue(x => x.UserPassword)
            .Where(value => !string.IsNullOrEmpty(value)) // if CarNumber is not Empty
            .Subscribe(value =>
            {
                AuthorizationScreenButtons(true);
            });
        this.WhenAnyValue(x => x.UserPassword)
            .Where(value => !string.IsNullOrEmpty(value) == false) // if CarNumber is Empty
            .Subscribe(value =>
            {
                AuthorizationScreenButtons(false);
            });
        
    }

    private static void ExitApplicationCommandExecute()
    {
        App.ShutdownApplication();
    }


    private void DoAuthorization(Action<LoginUserRecord?> callback)
    {
        _mutexObj.WaitOne();

        // Wait until the dialog is shown on the screen.
        while (!_isDialogLoaded)
        {
        }

        // Идея в том, чтобы вернуть не false, а UserRecord или null.
        // var authorizationResult = false;
        LoginUserRecord? authorizationResult = null;

        // var userInitial = "";

        try
        {
            var checkDatabaseConnectionCommand = new CheckDatabaseConnectionCommand();
            var checkAuthorizationCommand = new CheckAuthorizationCommand(UserLogin, UserPassword);

            checkDatabaseConnectionCommand.NotifyStatus += status => Message = status;
            checkAuthorizationCommand.NotifyStatus += status => Message = status;

            var invoker = new DuckieCommandInvoker();

            invoker.SetCommand(checkDatabaseConnectionCommand);
            invoker.ExecuteCommand();

            //invoker.SetCommand(checkAuthorizationCommand);
            //invoker.ExecuteCommand(out authorizationResult);

            authorizationResult = Users.CheckLoginandPassword(UserLogin, UserPassword);

            // userInitial = Users.getUserInitial(UserLogin);
        }
        catch (ThreadAbortException e) // thread aborted
        {
        }
        catch (ThreadInterruptedException) // thread interrupted
        {
        }
        finally
        {
            // After all tasks are finished, the dialog will be closed.
            Dispatcher.UIThread.InvokeAsync(() => DialogHost.Close(AUTHORIZATION_DIALOGS));
            _mutexObj.ReleaseMutex();
        }

        callback(authorizationResult);
        // initials(userInitial);
    }


    private void DialogAttachedToVisualTreeHandler(bool status)
    {
        _isDialogLoaded = true;
    }

    private LoginUserRecord? _authorizedUserRecord;

    private async void BeginAuthorizationCommandExecute()
    {
        _processDialog = CreateDialog();

        // "userInitials" грамматически вернее.
        // var initialsUser = "";

        var authorizationJobThread = new Thread(
            () => DoAuthorization(result => _authorizedUserRecord = result)
        );

        // Start the thread and then show the dialog. The thread will wait for showing the dialog on the screen.
        authorizationJobThread.Start();
        var dialogResult = (await DialogHost.Show(_processDialog, AUTHORIZATION_DIALOGS))!;

        // todo: refact: Можно переписать результат с использованием перечисления кнопок диалогового окна. Использовать Cancel.
        var result = (dialogResult, _authorizedUserRecord != null);

        // authResult.initials - здесь инициалы. Можно передавать дальше объект.

        if (result.Equals((false, false))) AuthorizationCanceled(authorizationJobThread);
        if (result.Equals((null, false)!)) AuthorizationFailed();
        if (result.Equals((null, true)!)) AuthorizationGranted();
    }

    private SpinnerDialog CreateDialog()
    {
        var output = SpinnerDialog.GetNewDialog(this);
        output.Notify += DialogAttachedToVisualTreeHandler;
        return output;
    }

    private static void AuthorizationCanceled(Thread thread)
    {
        thread.Interrupt();
        thread.Join();
    }

    private async void AuthorizationFailed()
    {
        var msg = Localization.GetString(() => ErrorMessages._301_UserHasNoAccessRights, ResourceTypes.ErrorMessages);
        _errorDialog = new ErrorDialog(msg);
        await DialogHost.Show(_errorDialog, AUTHORIZATION_DIALOGS);
    }

    private void AuthorizationGranted()
    {
        PagerViewModel?.SwitchPage(1);

        // Страница 1, это MainKassaScreenViewModel
        (PagerViewModel?.CurrentPage as MainKassaScreenViewModel)?.AddLoggedInUser(_authorizedUserRecord);
    }

    private void AuthorizationScreenButtons(bool status)
    {
        if (status == false)
        {
            BackButtonIconPath = "/Assets/icon_back.svg";
            BackButtonTextColor = "Black";
            LoginIconPath = "/Assets/icon_open_lock_disabled.svg";
            LoginTextColor = "#B5B8B1";
            LoginButtonEnabled = false;
            LoginButtonColor = "#6a91a1";
            
        }
        else
        {
            BackButtonIconPath = "/Assets/icon_back.svg";
            BackButtonTextColor = "Black";
            LoginIconPath = "/Assets/icon_open_lock.svg";
            LoginTextColor = "Black";
            LoginButtonEnabled = true;
            LoginButtonColor = "#7a92a1";
        }
    }
}