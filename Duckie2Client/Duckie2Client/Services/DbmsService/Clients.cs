using System;
using System.Collections.Generic;
using System.Linq;
using Duckie2Client.Enums.Flags;
using Duckie2Client.Models.Database;
using Duckie2Client.Services.DbmsService.Records;
using Duckie2Client.Services.DbmsService.Records.Builders;

namespace Duckie2Client.Services.DbmsService;

/// <summary>
/// Contains functionality for working with the “Client” database entity.
/// </summary>
public class Clients : CrudOperationsBase
{
    public override object Read<TDataModel>(RecordReadFlags readFlags)
    {
        object result;

        using (var db = new DbmsService())
        {
            result = db.Clients.ToList();
        }

        return (List<TDataModel>)result;
    }

    public override DataModelOperationResult Create<T>(RecordBuilderBase<T> builder)
    {
        using var db = new DbmsService();

        var builtClient = builder.Build() as ClientRecord;

        // communication mean

        var newClientCommunicationMeans = new List<CommunicationMean>();

        foreach (var communicationMean in builtClient?.CommunicationMeans!)
        {
            var newCommunicationMean = new CommunicationMean();
            var communicationMeanRecord = communicationMean;
            PropertySetter.SetProperties<CommunicationMeanRecord, CommunicationMean>(
                ref communicationMeanRecord,
                ref newCommunicationMean);
            newClientCommunicationMeans.Add(newCommunicationMean);
        }


        // bonus

        var bonusRecord = builtClient.Bonus;
        var newClientBonus = new ClientBonus();
        PropertySetter.SetProperties<ClientBonusRecord, ClientBonus>(ref bonusRecord!, ref newClientBonus);

        // vehicle


        var newClientVehicles = new List<Vehicle>();

        foreach (var vehicle in builtClient.Vehicles)
        {
            // todo: not found error
            var existingPriceType = db.PriceTypes.First(pt => pt.Id.Equals(vehicle.PriceType.Id));

            var newVehicle = new Vehicle
            {
                Licence = null!,
                PriceType = existingPriceType
            };
            var vehicleRecord = vehicle;

            PropertySetter.SetProperties<VehicleRecord, Vehicle>(ref vehicleRecord, ref newVehicle);
            newClientVehicles.Add(newVehicle);
        }


        var newClient = new Client
        {
            // FirstName = builtClient?.FirstName,
            // LastName = builtClient?.LastName,
            // Notes = builtClient?.Notes,
            // FirstRegistration = builtClient!.FirstRegistration,

            // CommunicationMeans = newClientCommunicationMean,
            CommunicationMeans = newClientCommunicationMeans,
            Bonus = newClientBonus,
            Vehicles = newClientVehicles
        };
        PropertySetter.SetProperties<ClientRecord, Client>(ref builtClient, ref newClient);

        db.Clients.Add(newClient);
        db.SaveChanges();

        return DataModelOperationResult.Successful;
    }
}