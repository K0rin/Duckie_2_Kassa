using System;
using System.Collections.Generic;
using System.Linq;
using Duckie2Client.Models;
using Duckie2Client.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Duckie2Client.Services.DbmsService;

/// <summary>
/// Contains functionality for working with the "Vehicle" database entity.
/// </summary>
public class Vehicles : CrudOperationsBase
{

    public static VehiclesRecord? FindVehicle(string licence)
    {
        VehiclesRecord returnResult = null;

        using var db = new DbmsService();

        var foundVehicle = db.Vehicles
            .Where(v=> v.Licence.Equals(licence))
            .Include(vehicle => vehicle.Clients)
            .FirstOrDefault(v => v.Licence == licence);

        if (foundVehicle == null) return returnResult;

        returnResult = new VehiclesRecord(foundVehicle.Id,foundVehicle.Licence, foundVehicle.Clients);

        return returnResult;
    }
}