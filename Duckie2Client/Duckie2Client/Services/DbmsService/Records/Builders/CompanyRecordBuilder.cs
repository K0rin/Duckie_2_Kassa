using System.Collections.Generic;

namespace Duckie2Client.Services.DbmsService.Records.Builders;

public class CompanyRecordBuilder : RecordBuilderBase<CompanyRecord>
{
    public void AddName(string value)
    {
        GetProduct().Name = value;
    }

    public void AddAddress(string value)
    {
        GetProduct().Address = value;
    }

    public void AddRegistrationNumber(string value)
    {
        GetProduct().RegistrationNumber = value;
    }

    public void AddVehicle(List<VehicleRecordBuilder> value)
    {
        GetProduct().Vehicles = [];
        foreach (var vehicle in value) GetProduct().Vehicles?.Add(vehicle.Build());
    }
}