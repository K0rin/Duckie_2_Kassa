using Duckie2Client.Models.Database;
using System;
using System.Collections.Generic;

namespace Duckie2Client.Models;

public record ServicesRecord(Guid Id, string Licence, List<Client> VehicleClients, Guid PriceTypeId);