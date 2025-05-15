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

    public static void AddWashes(ObservableCollection<PricesRecord> listServices, Guid branchId, Guid clientId, Guid companyId, Guid vehicleId, int paymentType, int pollutionType)
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

    public static Wash FindLastWash()
    {
        using var db = new DbmsService();
        var foundWash = db.Washes
            .OrderByDescending(wash => wash.DateTime)
            .FirstOrDefault();
        return foundWash;

    }

    public static List<OrderRecord> FindAllWashesPerPeriod() 
    {
        Wash lastWash = FindLastWash();
        DateTime requiredDate;
        requiredDate = lastWash.DateTime.AddDays(-300);
        using var db = new DbmsService();
        var foundWashes = db.Washes
            .Where(ws => ws.DateTime > requiredDate)
            .Include(ws => ws.Client)
            .ThenInclude(client => client.CommunicationMeans)
            .Include(ws => ws.Vehicle)
            .Include(ws => ws.TradeUnits)
            .ThenInclude(td => td.Names.Where(nm => nm.Locale == "EN"))
            .Include(ws => ws.TradeUnits)
            .ThenInclude(td => td.Prices)
            .ThenInclude(pr => pr.Value)
            .Include(ws => ws.Branch)
            .ToList();
        
        List<OrderRecord> washes = new List<OrderRecord>();

        foreach (var foundWash in foundWashes) 
        {
            List<OrderGoodsRecord> orderGoods = new List<OrderGoodsRecord>();
            List<OrderServicesRecord> orderServices = new List<OrderServicesRecord>();
            List<ClientPhonesRecord> clientPhonesRecords = new List<ClientPhonesRecord>();
            foreach (var tradeUnit in foundWash.TradeUnits)
            {

                if (tradeUnit.IsGood == true)
                {
                    string goodName = "";
                    decimal goodPrice = 0;
                    foreach (var price in tradeUnit.Prices)
                    {
                        goodPrice = price.Value.Value;
                    }
                    foreach (var name in tradeUnit.Names)
                    {
                        goodName = name.Value;
                    }

                    OrderGoodsRecord result = new OrderGoodsRecord(foundWash.Id, tradeUnit.Id, goodName, goodPrice);
                    orderGoods.Add(result);
                }
                else
                {
                    string serviceName = "";
                    decimal servicePrice = 0;
                    foreach (var price in tradeUnit.Prices)
                    {
                        servicePrice = price.Value.Value;
                    }
                    foreach (var name in tradeUnit.Names)
                    {
                        serviceName = name.Value;
                    }
                    OrderServicesRecord result = new OrderServicesRecord(foundWash.Id, tradeUnit.Id, serviceName, servicePrice);
                    orderServices.Add(result);
                }

            }
            string paymentDescription = "";
            int payment = foundWash.PaymentType;
            if (payment == 0)
            {
                paymentDescription = "Cash";
            }
            else if (payment == 1)
            {
                paymentDescription = "Debit Card";
            }
            else if (payment == 2)
            {
                paymentDescription = "Loan";
            }
            foreach (var comm in foundWash.Client.CommunicationMeans)
            {
                ClientPhonesRecord result = new ClientPhonesRecord(foundWash.Client.Id, comm.Phone, comm.Email);
                clientPhonesRecords.Add(result);
            }
            OrderRecord orderRecord = new OrderRecord(foundWash.Id, foundWash.Vehicle.Id, foundWash.Vehicle.Licence, foundWash.Client.Id, foundWash.Client.FirstName, foundWash.Client.LastName, clientPhonesRecords, orderGoods, orderServices, paymentDescription, foundWash.Branch.Name, foundWash.Branch.Address);
            washes.Add(orderRecord);
        }

        return washes;
    }

    public static OrderRecord FindWashById(Guid id) 
    {
        using var db = new DbmsService();
        var foundWash = db.Washes
            .Where(ws => ws.Id.Equals(id))
            .Include(ws => ws.Client)
            .ThenInclude(client => client.CommunicationMeans)
            .Include(ws => ws.Vehicle)
            .Include(ws => ws.TradeUnits)
            .ThenInclude(td => td.Names.Where(nm => nm.Locale == "EN"))
            .Include(ws => ws.TradeUnits)
            .ThenInclude(td => td.Prices)
            .ThenInclude(pr => pr.Value)
            .Include(ws => ws.Branch)
            .FirstOrDefault();

        List<OrderGoodsRecord> orderGoods = new List<OrderGoodsRecord>();
        List<OrderServicesRecord> orderServices = new List<OrderServicesRecord>();
        List<ClientPhonesRecord> clientPhonesRecords = new List<ClientPhonesRecord>();
        foreach (var tradeUnit in foundWash.TradeUnits) 
        {
            
            if (tradeUnit.IsGood == true)
            {
                string goodName = "";
                decimal goodPrice = 0;
                foreach (var price in tradeUnit.Prices)
                {
                    goodPrice = price.Value.Value;
                }
                foreach (var name in tradeUnit.Names)
                {
                    goodName = name.Value;
                }
                
                OrderGoodsRecord result = new OrderGoodsRecord(foundWash.Id, tradeUnit.Id, goodName, goodPrice);
                orderGoods.Add(result);
            }
            else 
            {
                string serviceName = "";
                decimal servicePrice = 0;
                foreach (var price in tradeUnit.Prices)
                {
                    servicePrice = price.Value.Value;
                }
                foreach (var name in tradeUnit.Names)
                {
                    serviceName = name.Value;
                }
                OrderServicesRecord result = new OrderServicesRecord(foundWash.Id, tradeUnit.Id, serviceName, servicePrice);
                orderServices.Add(result);
            }
             
        }
        string paymentDescription = "";
        int payment = foundWash.PaymentType;
        if (payment == 0)
        {
            paymentDescription = "Cash";
        }
        else if (payment == 1)
        {
            paymentDescription = "Debit Card";
        }
        else if (payment == 2) 
        {
            paymentDescription = "Loan";
        }
        foreach (var comm in foundWash.Client.CommunicationMeans) 
        {
            ClientPhonesRecord result = new ClientPhonesRecord(foundWash.Client.Id, comm.Phone, comm.Email);
            clientPhonesRecords.Add(result);
        }
        OrderRecord orderRecord = new OrderRecord(foundWash.Id, foundWash.Vehicle.Id, foundWash.Vehicle.Licence, foundWash.Client.Id, foundWash.Client.FirstName, foundWash.Client.LastName, clientPhonesRecords, orderGoods, orderServices, paymentDescription, foundWash.Branch.Name, foundWash.Branch.Address); 

        return orderRecord;
    }

    public static void WashCompleted(Guid id) 
    {
        using var db = new DbmsService();
        var foundWash = db.Washes
            .Where(ws => ws.Id.Equals(id))
            .Include(ws => ws.Client)
            .ThenInclude(client => client.CommunicationMeans)
            .Include(ws => ws.Vehicle)
            .Include(ws => ws.TradeUnits)
            .ThenInclude(td => td.Names.Where(nm => nm.Locale == "EN"))
            .Include(ws => ws.TradeUnits)
            .ThenInclude(td => td.Prices)
            .ThenInclude(pr => pr.Value)
            .Include(ws => ws.Branch)
            .FirstOrDefault();

        foundWash.Status = 1;
        db.Washes.Update(foundWash);
        db.SaveChanges();
    }
}