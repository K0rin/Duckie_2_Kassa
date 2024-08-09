using System;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using Duckie2Client.Libs;
using Duckie2Client.Services.AppLoading;
using ReactiveUI;

namespace Duckie2Client.ViewModels;

public class AuthorizationScreenViewModel : ViewModelBase
{
    private bool _isSpinnerVisible;

    public bool IsSpinnerVisible
    {
        get => _isSpinnerVisible;
        set => this.RaiseAndSetIfChanged(ref _isSpinnerVisible, value);
    }

    private bool _isAuthControlsVisible;

    public bool IsAuthControlsVisible
    {
        get => _isAuthControlsVisible;
        set => this.RaiseAndSetIfChanged(ref _isAuthControlsVisible, value);
    }

    public ReactiveCommand<object, Unit> BeginAuthorizationCommand { get; set; }

    private string _message;

    public string Message
    {
        get => _message;
        set => this.RaiseAndSetIfChanged(ref _message, value);
    }

    public AuthorizationScreenViewModel()
    {
        IsSpinnerVisible = false;
        IsAuthControlsVisible = true;
        BeginAuthorizationCommand =
            ReactiveCommand.Create<object>(BeginAuthorizationCommandExecute);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value">Reference to the parent's view data context.</param>
    // ReSharper disable once MemberCanBeMadeStatic.Local
    private async void BeginAuthorizationCommandExecute(object value)
    {
        IsSpinnerVisible = true;
        IsAuthControlsVisible = false;

        var jobRunner = new AppLoading();

        jobRunner.ActionCompleted += actionMessage => Message = actionMessage;
        
        var jobList = new List<LoadingJob>
        {
            new DatabaseConnectionCheck()
        };
        
        try
        {
            //todo: refact: Испльзовать данный конструктор и для загрузги приложения. 
            await jobRunner.LoadAppActionAsync(jobList);
            // ::debug::
            await Task.Delay(3000);
        }
        catch (DuckieException e)
        {
            Console.WriteLine(e.Message);
            return;
        }

        Message = "ready";

        return;
        // Switch to the Main Console Screen.
        ((ConsoleWindowViewModel)value).SwitchPage(1);
    }

}