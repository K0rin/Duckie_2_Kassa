using System;

namespace Duckie2Client.Models;

public record OrderServicesRecord(Guid WashId, Guid ServiceId, string ServiceName, decimal ServicePrice);