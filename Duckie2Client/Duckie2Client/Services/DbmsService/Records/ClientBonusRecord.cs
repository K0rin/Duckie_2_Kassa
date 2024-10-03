using System;

namespace Duckie2Client.Services.DbmsService.Records;

public class ClientBonusRecord : RecordBase
{
    public Guid ClientId { get; set; }
    public decimal Summa { get; set; }
    public DateTime EndDateTime { get; set; }
}