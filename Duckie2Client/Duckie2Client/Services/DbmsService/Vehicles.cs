using System;
using System.Collections.Generic;
using System.Linq;
using Duckie2Client.Models;
using Duckie2Client.Models.Database;

namespace Duckie2Client.Services.DbmsService;

/// <summary>
/// Contains functionality for working with the "Vehicle" database entity.
/// </summary>
public class Vehicles : CrudOperationsBase
{

    public static VehiclesRecord? findVehicle(string licence)
    {
        VehiclesRecord returnResult = null;

        using var db = new DbmsService();

        var foundVehicle = db.Vehicles
            .Select(p => new Vehicle
            {
                Id = p.Id,
                Licence = p.Licence,
                PriceType = p.PriceType,
                Clients = p.Clients
            })
            .FirstOrDefault(v => v.Licence == licence);

        if (foundVehicle == null) return returnResult;

        returnResult = new VehiclesRecord(foundVehicle.Id,foundVehicle.Licence, foundVehicle.Clients);

        return returnResult;
    }
}