using System;
using System.Collections.Generic;
using System.Reactive;
using System.Security.Cryptography;
using Duckie2Client.Enums.Flags;
using Duckie2Client.Libs.SecretStrings;
using Duckie2Client.Services.DbmsService;
using Duckie2Client.Services.DbmsService.Records;
using Duckie2Client.Services.DbmsService.Records.Builders;
using Duckie2Client.ViewModels.Base;
using Duckie2Client.Views.Dialogs;
using ReactiveUI;

namespace Duckie2Client.ViewModels.Screens.ManagerConsole;

// todo: Error on database.

public class PersonnelScreenViewModel : ViewModelBase, ITabViewModel<List<string>>
{
    public static string DialogIdentifier => "PersonnelScreenDialogs";
    public List<string>? DataPayload { get; set; }

    public ReactiveCommand<Unit, Unit> AddUserCommand { get; }
    public ReactiveCommand<Unit, Unit> DeleteUserCommand { get; }
    public ReactiveCommand<Unit, Unit> UpdateCommand { get; }


    // todo: DRY
    private ErrorDialog? _errorDialog;

    public PersonnelScreenViewModel()
    {
        AddUserCommand = ReactiveCommand.Create(AddUserExecute);
        DeleteUserCommand = ReactiveCommand.Create(DeleteUserExecute);
        UpdateCommand = ReactiveCommand.Create(UpdateUserExecute);
    }

    private void ShowDataConsistencyError()
    {
        // todo: show this message in a dialog.

        const string MSG =
            "DUCKIE_EXCEPTION: Нарушение целостности записей в базе данных.\n" +
            "Запрашиваемая запись не найдена в базе, но предполагается, что она должна существовать .\n" +
            "Необходима проверка базы данных.\n" +
            "Дальнейшая работа с программой может увеличит несогласованность данных.";
        throw new Exception(MSG);
    }

    private void UpdateUserExecute()
    {
        // try
        // {
        //     // var communication = new CommunicationMeanRecord
        //     // {
        //     //     Id = Guid.Parse("83777285-10FF-4549-8761-C3C0911E8963"),
        //     //     ClientId = Guid.Parse("304DF7DB-07A1-4DA2-BC11-92CA3A8CB49B"),
        //     //
        //     //     // todo: Validation of an email and a phone.
        //     //
        //     //     Phone = "<phone 6 updated>"
        //     // };
        //     var salaryRate = new RateRecord
        //     {
        //         // todo: Take the value from the settings.
        //         // todo: error: the settings file not found.
        //
        //         // todo: Get latest salary rate.
        //
        //         Id = Guid.Parse("C25BABDB-29F3-46F1-BB07-C298A976B54B"),
        //         Value = 66
        //     };
        //
        //
        //     var updateUser = new UserRecord
        //     {
        //         Id = Guid.Parse("304DF7DB-07A1-4DA2-BC11-92CA3A8CB49B")
        //         // FirstName = "<firstname6>updated"
        //         // Communication = [communication]
        //     };
        //
        //     var updateResult = new Users().Update<UserRecord>(updateUser);
        //
        //     if (!updateResult.Equals(DataModelOperationResult.RecordNotFound)) return;
        //
        //     ShowDataConsistencyError();
        // }
        // catch (Exception e)
        // {
        //     Console.WriteLine(e);
        //     throw;
        // }
    }

    private void DeleteUserExecute()
    {
        try
        {
            // todo: Get selected rows from the table.

            var user1 = new UserRecordBuilder();
            var user2 = new UserRecordBuilder();
            user1.AddId(Guid.Parse("62A24E41-922C-47E8-8C85-9E4D022BA932"));
            user2.AddId(Guid.Parse("5C16F9D4-C22A-41B9-9735-DC4C248C5826"));

            var usersToRemove = new List<RecordBuilderBase<UserRecord>> { user1, user2 };
            var deleteResult = new Users().DeleteMany(usersToRemove);

            if (!deleteResult.Equals(DataModelOperationResult.RecordNotFound)) return;

            ShowDataConsistencyError();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    private void AddUserExecute()
    {
        // todo: show spinner dialog.

        for (var i = 0; i < 10; i++)
        {
            var newUserRecordBuilder = new UserRecordBuilder();
            newUserRecordBuilder.AddFirstName($"<firstname{i}>");
            newUserRecordBuilder.AddBranch(new Guid("6D074317-4514-4444-AF39-0A65F4A4BE05"));
            newUserRecordBuilder.AddLogin($"<login{i}>");
            newUserRecordBuilder.AddLastName($"<lastname{i}>");

            var todayDateOnly = DateOnly.FromDateTime(DateTime.Today);
            newUserRecordBuilder.AddRegistrationDate(todayDateOnly);

            var communicationMeanBuilder = new CommunicationMeanBuilder();
            communicationMeanBuilder.AddEmail($"<email{i}>");
            communicationMeanBuilder.AddPhone($"<phone{i}>");
            newUserRecordBuilder.AddCommunication(communicationMeanBuilder);

            var salaryRateRecordBuilder = new RateBuilder();
            salaryRateRecordBuilder.AddRateValue(i);
            salaryRateRecordBuilder.AddStartDate(todayDateOnly);
            salaryRateRecordBuilder.AddEndDate(todayDateOnly.AddDays(1));
            newUserRecordBuilder.AddSalaryRate([salaryRateRecordBuilder]);

            // False by default due to the Operator user role.
            newUserRecordBuilder.AddIsStaff(false);

            using (var sha256 = SHA256.Create())
            {
                var hashedPassword = SecretStrings.GetHash($"<password{i}>", sha256);
                newUserRecordBuilder.AddPassword(hashedPassword);
            }

            // var result = new Users().Create(newUserRecordBuilder.Build());
            var result = new Users().Create(newUserRecordBuilder);
            if (!result.Equals(DataModelOperationResult.RecordNotFound)) continue;
            ShowDataConsistencyError();
        }
    }

    // todo: refact: Часть интерфейса. Можно не реализовывать, если не надо.
    public void OnScreenClose()
    {
        // todo: Запрос на сохранение не сохраненных данных.
        // Console.WriteLine(@"batch service close");
    }
}