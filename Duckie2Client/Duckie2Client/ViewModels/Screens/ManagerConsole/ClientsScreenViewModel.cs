using System;
using System.Collections.Generic;
using System.Reactive;
using Duckie2Client.Enums.Flags;
using Duckie2Client.Services.DbmsService;
using Duckie2Client.Services.DbmsService.Records.Builders;
using Duckie2Client.ViewModels.Base;
using ReactiveUI;

namespace Duckie2Client.ViewModels.Screens.ManagerConsole;

public class ClientsScreenViewModel : ViewModelBase, ITabViewModel<List<string>>
{
    public static object DialogIdentifier => "ClientsScreenDialogs";
    public List<string>? DataPayload { get; set; }
    public ReactiveCommand<Unit, Unit> AddClientCommand { get; }

    public ClientsScreenViewModel()
    {
        AddClientCommand = ReactiveCommand.Create(AddClientCommandExecute);
    }

    private void AddClientCommandExecute()
    {
        // fake client ---
        var fc = new DataFaker.Main().GetClient();
        var fakePriceTypeId = Guid.Parse("EE9124D0-F5C3-4CC1-A551-E8E47E102BA4");
        // --- fake client

        var newClientBuilder = new ClientRecordBuilder();
        newClientBuilder.AddFirstName(fc.FirstName!);
        newClientBuilder.AddLastName(fc.LastName!);
        newClientBuilder.AddNotes(fc.Notes!);
        newClientBuilder.AddFirstRegistrationDateTime(fc.RegistrationDateTime);

        var communicationMeanBuilder = new CommunicationMeanBuilder();
        communicationMeanBuilder.AddEmail(fc.Email!);
        communicationMeanBuilder.AddPhone(fc.Phone!);
        newClientBuilder.AddCommunicationMean([communicationMeanBuilder]);

        // var clientBonusRecordBuilder = new ClientBonusRecordBuilder();
        // clientBonusRecordBuilder.AddSumma(fc.Bonus.Summa);
        // clientBonusRecordBuilder.AddEndDateTime(fc.Bonus.BonusEndDateTime);
        // newClientBuilder.AddBonus(clientBonusRecordBuilder);

        var vehiclePriceTypeBuilder = new PriceTypeRecordBuilder();
        vehiclePriceTypeBuilder.AddId(fakePriceTypeId);

        var vehicleRecordBuilder = new VehicleRecordBuilder();
        vehicleRecordBuilder.AddLicence(fc.Vehicle.Licence!);
        vehicleRecordBuilder.AddPriceType(vehiclePriceTypeBuilder);
        newClientBuilder.AddVehicle([vehicleRecordBuilder]);

        var result = new Clients().Create(newClientBuilder);
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
}