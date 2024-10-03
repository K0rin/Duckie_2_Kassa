namespace Duckie2Client.Services.DbmsService.Records.Builders;

public class VehicleRecordBuilder : RecordBuilderBase<VehicleRecord>
{
    public void AddLicence(string value)
    {
        GetProduct().Licence = value;
    }

    public void AddPriceType(PriceTypeRecordBuilder value)
    {
        GetProduct().PriceType = value.Build();
    }
}