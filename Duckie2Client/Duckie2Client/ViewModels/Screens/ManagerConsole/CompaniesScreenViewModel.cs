using System;
using System.Collections.Generic;
using System.Reactive;
using Duckie2Client.Enums.Flags;
using Duckie2Client.Services.DbmsService;
using Duckie2Client.Services.DbmsService.Records.Builders;
using Duckie2Client.ViewModels.Base;
using Duckie2Client.Views.Dialogs;
using ReactiveUI;

namespace Duckie2Client.ViewModels.Screens.ManagerConsole;

public class CompaniesScreenViewModel : ViewModelBase, ITabViewModel<List<string>>
{
    public static string DialogIdentifier => "CompaniesScreenDialogs";
    public List<string>? DataPayload { get; set; }
    public ReactiveCommand<Unit, Unit> AddCompanyCommand { get; set; }

    // todo: DRY
    private ErrorDialog? _errorDialog;

    public CompaniesScreenViewModel()
    {
        InitializeCommands();
    }

    private void InitializeCommands()
    {
        AddCompanyCommand = ReactiveCommand.Create(AddCompanyCommandExecute);
    }

    private void AddCompanyCommandExecute()
    {
        // fake  ---
        var fakeCompany = new DataFaker.Main().GetCompany();
        var fakeVehicleLicence = new DataFaker.Main().GetVehicleLicence();
        var fakePriceTypeId = Guid.Parse("EE9124D0-F5C3-4CC1-A551-E8E47E102BA4");
        // --- fake  

        var newCompanyRecordBuilder = new CompanyRecordBuilder();
        newCompanyRecordBuilder.AddAddress(fakeCompany.Address);
        newCompanyRecordBuilder.AddName(fakeCompany.Name);
        newCompanyRecordBuilder.AddRegistrationNumber(fakeCompany.RegistrationNumber);

        var vehiclePriceTypeBuilder = new PriceTypeRecordBuilder();
        vehiclePriceTypeBuilder.AddId(fakePriceTypeId);

        var vehicleRecordBuilder = new VehicleRecordBuilder();
        vehicleRecordBuilder.AddLicence(fakeVehicleLicence);
        vehicleRecordBuilder.AddPriceType(vehiclePriceTypeBuilder);
        newCompanyRecordBuilder.AddVehicle([vehicleRecordBuilder]);

        var result = new Companies().Create(newCompanyRecordBuilder);
        if (result.Equals(DataModelOperationResult.RecordNotFound)) ShowDataConsistencyError();
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

    // todo: refact: Часть интерфейса. Можно не реализовывать, если не надо.
    public void OnScreenClose()
    {
        // todo: Запрос на сохранение не сохраненных данных.
        // Console.WriteLine(@"batch service close");
    }
}