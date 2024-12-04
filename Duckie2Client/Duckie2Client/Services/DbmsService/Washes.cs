using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using Bogus.DataSets;
using Duckie2Client.Models;
using Duckie2Client.Models.Database;
using DynamicData;
using Microsoft.EntityFrameworkCore;
using SkiaSharp;

namespace Duckie2Client.Services.DbmsService;

/// <summary>
/// Contains functionality for working with the "Vehicle" database entity.
/// </summary>
public class Washes : CrudOperationsBase
{

    public static void AddWashes(ObservableCollection<PricesRecord> listServices, Guid branchId, Guid clientId, Guid companyId, Guid vehicleId, int paymentType)
    {

        using var db = new DbmsService();

        List<TradeUnit> tradeUnits = new List<TradeUnit>();
        foreach (PricesRecord pr in listServices) 
        {
            var tradeUnit = db.TradeUnits.
                Where(td => td.Id.Equals(pr.TradeUnitId)).
                Include(t => t.Names).
                Include(p => p.Prices);

            tradeUnits.Add(tradeUnit);
        }

        var foundClient = db.Clients
            .Where(client => client.Id == clientId)
            .Include(client => client.Vehicles)
            .FirstOrDefault();

        var foundVehicle = db.Vehicles
            .Where(v => v.Id == vehicleId)
            .Include(vehicle => vehicle.Clients)
            .ThenInclude(comm => comm.CommunicationMeans)
            .FirstOrDefault(v => v.Id == vehicleId);

        var foundCompanies = db.Companies
            .Where(company => company.Id == companyId)
            .FirstOrDefault();

        var foundBranch = db.Branches
            .Where(branch => branch.Id == branchId)
            .FirstOrDefault();

        Wash wash = new Wash
        {
            //Id = Guid.NewGuid(),
            Client = foundClient,
            Branch = foundBranch,
            Company = foundCompanies,
            Vehicle = foundVehicle,
            TradeUnits = tradeUnits,
            DateTime = DateTime.Now,
            Status = 0,
            PaymentType = paymentType,
        
        };

        //var foundVehicle = db.Vehicles
        //    .Where(v => v.Licence.Equals(vehicle.Licence))
        //    .FirstOrDefault(v => v.Licence == vehicle.Licence);
        db.Washes.Add(wash);
        db.Vehicles.Update(foundVehicle);
        db.Clients.Update(foundClient);
        db.Companies.Update(foundCompanies);
        db.Branches.Update(foundBranch);
        db.SaveChanges();
    }
}