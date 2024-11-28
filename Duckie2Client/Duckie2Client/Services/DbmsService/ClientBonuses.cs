using System;
using System.Collections.Generic;
using System.Linq;
using Duckie2Client.Enums.Flags;
using Duckie2Client.Models;
using Duckie2Client.Models.Database;
using Duckie2Client.Services.DbmsService.Records;
using Duckie2Client.Services.DbmsService.Records.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ReactiveUI;

namespace Duckie2Client.Services.DbmsService;

public class ClientBonuses : CrudOperationsBase
{
    public static decimal FindBoonus(Guid ClientId)
    {

        using var db = new DbmsService();



        var foundBonus = db.ClientBonuses
            .Where(c => c.ClientId == ClientId && c.EndDateTime >= DateTime.Now)
            .FirstOrDefault();

        if (foundBonus != null)
        {
            return foundBonus.Summa;
        }

        var zero = 0;

        return zero;

        //foundVehicle.Clients.Add(newClient);
        //db.Vehicles.Update(foundVehicle);
        //db.SaveChanges();
    }
}