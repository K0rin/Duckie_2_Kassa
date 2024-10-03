using System;
using System.Collections.Generic;

namespace Duckie2Client.Services.DbmsService.Records.Builders;

public class ClientRecordBuilder : RecordBuilderBase<ClientRecord>
{
    public void AddFirstName(string value)
    {
        GetProduct().FirstName = value;
    }

    public void AddLastName(string value)
    {
        GetProduct().LastName = value;
    }

    public void AddNotes(string value)
    {
        GetProduct().Notes = value;
    }

    public void AddFirstRegistrationDateTime(DateTime value)
    {
        GetProduct().FirstRegistration = value;
    }

    public void AddCommunicationMean(List<CommunicationMeanBuilder> value)
    {
        GetProduct().CommunicationMeans = [];

        foreach (var commMean in value)
        {
            commMean.AddClienId(GetProduct().Id);
            GetProduct().CommunicationMeans.Add(commMean.Build());
        }
    }

    public void AddBonus(ClientBonusRecordBuilder value)
    {
        GetProduct().Bonus = value.Build();
    }

    public void AddVehicle(List<VehicleRecordBuilder> value)
    {
        GetProduct().Vehicles = [];
        foreach (var vehicle in value) GetProduct().Vehicles.Add(vehicle.Build());
    }
}