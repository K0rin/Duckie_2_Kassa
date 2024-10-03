namespace Duckie2Client.Services.DbmsService.Records;

public class VehicleRecord : RecordBase
{
    public string Licence { get; set; }
    public PriceTypeRecord PriceType { get; set; }
}