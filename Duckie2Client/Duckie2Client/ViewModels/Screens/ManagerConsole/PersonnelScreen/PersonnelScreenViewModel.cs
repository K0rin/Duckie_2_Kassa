using System;
using System.Collections.Generic;
using System.Reactive;
using Duckie2Client.Services.DbmsService;
using Duckie2Client.Services.DbmsService.Records;
using Duckie2Client.ViewModels.Base;
using Duckie2Client.Views.Dialogs;
using ReactiveUI;

namespace Duckie2Client.ViewModels.Screens.ManagerConsole.PersonnelScreen;

// todo: Error on database.

public class PersonnelScreenViewModel : ViewModelBase, ITabViewModel<List<string>>
{
    // todo: refact: перенести константу в интерфейс ITabViewModel.
    private const string DIALOG_IDENTIFIER = "PersonnelScreenDialogs";
    public List<string>? DataPayload { get; set; }

    public ReactiveCommand<Unit, Unit> AddUserCommand { get; }
    public ReactiveCommand<Unit, Unit> DeleteUserCommand { get; }

    // todo: DRY
    private ErrorDialog? _errorDialog;

    public PersonnelScreenViewModel()
    {
        AddUserCommand = ReactiveCommand.Create(AddUserExecute);
        DeleteUserCommand = ReactiveCommand.Create(DeleteUserExecute);
    }

    private void DeleteUserExecute()
    {
        Console.WriteLine("delete");
    }


    private void AddUserExecute()
    {
        // todo: show spinner dialog.

        for (var i = 0; i < 10; i++)
        {
            var userRecord = new UserRecord
            {
                Id = Guid.NewGuid()
            };

            var branch = new BranchRecord
            {
                // only existing
                Id = new Guid("6D074317-4514-4444-AF39-0A65F4A4BE05")
            };

            var communication = new CommunicationMeanRecord
            {
                Id = Guid.NewGuid(),
                ClientId = userRecord.Id,

                // todo: Validation of an email and a phone.

                Email = $"<email{i}>",
                Phone = $"<phone{i}>"
            };
            var todayDateOnly = DateOnly.FromDateTime(DateTime.Today);
            var salaryRate = new RateRecord
            {
                // todo: Take the value from the settings.
                // todo: error: the settings file not found.

                Id = Guid.NewGuid(),
                Value = i,
                StartDate = todayDateOnly,
                EndDate = todayDateOnly.AddDays(1)
            };

            // todo: functionality for adding many values.

            userRecord.Branch = [branch];

            // todo: functionality for adding many values.

            userRecord.Communication = [communication];
            // False by default due to the Operator user role.
            userRecord.IsStaff = false;

            // todo: Validate a login, first name, last name.

            userRecord.Login = $"<login{i}>";
            // todo: secret string.
            userRecord.Password = $"<password{i}>";
            userRecord.FirstName = $"<firstname{i}>";
            userRecord.LastName = $"<lastname{i}>";
            userRecord.RegistrationDate = todayDateOnly;
            userRecord.SalaryRate = salaryRate;

            // Call meth to create.
            new Users().Create(userRecord);
        }
    }

    // todo: refact: Часть интерфейса. Можно не реализовывать, если не надо.
    public void OnScreenClose()
    {
        // todo: Запрос на сохранение не сохраненных данных.
        // Console.WriteLine(@"batch service close");
    }
}