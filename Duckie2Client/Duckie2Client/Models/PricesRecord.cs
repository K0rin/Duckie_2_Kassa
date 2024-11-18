using Duckie2Client.Models.Database;
using System;
using System.Collections.Generic;

namespace Duckie2Client.Models;

public record PricesRecord(Guid PriceId, Guid TradeUnitId, string TradeUnitName, Guid RateId, decimal RateValue, TimeOnly? ProcessTime, TimeOnly? CurrentTime, TimeOnly? TimeToStop);