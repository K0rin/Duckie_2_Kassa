using System;

namespace Duckie2Client.Models;

public record OrderGoodsRecord(Guid WashId, Guid GoodId, string GoodName, decimal GoodPrice);