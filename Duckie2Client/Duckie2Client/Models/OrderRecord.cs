using System;
using System.Collections.Generic;

namespace Duckie2Client.Models;

public record OrderRecord(Guid Id, Guid VehicleId, string Carnumber, Guid ClientId, string ClientFirstName, string ClientLastName, List<ClientPhonesRecord> ClientPhones,List<OrderGoodsRecord> ?GoodsList, List<OrderServicesRecord> ?ServiceList, string Payment, string BranchName, string BranchAddress);