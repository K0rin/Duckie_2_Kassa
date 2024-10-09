namespace Duckie2Client.Services.DbmsService.Records;

public class PriceRecord : RecordBase
{
    public TradeUnitRecord TradeUnit { get; set; }
    public RateRecord Value { get; set; }
    public PriceTypeRecord PriceType { get; set; }
}