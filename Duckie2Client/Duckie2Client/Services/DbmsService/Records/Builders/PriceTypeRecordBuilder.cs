namespace Duckie2Client.Services.DbmsService.Records.Builders;

public class PriceTypeRecordBuilder : RecordBuilderBase<PriceTypeRecord>
{
    public void AddName(string value)
    {
        GetProduct().Name = value;
    }
}