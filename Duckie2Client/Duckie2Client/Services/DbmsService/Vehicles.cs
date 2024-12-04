using System;
using System.Collections.Generic;
using System.Linq;
using Bogus.DataSets;
using Duckie2Client.Models;
using Duckie2Client.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Duckie2Client.Services.DbmsService;

/// <summary>
/// Contains functionality for working with the "Vehicle" database entity.
/// </summary>
public class Vehicles : CrudOperationsBase
{

    public static VehiclesRecord? FindVehicleRecord(string licence)
    {
        VehiclesRecord returnResult = null;

        using var db = new DbmsService();

        var foundVehicle = db.Vehicles
            .Where(v=> v.Licence.Equals(licence))
            .Include(vehicle => vehicle.Clients)
            .ThenInclude(comm => comm.CommunicationMeans)
            .FirstOrDefault(v => v.Licence == licence);

        if (foundVehicle == null) return returnResult;

        var priceType = db.Vehicles
            .Where(v => v.Licence.Equals(licence))
            .Include(vehicle => vehicle.PriceType)
            .FirstOrDefault(v => v.Licence == licence);

        returnResult = new VehiclesRecord(foundVehicle.Id,foundVehicle.Licence, foundVehicle.Clients, priceType.PriceType.Id);

        return returnResult;
    }

    public static Guid FindVehicleId(string licence)
    {
        VehiclesRecord returnResult = null;

        using var db = new DbmsService();

        var foundVehicle = db.Vehicles
            .Where(v => v.Licence.Equals(licence))
            .Include(vehicle => vehicle.Clients)
            .ThenInclude(comm => comm.CommunicationMeans)
            .FirstOrDefault(v => v.Licence == licence);

        return foundVehicle.Id;
    }
    public static void AddClientToVehicle(string licence, string firstName, string lastName, string phone, string email, string notes)
    {

        using var db = new DbmsService();

        var foundVehicle = db.Vehicles
            .Where(v => v.Licence.Equals(licence))
            .Include(vehicle => vehicle.Clients)
            .ThenInclude(comm => comm.CommunicationMeans)
            .FirstOrDefault(v => v.Licence == licence);

        CommunicationMean comm = new CommunicationMean();
        comm.Phone = phone;
        comm.Email = email;

        Client newClient = new Client();
        newClient.FirstName = firstName;
        newClient.LastName = lastName;
        newClient.Notes = notes;
        newClient.FirstRegistration = DateTime.Now;
        newClient.CommunicationMeans = [comm];

        foundVehicle.Clients.Add(newClient);
        db.Vehicles.Update(foundVehicle);
        db.SaveChanges();
    }
}