namespace Duckie2Client.Services.DbmsService.Records.Builders;

public class TradeUnitLocalizationRecordBuilder : RecordBuilderBase<TradeUnitLocalizationRecord>
{
    public void AddLocale(string value)
    {
        GetProduct().Locale = value;
    }

    public void AddValue(string value)
    {
        GetProduct().Value = value;
    }
}