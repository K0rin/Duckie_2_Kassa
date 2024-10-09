using System;
using System.Collections.Generic;

namespace Duckie2Client.Services.DbmsService.Records;

public class TradeUnitRecord : RecordBase
{
    public bool IsGood { get; set; }
    public TimeOnly? ProcessTime { get; set; }
    public bool IsIgnoreDiscounts { get; set; }
    public List<PriceRecord> Prices { get; set; }
    public List<TradeUnitLocalizationRecord> Names { get; set; }
}