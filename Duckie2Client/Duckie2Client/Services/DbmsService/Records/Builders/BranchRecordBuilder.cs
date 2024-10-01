namespace Duckie2Client.Services.DbmsService.Records.Builders;

public class BranchRecordBuilder : RecordBuilderBase<BranchRecord>
{
    public void AddName(string value)
    {
        GetProduct().Name = value;
    }

    public void AddAddress(string value)
    {
        GetProduct().Address = value;
    }
}